using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Dados do Inimigo (ScriptableObject)")]
    public EnemyData data;

    private float speed;
    private int currentHealth;

    [Header("Movimento de patrulha")]
    public float distance = 5f;
    private Vector3 startPosition;
    private bool movingRight = true;
    private Vector3 initialScale;

    [Header("Outros componentes")]
    public Animator anim;

    void Start()
    {
        // Pega os valores do ScriptableObject
        if (data != null)
        {
            speed = data.moveSpeed;
            currentHealth = data.maxHealth;
            // opcional: usar o nome
            gameObject.name = data.enemyName;
        }
        else
        {
            // fallback se não tiver data atribuída
            speed = 2f;
            currentHealth = 3;
        }

        startPosition = transform.position;
        initialScale = transform.localScale;
    }

    void Update()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);

        if (movingRight)
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);

        if (movingRight && transform.position.x >= startPosition.x + distance)
            movingRight = false;
        else if (!movingRight && transform.position.x <= startPosition.x - distance)
            movingRight = true;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        if (anim != null)
            anim.SetTrigger("Die");   // muda para o estado Death

        Destroy(gameObject, 1f);      // tempo >= duração da animação de Death
    }

    // Exemplo de acesso ao dano de contato:
    public int GetContactDamage()
    {
        return data != null ? data.contactDamage : 0;
    }
}
