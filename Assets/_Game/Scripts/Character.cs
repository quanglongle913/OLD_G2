using DesignPatterns.ObjectPool;
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

    [Tooltip("Pool Parent Object")]
    [SerializeField] private GameObject BrickStackParent;
    [SerializeField] private ObjectPool objectPoolBrickColor;

    [SerializeField] protected int index;


    protected List<GameObject> listBrickObject;

    private string currentAnimName;
    public float meleeRange = 0.1f;
    protected int brickCount;

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
        //Create Pooling Object in BrickStackParent of Player
        GenerateNewObject(index);
        SetPollBrickPos();
        brickCount = 0;
    }

    //ham huy
    public virtual void OnDespawn()
    {
        if (characterManager != null)
        {
            characterManager.AddBrick -= AddBrick;
        }
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

    private void SetPollBrickPos()
    {
        if (BrickStackParent.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < BrickStackParent.gameObject.transform.childCount; i++)
            {
                BrickStackParent.gameObject.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(0, i, 0);
                BrickStackParent.gameObject.transform.GetChild(i).gameObject.transform.localScale = new Vector3(1, 0.96f, 1);
                BrickStackParent.gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    private void GenerateNewObject(int index)
    {
        for (int i = 0; i < index; i++)
        {
            GameObject brickObject = objectPoolBrickColor.GetPooledObject().gameObject.gameObject;

            if (brickObject == null)
            {
                return;
            }
            brickObject.transform.SetParent(BrickStackParent.transform);
            brickObject.SetActive(true);
        }

    }
    protected void ActiveBrickForSeconds(float time, GameObject gameObject)
    {
        if (brickCount<index)
        {
            StartCoroutine(ActiveBrickCoroutine(time, gameObject));
            brickCount++;
            for (int i = 0; i < brickCount; i++)
            {
                if (!BrickStackParent.gameObject.transform.GetChild(i).gameObject.activeSelf)
                {
                    BrickStackParent.gameObject.transform.GetChild(i).gameObject.SetActive(true);
                }

            }
        }
        

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
