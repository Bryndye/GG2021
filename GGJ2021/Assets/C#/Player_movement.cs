using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : Singleton<Player_movement>
{
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
    bool jumpedANim;

    [Header("OnGround")]
    [SerializeField] bool atterir;
    [SerializeField] bool onGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform penteCheck, penteCheck2;
    bool rightSide, leftSide;

    private void Awake()
    {
        /*if (Instance != this)
            Destroy(gameObject);*/
        cc = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        cm = CanvasManager.Instance;
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
        CheckPente();
        onGround = CheckGround();
        if (!InCinematic && CanMove)
        {
            Jump();
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
            if (!jumpedANim)
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
            jumpedANim = true;
            Invoke(nameof(Jumped), 0.2f);
        }
    }
    public void Jumped()
    {
        jumped = false;
        Invoke(nameof(endanimJump), 0.3f);
    }
    private void endanimJump()
    {
        jumpedANim = false;
    }

    private bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, layerGround);
    }
    private void CheckPente()
    {
        RaycastHit2D rayRighRight = Physics2D.Raycast(penteCheck2.position, Vector2.right, 0.1f );
        RaycastHit2D rayRighDown = Physics2D.Raycast(penteCheck2.position, Vector2.down, 0.1f);
        RaycastHit2D rayLeftLeft = Physics2D.Raycast(penteCheck.position, Vector2.left, 0.1f);
        RaycastHit2D rayLeftDown = Physics2D.Raycast(penteCheck.position, Vector2.down, 0.1f);
        Debug.DrawRay(penteCheck2.position, Vector2.right * 0.1f);
        Debug.DrawRay(penteCheck2.position, Vector2.down * 0.1f);
        Debug.DrawRay(penteCheck.position, Vector2.left * 0.1f);
        Debug.DrawRay(penteCheck.position, Vector2.down * 0.1f);

        if (rayRighRight.collider != null && rayRighRight.collider.CompareTag("pente") || rayRighDown.collider != null && rayRighDown.collider.CompareTag("pente"))
        {
            rightSide = true;
            //Debug.Log("sur une pente a droite");
        }
        else
        {
            rightSide = false;
            //Debug.Log("pas pente droite");
        }
        if (rayLeftLeft.collider != null && rayLeftLeft.collider.CompareTag("pente") || rayLeftDown.collider != null && rayLeftDown.collider.CompareTag("pente"))
        {
            leftSide = true;
            //Debug.Log("sur une pente a gauche");
        }
        else
        {
            leftSide = false;
            //Debug.Log("pas pente gauche");
        }

        if (rightSide && Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.gravityScale = 0;
        }
        else if (rightSide && Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.gravityScale = 5;
        }
        else if(rightSide && Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.gravityScale = 0;
        }

        if (leftSide && Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.gravityScale = 5;
        }
        else if (leftSide && Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.gravityScale = 0;
        }
        else if (leftSide && Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.gravityScale = 0;
        }

        if (!leftSide && !rightSide)
        {
            rb.gravityScale = 5;
        }
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
    CanvasManager cm;
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
            if (es.Index < es.IndexMax)
            {
                cm.PressF.SetActive(true);
            }
            canInteract = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Souvenir") && es.Index < es.IndexMax)
        {
            cm.PressF.SetActive(true);
        }
        else
        {
            cm.PressF.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Souvenir"))
        {
            cm.PressF.SetActive(false);
            canInteract = false;
        }
    }
    #endregion
}
