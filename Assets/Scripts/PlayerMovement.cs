using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, Inputs.IPlayerActions
{

    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRB;
    private Vector2 moveInput;
    private Animator playerAnimator;
    private Inputs _i;
    public int HP = 5;
    public bool alive = true;
    public WeaponManager weaponManager;
    private void Awake()
    {
        //busca el gameobject con el tag weaponmanager
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
        if (HP <= 0)
        {
            playerAnimator.SetBool("IsDying", true);
            alive = false;
            if (weaponManager.activeParticleSistem != null)
            {
                Destroy(weaponManager.activeParticleSistem.gameObject);
            }
        }
    }

    //espera a que la animacion de muerte termine para poner isdyin en false
    public void Die()
    {
        playerAnimator.SetBool("IsDying", false);
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
}
