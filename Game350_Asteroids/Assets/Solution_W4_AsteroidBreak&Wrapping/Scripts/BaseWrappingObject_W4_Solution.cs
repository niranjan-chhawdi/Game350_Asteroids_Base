using UnityEngine;

public class BaseWrappingObject_W4_Solution : MonoBehaviour
{
    protected Bounds screenBounds;

    protected void Awake()
    {
        SetupBounds();
    }
    protected void SetupBounds()
    {
        //Debug.Log("SETTING UP BOUNDS");
        //calculate level bounds by using camera and camera's screen bounds
        //Credit to: (Graham, 2021)
        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    protected void ScreenWrap(Rigidbody2D rb)
    {
        // Move to the opposite side of the screen if the player leaves the screen
        ////Credit to: (Graham, 2021)
        if (rb.position.x > screenBounds.max.x + 0.5f)
        {
            rb.position = new Vector2(screenBounds.min.x - 0.5f, rb.position.y);
        }
        else if (rb.position.x < screenBounds.min.x - 0.5f)
        {
            rb.position = new Vector2(screenBounds.max.x + 0.5f, rb.position.y);
        }
        else if (rb.position.y > screenBounds.max.y + 0.5f)
        {
            rb.position = new Vector2(rb.position.x, screenBounds.min.y - 0.5f);
        }
        else if (rb.position.y < screenBounds.min.y - 0.5f)
        {
            rb.position = new Vector2(rb.position.x, screenBounds.max.y + 0.5f);
        }
    }
}
