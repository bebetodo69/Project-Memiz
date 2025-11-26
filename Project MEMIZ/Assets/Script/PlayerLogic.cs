using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int totaljump;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform look;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float cameraSpeed;
    private int jumpLes;
    private bool canjump;
    private bool isGroundCheck;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float inputDirection;
    private bool isDirectionRight = true;
    private Rigidbody2D rb2d;

    // Nossa variável de vida para exemplo
    public int playerHealth = 3;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpLes = totaljump;
    }

    void Update()
    {
        GetInputMove();
        DirectionCheck();
        Canjump();
        MoveAnim();
        jumpAnim();
    }

    private void FixedUpdate()
    {
        MoveLogic();
        CheckArea();
        CameraMove();
    }

    void CameraMove()
    {
        cameraTarget.position = Vector3.MoveTowards(cameraTarget.position, look.position, cameraSpeed);
    }
    void Canjump()
    {
        if (isGroundCheck && rb2d.linearVelocity.y <= 0)
        {
            jumpLes = totaljump;
        }
        canjump = jumpLes > 0; // Simplificado
    }

    void CheckArea()
    {
        isGroundCheck = Physics2D.OverlapCircle(groundCheck.position, groundDist, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundDist);
    }

    void DirectionCheck()
    {
        if (isDirectionRight && inputDirection < 0)
        {
            Flip();
        }
        else if (!isDirectionRight && inputDirection > 0)
        {
            Flip();
        }
    }
    
    void GetInputMove()
    {
        inputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
    }

    void MoveLogic()
    {
        rb2d.linearVelocity = new Vector2(inputDirection * moveSpeed, rb2d.linearVelocity.y);
    }

    void MoveAnim()
    {
        anim.SetFloat("HorizontalAnim", Mathf.Abs(rb2d.linearVelocity.x));
    }

    void jump()
    {
        if (canjump)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            jumpLes--;
        }
    }

    void jumpAnim()
    {
        anim.SetFloat("VerticalAnim", rb2d.linearVelocity.y);
        anim.SetBool("groundCheck", isGroundCheck);
    }

    void Flip()
    {
        isDirectionRight = !isDirectionRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    // --- DETECÇÃO DE DANO DO MONSTRO ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Monstros usam tag "Enemy" no corpo
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            // Opcional: Só toma dano se o contato for lateral/baixo (não a cabeça)
            Vector2 contactPoint = collision.contacts[0].point;
            if (contactPoint.y < transform.position.y)
            {
                TakeDamage();
            }
        }
    }

    void TakeDamage()
    {
        playerHealth--;
        Debug.Log("Player tomou dano! Vida restante: " + playerHealth);
        anim.SetTrigger("Damage"); // Animação de dano, se configurado
        if (playerHealth <= 0)
        {
            Debug.Log("Player morreu!");
            // Destroy(gameObject); // ou lógica de morte/fim de jogo
        }
    }
}
