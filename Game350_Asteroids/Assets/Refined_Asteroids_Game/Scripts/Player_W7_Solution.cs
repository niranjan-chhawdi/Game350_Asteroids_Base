using System;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_W7_Solution : BaseWrappingObject_W7_Solution
{

    [SerializeField] private Transform firePoint;
    private Rigidbody2D rb;

    private GameObject thrustVisual;

    private SpriteRenderer sprite;

    [SerializeField]
    private Solution_W4_OPool objectPool;
    //private Solution_W3_Bullet bulletPrefab;

    [SerializeField]
    private float thrustSpeed = 0.03f;

    [SerializeField]
    private float rotationSpeed = 0.000000000000000008f;

    private bool isInvulnerable = false;
    private float invulnerabilityTime = 3f;
    private IPlayerState currentState;

    //private Bounds screenBounds;

    private void Awake()
    {
        //base.Awake();

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        thrustVisual = transform.GetChild(0).gameObject;
        SetThrustVisual(false);

        objectPool = FindFirstObjectByType<Solution_W4_OPool>();

        ChangeState(new NormalPlayerState());

        //REMOVE VV
        //calculate level bounds by using camera and camera's screen bounds
        //Credit to: (Graham, 2021)
        //screenBounds = new Bounds();
        //screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(Vector3.zero));
        //screenBounds.Encapsulate(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f)));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Save for later
    }

    // Update is called once per frame
    // void Update()
    // {
    //     thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

    //     if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
    //     {
    //         turnDirection = 1f;
    //     }
    //     else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
    //     {
    //         turnDirection = -1f;
    //     }
    //     else
    //     {
    //         turnDirection = 0f;
    //     }

    //     if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
    //     {
    //         Shoot();
    //     }
    // }
    

    private void FixedUpdate()
    {
        ScreenWrap(rb);
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit(this);
        }

        currentState = newState;
        currentState.Enter(this);
    }

     public void Thrust()
    {
        rb.AddForce(transform.up * thrustSpeed);
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.forward, rotationSpeed);
        rb.angularVelocity = 0f;
    }

    public void TurnRight()
    {
        transform.Rotate(Vector3.forward, -rotationSpeed);
        rb.angularVelocity = 0f;
    }

    public void Shoot()
    {
        //Solution_W3_Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //bullet.Shoot(transform.up); 

        Debug.Log("[PLAYER] Single Shooting");

        Solution_W4_OpBullet bullet = (Solution_W4_OpBullet)objectPool.GetBulletPooledObject();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        bullet.Shoot(firePoint.right);
    }

    public void DoubleShoot()
    {
        Debug.Log("[PLAYER] Double Shooting");

        float spreadAngle = 10f;

        Solution_W4_OpBullet bulletLeft = (Solution_W4_OpBullet)objectPool.GetBulletPooledObject();
        bulletLeft.transform.position = firePoint.position;
        bulletLeft.transform.rotation = firePoint.rotation;
        bulletLeft.Shoot(Quaternion.Euler(0, 0, -spreadAngle) * firePoint.right);

        Solution_W4_OpBullet bulletRight = (Solution_W4_OpBullet)objectPool.GetBulletPooledObject();
        bulletRight.transform.position = firePoint.position;
        bulletRight.transform.rotation = firePoint.rotation;
        bulletRight.Shoot(Quaternion.Euler(0, 0, spreadAngle) * firePoint.right);
    }

    

    public void SetThrustVisual(bool show)
    {
        thrustVisual.SetActive(show);
    }

    public void SetPlayerAlpha(float alpha)
    {
        Color newColor = sprite.color;
        newColor.a = alpha;
        sprite.color = newColor;
    }

    public void ResetPlayer()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        ChangeState(new InvulnerablePlayerState());

        Invoke(nameof(SetNormalState), invulnerabilityTime);
    }

    private void SetNormalState()
    {
        ChangeState(new NormalPlayerState());
    }

    public void Die()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        SetThrustVisual(false);
        ChangeState(new DeadPlayerState());

        GameManager_W7_Solution.Instance.OnPlayerDeath(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.HandleCollision(this, collision);
    }

   

   


    //REMOVE VV
    /*private void ScreenWrap()
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
    }*/
}
