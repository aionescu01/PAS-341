using Pathfinding;
using UnityEngine;


public class Enemy2Script : MonoBehaviour
{
    Transform target;                       // target object 

    [SerializeField] float movementSpeed = 2f;               // enemy movement speed

    static float distanceToPlayer = 10.0f;          // distance between enemy and target

    public int maxHealth = 100;
    public int currentHealth;

    HealthBar healthBar;
    private ScoreManager playerScoreManager;
    Canvas playerCanvas;

    private bool pointAdded = false;

    EnemyGunScript enemyGunScript;
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

        enemyGunScript = transform.Find("Bip001").Find("Bip001 Pelvis").Find("Bip001 Spine").Find("Bip001 R Clavicle").Find("Bip001 R UpperArm").Find("Bip001 R Forearm").Find("Bip001 R Hand").GetChild(0).gameObject.GetComponent<EnemyGunScript>();
    }

    void Update()
    {
        healthBar.SetHealth(currentHealth);

        distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer < 10.0f)
        {
            enemyGunScript.Shoot();
        }

        MoveEnemy();

        // daca inamicul a murit, ii oprim miscarea (ca sa nu mai vina spre player in timpul animatiei de moarte)
        if (currentHealth <= 0) 
        {
            movementSpeed = 0.0f;
            if (pointAdded == false)
            {
                playerScoreManager?.AddPoint();
                pointAdded = true;
                Enemy2Spawner.noOfEnemiesAlive--;
            }
        }
    }

    void MoveEnemy()
    {
        RotateEnemy();

        if (isClose() == false)
        {
            transform.position += transform.forward * movementSpeed * Time.deltaTime;       // chasing the target
        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
        }
    }
    
    void RotateEnemy()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));         // rotate enemy to face the target 
    }

    public static bool isClose()
    {
        if (distanceToPlayer < 10.0f)
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
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
