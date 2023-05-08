using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    [SerializeField] [Range(0f, 15f)] float movementSpeed = 1f;               //player's movement speed

    [SerializeField] [Range(1f, 10f)] float jumpForce = 1f;                   //player's jump force

    static bool isGrounded;                 //is player on the ground or not
    Rigidbody rb;                           //player rigidbody

    [SerializeField] LayerMask groundLayer;                  //layer to be detected as ground

    [SerializeField] [Range(0f, 0.5f)] float groundDistance = 0.01f;          //distance from player body to ground

    float noOfBulletsInRoundPistol;                 //number of bullets in the pistol's round
    float noOfBulletsPistol;                        //number of bullets left besides the ones in the pistol
    int maxNoOfBulletsInRoundPistol;                //the maximum number of bullets that a pistol round can have

    [SerializeField] GameObject pistol;                         //player's pistol

    float noOfBulletsInRoundRifle;               //number of bullets in the rifle's round
    float noOfBulletsRifle;                      //number of bullets left besides the ones in the rifle
    int maxNoOfBulletsInRoundRifle;              //the maximum number of bullets that a rifle round can have

    [SerializeField] GameObject rifle;                         //player's rifle

    public static float noOfBulletsInRoundActive;       //number of bullets in the gun's round
    float noOfBulletsActive;                            //number of bullets left besides the ones in the gun
    int maxNoOfBulletsInRoundActive;                    //the maximum number of bullets that a round can have

    GameObject activeGun;                        //player's active gun

    string activeGunString;

    [SerializeField] Animator animator;                 //player's animator

    int reloadTime;                             //how much it takes(in ms) to reload the gun

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public TextMeshProUGUI ammoText;

    public static bool isAlive = true;

    bool isGodModeActive = false;
    float noOfBulletsRifleGodMode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Set the bullet number bounds
        noOfBulletsPistol = Mathf.Infinity;
        noOfBulletsInRoundPistol = 15;
        maxNoOfBulletsInRoundPistol = 15;

        noOfBulletsRifle = 90;
        noOfBulletsInRoundRifle = 30;
        maxNoOfBulletsInRoundRifle = 30;

        noOfBulletsActive = Mathf.Infinity;
        noOfBulletsInRoundActive = 15;
        maxNoOfBulletsInRoundActive = 15;

        noOfBulletsRifleGodMode = Mathf.Infinity;

        activeGun = pistol;

        activeGunString = "pistol";

        reloadTime = 850;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        ammoText.text = noOfBulletsInRoundPistol.ToString() + "/" + "\u221E";
    }

    void Update()
    {
        HandleReset();

        //Ground Check
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
        
        HandleJump();
        HandleShootInput();
        HandleReloadInput();

        switchWeapon();

        activateGodMode();

        if(activeGunString == "pistol")
        {
            ammoText.text = noOfBulletsInRoundActive.ToString() + "/" + "\u221E";
            noOfBulletsInRoundPistol = noOfBulletsInRoundActive;
        }
        else
        {
            if (isGodModeActive)
            {
                ammoText.text = noOfBulletsInRoundActive.ToString() + "/" + "\u221E";
            }
            else
            {
                ammoText.text = noOfBulletsInRoundActive.ToString() + "/" + noOfBulletsRifle.ToString();
                noOfBulletsInRoundRifle = noOfBulletsInRoundActive;
                noOfBulletsRifle = noOfBulletsActive;
            }
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            TakeDamage(20);
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

    void switchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            noOfBulletsActive = noOfBulletsPistol;
            noOfBulletsInRoundActive = noOfBulletsInRoundPistol;
            maxNoOfBulletsInRoundActive = maxNoOfBulletsInRoundPistol;

            ammoText.text = noOfBulletsInRoundActive.ToString() + "/" + "\u221E";

            activeGunString = "pistol";

            pistol.SetActive(true);
            rifle.SetActive(false);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            noOfBulletsActive = noOfBulletsRifle;
            noOfBulletsInRoundActive = noOfBulletsInRoundRifle;
            maxNoOfBulletsInRoundActive = maxNoOfBulletsInRoundRifle;

            ammoText.text = noOfBulletsInRoundActive.ToString() + "/" + noOfBulletsRifle.ToString();

            activeGunString = "rifle";

            pistol.SetActive(false);
            rifle.SetActive(true);
        }
    }

    void activateGodMode()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isGodModeActive)
        {
            isGodModeActive = true;
            noOfBulletsActive = noOfBulletsRifleGodMode;
        }
        else if (Input.GetKeyDown(KeyCode.G) && isGodModeActive)
        {
            isGodModeActive = false;

            if (activeGunString == "rifle")
            {
                noOfBulletsActive = noOfBulletsRifle;
            }
        }
    }

    void FixedUpdate()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    //Player move
    void HandleMovementInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (animator.GetBool("isReloading") == false && animator.GetBool("isShooting") == false)
        {
            rb.velocity = new Vector3(h * movementSpeed, rb.velocity.y, v * movementSpeed);
            rb.velocity.Normalize();
        }
    }

    //Player jump
    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && animator.GetBool("isShooting") == false && animator.GetBool("isReloading") == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    //Player reset
    void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.position = new Vector3(0, 1f, 0);     //resets player to the center of the map
        }
    }

    //Player shoots with gun using left click
    void HandleShootInput()
    {
        if (Input.GetMouseButton(0) && animator.GetBool("isReloading") == false && isGrounded && animator.GetBool("isRunning") == false)
        {
            
            if (activeGun.transform.position.y >= 2.36)
            { 
                GunScript.Instance.Shoot(); 
            }
        }
    }

    //Rotate player with gun barrel 
    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        int layerMask = 1 << LayerMask.NameToLayer("Ground");   //layermask for the ground

        // Apply raycast with a distance of 50, using the ground layermask
        if (Physics.Raycast(_ray, out _hit, 50f, layerMask))
        {
            //Added offset for raycast
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z - gameObject.GetComponent<CapsuleCollider>().center.y));
        }
    }

    //Player reloads the gun
    async void HandleReloadInput()
    {
        if (Input.GetKey(KeyCode.R) && noOfBulletsActive > 0 && isGrounded && animator.GetBool("isReloading") == false && animator.GetBool("isShooting") == false && isGrounded && animator.GetBool("isRunning") == false)
        {
            animator.SetBool("isReloading", true);      //now the animator knows that the player is reloading the gun
            
            await Task.Delay((int)(reloadTime * 1/Time.timeScale));     //add delay of 1.5s to reload task

            if (noOfBulletsActive >= maxNoOfBulletsInRoundActive)
            {
                noOfBulletsActive = noOfBulletsActive - maxNoOfBulletsInRoundActive + noOfBulletsInRoundActive;
                noOfBulletsInRoundActive = maxNoOfBulletsInRoundActive;
            }
            else
            {
                float max_bullets = maxNoOfBulletsInRoundActive - noOfBulletsInRoundActive;       //the maximum number of bullets that can be added in the round
                
                if (noOfBulletsActive <= max_bullets)
                {
                    //noOfBulletsInRoundPistol += noOfBulletsPistol;
                    noOfBulletsActive = 0;
                }
                else
                {
                    noOfBulletsInRoundActive = maxNoOfBulletsInRoundActive;
                    noOfBulletsActive -= max_bullets;
                }
            }

            animator.SetBool("isReloading", false);     //now the animator knows that the player is not reloading the gun
        }
    }

    //Check from other Scripts if Player is grounded
    public static bool getGrounded()
    {
        return isGrounded;
    }

    void OnCollisionEnter(Collision collision)
    {
        //daca se atinge playerul de inamic/glontul inamicului ia damage
        if (collision.gameObject.tag == "Enemy1" && !isGodModeActive)
        {
            TakeDamage(30);
        } 
        else if (collision.gameObject.tag == "EnemyBullet" && !isGodModeActive)
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void addBullets(int num)
    {
        noOfBulletsRifle += num;

	    if (activeGunString == "rifle"){
		    noOfBulletsActive = noOfBulletsRifle;
	    }
    }

    public void addHealth(int num)
    {
        currentHealth = Mathf.Min(100, currentHealth + num);
    }
}