using UnityEngine;

public class TruggerDmage : MonoBehaviour
{
    public HeartSysten heart;

    private void OncollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            heart.vida--;
        }
    }
}
