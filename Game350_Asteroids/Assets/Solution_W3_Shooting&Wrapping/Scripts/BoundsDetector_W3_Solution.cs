using UnityEngine;

public class BoundsDetector_W3_Solution : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid") || collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); //Object Pooling?
        }
    }
}
