using UnityEngine;


public class Heart_up : MonoBehaviour
{   
    public GameObject pickupEffect;
    private PlayerScript playerscript;
    private ScoreManager scoreManager;
    public GameObject player;
    private Canvas canvas;
    public MeshRenderer rnd;

    private bool hasRespawned = false;
    private int oldScore = 0;
    public float duration = 1;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Score").GetComponent<Canvas>();
        scoreManager = canvas.GetComponent<ScoreManager>();
    }

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
           Pickup();
        } 
        else if (Other.CompareTag("Ground"))
        {
            Transform playerPos = player.GetComponent<Transform>();

            //random X axis position
            float offset_x = Random.Range(-20f, 20f);

            if (offset_x > -3 && offset_x < 0)
            {
                offset_x = -3;
            }
            else if (offset_x < 3 && offset_x >= 0)
            {
                offset_x = 3;
            }

            float PosX = playerPos.position.x + offset_x;

            if (PosX > 0)
            {
                PosX = Mathf.Min(PosX, 370f);
            }
            else if (PosX < 0)
            {
                PosX = Mathf.Max(PosX, -370f);
            }

            //random Z axis pozition
            float offset_z = UnityEngine.Random.Range(-20f, 20f);

            if (offset_z > -3 && offset_z < 0)
            {
                offset_z = -3;
            }
            else if (offset_z < 3 && offset_z >= 0)
            {
                offset_z = 3;
            }

            float PosZ = playerPos.position.z + offset_z;

            if (PosZ > 0)
            {
                PosZ = Mathf.Min(PosZ, 370f);
            }
            else if (PosZ < 0)
            {
                PosZ = Mathf.Max(PosZ, -370f);
            }

            Vector3 newPos = new Vector3(PosX, 1, PosZ);

            transform.position = newPos;
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        
        playerscript = player.GetComponent<PlayerScript>();
        playerscript.currentHealth = 100;

        rnd.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        hasRespawned = false;
    }

    private void Update()
    {
        if (scoreManager.score % 25 == 0 && scoreManager.score > 0 && hasRespawned == false && scoreManager.score!=oldScore)
        {
            hasRespawned = true;

            //random power-up position
            Transform playerPos = player.GetComponent<Transform>();

            //random X axis position
            float offset_x = Random.Range(-20f, 20f);

            if (offset_x > -3 && offset_x < 0)
            {
                offset_x = -3;
            }
            else if (offset_x < 3 && offset_x >= 0)
            {
                offset_x = 3;
            }

            float PosX = playerPos.position.x + offset_x;

            if (PosX > 0)
            {
                PosX = Mathf.Min(PosX, 370f);
            }
            else if (PosX < 0)
            {
                PosX = Mathf.Max(PosX, -370f);
            }

            //random Z axis pozition
            float offset_z = UnityEngine.Random.Range(-20f, 20f);

            if (offset_z > -3 && offset_z < 0)
            {
                offset_z = -3;
            }
            else if (offset_z < 3 && offset_z >= 0)
            {
                offset_z = 3;
            }

            float PosZ = playerPos.position.z + offset_z;

            if (PosZ > 0)
            {
                PosZ = Mathf.Min(PosZ, 370f);
            }
            else if (PosZ < 0)
            {
                PosZ = Mathf.Max(PosZ, -370f);
            }

            Vector3 newPos = new Vector3(PosX, 1, PosZ);    //the new position for the power-up

            transform.position = newPos;

            rnd.enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            oldScore = scoreManager.score;
        }
        else if (scoreManager.score == 0)
        {
            rnd.enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}