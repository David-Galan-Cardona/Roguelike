using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, Inputs.IPlayerActions
{

    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRB;
    private Vector2 moveInput;
    public Animator playerAnimator;
    private Inputs _i;
    public int HP = 10;
    public int money = 0;
    public bool alive = true;
    public int round = 1;
    public WeaponManager weaponManager;
    public static PlayerMovement instance;
    public Canvas deathMenu;

    //Hud
    public TextMeshProUGUI HPHUD;
    public TextMeshProUGUI MoneyHUD;
    public TextMeshProUGUI RoundHUD;
    public Image SwordHUD;
    public Image OrbHUD;
    public Image FlamethrowerHUD;
    private void Awake()
    {
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        _i = new Inputs();
        _i.Player.SetCallbacks(this);
    }
    private void OnEnable()
    {
        _i.Enable();
    }
    private void OnDisable()
    {
        _i.Disable();  
    }

    void Update()
    {
        if (alive == true)
        {
            playerAnimator.SetFloat("Horizontal", moveInput.x);
            playerAnimator.SetFloat("Vertical", moveInput.y);
            playerAnimator.SetFloat("speed", moveInput.sqrMagnitude);
        }
        
    }
    private void FixedUpdate()
    {
        if (alive == true)
        {
            playerRB.MovePosition(playerRB.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (alive == true)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }
    public void CheckIfAlive()
    {
        HPHUD.text = HP.ToString();
        gameObject.GetComponent<AudioManager>().PlaySFX(gameObject.GetComponent<AudioManager>().playerDamage);
        if (HP <= 0)
        {
            playerAnimator.SetBool("IsDying", true);
            alive = false;
            if (weaponManager.activeParticleSistem != null)
            {
                Destroy(weaponManager.activeParticleSistem.gameObject);
            }
            deathMenu.GetComponent<Canvas>().enabled = true;
        }
    }

    public void Die()
    {
        playerAnimator.SetBool("IsDying", false);
    }

    void Start()
    {
        HPHUD.text = "10";
        MoneyHUD.text = "0";
        RoundHUD.text = "LVL 0";
        OrbHUD.enabled = false;
        FlamethrowerHUD.enabled = false;
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateHud(bool orb, bool fire)
    {
        RoundHUD.text = ("LVL" + round.ToString());
        HPHUD.text = HP.ToString();
        MoneyHUD.text = money.ToString();
        if (orb == true)
        {
            OrbHUD.enabled = true;
        }
        if (fire == true)
        {
            FlamethrowerHUD.enabled = true;
        }
    }
}
