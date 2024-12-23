using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour, Inputs.IWeaponActions
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bulletPrefab;
    public GameObject flamethrowerPrefab;
    public GameObject meleePrefab;
    public GameObject Weapon;
    public Transform bulletTransform;
    public bool canShoot;
    public float timer;
    public float timeBetweenShots;
    private Inputs _i;
    public int weaponHolded = 1;
    private GameObject activeParticleSistem;
    private GameObject bullet;
    private Animator weaponAnimator;
    public Sprite Orbe;
    public Sprite Espada;
    public Sprite OrbeFuego;
    //spawner
    public static WeaponManager instance;
    public Stack<GameObject> bullets;
    public GameObject spawnPoint;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponAnimator = Weapon.GetComponent<Animator>();
        weaponAnimator.SetInteger("WeaponAnimated", 1);
    }
    private void Awake()
    {
        instance = this;
        bullets = new Stack<GameObject>();
        _i = new Inputs();
        _i.Weapon.SetCallbacks(this);
    }
    private void OnEnable()
    {
        _i.Enable();
        _i.Weapon.Shoot.canceled += OnStopShooting;
    }
    private void OnDisable()
    {
        _i.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint");
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenShots)
            {
                canShoot = true;
                timer = 0;
            }
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
        obj.transform.position = spawnPoint.transform.position;
        return obj;
    }
    public GameObject Peek()
    {
        return bullets.Peek();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (canShoot)
        {
            if (weaponHolded == 1)
            {
                canShoot = false;
                //activa el collider de la espada
                Weapon.GetComponent<PolygonCollider2D>().enabled = true;
                //haz la animacion de weapon
                weaponAnimator = Weapon.GetComponent<Animator>();
                weaponAnimator.SetTrigger("Melee");
                StartCoroutine(wait());
            }
            else if (weaponHolded == 2)
            {
                canShoot = false;
                //Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
                if (bullets.Count > 0)
                {
                    Pop();
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
                    bullet.GetComponent<WeaponDamageScript>().damage = 1;
                    bullet.GetComponent<WeaponDamageScript>().knockback = false;
                }
            }
            else if (weaponHolded == 3)
            {
                if (context.started)
                {
                    canShoot = false;
                    activeParticleSistem = Instantiate(flamethrowerPrefab, bulletTransform.position, Quaternion.identity);
                    activeParticleSistem.transform.parent = bulletTransform;
                    activeParticleSistem.transform.localScale = new Vector3(1, 1, 1);
                    activeParticleSistem.GetComponent<ParticleHit>().damage = 0.1f;
                }
            }
        }
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(0.125f);
        Weapon.GetComponent<PolygonCollider2D>().enabled = false;
        weaponAnimator.SetBool("Melee", false);
    }

    public void OnStopShooting(InputAction.CallbackContext context)
    {
        if (weaponHolded == 3)
        {
            if (activeParticleSistem != null)
            {
                Destroy(activeParticleSistem);
            }
        }
    }

    public void OnSwitchWeapon1(InputAction.CallbackContext context)
    {
        if (weaponHolded == 3)
        {
            if (activeParticleSistem != null)
            {
                Destroy(activeParticleSistem);
            }
        }
        if (weaponHolded != 1)
        {
            weaponHolded = 1;
            Weapon.GetComponent<SpriteRenderer>().sprite = Espada;
            weaponAnimator = Weapon.GetComponent<Animator>();
            weaponAnimator.SetInteger("WeaponAnimated", 1);
        }
        timeBetweenShots = 1;
            
    }

    public void OnSwitchWeapon2(InputAction.CallbackContext context)
    {
        if (weaponHolded == 3)
        {
            if (activeParticleSistem != null)
            {
                Destroy(activeParticleSistem);
            }
        }
        if (weaponHolded != 2)
        {
            weaponHolded = 2;
            Weapon.GetComponent<SpriteRenderer>().sprite = Orbe;
            weaponAnimator.SetInteger("WeaponAnimated", 2);
        }
        timeBetweenShots = 0.5f;
        
            
    }

    public void OnSwitchWeapon3(InputAction.CallbackContext context)
    {
        if (weaponHolded != 3)
        {
            weaponHolded = 3;
            Weapon.GetComponent<SpriteRenderer>().sprite = OrbeFuego;
            weaponAnimator.SetInteger("WeaponAnimated", 3);
        }
        

    }

    public void OnSwitchWeapon4(InputAction.CallbackContext context)
    {
        if (weaponHolded == 3)
        {
            if (activeParticleSistem != null)
            {
                Destroy(activeParticleSistem);
            }
        }
        if (weaponHolded != 4)
        {
            weaponHolded = 4;
            Weapon.GetComponent<SpriteRenderer>().sprite = Orbe;
            weaponAnimator.SetInteger("WeaponAnimated", 4);
        }
    }
}
