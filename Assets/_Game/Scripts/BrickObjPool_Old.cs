using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickObjPool_Old : MonoBehaviour
{
    [SerializeField] private GameObject prefabsBrickGreen;
    [SerializeField] private GameObject prefabsBrickPurple;
    [SerializeField] private GameObject prefabsBrickRed;
    [SerializeField] private GameObject prefabsBrickYellow;

    [SerializeField] private GameObject PoolParent;
    [SerializeField] private int Row;
    [SerializeField] private int Column;

    private int poolSize;

    private void Awake()
    {
        poolSize = Row * Column;
        GenerateAllObject();
        SetPollBrickPos();
    }

    private void SetPollBrickPos()
    {

        for (int i = 0; i< Row; i++) 
        {
            for (int j = 0; j < Column; j++) 
            {
                
                int index = Row * j + i;
                //row =12 column =10 ///TEST LOGIC
                //i=0 =>z=5 i=1=>z=4 => Z=5-i
                //j=0 x=-6,j=1 x=-5,j=2 x=-4,j=3 x=-3, x=5 j=10
                if (PoolParent.gameObject.transform.GetChild(index).gameObject == null)
                {
                    return;
                }
                PoolParent.gameObject.transform.GetChild(index).gameObject.transform.position = new Vector3((j-(Row/2)),0,((Column/2)-i));
                PoolParent.gameObject.transform.GetChild(index).gameObject.SetActive(true);
            }
        }
       
    }

    private void GenerateNewObject(GameObject brickObj)
    {
        GameObject brick = Instantiate(brickObj);
        brick.transform.SetParent(PoolParent.transform);
        brick.SetActive(false);
    }
    private void GenerateAllObject()
    {
        int coun1 = 0, coun2 = 0, coun3 = 0, coun4 = 0;
        for (int i = 0; i < poolSize; i++)
        {
            int randomIndex = UnityEngine.Random.Range(1, 5);
            switch (randomIndex)
            {
                case 1:
                    if (coun1 < poolSize / 4)
                    {
                        coun1++;
                        GenerateNewObject(prefabsBrickGreen);
                    }
                    else
                    {
                        int random = UnityEngine.Random.Range(1, 4);
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
                        GenerateNewObject(prefabsBrickPurple);
                    }
                    else
                    {
                        int random = UnityEngine.Random.Range(1, 4);
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
                        GenerateNewObject(prefabsBrickRed);
                    }
                    else
                    {
                        int random = UnityEngine.Random.Range(1, 4);
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
                        GenerateNewObject(prefabsBrickYellow);
                    }
                    else
                    {
                        int random = UnityEngine.Random.Range(1, 4);
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
