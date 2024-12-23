using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float force;
    public float LifeTime = 2f;
    public GameObject shooter;

    public abstract void OnEnable();

    public abstract void FindShooter(GameObject spawnpoint);

    public void Update()
    {
        EndOfLifeTime();
    }

    public abstract void EndOfLifeTime();

    public abstract void OnTriggerEnter2D(Collider2D collision);
}
