using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{
    public UnityAction<GameObject> AddBrick;
    public UnityAction<GameObject> RemoveBrick;
    public UnityAction<GameObject> ClearBrick;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick")
        {
            //Debug.Log(other.gameObject.name);
            AddBrick(other.gameObject);
        }
    }
}
