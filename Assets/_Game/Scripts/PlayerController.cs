using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _fixedJoystick;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;

    private string currentAnin;
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_fixedJoystick.Horizontal * _moveSpeed, _rigidbody.velocity.y,_fixedJoystick.Vertical * _moveSpeed);
        if (_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(-_rigidbody.velocity);
            ChangeAnin("Run");
            //Debug.Log("Horizontal: " + _fixedJoystick.Horizontal + "|| Vertical: " + _fixedJoystick.Vertical);
        }
        else if (_fixedJoystick.Horizontal == 0 || _fixedJoystick.Vertical == 0)
        {
            ChangeAnin("Idle");
            //Debug.Log("Horizontal: " + _fixedJoystick.Horizontal + "|| Vertical: " + _fixedJoystick.Vertical);
        }
       

    }
    private void ChangeAnin(string aninName)
    {
        if (currentAnin != aninName)
        {
            _animator.ResetTrigger(aninName);
            currentAnin = aninName;
            _animator.SetTrigger(currentAnin);
        }

    }
}
