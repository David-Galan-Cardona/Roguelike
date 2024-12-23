using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "StatesSO/Chase")]


public class ChaseState : StatesSO
{
    public override void OnStateEnter(AEnemyBehaviour ec)
    {
        ec.target = GameObject.FindGameObjectWithTag("Player");
        ec.rb = ec.GetComponent<Rigidbody2D>();
    }

    public override void OnStateExit(AEnemyBehaviour ec)
    {
        //stop following target
        ec.rb.velocity = Vector2.zero;
    }

    public override void OnStateUpdate(AEnemyBehaviour ec)
    {
        //follow target position
        //if target is left of enemy, ,invert sprite
        if (ec.target.transform.position.x < ec.transform.position.x)
        {
            ec.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            ec.transform.localScale = new Vector3(1, 1, 1);
        }
        ec.rb.velocity = (ec.target.transform.position - ec.transform.position).normalized * ec.speed;
    }
}
