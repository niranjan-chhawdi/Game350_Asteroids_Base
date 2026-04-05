public class TurnLeftCommand : ICommand
{
    private Player_W7_Solution player;

    public TurnLeftCommand(Player_W7_Solution player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.TurnLeft();
    }
}
