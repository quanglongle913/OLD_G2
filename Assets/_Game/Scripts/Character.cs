using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterManager))]
public class Character : MonoBehaviour
{
    
    [SerializeField] private CharacterManager characterManager;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject brickParent;
    [SerializeField] protected GameObject brickTargetObj;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] protected float cooldownWindow = 5.0f;
    
    

    protected List<GameObject> listBrickObject;

    private string currentAnimName;
    public float meleeRange = 0.1f;

    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        StartCoroutine(OnInitCoroutine(brickTargetObj, 0.2f));
        if (characterManager != null)
        {
            characterManager.AddBrick += AddBrick;
        }
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
    protected bool IsInMeleeRangeOf(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance < meleeRange;
    }
    protected void MoveTowards(NavMeshAgent agent,Transform target)
    {
        agent.SetDestination(target.position);
    }
    protected List<GameObject> sortList(List<GameObject> listObj)
    {
        //sap xep theo khoang cach gan nhat voi Enemy
        GameObject gameObject;
        for (int i = 0; i < listObj.Count - 1; i++)
        {
            for (int j = 0; j < listObj.Count; j++)
            {
                if (Vector3.Distance(transform.position, listObj[i].gameObject.transform.position) < Vector3.Distance(transform.position, listObj[j].gameObject.transform.position))
                {
                    gameObject = listObj[i];
                    listObj[i] = listObj[j];
                    listObj[j] = gameObject;
                }
            }
        }
        return listObj;
    }
    protected void RotateTowards(GameObject gameObject, Transform target)
    {
        //Enemy
        Vector3 direction = (target.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion lookRotation = Quaternion.LookRotation(-direction, Vector3.up);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }
    protected void RotateTowards(GameObject gameObject, Vector3 direction)
    {
        //PLayer
        transform.rotation = Quaternion.LookRotation(direction);
    }

    
    protected void ActiveBrickForSeconds(float time, GameObject gameObject)
    {
        StartCoroutine(ActiveBrickCoroutine(time, gameObject));
    }
    protected IEnumerator ActiveBrickCoroutine(float time, GameObject gameObject)
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        gameObject.SetActive(true);
    }
    protected IEnumerator OnInitCoroutine(GameObject brickTargetObj, float time)
    {

        yield return new WaitForSeconds(time);
        //Debug.Log("childCount: " + brickParent.gameObject.transform.childCount);
        for (int i = 0; i < brickParent.gameObject.transform.childCount; i++)
        {
            if (brickParent.gameObject.transform.GetChild(i).gameObject.name == brickTargetObj.gameObject.name)
            {
                if (listBrickObject != null)
                {
                    listBrickObject.Add(brickParent.gameObject.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    private void AddBrick(GameObject arg0)
    {
        if (arg0.gameObject.name == brickTargetObj.gameObject.name)
        {
            // Debug.Log(brickTargetObj.gameObject.name);
            ActiveBrickForSeconds(cooldownWindow, arg0.gameObject);
        }
    }
    private void RemoveBrick()
    {
        
    }
    private void ClearBrick()
    {

    }
    private void Stage()
    {

    }
}
