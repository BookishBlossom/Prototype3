using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatedPlayerController : MonoBehaviour
{
    //Movement Variables
    private float verticalInput;
    public float moveSpeed;

    private float horizontalInput;
    public float turnSpeed;
    public ParticleSystem dirt;

    //Jumping Variables
    private Rigidbody rb;
    public float jumpForce;
    public bool isOnGround;

    //animation variables
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        //Get Components
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Forward and Backward Movement
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * verticalInput);

        //Activate run or idle animation
        animator.SetFloat("verticalInput", Mathf.Abs (verticalInput));

        //Rotation
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);

        //Jumping
        if(Input.GetKeyDown(KeyCode.Space)  && isOnGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetBool("isOnGround", isOnGround);
        }

        //shoot
        if(Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("shoot");
        }

        if (verticalInput > 0 && !dirt.isPlaying)
        {
            dirt.Play();
        }
        else if (verticalInput <= 0)
        {
            dirt.Stop();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("isOnGround", isOnGround);

        }
    }
}
