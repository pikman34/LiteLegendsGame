using UnityEngine;


public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackDamage = 10;

    public int level = 1;
    public int experience;
    public int xpToNextLevel = 50;

    public UIManager ui;

    void Start()
    {
        currentHealth = maxHealth;
        ui.UpdateHealthBar(currentHealth, maxHealth);
        ui.UpdateXPBar(experience, xpToNextLevel);

    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        ui.ShowDamagePopup(amount);
        ui.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        ui.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void GainXP(int amount)
    {
        experience += amount;
        if (experience >= xpToNextLevel)
        {
            LevelUp();
        }
        ui.UpdateXPBar(experience, xpToNextLevel);
    }

    void LevelUp()
    {
        level++;
        experience = 0;
        xpToNextLevel += 25;
        maxHealth += 20;
        attackDamage += 5;
        currentHealth = maxHealth;

        ui.UpdateHealthBar(currentHealth, maxHealth);
    }
}
