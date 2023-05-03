using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerController : Character
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;
   

    [SerializeField] private float moveSpeed;

   
    public void Update()
    {
        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * moveSpeed, _rigidbody.velocity.y,_fixedJoystick.Vertical * moveSpeed);
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(-_rigidbody.velocity);
            ChangeAnim("Run");
            //Debug.Log("Horizontal: " + _fixedJoystick.Horizontal + "|| Vertical: " + _fixedJoystick.Vertical);
        }
        else if (_fixedJoystick.Horizontal == 0 || _fixedJoystick.Vertical == 0)
        {
            ChangeAnim("Idle");
            //Debug.Log("Horizontal: " + _fixedJoystick.Horizontal + "|| Vertical: " + _fixedJoystick.Vertical);
        }
       

    }
}
