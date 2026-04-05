using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_W3_Solution : MonoBehaviour
{
    private Rigidbody2D rb;

    private GameObject thrustVisual;

    [SerializeField]
    private Solution_W3_Bullet bulletPrefab;

    [SerializeField]
    private float thrustSpeed = 1f;

    private bool thrusting { get; set; }


    [SerializeField]
    private float rotationSpeed = 0.1f;
    private float turnDirection;

    private Bounds screenBounds;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        thrustVisual = transform.GetChild(0).gameObject;
        ShowThrustVisual(false);

        //calculate level bounds by using camera and camera's screen bounds
        //Credit to: (Graham, 2021)
        screenBounds = new Bounds();
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Save for later
    }

    // Update is called once per frame
    void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1f;
        }
        else
        {
            turnDirection = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        ShowThrustVisual(thrusting);
        if (thrusting)
        {
            rb.AddForce(transform.up * thrustSpeed);
            
        }

        if (turnDirection != 0f)
        {
            //rb.AddTorque(rotationSpeed * turnDirection);
            transform.Rotate(Vector3.forward, rotationSpeed * turnDirection);
            rb.angularVelocity = 0f;
        }

        ScreenWrap();
    }

    private void Shoot()
    {
        Solution_W3_Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Shoot(transform.up); 
    }

    private void ShowThrustVisual(bool show)
    {
        thrustVisual.SetActive(show);
    }

    private void ScreenWrap()
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
