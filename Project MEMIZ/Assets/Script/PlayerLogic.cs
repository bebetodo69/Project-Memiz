using System;
using System.Reflection.Emit;
using UnityEngine;
using Unity.Collections;

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
    
    public void SaveAtCurrentPosition()
    {
        SaveData data = new SaveData();
        data.playerX = transform.position.x;
        data.playerY = transform.position.y;
        data.playerZ = transform.position.z;
        data.playerHealth = playerHealth; // se quiser salvar vida

        JsonSaveSystem.SaveGame(data);
    }
    
    public GameOverManager gameOverManager;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float inputDirection;
    private bool isDirectionRight = true;
    private Rigidbody2D rb2d;

    // VIDA DO PLAYER
    public int playerHealth = 3;
    public int maxHealth = 3;
    public HeartSysten heartSysten;

    // SISTEMA DE CORAÇÕES NA UI
    public HeartSysten heartSystem;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpLes = totaljump;

        // CARREGAR SAVE
        if (JsonSaveSystem.HasSave())
        {
            SaveData data = JsonSaveSystem.LoadGame();
            if (data != null)
            {
                transform.position = new Vector3(data.playerX, data.playerY, data.playerZ);
                playerHealth = data.playerHealth;
            }
        }

        if (heartSystem != null)
        {
            heartSystem.vidaMaxima = maxHealth;
            heartSystem.vida = playerHealth;
            heartSystem.AtualizarCoroes();
        }
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

        canjump = jumpLes > 0;
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
        anim.SetFloat("HorizontalAnim", rb2d.linearVelocity.x);
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

    // --------------------------
    // DANO QUANDO ENCOSTA NO MONSTRO
    // --------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // corpo do monstro com Tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        playerHealth--;
        if (playerHealth < 0) playerHealth = 0;

        Debug.Log("Player tomou dano! Vida restante: " + playerHealth);

        if (heartSystem != null)
        {
            heartSystem.vida = playerHealth;
            heartSystem.AtualizarCoroes();
        }

        if (playerHealth <= 0)
        {
            Debug.Log("Player morreu!");

            if (gameOverManager != null)
            {
                gameOverManager.ShowGameOver();
            }
        }
    }

}
