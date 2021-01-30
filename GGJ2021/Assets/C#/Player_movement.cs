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
    public Rigidbody2D rb;

    [Header("Movement")]
    [Range(0, 5)] public float speed;
    [SerializeField][Range(0, 200)] float acceleration;
    [SerializeField] SpriteRenderer spritePlayer;
    public Animator Anim_Player;

    [Header("Jump")]
    [SerializeField] [Range(0, 1)] float airControl = 0.5f;
    [SerializeField] float forceJump = 400;
    float hangtime = 0.2f;
    float hangcounter;
    bool jumped;

    [Header("OnGround")]
    [SerializeField] bool onGround;
    [SerializeField] LayerMask layerGround;
    [SerializeField] Transform groundCheckPoint, groundCheckPoint2;

    [Header("Souvenirs")]
    [SerializeField] Transform spawnSouv;

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
            DeposerSouvManage();
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = spawn.position;
            }
        }
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
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
        if (Input.GetKeyDown(KeyCode.Z) && hangcounter > 0 && !jumped)
        {
            //rb.velocity = new Vector2(rb.velocity.x, forceJump);
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
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, layerGround) || Physics2D.OverlapCircle(groundCheckPoint2.position, 0.1f, layerGround);
    }
    #endregion

    private void DeposerSouvManage()
    {
        if (Input.GetKeyDown(KeyCode.F) && onGround)
        {
            CanMove = false;
            //Anim deposer + anim event depose
            Deposer();
        }
    }
    public void Deposer()
    {
        Instantiate(Resources.Load<GameObject>("Particle_souvenir"),spawnSouv.position, Quaternion.identity);
        CanMove = true;
    }
}
