using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;
    private Vector3 startPosition;
    private bool movingRight = true;
    private Vector3 initialScale;

    public Animator anim;

    public void Die()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) return;
        anim.SetTrigger("Die");
        Destroy(gameObject, 0.5f);
    }

    public class Enemy : MonoBehaviour
    {
        public EnemyData data;   // ← aqui você “liga” o ScriptableObject

        int currentHealth;

        void Start()
        {
            // currentHealth = data.maxHealth;   // COMENTADO
            currentHealth = 3;                  // teste fixo
        }

        public void TakeDamage(int amount)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
                Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                // exemplo usando o dano vindo do ScriptableObject
                // var player = collision.collider.GetComponent<PlayerLogic>();
                // if (player != null) player.TakeDamage(data.contactDamage);
            }
        }
    }

    void Update()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);

        // Vira o monstro para o lado certo
        if (movingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
        }

        if (movingRight && transform.position.x >= startPosition.x + distance)
        {
            movingRight = false;
        }
        else if (!movingRight && transform.position.x <= startPosition.x - distance)
        {
            movingRight = true;
        }
    }
}