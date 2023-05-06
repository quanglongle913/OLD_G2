using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterManager : MonoBehaviour
{
    public UnityAction<GameObject> AddBrick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Brick")
        {
            //Debug.Log(other.gameObject.name);
            AddBrick(other.gameObject);
        }

    }
}
