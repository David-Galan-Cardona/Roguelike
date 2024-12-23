using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : ABullet
{
    private Vector3 target;
    private Camera mainCam;
    public Transform transformObj;
    void Start()
    {
    }
    public override void OnEnable()
    {
        shooter = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(shooter);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        target = mainCam.ScreenToWorldPoint(Input.mousePosition);
        LifeTime = 2f;
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = target - shooter.transform.position;
        Vector3 rotation = target - shooter.transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot + 90);
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            WeaponManager.instance.Push(gameObject);
            Debug.Log("Bullet destroyed");
        }
    }

    public override void FindShooter(GameObject spawnpoint)
    {
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
            WeaponManager.instance.Push(gameObject);
        }
    }
}
