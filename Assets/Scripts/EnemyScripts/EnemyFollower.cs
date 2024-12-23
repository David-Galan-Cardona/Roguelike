using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : AEnemyBehaviour
{
    private ChaseBehaviour _chaseB;
    public Animator _animator;
    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GoToState<ChaseState>();
            _animator.SetBool("Follow", true);
            collision.gameObject.GetComponent<PlayerMovement>().HP -= 1;
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
        if (HP < 1)
        {
            GoToState<DieState>();
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _animator.SetBool("Boom", true);
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
