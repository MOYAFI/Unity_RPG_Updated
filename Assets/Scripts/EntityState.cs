using UnityEngine;

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;
    protected Rigidbody2D rb;
    protected PlayerInputSet inputSet;

    protected Animator anim;

    protected float cooldownTimer = 0f;
    private float coolDownLimite = 4f; // Example cooldown duration

    protected float stateTimer;
    public bool triggerCalled;


    public EntityState(Player player ,StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        inputSet = player.inputSet;
    }

    public virtual void Enter()
    {
        triggerCalled = false; // Reset trigger called flag

        anim.SetBool(animBoolName, true);

        cooldownTimer = coolDownLimite; // Reset cooldown timer when entering the state
    }

    public virtual void Update()
    {
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        stateTimer -= Time.deltaTime; // Decrease the dash duration over time

        if (inputSet.Player.Dash.WasPerformedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
    }

    public virtual void Exit()
    {
       anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (stateMachine.CurrentState == player.dashState)
            return false;
        if (player.WallDetected)
            return false;

        return true;
    }
}
