using UnityEngine;

public class PlayerBasicAttackState : EntityState
{
    private float attackTimer;

    private float lastTimeAttacked;

    private const int FirstAttackIndex = 1;
    private int comboIndex = 1;
    private int maxComboLimit = 3;
    public PlayerBasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        if (comboIndex != player.attackVelocity.Length)
            comboIndex = player.attackVelocity.Length;
    }

    public override void Enter()
    {
        base.Enter();
        
        ResetComboIndexIfNeeded();
        
        anim.SetInteger("BasicAttackIndex", comboIndex);

        ApplyAttackVelocity();
    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
        
        lastTimeAttacked = Time.time;
        
        comboIndex++;
    }

    private void HandleAttackVelocity()
    {
        attackTimer -= Time.deltaTime; // Decrease the attack timer

        if (attackTimer < 0f)
            player.SetVelocity(0, rb.linearVelocityY);
    }

    private void ApplyAttackVelocity()
    {
        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];
        
        attackTimer = player.attackVelocityDuration; // Set the attack timer to the duration of the attack velocity
        player.SetVelocity(attackVelocity.x * player.facingDirection, attackVelocity.y);
    }
    
    private void ResetComboIndexIfNeeded()
    {
        
        if (comboIndex > maxComboLimit || Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstAttackIndex;
    }

}
