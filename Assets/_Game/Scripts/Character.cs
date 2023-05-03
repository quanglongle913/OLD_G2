using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private string currentAnimName;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    { 
    
    }
    //ham huy
    public virtual void OnDespawn()
    {

    }
    public virtual void OnTriggerOtherPlayer()
    {

    }
    protected void ChangeAnim(string animName)
    {

        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
