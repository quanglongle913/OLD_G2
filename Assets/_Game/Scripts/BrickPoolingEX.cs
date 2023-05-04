using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DesignPatterns.ObjectPool
{
    // This is an example client that uses our simple object pool.
    public class BrickPoolingEX : MonoBehaviour
    {
       
        [Tooltip("Pool Parent Object")]
        [SerializeField] private GameObject PoolParent;

        //[SerializeField] private GameObject projectile;

        [SerializeField] float cooldownWindow = 0.1f;
        [Tooltip("Reference to Object Pool")]
        [SerializeField] private ObjectPool objectPoolBrickGreen;
        [SerializeField] private ObjectPool objectPoolBrickPurple;
        [SerializeField] private ObjectPool objectPoolBrickRed;
        [SerializeField] private ObjectPool objectPoolBrickYellow;

        //[SerializeField] ObjectPool objectPool;


        [SerializeField] private int Row;
        [SerializeField] private int Column;

        private int poolSize;
 
        private void Awake()
        {
            
        }
        private void Start()
        {
                poolSize = Row * Column;
                GenerateAllObject();
                SetPollBrickPos();
        }
        private void FixedUpdate()
        {
            /*// shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot && objectPool != null)
            {

                // get a pooled object instead of instantiating
                GameObject bulletObject = objectPool.GetPooledObject().gameObject;

                if (bulletObject == null)
                    return;

                bulletObject.SetActive(true);

                // align to gun barrel/muzzle position
                bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

                // move projectile forward
                bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);

                // turn off after a few seconds
                ExampleProjecBrickPool projectile = bulletObject.GetComponent<ExampleProjecBrickPool>();
                projectile?.Deactivate(); //?  if's actived -> call Deactivate

                // set cooldown delay
                nextTimeToShoot = Time.time + cooldownWindow;

            }*/
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
                        PoolParent.gameObject.transform.GetChild(index).gameObject.transform.position = new Vector3((j - (Row / 2)), 0, ((Column / 2) - i));
                        PoolParent.gameObject.transform.GetChild(index).gameObject.SetActive(true);
                    }
                }
            }

        }

        private void GenerateNewObject(ObjectPool objectPoolBrickColor)
        {
            //Debug.Log(objectPool.gameObject.name);
            if (objectPoolBrickColor != null)
            {
                //Basic 
                //GameObject brickObject = Instantiate(brickObj);

                //==================================================
                //New Code 
                // get a pooled object instead of instantiating
                GameObject brickObject = objectPoolBrickColor.GetPooledObject().gameObject.gameObject;
                
                if (brickObject == null)
                {
                    return;
                }

                brickObject.transform.SetParent(PoolParent.transform);
                brickObject.SetActive(true);
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
                            GenerateNewObject(objectPoolBrickGreen);
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
                            GenerateNewObject(objectPoolBrickPurple);
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
                            GenerateNewObject(objectPoolBrickRed);
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
                            GenerateNewObject(objectPoolBrickYellow);
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
}