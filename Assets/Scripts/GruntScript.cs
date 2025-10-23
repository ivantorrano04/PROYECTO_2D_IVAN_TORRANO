using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform John;
    public GameObject BulletPrefab;
    public float Speed = 2f; // Velocidad de caminar

    private int Health = 3;
    private float LastShoot;
    private Rigidbody2D rb;
    private bool isGrounded = true; // Puedes mejorar esto con un GroundCheck si lo necesitas

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (John == null) return;
        

        Vector3 direction = John.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(John.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

     void FixedUpdate()
    {

        // Mover hacia John (solo en X)
        float directionX = Mathf.Sign(John.position.x - transform.position.x);
        rb.velocity = new Vector2(directionX * Speed, rb.velocity.y);
    }


    private void Shoot()
    {
        Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0) Destroy(gameObject);
    }
}