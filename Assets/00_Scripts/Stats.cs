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
    public float maximumStamina = 100.0f;
    [HideInInspector] public float currentStamina;

    [Space(5)]
    [Header("Movement Stats")]
    public float runSpeed;
    public float walkSpeed;
    [HideInInspector] public float currentSpeed;

    [Space(5)]
    [Header("Movement Bools")]
    public bool canMove;
    public bool canRun;
    public bool canJump;

    [Space(5)]
    [Header("Stat Cooldowns")]
    public float primaryWeaponCooldown;
    public float secondaryWeaponCooldwon;
    public float shieldCooldown;
}
