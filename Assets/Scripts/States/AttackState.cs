using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "StatesSO/Attack")]
public class AttackState : StatesSO
{

    public override void OnStateEnter(AEnemyBehaviour ec)
    {
        ec.target = GameObject.FindGameObjectWithTag("Player");
        ec.rb = ec.GetComponent<Rigidbody2D>();
    }

    public override void OnStateExit(AEnemyBehaviour ec)
    {
    }

    public override void OnStateUpdate(AEnemyBehaviour ec)
    {
        //rotate turret to face target
        Vector2 direction = ec.target.transform.position - ec.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ec.rb.rotation = angle-90;
        //shoot
        if (ec.timeBetweenShots <= 0)
        {
            ec.timeBetweenShots = 1f;
            ec.Shoot();
        }
        else
        {
            ec.timeBetweenShots -= Time.deltaTime;
        }
    }
}