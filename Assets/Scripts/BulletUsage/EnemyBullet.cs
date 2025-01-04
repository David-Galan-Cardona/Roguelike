using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : ABullet
{
    private Vector3 target;
    public Transform transformObj;
    public EnemyTurret spawner;
    public int damage = 1;
    void Start()
    {
    }
    public override void OnEnable()
    {
        LifeTime = 2f;
        if (shooter == null)
        {
            StartCoroutine(Wait());
        }
        else
        {
            Debug.Log(shooter);
            //busca player
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            rb = GetComponent<Rigidbody2D>();
            Vector3 direction = target - shooter.transform.position;
            Vector3 rotation = target - shooter.transform.position;
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot + 90);
            spawner = shooter.GetComponent<EnemyTurret>();
        }

    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerMovement>().HP -= damage;
                spawner.Push(gameObject);
                collision.gameObject.GetComponent<PlayerMovement>().CheckIfAlive();
                collision.gameObject.GetComponent<PlayerMovement>().UpdateHud(false, false);
            }
            else
            {
                spawner.Push(gameObject);
            }
        }
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        OnEnable();
    }

    public override void FindShooter(GameObject spawnpoint)
    {
        shooter = spawnpoint;
    }

    public override void EndOfLifeTime()
    {
        if (LifeTime > 0)
        {
            LifeTime -= Time.deltaTime;
        }
        else
        {
            LifeTime = 2f;
            spawner.Push(gameObject);
        }
    }
}
