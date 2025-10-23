using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    public int maxBalas = 10;      // Balas máximas
    private int balasActuales;     // Balas actuales (empieza lleno)
    public float Speed = 5f;
    public float JumpForce = 8f;
    public GameObject BulletPrefab;
    public Transform GroundCheck;
    public float FallThreshold = -10f;

    // 👇 SOLO AÑADO ESTO para la barra de vida (NO toco nada del movimiento)
    public float vidaMaxima = 5f; // Valor editable en el Inspector
    private float vidaActual;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 5; // Tu sistema original sigue intacto
                            // Sistema de puntuación
    private int puntuacion = 0;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        vidaActual = vidaMaxima; // Inicializamos la vida compatible con la UI
        balasActuales = maxBalas;
        puntuacion = 0; // 👈 Reiniciar al empezar
    }

    void Update()
    {
        // Reiniciar si se cae del mapa
        if (transform.position.y < FallThreshold)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
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
            if (balasActuales > 0)
            {
                Shoot();
                balasActuales--;
                LastShoot = Time.time;
            }
        }
        // Recargar con la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Recargar();
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
        vidaActual = Health; // 👈 Sincronizamos con el sistema de vida de la UI

        if (Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 👇 Propiedad para que BarraVida lea la vida actual (sin romper tu lógica)
    public float VidaActual => vidaActual;
    // Propiedad para que otros scripts lean las balas
    public int BalasActuales => balasActuales;

    public void Recargar()
    {
        balasActuales = maxBalas;
    }
    // Para que otros scripts (como el enemigo) sumen puntos
    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
    }

    // Para que el contador de UI lea la puntuación
    public int ObtenerPuntuacion()
    {
        return puntuacion;
    }
    public void Curar(int cantidad)
{
    vidaActual += cantidad;
    // Asegúrate de no superar la vida máxima
    if (vidaActual > vidaMaxima)
    {
        vidaActual = vidaMaxima;
    }
    // También actualizamos Health (tu sistema original)
    Health = Mathf.CeilToInt(vidaActual);
}
}