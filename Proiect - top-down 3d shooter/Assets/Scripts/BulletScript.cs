using UnityEngine;


public class BulletScript : MonoBehaviour
{
    [SerializeField] float projectileSpeed;      // bullet speed

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
        // When the bullet hits the wall/the ground/enemies, it will disappear
        if (collision.gameObject.CompareTag("Bounds") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy1") || collision.gameObject.CompareTag("Enemy2"))
        {
            gameObject.SetActive(false);
        }
    }
}