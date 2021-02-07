using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float _moveSpeed = 5;
    public Vector3 _moveDir;

    private bool _isStunned;
    private float _stunnedUntil;
    public Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (_isStunned == false)
            _rb.AddForce(_moveDir * _moveSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public void Stun(float stunDuration)
    {
        _isStunned = true;
        Invoke("RemoveStun", stunDuration);
        
    }

    private void RemoveStun()
    {
        _isStunned = false;
    }
}
