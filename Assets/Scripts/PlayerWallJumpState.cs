using UnityEngine;

public class PlayerWallJumpState : EntityState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(player.wallJumpDirForse.x * -player.facingDirection, player.wallJumpDirForse.y); // Reset vertical velocity on wall jump
    }
    public override void Update()
    {
        base.Update();

        if (!player.WallDetected)
            stateMachine.ChangeState(player.fallState); // Change to fall state if no wall detected
    }

    public override void Exit()
    {
        base.Exit();
    }

}
