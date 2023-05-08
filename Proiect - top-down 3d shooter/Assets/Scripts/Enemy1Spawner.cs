using System.Collections;
using UnityEngine;
using Pathfinding;

public class Enemy1Spawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy1Prefab;

    Transform player;

    public static Enemy1Spawner Instance;

    public static int noOfEnemiesAlive = 0;

    GameObject _parent;

    //private AIDestinationSetter _destinationSetter;

    void Awake()
    {
        Instance = GetComponent<Enemy1Spawner>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    void Start()
    {
        
        _parent = GameObject.Find("Enemy1");
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (noOfEnemiesAlive < 20)
        {
            float ranX = Random.Range(-25, 25);
            float ranZ = Random.Range(-25, 25);

            if (ranX > -15 && ranX < 0)
            {
                ranX = -15;
            }
            else if (ranX < 15 && ranX >= 0)
            {
                ranX = -15;
            }

            if (ranZ > -15 && ranZ < 0)
            {
                ranZ = -15;
            }
            else if (ranZ < 15 && ranZ >= 0)
            {
                ranZ = -15;
            }

            Vector3 spawnPosition = new Vector3(player.position.x + ranX, 0.5f, player.position.x + ranZ);

            GameObject enemy = Instantiate(Enemy1Prefab, spawnPosition, Quaternion.identity);

            //enemy._destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
            //enemy._destinationSetter.target = player.transform;

            enemy.transform.Find("Canvas").Find("Health bar").gameObject.GetComponent<HealthBar>().fill.fillAmount = 1;

            enemy.tag = "Enemy1";
            enemy.transform.parent = _parent.transform;

            noOfEnemiesAlive++;
        }

        yield return new WaitForSeconds(7);

        if (PlayerScript.isAlive)
        {
            StartCoroutine(Spawn());
        }
    }
}