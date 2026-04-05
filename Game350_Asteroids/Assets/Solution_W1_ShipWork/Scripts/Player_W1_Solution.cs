using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_W1_Solution : MonoBehaviour
{
    private Rigidbody2D rb;

    //thruster visual we will display when going forward
    private GameObject thrustVisual;

    [SerializeField]
    private float thrustSpeed = 1f;

    private bool thrusting { get; set; }
    

    [SerializeField]
    private float rotationSpeed = 0.1f;
    private float turnDirection; //we will track which direction to turn the ship

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        thrustVisual = transform.GetChild(0).gameObject; //asssumes thruster is always the first child
        ShowThrustVisual(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Save for later
    }

    // Update is called once per frame
    void Update()
    {
        //going old school for key inputs. OPTIONAL TODO: update to modern unity inputs
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
            //rb.AddTorque(rotationSpeed * turnDirection); //try this first
            transform.Rotate(Vector3.forward, rotationSpeed * turnDirection); //this feels better
        }

        
    }

    //will handle activating or deactivating the thrust visual
    private void ShowThrustVisual(bool show)
    {
        thrustVisual.SetActive(show);
    }
}
