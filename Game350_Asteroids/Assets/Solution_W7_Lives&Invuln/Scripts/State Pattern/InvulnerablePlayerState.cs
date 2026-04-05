using UnityEngine;

public class InvulnerablePlayerState : IPlayerState
{
    public void Enter(Player_W7_Solution player)
    {
        player.SetPlayerAlpha(0.5f);
    }

    public void Exit(Player_W7_Solution player)
    {
        player.SetPlayerAlpha(1f);
    }

    public void HandleCollision(Player_W7_Solution player, Collision2D collision)
    {
    }
}
