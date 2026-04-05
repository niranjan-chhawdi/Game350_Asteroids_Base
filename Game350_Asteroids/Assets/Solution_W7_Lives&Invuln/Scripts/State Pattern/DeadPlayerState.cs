using UnityEngine;

public class DeadPlayerState : IPlayerState
{
    public void Enter(Player_W7_Solution player)
    {
        player.SetThrustVisual(false);
    }

    public void Exit(Player_W7_Solution player)
    {
    }

    public void HandleCollision(Player_W7_Solution player, Collision2D collision)
    {
    }
}
