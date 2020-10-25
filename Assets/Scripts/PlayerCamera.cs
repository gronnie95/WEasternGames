using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject Player;
    private Transform Camera;
    private Transform CameraPivot;

    private Vector3 LocalRotation;
    private float CameraDistance;

    public float MouseSensitivity = 2f;
    public float ScrollSensitivity = 2f;
    public float OrbitSpeed = 5f;
    public float ScrollSpeed = 5f;

    public bool CameraDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        this.Camera = this.transform;
        this.CameraDistance = 2.7f;
        this.transform.parent.position = Player.transform.position;
        this.CameraPivot = this.transform.parent;
    }

    // Update is called once per frame, after Update() on every game object in the scene, otherwise it will some poping issues or with behaviour.
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.CameraDisabled = !CameraDisabled;
        }

        if (!CameraDisabled)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                LocalRotation.y = Mathf.Clamp(LocalRotation.y, -25f, 70f);
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitivity; //control how much scroll every frame

                //Make camera zoom faster the further away it is from the target
                //ScrollAmount *= this.CameraDistance * 0.3f;

                this.CameraDistance += ScrollAmount * -1f;
                this.CameraDistance = Mathf.Clamp(this.CameraDistance, 1.5f, 100f);
            }

            Quaternion QT = Quaternion.Euler(LocalRotation.y, LocalRotation.x, 0);
            this.CameraPivot.rotation = Quaternion.Lerp(CameraPivot.rotation, QT, Time.deltaTime * OrbitSpeed);

            if (this.Camera.localPosition.z != this.CameraDistance * -1f)
            {
                this.Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this.Camera.localPosition.z, this.CameraDistance * -1f, Time.deltaTime * ScrollSpeed));
            }
        }

        Vector3 camDirection = this.CameraPivot.transform.position - this.Camera.transform.position;
        
    }

    private void MouseWheelControl()
    {
        //mouseDetection.y > 0 && mouseDetection.x == 0
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(Player.transform.position, Vector3.right, 20 * Time.deltaTime);
        }

    }
}
