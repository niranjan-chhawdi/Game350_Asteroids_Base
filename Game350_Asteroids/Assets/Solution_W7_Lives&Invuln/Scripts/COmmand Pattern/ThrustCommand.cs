public class ThrustCommand : ICommand
{
    private Player_W7_Solution player;

    public ThrustCommand(Player_W7_Solution player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.Thrust();
    }
}
