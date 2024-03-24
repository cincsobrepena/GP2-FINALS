using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public AudioSource gunshot;
    public GameObject bulletPrefab; 
    public Transform firePoint; 

    public float bulletSpeed = 20f;
    public float shootingInterval = 1f;
    private float shootingTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        gunshot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                gunshot.Play();
                shootingTimer = 0f;
            }
        }
        
    }

    private void Shoot()
    {
        

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody bulletRigidBody = bullet.GetComponent<Rigidbody>();
        bulletRigidBody.velocity = transform.forward * bulletSpeed;
        

    }
}
