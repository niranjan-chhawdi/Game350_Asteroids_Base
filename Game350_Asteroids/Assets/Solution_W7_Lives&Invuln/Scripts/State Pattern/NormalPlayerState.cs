using UnityEngine;

public class NormalPlayerState : IPlayerState
{
    public void Enter(Player_W7_Solution player)
    {
        player.SetPlayerAlpha(1f);
    }

    public void Exit(Player_W7_Solution player)
    {
    }

    public void HandleCollision(Player_W7_Solution player, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            player.Die();
        }
    }
}
