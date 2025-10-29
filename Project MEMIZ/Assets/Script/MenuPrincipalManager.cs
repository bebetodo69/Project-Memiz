using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string Fase1;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    
    public void Jogar()
    {
        SceneManager.LoadScene("Fase1");
    }
    
    public void AbriOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    
    public void SairJogo()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
