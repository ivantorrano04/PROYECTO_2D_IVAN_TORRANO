using UnityEngine;

public class GruntScript : MonoBehaviour
{
    public Transform John;
    public GameObject BulletPrefab;
    public float Speed = 2f;
    public float stopDistance = 1.2f; // 👈 Distancia mínima a la que se detiene

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

        // Mirar hacia John
        if (John.position.x >= transform.position.x)
            transform.localScale = Vector3.one; // (1,1,1)
        else
            transform.localScale = new Vector3(-1, 1, 1);

        float distance = Mathf.Abs(John.position.x - transform.position.x);

        // Disparar solo si está MUY cerca
        if (distance < 1.0f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (John == null) return;

        float distance = Mathf.Abs(John.position.x - transform.position.x);
        float directionX = Mathf.Sign(John.position.x - transform.position.x);

        // Solo moverse si está más lejos que la distancia de parada
        if (distance > stopDistance)
        {
            rb.velocity = new Vector2(directionX * Speed, rb.velocity.y);
        }
        else
        {
            // Detenerse completamente en X
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
            Destroy(gameObject);
        }
    }
}