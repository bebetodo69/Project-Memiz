using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManagerPoints.Instance != null)
                GameManagerPoints.Instance.AddPoints(value);

            Destroy(gameObject);
        }
    }
}
