using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public int maxHealth;
    public Image healthBar;

    int currentHealth;
    GameController gameController;

    AudioSource audioSource;
    
    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;
        healthBar.fillAmount = (float) currentHealth / maxHealth;
        
        if (currentHealth <= 0)
        {
            gameController.GameOver();
        }
        
        Camera.main.gameObject.GetComponent<Animator>().SetTrigger("Shake");
        audioSource.Play();
    }
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        currentHealth = maxHealth;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
}
