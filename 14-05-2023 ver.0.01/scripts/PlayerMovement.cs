using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 15f;
    private bool isFacingRight = true;

    private bool isJumping;
    //0.2
    private float coyoteTime = 50f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.08f;
    private float jumpBufferCounter;

    private bool doubleJump;
    public int jumpy = 1;
    //dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //смерть и респавн
    private Collider2D _collider;
    private Vector2 _respawnPoint;



    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private AudioSource Jumping; //Звук прыжка
    [SerializeField]
    private bool _active = true;


    private void Start() 
    {
        _collider = GetComponent<Collider2D>();
        SetRespawnPoint(transform.position);
    }

    private void Update()
    {
        if (!_active) { return; }

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && !Input.GetButton("Jump")) 
        {
            doubleJump = false;
        }

        if (IsGrounded() || doubleJump)
        {
            coyoteTimeCounter = coyoteTime;
            
            
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
            
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
            
            //if (jumpy > 1) { --jumpy; doubleJump; } 
           // else if(jumpy <1)  { doubleJump = !doubleJump; }
            Jumping.Play();
            doubleJump = !doubleJump;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
        //if(rb.velocity.y <0f && !isJumping)
        //{
            //!!!
        //    doubleJump = true;
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();

        //Падает ниже позиции - грузит сцену и начинает с нуля
        if (transform.position.y < -5.24f) 
        {
            SceneManager.LoadScene("SampleScene");
        }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.1f);
        isJumping = false;
    }

    private void MiniJump() 
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower / 2);
        //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 20);
    }


    public void SetRespawnPoint(Vector2 position) 
    {
        _respawnPoint = position;
    }

    public void Die() 
    {
        _active = false;
        _collider.enabled = false;
        MiniJump();
        StartCoroutine(Respawn());

    }

    private IEnumerator Respawn() 
    {
        yield return new WaitForSeconds(1f);
        transform.position = _respawnPoint;
        _active = true;
        _collider.enabled = true;
        MiniJump();
    }

    
}


       
       