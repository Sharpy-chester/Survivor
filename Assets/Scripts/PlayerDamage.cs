using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] int maxPlayerHealth;
    [SerializeField] int currentPlayerHealth;
    XPSystem xpSystem;
    [SerializeField] Slider healthSlider;
    [SerializeField] Text healthText;

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] AudioClip playerHurtSound;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] AudioSource damageAudioSource;

    void Awake()
    {
        xpSystem = FindObjectOfType<XPSystem>();
        currentPlayerHealth = maxPlayerHealth;
        UpdateHealthUI();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            RemoveHealth();
        }
        else if (col.gameObject.CompareTag("XP"))
        {
            xpSystem.HitXP(col.gameObject);
        }
    }

    void RemoveHealth() //will probs need to make this public in further development
    {
        damageAudioSource.Play();
        --currentPlayerHealth;
        UpdateHealthUI();
        CheckHealth();
    }

    void RemoveHealth(int healthToRemove)
    {
        currentPlayerHealth -= healthToRemove;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        //it may say that the cast is redundant, but when i remove the cast it breaks. Ignore the warning as its a massive liar
        healthSlider.value = (float)currentPlayerHealth / (float)maxPlayerHealth;
        healthText.text = currentPlayerHealth + "/" + maxPlayerHealth;
    }

    void CheckHealth()
    {
        if (currentPlayerHealth <= 0)
        {
            //Game Over
            damageAudioSource.clip = playerDeathSound;
            damageAudioSource.Play();
            Destroy(GetComponent<PlayerMovement>());
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Destroy(xpSystem);
            Destroy(this);
        }
    }

    public void HealPlayer(int amount)
    {
        currentPlayerHealth += amount;
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        UpdateHealthUI();
    }
}
