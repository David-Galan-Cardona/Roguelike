using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public GameObject activeParticleSistem;
    private GameObject bullet;
    private Animator weaponAnimator;
    public Sprite Orbe;
    public Sprite Espada;
    public Sprite OrbeFuego;
    public GameObject player;
    //weapon levels
    public int swordLevel = 1;
    public int orbLevel = 1;
    public int flamethrowerLevel = 1;
    //spawner
    public static WeaponManager instance;
    public Stack<GameObject> bullets;
    public GameObject spawnPoint;
    //Hud
    public Image SwordHUD;
    public Image OrbHUD;
    public Image FlamethrowerHUD;
    public Sprite Selected;
    public Sprite Unselected;
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        weaponAnimator = Weapon.GetComponent<Animator>();
        weaponAnimator.SetInteger("WeaponAnimated", 1);
    }
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

    void Update()
    {
        if (player.GetComponent<PlayerMovement>().alive == true)
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
        if (canShoot && player.GetComponent<PlayerMovement>().alive == true)
        {
            if (weaponHolded == 1)
            {
                canShoot = false;
                Weapon.GetComponent<WeaponDamageScript>().damage = 1 * swordLevel;
                Weapon.GetComponent<PolygonCollider2D>().enabled = true;
                weaponAnimator = Weapon.GetComponent<Animator>();
                weaponAnimator.SetTrigger("Melee");
                StartCoroutine(wait());
            }
            else if (weaponHolded == 2)
            {
                canShoot = false;
                if (bullets.Count > 0)
                {
                    Pop();
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
                    bullet.GetComponent<WeaponDamageScript>().damage = 1*orbLevel;
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
                    activeParticleSistem.GetComponent<ParticleHit>().damage = 0.1f*flamethrowerLevel;
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
            SwordHUD.sprite = Selected;
            OrbHUD.sprite = Unselected;
            FlamethrowerHUD.sprite = Unselected;
        }
        timeBetweenShots = 1;
            
    }

    public void OnSwitchWeapon2(InputAction.CallbackContext context)
    {
        if (orbLevel != 0)
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
                SwordHUD.sprite = Unselected;
                OrbHUD.sprite = Selected;
                FlamethrowerHUD.sprite = Unselected;
            }
            timeBetweenShots = 0.5f;
        }
    }

    public void OnSwitchWeapon3(InputAction.CallbackContext context)
    {
        if (flamethrowerLevel != 0)
        {
            if (weaponHolded != 3)
            {
                weaponHolded = 3;
                Weapon.GetComponent<SpriteRenderer>().sprite = OrbeFuego;
                weaponAnimator.SetInteger("WeaponAnimated", 3);
                SwordHUD.sprite = Unselected;
                OrbHUD.sprite = Unselected;
                FlamethrowerHUD.sprite = Selected;
            }
        }
    }

    public void OnSwitchWeapon4(InputAction.CallbackContext context)
    {
        //no hay weapon 4, pero si se añade se puede usar este metodo
        if (flamethrowerLevel != 0)
        {
            if (weaponHolded != 3)
            {
                weaponHolded = 3;
                Weapon.GetComponent<SpriteRenderer>().sprite = OrbeFuego;
                weaponAnimator.SetInteger("WeaponAnimated", 3);
                SwordHUD.sprite = Unselected;
                OrbHUD.sprite = Unselected;
                FlamethrowerHUD.sprite = Selected;
            }
        }
        /*if (weaponHolded == 3)
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
        }*/
    }
}
