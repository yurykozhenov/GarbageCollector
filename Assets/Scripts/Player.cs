using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public int maxHealth;
    public float rotationOffset;
    public GameObject projectile;
    public GameObject muzzleFlash;
    public Transform shotPoint;
    public Transform muzzleFlashPoint;
    public Image healthBar;
    
    Rigidbody2D rb; 
    Animator animator;
    AudioSource audioSource;
    float timeToNextShot;
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
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        currentHealth = maxHealth;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + rotationOffset);

        if (timeToNextShot <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            timeToNextShot -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
           
        var movement = new Vector2(horizontalMovement, verticalMovement).normalized * speed;
        
        animator.SetFloat("Velocity", movement.magnitude);
        
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        Instantiate(muzzleFlash, muzzleFlashPoint.position, transform.rotation);
        Instantiate(projectile, shotPoint.position, transform.rotation);
        // TODO: Add weapon variations
//        Instantiate(projectile, shotPoint.position, transform.rotation * Quaternion.Euler(0, 0, 15));
//        Instantiate(projectile, shotPoint.position, transform.rotation * Quaternion.Euler(0, 0, -15));

        timeToNextShot = fireRate;
        animator.SetTrigger("Shoot");
        audioSource.Play();
    }
}
