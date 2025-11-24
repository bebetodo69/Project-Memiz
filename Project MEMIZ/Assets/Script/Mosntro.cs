using UnityEngine;

public class MonsterWalk : MonoBehaviour
{
    public float speed = 2f; 
    public float distance = 5f; 
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 direction = movingRight ? Vector3.right : Vector3.left;
        transform.Translate(direction * speed * Time.deltaTime);
        
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