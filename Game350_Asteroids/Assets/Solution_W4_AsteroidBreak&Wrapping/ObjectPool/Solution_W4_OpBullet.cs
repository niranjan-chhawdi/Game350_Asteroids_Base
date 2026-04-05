using UnityEngine;

public class Solution_W4_OpBullet : Solution_W4_BulletPoolObject
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 500f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        //Shoot(transform.up);//for testing
    }

    public void Shoot(Vector2 direction)
    {
        Debug.Log("[BULLET] Shooting");
        rb.AddForce(direction * speed);

    }

    public new void Release()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        base.Release();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(gameObject); //OBJECT POOLING?
        Debug.Log("[BULLET] Collision");
        Release();
    }
}
