using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Space(5)]
    [Header("References")]
    //WeaponManager weaponManager;
    //DamageReceiver damageReceiver;

    [Space(5)]
    [Header("Weapons")]
    public float primaryWeaponDamage = 1.0f;
    public float secondaryWeaponDamage = 25.0f;
    public float specialWeaponDamage = 100.0f;

    [Space(5)]
    [Header("Vitality Stats")]
    public float maximumHealth = 100.0f;
    [HideInInspector] public float currentHealth;
    public float maximumArmour = 100.0f;
    [HideInInspector] public float currentArmour;
    public float maximumShield = 100.0f;
    [HideInInspector] public float currentShield;
    public float maximumBoost = 100.0f;
    [HideInInspector] public float currentBoost;

    [Space(5)]
    [Header("Driving Stats")]
    public float maximumSpeed;
    public float maximumReverseSpeed;
    public float boostRate;
    public float turnRate;
    public float accelerationRate;
    public float deccelerationRate;
    [HideInInspector] public float currentSpeed;


    [Space(5)]
    [Header("Stat Cooldowns")]
    public float primaryWeaponCooldown;
    public float secondaryWeaponCooldwon;
    public float shieldCooldown;

    private void Start()
    {
        //weaponManager = GetComponent<WeaponManager>();
        //damageReceiver = GetComponent<DamageReceiver>();

        Mathf.Clamp(currentSpeed, maximumReverseSpeed, maximumSpeed);

        Mathf.Clamp(currentBoost, 0, maximumBoost);

        currentHealth = maximumHealth;
        currentShield = maximumShield;
        currentBoost = maximumBoost;
        currentArmour = maximumArmour;
    }
}
