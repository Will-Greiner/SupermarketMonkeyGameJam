using UnityEngine;
using UnityEngine.UI;
public class Base : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthBarFill;
    public GameObject endscreen;
    public GameObject losescreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            endscreen.SetActive(true);
            losescreen.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            //game over
        }
    }

    void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercent;
    }
}
