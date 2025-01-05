using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();
    }
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
            audioManager.PlaySFX(audioManager.doorOpen);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
