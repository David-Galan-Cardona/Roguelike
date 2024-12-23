using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private GameObject player;
    //public float damage;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 rotation = mousePos - player.transform.position;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg+180;
        transform.rotation = Quaternion.Euler(0f, 0f, rot + 90);

    }
    /*private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
                other.gameObject.GetComponent<EnemyFollow>().HP -= damage;
                other.gameObject.GetComponent<EnemyFollow>().CheckIfAlife();
            }
        }

    }*/

}
