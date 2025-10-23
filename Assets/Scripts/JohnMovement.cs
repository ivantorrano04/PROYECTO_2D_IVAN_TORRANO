using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 👈 Necesario para reiniciar la escena

public class JohnMovement : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 8f;
    public GameObject BulletPrefab;
    public Transform GroundCheck;

    // 👇 Añade este umbral de caída (ajústalo según tu mapa)
    public float FallThreshold = -10f;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Reiniciar si se cae del mapa
        if (transform.position.y < FallThreshold)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return; // Evita ejecutar el resto si ya se reinició
        }

        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(GroundCheck.position, Vector2.down * 0.1f, Color.green);

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
            Jump();

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Vector3 direction = (transform.localScale.x == 1.0f) ? Vector3.right : Vector3.left;
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health--;
        if (Health <= 0)
        {
            // Opcional: también reiniciar al morir
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}