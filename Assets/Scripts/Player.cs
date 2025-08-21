using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private StateMachine stateMachine;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public PlayerInputSet inputSet { get; private set; }




    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; } // Base class for air states
    public PlayerWallJumpState wallJumpState { get; private set; } // State for wall jumping
    public PlayerDashState dashState { get; private set; } // State for dashing
    public PlayerBasicAttackState basicAttackState { get; private set; } // State for basic attacks


    [Header("Player Movement Details")]
    public float moveSpeed { get; private set; } = 8f;
    public float jumpForce { get; private set; } = 12f;
    public float airSpeed { get; private set; } = .7f;
    private bool isFacingRight = true; // Track the facing direction of the player
    public int facingDirection { get; private set; } = 1; // 1 for right, -1 for left
    public Vector2 wallJumpDirForse;

    [Space]

    public float dashDuration = 0.2f;
    public float dashSpeed;


    [Header("Attack Details")]
    public Vector2[] attackVelocity;
    public float attackVelocityDuration = 0.1f;
    public float comboResetTime = 5.0f;


    [Header("Player Collision Detection")]
    [SerializeField]
    public LayerMask groundLayer;
    private float groundCheckDistance = 1.40f;
    [SerializeField] private float wallCheckDistance = 0.5f; // Distance to check for walls
    public bool GroundDetected;
    public bool WallDetected;

    public Vector2 moveInput { get; private set; }


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        inputSet = new PlayerInputSet();
        stateMachine = new StateMachine();

        // Initialize states
        idleState = new PlayerIdleState(this,stateMachine, "idle");
        moveState = new PlayerMoveState(this,stateMachine, "move");
        fallState = new PlayerFallState(this, stateMachine, "jumpFall");
        jumpState = new PlayerJumpState(this, stateMachine, "jumpFall");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "wallDetected");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "jumpFall");
        dashState = new PlayerDashState(this, stateMachine, "dash");
        basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
    }

    private void OnEnable()
    {
        // Enable the input set when the player is enabled
        inputSet.Enable();

        inputSet.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputSet.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        // Disable the input set when the player is disabled
        inputSet.Disable();
    }

    private void Start()
    {
        // Initialize the state machine with the idle state
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.Update_Actice_State();
        HandleCollisionDetection();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
        FlipHandler(xVelocity);
    }

    public void CallAnimationTrigger()
    {
        stateMachine.CurrentState.CallAnimationTrigger();
    }

    private void FlipHandler(float xVelocity)
    {
        if(xVelocity < 0 && isFacingRight)
            Flip();
        else if (xVelocity > 0 && !isFacingRight)
            Flip();
    }

    private void Flip()
    {
        transform.Rotate(0f,180f, 0f);
        isFacingRight = !isFacingRight;
        facingDirection *= -1; // Change the facing direction
    }

    private void HandleCollisionDetection()
    {
        GroundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        WallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,-groundCheckDistance));
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(wallCheckDistance * facingDirection, 0));
    }

}
