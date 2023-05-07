using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This is an example client that uses our simple object pool.
public class BrickPoolingEX : MonoBehaviour
{

    [Tooltip("Pool Parent Object")]
    [SerializeField] private GameObject PoolParent;

    //[SerializeField] float cooldownWindow = 5.0f;
    [Tooltip("Reference to Object Pool")]
    [SerializeField] private ObjectPool objectPoolBrickGreen;
    [SerializeField] private ObjectPool objectPoolBrickPurple;
    [SerializeField] private ObjectPool objectPoolBrickRed;
    [SerializeField] private ObjectPool objectPoolBrickYellow;

    //[SerializeField] ObjectPool objectPool;


    [SerializeField] private int Row;
    [SerializeField] private int Column;

    [SerializeField] private List<ObjectPool> ListPoolBrick;

    private int poolSize;
    //float nextTimeToShoot = 0;

    private void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        StartCoroutine(OnInitCoroutine(0.1f));
    }
    IEnumerator OnInitCoroutine(float time)
    {

        yield return new WaitForSeconds(time);
        poolSize = Row * Column;
        ListPoolBrick = new List<ObjectPool>();
        ListPoolBrick.Add(objectPoolBrickGreen);
        ListPoolBrick.Add(objectPoolBrickPurple);
        ListPoolBrick.Add(objectPoolBrickRed);
        ListPoolBrick.Add(objectPoolBrickYellow);
        GenerateAllObject();
        SetPollBrickPos();
    }
    private void SetPollBrickPos()
    {
        if (PoolParent.gameObject.transform.childCount > 0)
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Column; j++)
                {
                    int index = Row * j + i;
                    //row =12 column =10 ///TEST LOGIC
                    //i=0 =>z=5 i=1=>z=4 => Z=5-i
                    //j=0 x=-6,j=1 x=-5,j=2 x=-4,j=3 x=-3, x=5 j=10
                    PoolParent.gameObject.transform.GetChild(index).gameObject.transform.position = new Vector3((j - (Row / 2)), 0.05f, ((Column / 2) - i));
                    PoolParent.gameObject.transform.GetChild(index).gameObject.SetActive(true);
                }
            }
        }

    }

    private void GenerateNewObject(int index)
    {
        //Debug.Log(ListPoolBrick.Count +" : Index"+ index);
        if (ListPoolBrick.Count >= 4)
        {
            if (ListPoolBrick[index - 1] != null)
            {
                GameObject brickObject = ListPoolBrick[index - 1].GetPooledObject().gameObject.gameObject;

                if (brickObject == null)
                {
                    return;
                }
                brickObject.transform.SetParent(PoolParent.transform);
                brickObject.SetActive(true);
            }
        }


    }
    private void GenerateAllObject()
    {
        int coun1 = 0, coun2 = 0, coun3 = 0, coun4 = 0;
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = Random.Range(1, 5);
            switch (randomIndex)
            {
                case 1:
                    if (coun1 < poolSize / 4)
                    {
                        coun1++;
                        GenerateNewObject(randomIndex);
                    }
                    else
                    {
                        int random = Random.Range(1, 4);
                        if (random == 1)
                        {
                            goto case 2;
                        }
                        if (random == 2)
                        {
                            goto case 3;
                        }
                        if (random == 3)
                        {
                            goto case 4;
                        }
                        else goto default;
                    }
                    break;
                case 2:

                    if (coun2 < poolSize / 4)
                    {
                        coun2++;
                        GenerateNewObject(randomIndex);
                    }
                    else
                    {
                        int random = Random.Range(1, 4);
                        if (random == 1)
                        {
                            goto case 1;
                        }
                        if (random == 2)
                        {
                            goto case 3;
                        }
                        if (random == 3)
                        {
                            goto case 4;
                        }
                        else goto default;
                    }
                    break;
                case 3:
                    if (coun3 < poolSize / 4)
                    {
                        coun3++;
                        GenerateNewObject(randomIndex);
                    }
                    else
                    {
                        int random = Random.Range(1, 4);
                        if (random == 1)
                        {
                            goto case 1;
                        }
                        if (random == 2)
                        {
                            goto case 2;
                        }
                        if (random == 3)
                        {
                            goto case 4;
                        }
                        else goto default;
                    }
                    break;
                case 4:
                    if (coun4 < poolSize / 4)
                    {
                        coun4++;
                        GenerateNewObject(randomIndex);
                    }
                    else
                    {
                        int random = Random.Range(1, 4);
                        if (random == 1)
                        {
                            goto case 1;
                        }
                        if (random == 2)
                        {
                            goto case 2;
                        }
                        if (random == 3)
                        {
                            goto case 3;
                        }
                        else goto default;
                    }
                    break;
                default:
                    Debug.Log("Error");
                    break;
            }
        }
    }
}
