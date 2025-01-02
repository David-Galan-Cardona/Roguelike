using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : AEnemyBehaviour
{
    private GameObject bullet;
    public GameObject bulletPrefab;
    public Transform bulletTransform;
    public Stack<GameObject> bullets;
    public Enemy_DoorScript _enemyDoorScript;
    public PlayerMovement Player;

    private void Awake()
    {
        bullets = new Stack<GameObject>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        HP = 3 + Player.round;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<AttackState>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<IdleState>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().HP -= 1;
        }
    }

    public void CheckIfAlife()
    {
        if (HP < 1)
        {
            GoToState<DieState>();
            _enemyDoorScript = GetComponent<Enemy_DoorScript>();
            _enemyDoorScript.EnemyDefeated();
            Player.money += 1;
        }
    }
    private void Update()
    {
        CurrentState.OnStateUpdate(this);
    }
    public override void Shoot()
    {
        if (bullets.Count > 0)
        {
            Pop();
        }
        else
        {
            bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().FindShooter(gameObject);
            bullet.GetComponent<EnemyBullet>().damage = 1+Player.round/4;
        }
    }
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        bullets.Push(obj);
    }
    public GameObject Pop()
    {
        GameObject obj = bullets.Pop();
        obj.SetActive(true);
        obj.transform.position = gameObject.transform.position;
        return obj;
    }
}
