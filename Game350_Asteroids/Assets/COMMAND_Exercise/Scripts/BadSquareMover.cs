using UnityEngine;

public class BadSquareMover : MonoBehaviour
{
    /*
     * CHALLENGE: Convert Square Mover to use the Command Pattern
     * Step1: ICommand interface with execute and undo
     * Step2: Different Movement command objects using interface, with reference to mover, constructor, execute and undo commands
     * Step3: CommandInvoker that invokes commands when they come in
     * Step4: SquareMover with methods invoked by commands
     * Step5: InputManager where pressing a button runs a player command method that tells command invoker to execute the appropriate command
     */

    [SerializeField]
    private float stepSize = 10f; //how far does square move

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
    }

    void MoveUp()
    {
        transform.position += Vector3.up * stepSize;
    }

    void MoveDown()
    {
        transform.position += Vector3.down * stepSize;
    }

    void MoveLeft()
    {
        transform.position += Vector3.left * stepSize;
    }

    void MoveRight()
    {
        transform.position += Vector3.right * stepSize;
    }
}
