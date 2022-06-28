using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    PlayerShoot playerShoot;
    PlayerMovement playerMove;
    PlayerDamage playerDamage;
    XPSystem xpSystem;
    [Range(0.7f, 1f)]
    [SerializeField] float fireRateModifier = 0.9f;
    [Range(0.5f, 2f)]
    [SerializeField] float moveSpeedIncrease = 1f;
    [Range(1, 3)]
    [SerializeField] int healAmount = 1;


    void Awake()
    {
        playerShoot = FindObjectOfType<PlayerShoot>();
        playerMove = FindObjectOfType<PlayerMovement>();
        playerDamage = FindObjectOfType<PlayerDamage>();
        xpSystem = FindObjectOfType<XPSystem>();
    }

    public void FireRate()
    {
        playerShoot.IncreaseFireRate(fireRateModifier);
        xpSystem.FinishUpgrade();
    }

    public void MoveSpeed()
    {
        playerMove.IncreaseMoveSpeed(moveSpeedIncrease);
        xpSystem.FinishUpgrade();
    }

    public void Heal()
    {
        playerDamage.HealPlayer(healAmount);
        xpSystem.FinishUpgrade();
    }
}
