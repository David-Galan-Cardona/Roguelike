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
    private void Awake()
    {
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
        playerAnimator.SetFloat("Horizontal", moveInput.x);
        playerAnimator.SetFloat("Vertical", moveInput.y);
        playerAnimator.SetFloat("speed", moveInput.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + moveInput * speed * Time.fixedDeltaTime);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
}
