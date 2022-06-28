using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    [SerializeField] int currentXP;
    [SerializeField] Slider XPSlider;
    int currentLevel;
    [SerializeField] int currentLevelXP = 10;
    int levelXPPercentage;
    [Range(1.1f, 1.8f)]
    [SerializeField] float percentageIncreasePerLevel;
    [SerializeField] Text levelText;
    [SerializeField] GameObject upgradeScreen;

    [SerializeField] float maxSpeed;

    GameObject player;

    void Awake()
    {
        currentLevel = 1;
        currentXP = 0;
        levelXPPercentage = 0;
        levelText.text = "Level: " + currentLevel.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //i have to do this as when i made it a child of the player it fucked everything up
        transform.position = player.transform.position;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("XP"))
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            Vector2 dir = col.transform.position - transform.position;
            dir.Normalize();
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(-dir * maxSpeed);
            }
        }
    }

    void UpdateXPUI()
    {
        levelText.text = "Level: " + currentLevel;
        XPSlider.value = (float)currentXP / (float)currentLevelXP;
        /*if (currentXP != 0)
        {
            
        } 
        else
        {
            XPSlider.value = 0;
        }*/
        if (currentXP >= currentLevelXP) 
        {
            LevelUp();
        }
    }

    public void HitXP(GameObject XPObj)
    {
        Destroy(XPObj);
        //maybe do particle effect here
        ++currentXP;
        UpdateXPUI();
    }

    void LevelUp()
    {
        ++currentLevel;
        currentXP = 0;
        currentLevelXP = (int)((float)currentLevelXP * percentageIncreasePerLevel); //lol
        UpdateXPUI();
        upgradeScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void FinishUpgrade()
    {
        Time.timeScale = 1;
        upgradeScreen.SetActive(false);
    }
}
