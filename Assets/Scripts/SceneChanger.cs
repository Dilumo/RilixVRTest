using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("Nome da Cena para Troca")]
    [SerializeField] private string sceneName; // Nome da cena que será carregada


    public void ChangeScene()
    {
        // Carrega a cena especificada
        SceneManager.LoadScene(sceneName);
    }
}
