using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : AEnemyBehaviour
{
    public int damage;
    private Enemy_DoorScript _enemyDoorScript;
    public PlayerMovement Player;
    public GameObject HPBar;
    public float percentageHP = 100;
    public float HPBarScale = 0.5f;
    public float CurrentHPBarScale;

    private void HPBarUpdate()
    {
        percentageHP = HP * 100 / MaxHP;
        CurrentHPBarScale = HPBarScale * percentageHP / 100;
        HPBar.transform.localScale = new Vector3(CurrentHPBarScale, HPBar.transform.localScale.y, HPBar.transform.localScale.z);
    }
    private void Start()
    {
        //busca player
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        damage = 1 + Player.round/2;
        MaxHP = 2 + Player.round*1.3f;
        HP = MaxHP;
        //busca tag hpbar
        HPBar = transform.Find("HPBar").gameObject;
        HPBarUpdate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<ChaseState>();
            _animator.SetBool("Follow", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<IdleState>();
            _animator.SetBool("Follow", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<DieState>();
            _animator.SetBool("Boom", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //deactivate the collider
            GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<PlayerMovement>().HP -= damage;
            collision.gameObject.GetComponent<PlayerMovement>().CheckIfAlive();
            //get enemydoorscript and call enemydefeated
            _enemyDoorScript = GetComponent<Enemy_DoorScript>();
            _enemyDoorScript.EnemyDefeated();
            Player.UpdateHud(false, false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spawnpoint"))
        {
            if (HP > 0)
            {
                GoToState<ChaseState>();
            }
            _animator.SetBool("Boom", false);
        }
    }

    public void CheckIfAlife()
    {
        Player.GetComponent<AudioManager>().PlaySFX(Player.GetComponent<AudioManager>().enemyDamage);
        HPBarUpdate();
        if (HP < 1)
        {
            GoToState<DieState>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _animator.SetBool("Boom", true);
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
    }
}
