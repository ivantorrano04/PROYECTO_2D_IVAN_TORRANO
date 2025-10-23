using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform John;
    public GameObject BulletPrefab;
    public float Speed = 2f;
    public float stopDistance = 1.2f;
    public float activationDistance = 4f; // 👈 Nueva: solo activo si John está a ≤ 4m

    private int Health = 2;
    private float LastShoot;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("¡El enemigo necesita un Rigidbody2D!");
        }
    }

    void Update()
    {
        if (John == null) return;

        // 👇 NUEVO: solo actuar si está dentro de la distancia de activación
        float distanceToJohn = Vector2.Distance(transform.position, John.position);
        if (distanceToJohn > activationDistance)
        {
            return; // No hacer nada si está lejos
        }

        // Mirar hacia John
        if (John.position.x >= transform.position.x)
            transform.localScale = Vector3.one;
        else
            transform.localScale = new Vector3(-1, 1, 1);

        float distance = Mathf.Abs(John.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (John == null) return;

        // 👇 NUEVO: solo moverse si está dentro de la distancia de activación
        float distanceToJohn = Vector2.Distance(transform.position, John.position);
        if (distanceToJohn > activationDistance)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y); // Detenerse si está lejos
            return;
        }

        float distance = Mathf.Abs(John.position.x - transform.position.x);
        float directionX = Mathf.Sign(John.position.x - transform.position.x);

        if (distance > stopDistance)
        {
            rb.velocity = new Vector2(directionX * Speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
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
        if (Health <= 0)
        {
            if (John != null)
            {
                John.GetComponent<JohnMovement>().SumarPuntos(20);
            }
            Destroy(gameObject);
        }
    }
}