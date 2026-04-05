public class ShootCommand : ICommand
{
    private Player_W7_Solution player;

    public ShootCommand(Player_W7_Solution player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Shoot();
    }
}
