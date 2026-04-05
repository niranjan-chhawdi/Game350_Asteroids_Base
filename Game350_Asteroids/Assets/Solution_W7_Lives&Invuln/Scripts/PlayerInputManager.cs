using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    private Player_W7_Solution player;

    private CommandInvoker invoker;
    private ICommand thrustCommand;
    private ICommand turnLeftCommand;
    private ICommand turnRightCommand;
    private ICommand shootCommand;

    private void Awake()
    {
        invoker = new CommandInvoker();

        thrustCommand = new ThrustCommand(player);
        turnLeftCommand = new TurnLeftCommand(player);
        turnRightCommand = new TurnRightCommand(player);
        shootCommand = new ShootCommand(player);
    }

    private void Update()
    {
        bool isThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        player.SetThrustVisual(isThrusting);

        if (isThrusting)
        {
            invoker.ExecuteCommand(thrustCommand);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            invoker.ExecuteCommand(turnLeftCommand);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            invoker.ExecuteCommand(turnRightCommand);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            invoker.ExecuteCommand(shootCommand);
        }
        if (Input.GetMouseButtonDown(1))
        {
            player.DoubleShoot();
        }
    }
}
