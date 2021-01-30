using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public bool InCinematic;

    [Header("Components")]
    private CapsuleCollider2D cc;
    private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField][Range(0, 200)] float speed;
    [SerializeField] Vector2 forceJump;
    [SerializeField] SpriteRenderer spritePlayer;

    [Header("OnGround")]
    [SerializeField] bool onGround;
    [SerializeField] LayerMask layerGround;

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
        }
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && onGround)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0);

            spritePlayer.transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        }
        //print(rb.velocity.x);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && onGround)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, 0);

            rb.AddForce(forceJump);
        }
    }

    private bool CheckGround()
    {
        float extraHeight = 0.05f;
        RaycastHit2D hit = Physics2D.CircleCast(cc.bounds.center - new Vector3(0,0.65f,0), 0.4f, Vector2.down, 0.01f, layerGround);

        Debug.DrawRay(cc.bounds.center, Vector2.down * (cc.bounds.extents.y + extraHeight));

        if (hit.collider != null)
        {
            // && hit.collider.gameObject.layer == layerGround
            print("bouh" + hit.collider.name);
            return true;
        }
        else
        {
            return false;
        }
    }
}
