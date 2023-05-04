using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject brickParent;
    [SerializeField] private string brickNameObj;

    private List<GameObject> listBrickObject;


    private GameObject brickTarget;
    public float meleeRange = 0.1f;
    public float rotationSpeed = 1000f;
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
        //Debug.Log("childCount: " + brickParent.gameObject.transform.childCount);
        
        for (int i = 0; i < brickParent.gameObject.transform.childCount; i++)
        {
            if (brickParent.gameObject.transform.GetChild(i).gameObject.name == brickNameObj)
            {
                if (listBrickObject != null)
                {
                    //Debug.Log("OK1");
                    listBrickObject.Add(brickParent.gameObject.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        sortList(listBrickObject);
        for (int i=0; i < listBrickObject.Count; i++) 
        {
            if (isBrickTarget)
            {
                if (listBrickObject[i].gameObject.activeSelf)
                {
                    //Debug.Log("OK1"); //code run enemy
                    isBrickTarget = false;
                    brickTarget = listBrickObject[i].gameObject;
                    StartCoroutine(ExampleCoroutine("Run", 0.1f, brickTarget, true));
                }
            }
        }
        
        if (brickTarget == null)
            return;
        if (IsInMeleeRangeOf(brickTarget.transform) && !isBrickTarget)
        {
            //Debug.Log("OK2");
            StartCoroutine(ExampleCoroutine("Idle", 0.1f, brickTarget,false));
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
            //Debug.Log("Rotation");
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
    public void sortList(List<GameObject> listObj) 
    { 
        //sap xep theo khoang cach gan nhat voi Enemy
        GameObject gameObject;
        for (int i = 0; i < listObj.Count-1; i++)
        {
            for (int j= 0; j < listObj.Count; j++)
            {
                if (Vector3.Distance(transform.position, listObj[i].gameObject.transform.position) < Vector3.Distance(transform.position, listObj[j].gameObject.transform.position))
                {
                    gameObject = listObj[i];
                    listObj[i] = listObj[j];
                    listObj[j] = gameObject;
                }
            }
        }
        this.listBrickObject = listObj;
    }
}
