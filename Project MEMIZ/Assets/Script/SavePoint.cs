using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLogic player = other.GetComponent<PlayerLogic>();
            if (player != null)
            {
                player.SaveAtCurrentPosition();
                Debug.Log("Jogo salvo no SavePoint!");
            }
        }
    }
}