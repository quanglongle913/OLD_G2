using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(CapsuleCollider))]
public class PlayerController : Character
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        listBrickObject = new List<GameObject>();
    }
    public override void OnInit()
    {
        base.OnInit();
    }
    public void Update()
    {
        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * moveSpeed, _rigidbody.velocity.y,_fixedJoystick.Vertical * moveSpeed);
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            RotateTowards(this.gameObject, -_rigidbody.velocity);
            ChangeAnim("Run");
        }
        else if (_fixedJoystick.Horizontal == 0 || _fixedJoystick.Vertical == 0)
        {
            ChangeAnim("Idle");
        }

    }
}
