using UnityEngine;


public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] float projectileSpeed;      //bullet speed

    void Update()
    {
        MoveProjectile();
    }

    // Bullet firing
    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // When the bullet hits the wall/the ground/player, it will disappear
        if (collision.gameObject.CompareTag("Bounds") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
