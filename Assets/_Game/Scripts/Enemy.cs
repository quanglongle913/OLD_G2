using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;

    private GameObject brickTarget;

    private bool isBrickTarget = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        listBrickObject = new List<GameObject>();
    }

    public override void OnInit()
    {
        base.OnInit();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Moving();
    }
    private void Moving()
    {
        listBrickObject = sortList(listBrickObject);
        for (int i = 0; i < listBrickObject.Count; i++)
        {
            if (isBrickTarget)
            {
                if (listBrickObject[i].gameObject.activeSelf)
                {
                    //Debug.Log("OK1"); //code run enemy
                    isBrickTarget = false;
                    brickTarget = listBrickObject[i].gameObject;
                    StartCoroutine(MoveCoroutine("Run", 0.2f, brickTarget));
                }
            }
        }

        if (brickTarget == null)
            return;
        if (IsInMeleeRangeOf(brickTarget.transform) && !isBrickTarget)
        {
            //Debug.Log("OK2");
            ChangeAnim("Idle");
            isBrickTarget = true;
        }
    }
    IEnumerator MoveCoroutine(string animName, float time, GameObject ObjTarget)
    {
        yield return new WaitForSeconds(time);
        ChangeAnim(animName);
        MoveTowards(agent, ObjTarget.transform);
        RotateTowards(this.gameObject, ObjTarget.transform);
    }
}
