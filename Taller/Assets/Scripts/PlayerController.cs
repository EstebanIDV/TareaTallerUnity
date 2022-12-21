using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.5f;
    public float jumpforce;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D rigidbody;
    private Animator animator;

    private Vector2 movement;


    private bool facingRight=true;
    private bool isGrounded=true;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalInput, 0f);
        if(horizontalInput<0f && facingRight==true){
            Flip();
        }else if(horizontalInput>0f && facingRight==false){
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if(Input.GetButtonDown("Jump")&& isGrounded == true){
            rigidbody.AddForce(Vector2.up*jumpforce, ForceMode2D.Impulse);
        }

    }

    void FixedUpdate()
    {
        float horizontalVelocity = movement.normalized.x*speed;
        rigidbody.velocity = new Vector2(horizontalVelocity, rigidbody.velocity.y);
    }

    void LateUpdate()
    {
        animator.SetBool("Walking", movement!=Vector2.zero);
        animator.SetBool("Jumped", !isGrounded);
    }

    void Flip(){
        facingRight=!facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX=localScaleX*-1f;
        transform.localScale= new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
}
