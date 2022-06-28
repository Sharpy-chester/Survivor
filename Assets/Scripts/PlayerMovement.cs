using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float maxMovementSpeed;
    [SerializeField] float acceleration;
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sprite;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
        Anim();
        FlipHandler();
    }

    void Movement()
    {
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && rb.velocity.magnitude < maxMovementSpeed)
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * acceleration * Time.deltaTime, Input.GetAxis("Vertical") * acceleration * Time.deltaTime), ForceMode2D.Force);
        }
    }

    void Anim()
    {
        if (rb.velocity != Vector2.zero && !anim.GetBool("Moving"))
        {
            anim.SetBool("Moving", true);
        }
        else if (rb.velocity == Vector2.zero && anim.GetBool("Moving"))
        {
            anim.SetBool("Moving", false);
        }
        if ((Mathf.Abs(rb.velocity.x) < .1f && Mathf.Abs(rb.velocity.x) > 0))
            rb.velocity = new Vector2(0, rb.velocity.y);
        if ((Mathf.Abs(rb.velocity.y) < .1f && Mathf.Abs(rb.velocity.y) > 0))
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    void FlipHandler()
    {
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
    }

    public void IncreaseMoveSpeed(float amount)
    {
        maxMovementSpeed += amount;
    }
}