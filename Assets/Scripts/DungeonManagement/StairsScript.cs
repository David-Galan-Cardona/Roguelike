using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsScript : MonoBehaviour
{
    private GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.transform.position = new Vector3(0, 0, 0);
            Player.GetComponent<PlayerMovement>().round++;
            //reload the SampleScene
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            
        }
    }
}
