using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : Singleton<Player_movement>
{
    public Transform spawn;
    public bool InCinematic = false;
    public bool CanMove = true;

    [Header("Components")]
    private CapsuleCollider2D cc;
    [SerializeField] public Rigidbody2D rb;

    [Header("Movement")]
    public float speed;
    [SerializeField] float acceleration;
    [SerializeField] SpriteRenderer spritePlayer;
    public Animator Anim_Player;

    [Header("Jump")]
    [SerializeField] [Range(0, 1)] float airControl = 0.5f;
    [SerializeField] float forceJump = 400;
    float hangtime = 0.2f;
    float hangcounter;
    bool jumped;

    [Header("OnGround")]
    [SerializeField] bool atterir;
    [SerializeField] bool onGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Transform groundCheckPoint, groundCheckPoint2;

    private void Awake()
    {
        if (Instance != this)
            Destroy(this);
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        CanMove = true;
    }

    private void FixedUpdate()
    {
        if (!InCinematic && CanMove)
        {
            Movement();
        }
    }
    private void Update()
    {
        onGround = CheckGround();
        if (!InCinematic && CanMove)
        {
            Jump();
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = spawn.position;
            }
            if (Input.GetKeyDown(KeyCode.F) && onGround && CanMove && canInteract)
            {
                if (es != null && es.Index < es.IndexMax)
                {
                    CanMove = false;
                    Anim_Player.SetTrigger("Interact");
                }
            }
        }
        if (!onGround)
        {
            atterir = false;
            if (!jumped)
            {
                Anim_Player.SetTrigger("Fall");
            }
        }
        CheckOnGroundFirst();
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Anim_Player.SetBool("isMoving", true);
            Anim_Player.SetTrigger("ChangeMove");
            if (!onGround)
            {
                rb.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * airControl * 100, 0);
            }
            else
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * acceleration, rb.velocity.y);
            }
            if (rb.velocity.x > speed)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            if (rb.velocity.x < -speed)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            //print(rb.velocity.x);
            spritePlayer.transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
        else
        {
            Anim_Player.SetBool("isMoving", false);
            Anim_Player.SetTrigger("ChangeMove");
        }
    }

    #region JUMP
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
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && hangcounter > 0 && !jumped)
        {
            //rb.velocity = new Vector2(rb.velocity.x, forceJump);

            Anim_Player.SetTrigger("Jump");
            Anim_Player.SetTrigger("ChangeMove");
            rb.AddForce(new Vector2(rb.velocity.x , forceJump));
            jumped = true;
            Invoke(nameof(Jumped), 0.2f);
        }
    }
    public void Jumped()
    {
        jumped = false;
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, layerGround);
    }
    private void CheckOnGroundFirst()
    {
        if (onGround && !atterir)
        {
            atterir = true;
            Anim_Player.SetTrigger("Land");
        }
    }
    #endregion

    #region VentSouvenir
    public Event_souvenirs es;
    bool canInteract;
    public void Deposer()
    {
        CanMove = true;
    }
    public void CallEvent()
    {
        es.SendSouvenir(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Souvenir"))
        {
            es = collision.GetComponent<Event_souvenirs>();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Souvenir"))
        {
            canInteract = false;
        }
    }
    #endregion
}
