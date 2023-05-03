using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject brickParent;
    [SerializeField] private string brickNameObj;


    private bool isBrickTarget=true;
    private GameObject brickTarget;
    public float meleeRange = 0.1f;
    public float rotationSpeed = 1000f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < brickParent.gameObject.transform.childCount; i++)
        {
            //Debug.Log("OK");
            if (isBrickTarget)
            {
                if (brickParent.gameObject.transform.GetChild(i).gameObject.activeSelf && brickParent.gameObject.transform.GetChild(i).gameObject.name == brickNameObj)
                {
                    //Debug.Log("OK");
                    isBrickTarget = false;
                    brickTarget = brickParent.gameObject.transform.GetChild(i).gameObject;
                    StartCoroutine(ExampleCoroutine("Run",0.2f, brickTarget,true));
                    //MoveTowards(brickTarget.transform);
                
                }
            }
        }
        if (IsInMeleeRangeOf(brickTarget.transform) && !isBrickTarget)
        {
            StartCoroutine(ExampleCoroutine("Idle", 0.2f, brickTarget,false));
            //MoveTowards(brickTarget.transform);
            
            brickTarget.SetActive(false);
            isBrickTarget = true;
            //Debug.Log("Move done ");
        }
    }
    IEnumerator ExampleCoroutine(string animName,float time,GameObject ObjTarget,bool isRotation)
    {
       
        yield return new WaitForSeconds(time);
        ChangeAnim(animName);
        MoveTowards(ObjTarget.transform);

        if (isRotation)
        {
            Debug.Log("Rotation");
            RotateTowards(ObjTarget.transform);
        }
        


    }
   
    private bool IsInMeleeRangeOf(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < meleeRange;
    }

    private void MoveTowards(Transform target)
    {
        agent.SetDestination(target.position);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion lookRotation = Quaternion.LookRotation(-direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
