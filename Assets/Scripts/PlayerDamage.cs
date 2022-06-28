using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] int maxPlayerHealth;
    [SerializeField] int currentPlayerHealth;

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] AudioSource damageAudioSource;

    void Awake()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            RemoveHealth();
        }
    }

    void RemoveHealth() //will probs need to make this public in further development
    {
        damageAudioSource.Play();
        --currentPlayerHealth;
        CheckHealth();
    }

    void RemoveHealth(int healthToRemove)
    {
        currentPlayerHealth -= healthToRemove;
    }

    void CheckHealth()
    {
        if (currentPlayerHealth <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void HealPlayer(int amount)
    {
        currentPlayerHealth += amount;
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
    }
}
