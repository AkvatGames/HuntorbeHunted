using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int enemiesRemaining = 0;
    public int livesRemaining = 0;

    public GameObject playerObject;
    float playerHealth;
    float playerArmour;
    float playerShield;
    float playerBoost;

    public TMP_Text enemiesRemainingText;
    public TMP_Text livesRemainingText;
    public TMP_Text healthRemainingText;
    public TMP_Text armourRemainingText;
    public TMP_Text shieldRemainingText;
    public TMP_Text boostRemainingText;



    //Bool Tool
    public bool isInMenu;
    public bool isInGame;

    //Level Complete Bools
    public bool level01Complete;
    public bool level02Complete;
    public bool level03Complete;
    public bool level04Complete;
    public bool level05Complete;
    public bool level06Complete;
    public bool level07Complete;
    public bool level08Complete;

    //Upgrade Bools
    public bool upgrage01;
    public bool upgrage02;
    public bool upgrage03;
    public bool upgrage04;
    public bool upgrage05;
    public bool upgrage06;
    public bool upgrage07;
    public bool upgrage08;
    public bool upgrage09;
    public bool upgrage10;
    public bool upgrage11;
    public bool upgrage12;
    public bool upgrage13;
    public bool upgrage14;
    public bool upgrage15;

    // Update is called once per frame
    void Update()
    {
        PrintEnemies();
        PrintLives();
        PrintHealth();
        PrintArmour();
        PrintShield();
        PrintBoost();

    }

    void GetHealth()
    {
        playerHealth = playerObject.GetComponent<Stats>().currentHealth;
    }

    public void PrintHealth()
    {
        GetHealth();
        healthRemainingText.text = ("Health: " + playerHealth);

    }

    void GetBoost()
    {
        playerBoost = playerObject.GetComponent<Stats>().currentBoost;
    }

    public void PrintBoost()
    {
        GetBoost();
        boostRemainingText.text = ("Boost: " + playerBoost);
    }

    void GetArmour()
    {
        playerArmour = playerObject.GetComponent<Stats>().currentArmour;
    }

    public void PrintArmour()
    {
        GetArmour();
        armourRemainingText.text = ("Armour: " + playerArmour);
    }

    void GetShield()
    {
        playerShield = playerObject.GetComponent<Stats>().currentShield;
    }

    public void PrintShield()
    {
        GetShield();
        shieldRemainingText.text = ("Shield: " + playerShield);
    }

    void GetLives()
    {
        //Setup here at a later date...
    }

    public void PrintLives()
    {
        livesRemainingText.text = ("Lives: " + livesRemaining);
    }

    void GetEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies == null)
        {
            enemiesRemaining = 0;
        }
        else
        {
            enemiesRemaining = enemies.Length;
        }
    }

    public void PrintEnemies()
    {
        GetEnemies();
        enemiesRemainingText.text = ("Enemies: " + enemiesRemaining);
    }
}
