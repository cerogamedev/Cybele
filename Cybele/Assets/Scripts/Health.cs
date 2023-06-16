using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Image healthBar;
    float health, maxHealth=100;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarFiller();
        if (health>=maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
            SceneManager.LoadScene("SampleScene");
    }
    void HealthBarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void Damage(float DamagePoints)
    {
        if (health>0)
        {
            health -= DamagePoints;
        }
    }
    public void HealthPlayer(float HealthPoints)
    {
        if (health<maxHealth)
        {
            health += HealthPoints;
        }
    }
}
