using UnityEngine;

public class DestroyedGarbage : MonoBehaviour
{
    public int score;
    
    GameController gameController;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        gameController.AddScore(score);
        Destroy(gameObject);
    }
}
