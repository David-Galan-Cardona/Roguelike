using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DoorScript : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> doors = new List<GameObject>();
    public Door doorScript;

    public void EnemyDefeated()
    {
        foreach (GameObject door in doors)
        {
            doorScript = door.GetComponent<Door>();
            doorScript.EnemyDefeated();
        }
    }
}
