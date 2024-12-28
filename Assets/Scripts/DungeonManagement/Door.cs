using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left,
        right,
        top,
        bottom
    }
    public DoorType doorType;
    public int enemiesToDefeat;

    public void EnemyDefeated()
    {
        enemiesToDefeat--;
        if (enemiesToDefeat <= 0 && gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
