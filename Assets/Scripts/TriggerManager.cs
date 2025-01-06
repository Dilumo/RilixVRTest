using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    // Singleton para facilitar acesso (opcional)
    public static TriggerManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Método chamado pelos triggers no spline
    public void TriggerEvent(TriggerableObject targetObject)
    {
        if (targetObject != null)
        {
            targetObject.Activate();
        }
        else
        {
            Debug.LogWarning($"Objeto {targetObject.gameObject.name} não tem o script TriggerableObject.");
        }
    }
}
