using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public Transform spawn;
    public bool InCinematic;

    [Header("Components")]
    private CapsuleCollider2D cc;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField][Range(0, 200)] float speed;
    [SerializeField] SpriteRenderer spritePlayer;

    [Header("Jump")]
    [SerializeField] float forceJump = 600;
    [SerializeField] float forceJumpHori = 40;
    private float hangtime = 0.2f;
    private float hangcounter;

    [SerializeField] float jumpBufferLenght = 0.1f;
    [SerializeField] float jumpBufferCount;

    [Header("OnGround")]
    [SerializeField] bool onGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Transform groundCheckPoint, groundCheckPoint2;

    private void Awake()
    {
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!InCinematic)
        {
            if (onGround)
            {
                Movement();
            }
        }
    }
    private void Update()
    {
        onGround = CheckGround();
        if (!InCinematic)
        {
            Jump();
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = spawn.position;
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && onGround)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, rb.velocity.y);

            spritePlayer.transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
    }

    private void Jump()
    {
        //manager hangtime before jump
        if (onGround)
        {
            hangcounter = hangtime;
        }
        else
        {
            hangcounter -= Time.deltaTime;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Z) && hangcounter > 0)
        {
            //rb.velocity = new Vector2(rb.velocity.x, forceJump);
            rb.AddForce(new Vector2(rb.velocity.x , forceJump));
        }
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, layerGround) || Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, layerGround);
    }
}
