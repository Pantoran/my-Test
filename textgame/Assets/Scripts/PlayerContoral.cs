using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoral : MonoBehaviour
{
    // Start is called before the first frame update

    public float MoveForce = 100;
    public float JumpForce = 300;
    private float fInput = 0.0f;
    private float MaxSpeed = 5;
    private Rigidbody2D rigidBody;
    private bool bFaceRight = true;
    private bool bGrounded = false;
    private Transform mGroundCheck;
    public bool bJump = false;

    void Awake()
    {
        // Setting up references.
        mGroundCheck = transform.Find("GroundCheck");

    }
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        // If the jump button is pressed and the player is grounded then the player should jump.
        if (Input.GetButtonDown("Jump") && bGrounded)
            bJump = true;
    }
    private void FixedUpdate()
    {
        fInput = Input.GetAxis("Horizontal");
        //获取方向键
        if (fInput > 0 && !bFaceRight)
        {
            flip();
        }
        else if (fInput < 0 && bFaceRight)
        {
            flip();//控制反向
        }



        if (fInput * rigidBody.velocity.x < MaxSpeed)
        {
            rigidBody.AddForce(Vector2.right * fInput * MoveForce);
        }
        //限制最大速度
        if (Mathf.Abs(rigidBody.velocity.x) > MaxSpeed)
        {
            rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * MaxSpeed, rigidBody.velocity.y);
        }


        if (bJump)
        {
            rigidBody.AddForce(new Vector2(0f, JumpForce));
            bJump = false;
        }

    }
    void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
        //转向
    }
}
