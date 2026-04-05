using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid_W7_Solution : BaseWrappingObject_W7_Solution
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

   //private Vector2 activeBounds = new Vector2(12, 6); //REMOVE

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
        //size = Random.Range(minSize, maxSize);
        //THIS IS NEEDED FOR SPLIT ASTEROID TO PROPERLY SET SIZE
        transform.localScale = Vector3.one * size;
        rb.mass = size;

        //Set the trajectory to move in the direction of the spawn
        Vector2 moveDirection = rotation * -transform.position;
        SetInitialMove(moveDirection);//temporary
    }

    private void FixedUpdate()
    {
        ScreenWrap(rb);
        //TODO? Clamp Speed
    }

    public void SetInitialMove(Vector2 dir)
    {
        rb.AddForce(dir.normalized * movementSpeed);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // check if we are big enough ti split
            if ((size * 0.5f) >= minSize)
            {
                //split; Should be a single call to a manager instance
                SplitAsteroid(this); //TODO:move to manager instance
                SplitAsteroid(this); //TODO:move to manager instance
            }

            //TODO: GAME MANAGER, notify of destruction of final piece

            // Destroy the asteroid anyway
            GameManager_W7_Solution.Instance.ScoreOnAsteroidDestroy();
            Destroy(gameObject); //NOTE: Should use Object Pool; May be moved to manager INSTANCE
        }
    }

    //TODO: this should be utilized by a spawner or manager, using object pooling
    private Asteroid_W7_Solution SplitAsteroid(Asteroid_W7_Solution parentAsteroid)
    {
        //calculate new position of split asteroid
        Vector2 position = parentAsteroid.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        // Create the new asteroid at a smaller size
        Asteroid_W7_Solution splitAst = Instantiate(this, position, transform.rotation);
        splitAst.size = size * 0.5f;//half the size of the original

        // Set a random trajectory
        splitAst.SetInitialMove(Random.insideUnitCircle.normalized);

        return splitAst;
    }

    public void SetRandomSize()
    {
        size = Random.Range(minSize, maxSize);
        transform.localScale = Vector3.one * size;
        rb.mass = size;
    }

    //REMOVE VV
    /*
    private void ScreenWrap() //TODO: Should be replaced by screen bounds method in Player, provided by game manager
    {
        // Alternative wrap
        if (rb.position.x > activeBounds.x + 0.5f)
        {
            rb.position = new Vector2(-activeBounds.x - 0.5f, rb.position.y);
        }
        else if (rb.position.x < -activeBounds.x - 0.5f)
        {
            rb.position = new Vector2(activeBounds.x + 0.5f, rb.position.y);
        }
        else if (rb.position.y > activeBounds.y + 0.5f)
        {
            rb.position = new Vector2(rb.position.x, -activeBounds.y - 0.5f);
        }
        else if (rb.position.y < -activeBounds.y - 0.5f)
        {
            rb.position = new Vector2(rb.position.x, activeBounds.y + 0.5f);
        }
    }*/

}
