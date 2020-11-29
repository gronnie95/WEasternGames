using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterMotor _motor;

    private void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
    }

    void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _motor._moveDir = moveDir;
    }
}
