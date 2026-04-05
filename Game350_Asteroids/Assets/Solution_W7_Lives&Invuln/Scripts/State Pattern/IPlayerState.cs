using UnityEngine;

public interface IPlayerState
{
    void Enter(Player_W7_Solution player);
    void Exit(Player_W7_Solution player);
    void HandleCollision(Player_W7_Solution player, Collision2D collision);
}
