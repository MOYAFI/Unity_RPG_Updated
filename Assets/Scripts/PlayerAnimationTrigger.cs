using UnityEngine;

public class PlayerAnimationTrigger: MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationEndEvent() {
        player.CallAnimationTrigger();
    } 
    
}
