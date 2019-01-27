using UnityEngine;

public class Garbage : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject destroyedGarbage;

    AudioSource audioSource;
    Vector3 rotationDirection;
    Transform homeTransform;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();        
        
        homeTransform = GameObject.FindWithTag("Home").GetComponent<Transform>();
        rotationDirection = new Vector3(0f, 0f, Random.Range(-2.0f, 2.0f) * 3);
    }

    void Update()
    {
        transform.Rotate(rotationDirection);
        
        // TODO: Set range in Unity as two variables
        transform.position = Vector2.MoveTowards(
            transform.position,
            homeTransform.position,
            Random.Range(speed - 2, speed + 2) * Time.deltaTime
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Check by tag or layer
        if (other.GetComponent<DestroyedGarbage>()) return;

        // TODO: Should create prefab for this
        var newGameObject = new GameObject();
        newGameObject.AddComponent<AudioSource>();
        newGameObject.AddComponent<DestroyByTime>();
        newGameObject.GetComponent<AudioSource>().clip = audioSource.clip;
        newGameObject.GetComponent<DestroyByTime>().time = 1f;
        Instantiate(newGameObject);
        
        Destroy(gameObject);

        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            return;
        }

        if (other.CompareTag("Home"))
        {
            other.GetComponent<Home>().TakeDamage(damage);
            return;
        }
        
        Instantiate(destroyedGarbage, transform.position, Quaternion.identity);
    }
}
