using UnityEngine;

public class PlayerStomp : MonoBehaviour
{
    public float bounceForce = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Head do monstro tem tag "EnemyHead"
        if (other.CompareTag("EnemyHead"))
        {
            var monster = other.GetComponentInParent<Monster>();
            if (monster != null)
            {
                monster.Die();
            }

            // Quicar o player para cima
            var rb = GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }
        }
    }
}