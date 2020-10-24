using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

/// <summary>
/// A Hummingbird ML Agent
/// </summary>
public class HummingBirdAgent : Agent
{
 [Tooltip("Force to apply when moving")]
 public float moveForce = 2f;

 [Tooltip("Speed to pitch up or down")]
 public float pitchSpeed = 100f;

 [Tooltip("Speed to rotate around the up axis")]
 public float yawSpeed = 100f;

 [Tooltip("Transform at the tip of the beak")]
 public Transform beakTip;

 [Tooltip("Agent's camera")] 
 public Camera agentCamera;

 [Tooltip("Training or gameplay mode")]
 public bool trainingMode;

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
 public float NectarObtained { get; private set; }
}
