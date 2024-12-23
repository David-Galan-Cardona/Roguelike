using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using static UnityEditor.VersionControl.Asset;

public abstract class AEnemyBehaviour : MonoBehaviour
{
    public float HP;
    public float speed;
    public StatesSO CurrentState;
    public GameObject target;
    public Rigidbody2D rb;
    public float timeBetweenShots;
    public Animator _animator;


    void Start()
    {
        /*_chaseB = GetComponent<ChaseBehaviour>();*/
    }

    public void GoToState<T>() where T : StatesSO
    {
        if (CurrentState.StatesToGo.Find(state => state is T))
        {
            CurrentState.OnStateExit(this);
            CurrentState = CurrentState.StatesToGo.Find(obj => obj is T);
            CurrentState.OnStateEnter(this);
        }
    }
    public abstract void Shoot();
}
