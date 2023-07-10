using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [Space(5)]
    [Header("Weapons")]
    public float primaryWeaponDamage = 1.0f;
    public float secondaryWeaponDamage = 25.0f;
    public float specialWeaponDamage = 100.0f;

    [Space(5)]
    [Header("Vitality Stats")]
    public float maximumHealth = 100.0f;
    [HideInInspector] public float currentHealth;

    [Space(5)]
    [Header("Movement Stats")]
    public float runSpeed;
    public float walkSpeed;
    [HideInInspector] public float currentSpeed;


    [Space(5)]
    [Header("Stat Cooldowns")]
    public float primaryWeaponCooldown;
    public float secondaryWeaponCooldwon;
    public float shieldCooldown;
}
