using UnityEngine;


public class GunScript : MonoBehaviour
{
    [SerializeField] Transform firingPoint;              //bullet firing point

    [SerializeField] GameObject projectilePrefab;        //bullet prefab

    [SerializeField] [Range(1f, 15f)] float firingSpeed;

    public static GunScript Instance;       //GunScript object instance
    float lastTimeShot = 0;                 //last time the player shot with the gun
    GameObject _parent;                     //gameobject where all the generated bullets will be placed as childs

    void Awake()
    {
        Instance = GetComponent<GunScript>();
    }

    void Start()
    {
        _parent = GameObject.Find("Bullets");
    }

    public void Shoot()
    {
        //Minimum time between two shots will be 1/firingSpeed seconds
        if (lastTimeShot + 1 / firingSpeed <= Time.time && PlayerScript.noOfBulletsInRoundActive > 0)
        {
            PlayerScript.noOfBulletsInRoundActive -= 1;

            lastTimeShot = Time.time;

            //A new bullet will be generated and fired
            GameObject bullet = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            bullet.tag = "PlayerBullet";
            bullet.transform.parent = _parent.transform;
        }
    }
}