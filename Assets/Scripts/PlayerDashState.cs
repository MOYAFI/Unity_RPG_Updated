using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;

    public PlayerDashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration; // Reset dash duration

        originalGravityScale = rb.gravityScale; // Store the original gravity scale
        rb.gravityScale = 0f; // Disable gravity during dash
    }
    public override void Update()
    {
        base.Update();
        
        player.SetVelocity(player.dashSpeed * player.facingDirection, 0f);

        CancelDashIfNeeded();

        if (stateTimer < 0f)
        {
            if (player.GroundDetected)
            stateMachine.ChangeState(player.idleState); // Change to idle state after dash cooldown
            else 
                stateMachine.ChangeState(player.fallState); // Change to fall state if not grounded
        }
    }

    public override void Exit()
    {
        base.Exit();

        rb.gravityScale = originalGravityScale; // Restore the original gravity scale
    }

    private void CancelDashIfNeeded()
    {
        if (player.WallDetected)
        {
            if (player.GroundDetected)
                stateMachine.ChangeState(player.idleState); // Change to idle state if grounded
            else
                stateMachine.ChangeState(player.wallSlideState); // Change to fall state if not grounded
        }
    }
}
