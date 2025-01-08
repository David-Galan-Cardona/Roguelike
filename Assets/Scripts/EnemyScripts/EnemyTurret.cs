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
    public GameObject HPBar;
    public float percentageHP = 100;
    public float HPBarScale = 0.5f;
    public float CurrentHPBarScale;
    private void HPBarUpdate()
    {
        percentageHP = HP * 100 / MaxHP;
        CurrentHPBarScale = HPBarScale * percentageHP / 100;
        HPBar.transform.localScale = new Vector3( HPBar.transform.localScale.x, CurrentHPBarScale, HPBar.transform.localScale.z);
    }

    private void Awake()
    {
        bullets = new Stack<GameObject>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        MaxHP = 3 + Player.round;
        HP = MaxHP;
        HPBar = transform.Find("HPBar").gameObject;
        HPBarUpdate();
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
            collision.gameObject.GetComponent<PlayerMovement>().CheckIfAlive();
            Player.UpdateHud(false, false);
        }
    }

    public void CheckIfAlife()
    {
        Player.GetComponent<AudioManager>().PlaySFX(Player.GetComponent<AudioManager>().enemyDamage);
        HPBarUpdate();
        if (HP < 1)
        {
            GoToState<DieState>();
            _enemyDoorScript = GetComponent<Enemy_DoorScript>();
            _enemyDoorScript.EnemyDefeated();
            Player.money += 1;
            Player.UpdateHud(false, false);
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
