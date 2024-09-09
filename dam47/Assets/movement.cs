using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementscrirpt : MonoBehaviour
{
    




    
    public float distance;
    
    private bool AmIFloating;
    public float groundDrag;
    [Header("Movement")]
    public float moveSpeed;
        
    public float maxAirVelocity = 10f; // Maximum velocity in the air
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    public LayerMask blower;
    bool grounded;


    public Transform orientation;
    float horizontalinput;
    float verticalinput;
    Vector3 moveDirection;
    Rigidbody rb;

    public float angle = -20f;
    private AudioSource audioSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        audioSource = GetComponent<AudioSource>();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (grounded == false) // Check if the object is in the air
        {
            CapVelocity();
        }
    }
    void CapVelocity()
    {
       Vector3 currentVelocity = rb.velocity;

        // Cap velocity for both forward/backward (z-axis) and strafing (x-axis) movements
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxAirVelocity, maxAirVelocity);
        currentVelocity.z = Mathf.Clamp(currentVelocity.z, -maxAirVelocity, maxAirVelocity);

        rb.velocity = currentVelocity;
       
    }
    private void MyInput()
    {
        horizontalinput = Input.GetAxisRaw("Horizontal");
        verticalinput = Input.GetAxisRaw("Vertical");
    }
    
    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.W)) {
            audioSource.Play();
        }
        if(Input.GetKeyUp(KeyCode.W)) {
            audioSource.Stop();
        }
        
        
        
        MyInput();
        
        
        grounded = (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatisGround));
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    
        AmIFloating = (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.75f, blower));
        if (AmIFloating)
        {rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 2f, rb.velocity.z);}
        
    }


    private void MovePlayer() {
        moveDirection = orientation.forward * verticalinput + orientation.right * horizontalinput;
        if (grounded) {
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else {
            rb.AddForce(moveDirection.normalized * moveSpeed * 3f, ForceMode.Force);
        }

    }
}
