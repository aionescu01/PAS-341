using Pathfinding;
using UnityEngine;


public class Enemy1Script : MonoBehaviour
{
    Transform target;                               // target object 

    [SerializeField] float movementSpeed = 2f;      // enemy movement speed

    static float distanceToPlayer=10.0f;            // distance between enemy and target

    public int maxHealth = 100;
    public int currentHealth;

    HealthBar healthBar;
    private ScoreManager playerScoreManager;
    Canvas playerCanvas;

    private bool pointAdded = false;
    private AIDestinationSetter _destinationSetter;


    void Start()
    {
        _destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        _destinationSetter.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerCanvas = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Canvas>();
        healthBar = transform.Find("Canvas").Find("Health bar").gameObject.GetComponent<HealthBar>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        playerScoreManager = playerCanvas.GetComponent<ScoreManager>();
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);

        //MoveEnemy();
        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        // if the enemy has dies, we stop its movement(so that it won't follow the player after death)
        if (currentHealth <= 0)
        {
            movementSpeed = 0.0f;
            if (pointAdded == false)
            {
                playerScoreManager?.AddPoint();
                pointAdded = true;
                Enemy1Spawner.noOfEnemiesAlive--;
            }
        }
    }

    void MoveEnemy()
    {
        RotateEnemy();
        transform.position += transform.forward * movementSpeed * Time.deltaTime;       // chasing the target
    }
    
    void RotateEnemy()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));         // rotate enemy to face the target
    }

    public static bool isClose()
    {
        if (distanceToPlayer < 5.0f)
            return true;
        else
            return false;
    }

    public bool isDead() 
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            TakeDamage(40);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}