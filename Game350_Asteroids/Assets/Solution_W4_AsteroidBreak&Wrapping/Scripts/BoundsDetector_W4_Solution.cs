using UnityEngine;

public class BoundsDetector_W4_Solution : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Debug.Log("[BOUNDS] Bullet Detected");
            collision.GetComponent<Solution_W4_OpBullet>().Release();
        }
    }
}
