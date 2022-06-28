using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Time.timeScale > 0)
        {
            Vector2 dir = transform.position - player.position;
            dir.Normalize();
            if (rb.velocity.magnitude < moveSpeed)
            {
                rb.AddForce(-dir * moveSpeed);
            }
            if (rb.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (rb.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
