using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public float damage;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particleCollision");
        if (other.gameObject != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit");
                if (other.gameObject.GetComponent<EnemyFollow>() != null)
                {
                    if (!(other.gameObject.GetComponent<EnemyFollow>().CurrentState is DieState))
                    {
                        other.gameObject.GetComponent<EnemyFollow>().HP -= damage;
                        other.gameObject.GetComponent<EnemyFollow>().CheckIfAlife();
                    }
                }
                else
                {
                    if (!(other.gameObject.GetComponent<EnemyTurret>().CurrentState is DieState))
                    {
                        other.gameObject.GetComponent<EnemyTurret>().HP -= damage;
                        other.gameObject.GetComponent<EnemyTurret>().CheckIfAlife();
                    }
                }
            }
        }

    }
}
