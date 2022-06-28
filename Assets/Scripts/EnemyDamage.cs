using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    int currentHealth;
    Animator anim;

    void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Bullet"))
        {
            Destroy(col.gameObject);
            --currentHealth;
            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if(currentHealth < 1)
        {
            anim.SetBool("Dead", true);
            Destroy(GetComponent<Collider2D>());
            Destroy(GetComponent<EnemyMovement>());
            GetComponent<AudioSource>().Play(); //if i change the sound on this and the sound is longer than the death anim, it will get cut off
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
