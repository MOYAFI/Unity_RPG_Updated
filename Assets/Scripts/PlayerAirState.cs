using UnityEngine;

public class PlayerAirState : EntityState
{
    public PlayerAirState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
    }
    public override void Update()
    {
        base.Update();
        // In Air Control
        if (player.moveInput.x != 0)
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.airSpeed), rb.linearVelocity.y);

        // If Wall Detected While in Air Chnage to Wall Slide State
        if (player.WallDetected)
            stateMachine.ChangeState(player.wallSlideState);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
