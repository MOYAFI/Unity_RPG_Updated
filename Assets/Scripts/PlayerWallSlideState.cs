using UnityEngine;

public class PlayerWallSlideState : PlayerAirState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }
    public override void Update()
    {
        base.Update();

        HandleWallSlide();

        if (inputSet.Player.Jump.WasPerformedThisFrame()) { 
        
            stateMachine.ChangeState(player.wallJumpState); // Change to wall jump state if jump is pressed
        }



        if (!player.WallDetected)
            stateMachine.ChangeState(player.fallState); // Change to fall state if no wall detected

        
        if (player.GroundDetected)
            stateMachine.ChangeState(player.idleState);


    }

    public override void Exit()
    {
        base.Exit();
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y == 0)
        {
            player.SetVelocity(rb.linearVelocityX, rb.linearVelocityY * .3f);
        }
        else
        {
            player.SetVelocity(rb.linearVelocityX, rb.linearVelocityY);
        }
    }

}
