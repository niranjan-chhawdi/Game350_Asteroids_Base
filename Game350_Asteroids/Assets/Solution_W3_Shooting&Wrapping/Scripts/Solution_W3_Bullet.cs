using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Solution_W3_Bullet : MonoBehaviour
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

        rb.AddForce(direction * speed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); //OBJECT POOLING?
    }
}
