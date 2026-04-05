using UnityEngine;

public class BaseWrappingObject_W6_Solution : MonoBehaviour
{
    protected Bounds screenBounds;

    private bool boundsInitialized = false;

    protected void ScreenWrap(Rigidbody2D rb)
    {
        if (!boundsInitialized)
        {
            Debug.Log("[BASE_WRAPPING]: Getting Bounds From Manager");
            screenBounds = GameManager_W6_Solution.Instance.GetBounds();

            boundsInitialized = true;
        }

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
