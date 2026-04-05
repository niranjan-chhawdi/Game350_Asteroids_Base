using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid_W2_Solution : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float size = 1f;
    [SerializeField]
    private float minSize = 0.35f;
    [SerializeField]
    private float maxSize = 1.65f;
    [SerializeField]
    private float movementSpeed = 50f;

    //TODO: Add max lifetime to asteroid so it is destroyed after a time?
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //TODO: Sprite renderer?
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Use random properties to add variety to spawned asteroids
        //TODO: array of sprites for asteroid to choose

        //varry the rotation of the asteroid
        float rangeVariance = 160f;
        float rotationVariance = Random.Range(-rangeVariance, rangeVariance);
        Quaternion rotation = Quaternion.AngleAxis(rotationVariance, Vector3.forward);
        transform.rotation = rotation;

        //set size of asteroid and set rigidbody mass so that it moves accoridng to mass
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size; //Vector being scaled on Z axis might be a problem, fix for future if so.
        rb.mass = size;

        //Set the trajectory to move in the direction of the spawn
        Vector2 moveDirection = rotation * -transform.position;
        SetInitialMove(moveDirection);//temporary
    }

    public void SetInitialMove(Vector2 dir)
    {
        rb.AddForce(dir.normalized * movementSpeed);
    }

}
