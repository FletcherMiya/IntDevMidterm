using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalMove;
    public float force;

    bool grounded = false;
    public float castDist = 0.2f;

    public float jumpHeight;
    public float speedLimit;

    bool jump = false;
    bool doublejump = true;
    bool walljump = false;
    bool walljumpEnabled = false;

    Vector3 startpos = new Vector3(-11.2f, 0.81f, 0f);

    public Particle burstParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            if(doublejump || grounded || (walljump && walljumpEnabled))
            {
                jump = true;
            }
        }
        
    }

    private void FixedUpdate()
    {
        HorizontalMove(horizontalMove);

        if(rb.transform.position.x < -6.15 && rb.transform.position.x > -7.15 && rb.transform.position.y < 22 && rb.transform.position.y > 20)
        {
            walljumpEnabled = true;
        }

        if (jump)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight));
            jump = false;
            if (!grounded && doublejump)
            {
                doublejump = false;
            }
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        if(hit.collider != null)
        {
            grounded = true;
            doublejump = true;
        }
        else
        {
            grounded = false;
        }

        if (burstParticles != null && hit && horizontalMove != 0)
        {
            Vector3 tempPos = new Vector3(transform.position.x, transform.position.y - 0.5f);
            Instantiate(burstParticles, tempPos, Quaternion.identity);
        }

        RaycastHit2D wallcheckleft = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
        RaycastHit2D wallcheckright = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);
        if ((wallcheckright.collider != null || wallcheckleft.collider != null) && horizontalMove != 0 && walljumpEnabled)
        {
            walljump = true;
            doublejump = true;
        }
        else
        {
            walljump = false;
        }


        if (transform.position.y < -10)
        {
            transform.position = startpos;
            walljumpEnabled = false;
        }

    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * Time.fixedDeltaTime * force;
        rb.velocity = new Vector3(moveX, rb.velocity.y);
        /*if(Mathf.Abs(rb.velocity.x) < speedLimit)
        {
            rb.AddForce(transform.right * moveX, ForceMode2D.Impulse);
        }*/
    }
}
