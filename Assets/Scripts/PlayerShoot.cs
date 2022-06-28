using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletTime = 10f;
    [SerializeField] Vector2 unflippedFiringPosition = new Vector2(0, 0);
    [SerializeField] Vector2 flippedFiringPosition = new Vector2(0, 0);
    [SerializeField] float fireRate = 1f;
    float timeSinceLastShot = 0f;
    SpriteRenderer sprite;
    Camera mainCamera;
    [SerializeField] AudioSource fireballAudioSource;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timeSinceLastShot > fireRate)
            {
                FireBullet();
                timeSinceLastShot = 0f;
            }
        }
    }

    void FireBullet()
    {
        fireballAudioSource.Play();
        GameObject bullet = Instantiate(bulletPrefab, transform);
        if (sprite.flipX)
        {
            bullet.transform.localPosition = flippedFiringPosition;
        }
        else
        {
            bullet.transform.localPosition = unflippedFiringPosition;
        }
        bullet.transform.parent = null;

        Vector2 dir = bullet.transform.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dir.Normalize();
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-dir.x * bulletSpeed, -dir.y * bulletSpeed));

        Destroy(bullet, 10f);
    }

    public void IncreaseFireRate(float amount)
    {
        fireRate *= amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(unflippedFiringPosition.x, unflippedFiringPosition.y, -1f), .1f);
        Gizmos.DrawSphere(new Vector3(flippedFiringPosition.x, flippedFiringPosition.y, -1f), .1f);
    }
}