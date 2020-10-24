using System.Diagnostics.CodeAnalysis;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Hummingbird.Scripts
{
    /// <summary>
    /// A Hummingbird ML Agent
    /// </summary>
    public class HummingbirdAgent : Agent
    {
        [Tooltip("Force to apply when moving")]
        public float moveForce = 2f;

        [Tooltip("Speed to pitch up or down")] public float pitchSpeed = 100f;

        [Tooltip("Speed to rotate around the up axis")]
        public float yawSpeed = 100f;

        [Tooltip("Transform at the tip of the beak")]
        public Transform beakTip;

        [Tooltip("Agent's camera")] public Camera agentCamera;

        [Tooltip("Training or gameplay mode")] public bool trainingMode;

        //RB of the agent 
        private Rigidbody _rigidbody;

        //The flower area the agent is in
        private FlowerArea _flowerArea;

        //Nearest flower to the agent
        private Flower _nearestFlower;

        //Allows for smoother pitch changes
        private float _smoothPitchChange = 0f;

        //Allows for smoother yaw changes
        private float _smoothYawChange = 0f;

        //Maximum angle that the bird can pitch up or down
        private const float MaxPitchAngle = 80f;

        //Maximum distance from the beak tip to accept nectar collision
        private const float BeakTipRadius = 0.008f;

        //Whether the agent is frozen (intentionally not flying)
        private bool _frozen = false;

        /// <summary>
        /// The amount of nectar the agent has obtained
        /// </summary>
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public float NectarObtained { get; private set; }

        /// <summary>
        /// Initialise the agent
        /// </summary>
        public override void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _flowerArea = GetComponentInParent<FlowerArea>();

            //If not train9ing mode no max step, player forever
            if (!trainingMode) MaxStep = 0;
        }

        /// <summary>
        /// Reset the agent when an episode begins
        /// </summary>
        public override void OnEpisodeBegin()
        {
            if (trainingMode)
            {
                //Only reset flowers in training     when there is one agent per area
                _flowerArea.ResetFlowers();
            }

            //Reset the amount of nectar obtained
            NectarObtained = 0f;

            //Zero out velocities so that movement stops before a new episode begins
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            //Default to spawning in front of a flower
            bool inFrontOFlower = true;

            
            if (trainingMode)
            {
                //Spawn in front of flower 50% of time
                inFrontOFlower = UnityEngine.Random.value > .5f;
            }

            //Move the agent to a new random position
            MoveToSafeRandomPosition(inFrontOFlower);

            //Recalculate the nearest flower now that the agent has moved
            UpdateNearestFlower();
        }

        /// <summary>
        /// Called when the action is received from either the player input or the neural net
        ///
        /// vectorAction[i] represents:
        /// Index = 0: move vector X (+1 = right -1 = left)
        /// Index = 1: move vector Y (+1 = up -1 = down)
        /// Index = 2: move vector Z (+1 = forward -1 = backward)
        /// Index = 3: pitch angle (+1 = pitch Up -1 =- pitch down)
        /// Index = 4: yaw angle (+1 = turn right- 1 = turn left)
        /// </summary>
        /// <param name="vectorAction">Actions to take</param>
        public override void OnActionReceived(float[] vectorAction)
        {
            //Don't take action if frozen
            if (_frozen) return;

            //Calculate movement vector
            Vector3 move = new Vector3(vectorAction[0], vectorAction[1], vectorAction[2]);

            //Add force in the direction of the move vector
            _rigidbody.AddForce(move * moveForce);

            //Get current rotation
            Vector3 rotationVector = transform.rotation.eulerAngles;

            //Calculate pitch and yaw rotation
            float pitchChange = vectorAction[3];
            float yawChange = vectorAction[4];

            //Calculate smooth rotation changes
            _smoothPitchChange = Mathf.MoveTowards(_smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
            _smoothYawChange = Mathf.MoveTowards(_smoothYawChange, yawChange, 2f * Time.fixedDeltaTime);

            //Calculate new pitch and yaw and clamp pitch
            float pitch = rotationVector.x + _smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;
            if (pitch > 180) pitch -= 360f;
            pitch = Mathf.Clamp(pitch, -MaxPitchAngle, MaxPitchAngle);

            float yaw = rotationVector.y + _smoothYawChange * Time.fixedDeltaTime * yawSpeed;

            //Apply new rotation 
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        /// <summary>
        /// Collect vector observations from the environment
        /// </summary>
        /// <param name="sensor">Vector sensor</param>
        public override void CollectObservations(VectorSensor sensor)
        {
            if (_nearestFlower == null)
            {
                sensor.AddObservation(new float[0]);
                return;
            }

            //Observe the agent's local rotation (4 observation)
            sensor.AddObservation(transform.localRotation.normalized);

            //Get a vector from the beak tip to the nearest flower
            Vector3 toFlower = _nearestFlower.FlowerCentrePosition - beakTip.position;

            //Observe a normalise vector pointing to the nearest flower (3 observations)
            sensor.AddObservation(toFlower.normalized);

            //Observe the dot product that indicates whether the beak tip is in front of the flower (1 observation)
            //(+1 means the beak tip is directly in front of -1 means directly behind)
            sensor.AddObservation(Vector3.Dot(toFlower.normalized, -_nearestFlower.FlowerUpVector.normalized));

            //Observe a dot product that indicates whether the beak is pointing towards the flower (1 observation)
            //(+1 means the beak is pointing directly towards -1 means directly away)
            sensor.AddObservation(Vector3.Dot(beakTip.forward.normalized, -_nearestFlower.FlowerUpVector.normalized));

            //Observe the relative distance from the beak-tip to the flower (1 observation)
            sensor.AddObservation(toFlower.magnitude / FlowerArea.AreaDiameter);

            //10 total observations
        }

        /// <summary>
        /// When behaviour type is set to heuristic only on the agent's behaviour params
        /// this function will be called. Its return values will be fed into
        /// <see cref="OnActionReceived(float[])"/> instead of using the neural net
        /// </summary>
        /// <param name="actionsOut">Output action array</param>
        public override void Heuristic(float[] actionsOut)
        {
            //Create placeholders for all movement/turning
            Vector3 forward = Vector3.zero;
            Vector3 left = Vector3.zero;
            Vector3 up = Vector3.zero;
            float pitch = 0f;
            float yaw = 0f;

            //Convert keyboard inputs to movement and turning
            //All values should be between -1 and 1

            //Forward/backward
            if (Input.GetKey(KeyCode.W)) forward = transform.forward;
            else if (Input.GetKey(KeyCode.S)) forward = -transform.forward;

            //Left/right
            if (Input.GetKey(KeyCode.A)) forward = -transform.right;
            else if (Input.GetKey(KeyCode.D)) forward = transform.right;

            //Up/down
            if (Input.GetKey(KeyCode.E)) up = transform.up;
            else if (Input.GetKey(KeyCode.C)) up = -transform.up;

            //Pitch up/down
            if (Input.GetKey(KeyCode.UpArrow)) pitch = 1f;
            else if (Input.GetKey(KeyCode.DownArrow)) pitch = -1f;

            //Turn left/right
            if (Input.GetKey(KeyCode.LeftArrow)) yaw = -1f;
            else if (Input.GetKey(KeyCode.RightArrow)) yaw = 1f;

            //Combine the movement vector and normalise
            Vector3 combined = (forward + left + up).normalized;

            //Add the 3 movement values, pitch and yaw to the actionsOut array
            actionsOut[0] = combined.x;
            actionsOut[1] = combined.y;
            actionsOut[2] = combined.z;
            actionsOut[3] = pitch;
            actionsOut[4] = yaw;
        }

        /// <summary>
        /// Prevent the agent from moving and taking actions
        /// </summary>
        public void FreezeAgent()
        {
            Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
            _frozen = true;
            _rigidbody.Sleep();
        }

        /// <summary>
        /// Resume agent moving and taking actions
        /// </summary>
        public void UnfreezeAgent()
        {
            Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
            _frozen = false;
            _rigidbody.WakeUp();
        }


        /// <summary>
        /// Move the agent to a safe random position, so that it does not collide with anything
        /// If in front of flower, point the beak at the flower
        /// </summary>
        /// <param name="inFrontOFlower">Whether or not to chose a spot in front of a flower</param>
        private void MoveToSafeRandomPosition(bool inFrontOFlower)
        {
            bool safePositionFound = false;
            int attemptsRemaining = 100; //Prevent an infinite loop
            Vector3 potentialPosition = Vector3.zero;
            Quaternion potentialRotation = new Quaternion();

            //Loop for a safe position 
            while (!safePositionFound && attemptsRemaining > 0)
            {
                attemptsRemaining--;

                if (inFrontOFlower)
                {
                    //Pick a random flower
                    Flower randomFlower = _flowerArea.Flowers[UnityEngine.Random.Range(0, _flowerArea.Flowers.Count)];

                    //Position 10 to 20cm in front of the flower
                    float distanceFromFlower = UnityEngine.Random.Range(.1f, .2f);
                    potentialPosition = randomFlower.transform.position + randomFlower.FlowerUpVector * distanceFromFlower;

                    //Point the beak at the flower(bird's head is centre of transform
                    Vector3 toFlower = randomFlower.FlowerCentrePosition - potentialPosition;
                    potentialRotation = Quaternion.LookRotation(toFlower, Vector3.up);
                }
                else
                {
                    //Pick a random height from ground
                    float height = UnityEngine.Random.Range(1.2f, 2.5f);

                    //Pick a random distance from tree
                    float radius = UnityEngine.Random.Range(2f, 7f);

                    //Pick a random direction rotated around the y-axis
                    Quaternion direction = Quaternion.Euler(0f, UnityEngine.Random.Range(-180f, 180f), 0f);

                    //Combine height radius and direction to pick a potential position
                    potentialPosition = _flowerArea.transform.position + Vector3.up * height + direction *
                        Vector3.forward * radius;

                    //Chose and set random starting pitch and yaw
                    float pitch = UnityEngine.Random.Range(-60f, 60f);
                    float yaw = UnityEngine.Random.Range(-180f, 180f);
                    potentialRotation = Quaternion.Euler(pitch, yaw, 0f);
                }

                //Check to see if the agent with collide with anything
                Collider[] colliders = Physics.OverlapSphere(potentialPosition, 0.05f);

                //Safe position has been found if  no colliders are overlapping
                safePositionFound = colliders.Length == 0;
            }

            Debug.Assert(safePositionFound, "could not find a safe position to spawn");

            //Set the position and rotation
            transform.position = potentialPosition;
            transform.rotation = potentialRotation;
        }

        /// <summary>
        /// Update the nearest flower to the agent
        /// </summary>
        private void UpdateNearestFlower()
        {
            foreach (Flower flower in _flowerArea.Flowers)
            {
                if (_nearestFlower == null && flower.HasNectar)
                {
                    //No current nearest flower and this flower has nectar
                    _nearestFlower = flower;
                }
                else if (flower.HasNectar)
                {
                    //Calculate distance to this flower and distance to the current nearest flower
                    float distanceToFlower = Vector3.Distance(flower.transform.position, beakTip.position);
                    float distanceToCurrentNearest = Vector3.Distance(_nearestFlower.transform.position, beakTip.position);

                    //If the current nearest flower is empty or this flower is closer update nearest flower
                    if (!_nearestFlower.HasNectar || distanceToFlower < distanceToCurrentNearest)
                    {
                        _nearestFlower = flower;
                    }
                }
            }
        }

        /// <summary>
    /// Called when the agent's collider enters a trigger collider
    /// </summary>
    /// <param name="other">The trigger collider</param>
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnterOrStay(other);
    }

    /// <summary>
    /// Called when the agent's collider stays in a trigger collider
    /// </summary>
    /// <param name="other">The trigger collider</param>
    private void OnTriggerStay(Collider other)
    {
        TriggerEnterOrStay(other);
    }

    /// <summary>
    /// Handles when the agen'ts collider enters or stays in a trigger collider
    /// </summary>
    /// <param name="collider">The trigger collider</param>
    private void TriggerEnterOrStay(Collider collider)
    {
        // Check if agent is colliding with nectar
        if (collider.CompareTag("nectar"))
        {
            Vector3 closestPointToBeakTip = collider.ClosestPoint(beakTip.position);

            // Check if the closest collision point is close to the beak tip
            // Note: a collision with anything but the beak tip should not count
            if (Vector3.Distance(beakTip.position, closestPointToBeakTip) < BeakTipRadius)
            {
                // Look up the flower for this nectar collider
                Flower flower = _flowerArea.GetFlowerFromNectar(collider);

                
                Debug.Log("Before feed: " + flower.NectarAmount);
                // Attempt to take .01 nectar
                // Note: this is per fixed timestep, meaning it happens every .02 seconds, or 50x per second
                float nectarReceived = flower.Feed(.01f);
                Debug.Log("After feed: " + flower.NectarAmount);
                
                // Keep track of nectar obtained
                NectarObtained += nectarReceived;

                if (trainingMode)
                {
                    // Calculate reward for getting nectar
                        float bonus = .02f * Mathf.Clamp01(Vector3.Dot(transform.forward.normalized, -_nearestFlower.FlowerUpVector.normalized));
                    AddReward(.01f );
                }

                // If flower is empty, update the nearest flower
                if (!flower.HasNectar)
                {
                    UpdateNearestFlower();
                }
            }
        }
    }

        /// <summary>
        /// Called when the agent collides when the agent collides with something solid
        /// </summary>
        /// <param name="other">The collision info</param>
        private void OnCollisionEnter(Collision other)
        {
            if (trainingMode && other.collider.CompareTag("boundary"))
            {
                //Collided with the area boundary, give a negative reward
                AddReward(-.5f);
            }
        }

        //Called every frame
        private void Update()
        {
            //Draw a line from the beak tip to the nearest flower
            if (_nearestFlower != null)
                Debug.DrawLine(beakTip.position, _nearestFlower.FlowerCentrePosition, Color.green);
        }

        //Called every 0.02 seconds
        private void FixedUpdate()
        {
            //Avoids scenario where nearest flower nectar is stolen by opponent and the nearest flower isn't updated
            if (_nearestFlower != null && !_nearestFlower.HasNectar)
                UpdateNearestFlower();
        }
    }
}