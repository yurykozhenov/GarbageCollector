using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public GameObject destroyEffect;
    
    void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            Destroy(gameObject);
            return;
        }

        // TODO: Check by tag or layer
        if (other.GetComponent<DestroyedGarbage>()) return;
        
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
