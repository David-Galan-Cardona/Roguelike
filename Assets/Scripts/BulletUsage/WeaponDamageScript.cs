using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WeaponDamageScript : MonoBehaviour
{
    public bool knockback = true;
    private GameObject player;
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //mira si tiene el componente EnemyFollow
            if (collision.gameObject.GetComponent<EnemyFollow>() != null)
            {
                if (knockback)
                {
                    Debug.Log("Knockback");
                    player = GameObject.FindGameObjectWithTag("Player");
                    collision.gameObject.GetComponent<EnemyFollow>().GoToState<KnockbackState>();
                    //push the enemy
                    Vector2 pushDirection = collision.transform.position - player.transform.position;
                    pushDirection = pushDirection.normalized;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(pushDirection * 2, ForceMode2D.Impulse);
                }
                Debug.Log("Enemy hit");
                collision.gameObject.GetComponent<EnemyFollow>().HP -= damage;
                collision.gameObject.GetComponent<EnemyFollow>().CheckIfAlife();
            }
            else
            {
                collision.gameObject.GetComponent<EnemyTurret>().HP -= damage;
                collision.gameObject.GetComponent<EnemyTurret>().CheckIfAlife();
            }
        }
    }
}
