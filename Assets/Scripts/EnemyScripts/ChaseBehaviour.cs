using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float Speed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Chase(Transform target, Transform self)
    {
        _rb.velocity = (target.position - self.position).normalized * Speed;
    }
    public void Run(Transform target, Transform self)
    {
        _rb.velocity = (target.position - self.position).normalized * -Speed;
    }

    public void StopChasing()
    {
        _rb.velocity = Vector2.zero;
    }
}
