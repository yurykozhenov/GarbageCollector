using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public int maxHealth;
    public Image healthBar;

    int currentHealth;
    GameController gameController;
    
    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;
        healthBar.fillAmount = (float) currentHealth / maxHealth;
        
        if (currentHealth <= 0)
        {
            gameController.GameOver();
        }
    }
    
    void Start()
    {
        currentHealth = maxHealth;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
}
