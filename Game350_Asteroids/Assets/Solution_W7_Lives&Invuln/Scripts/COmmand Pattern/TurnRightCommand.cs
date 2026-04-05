public class TurnRightCommand : ICommand
{
    private Player_W7_Solution player;

    public TurnRightCommand(Player_W7_Solution player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.TurnRight();
    }
}
