using UnityEngine;


public class EnemyGunScript : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;        //bullet prefab

    [SerializeField] [Range(1f, 10f)] float firingSpeed;

    float lastTimeShot = 0;             //last time the player shot with the gun

    GameObject _parent;                 //gameobject where all the generated bullets will be placed as childs

    void Start()
    {
        _parent = GameObject.Find("Enemy Bullets");
    }

    public void Shoot()
    {
        // Minimum time between two shots will be 1/firingSpeed seconds
        if (lastTimeShot + 1 / firingSpeed <= Time.time)
        {
            lastTimeShot = Time.time;

            // A new bullet will be generated and fired
            GameObject bullet = Instantiate(projectilePrefab, transform.position, transform.rotation);

            bullet.tag = "EnemyBullet";
            bullet.transform.parent = _parent.transform;
        }
    }
}