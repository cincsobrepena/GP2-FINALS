using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContoller : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;
    public int Health = 0;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            Vector3 direction = player.position - transform.position;

            direction.Normalize();

            var rotation = Quaternion.LookRotation(direction);
            Vector3 aimDirection = new Vector3(player.rotation.x, 0f, player.rotation.y);

            if (aimDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(rotation, player.rotation, 0.15f);
            }

            transform.position += direction * moveSpeed * Time.deltaTime;

            float moveMagnitude = transform.position.magnitude;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if(Health != 0)
            {
                Health-=1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
