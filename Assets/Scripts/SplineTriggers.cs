using Dreamteck.Splines;
using UnityEngine;

public class SplineTriggers : MonoBehaviour
{
    public SplineFollower splineFollower; // Componente SplineFollower
    public float triggerRange = 0.1f; // Tolerância para ativar o trigger

    // Lista de triggers (percentuais no spline)
    public float[] triggerPoints = { 0.02489907f, 0.5f, 0.8f };
    private bool[] triggered; // Para garantir que cada ponto seja acionado apenas uma vez

    void Start()
    {
        // Inicializa o array de controle
        triggered = new bool[triggerPoints.Length];
    }

    void Update()
    {
        float currentPercent = (float)splineFollower.result.percent;

        // Verifica cada trigger
        for (int i = 0; i < triggerPoints.Length; i++)
        {
            if (!triggered[i] && Mathf.Abs(currentPercent - triggerPoints[i]) <= triggerRange)
            {
                // Ativa o evento
                TriggerEvent(i);
                triggered[i] = true; // Marca como ativado
            }
        }
    }

    void TriggerEvent(int index)
    {
        Debug.Log($"Trigger {index + 1} ativado!");

        // Aqui você pode adicionar sons, animações ou outras ações
        PlaySound(index);
        PlayAnimation(index);
    }

    void PlaySound(int index)
    {
        // Substitua por seu sistema de áudio
        Debug.LogError($"Tocando som para Trigger {index + 1}", this);
    }

    void PlayAnimation(int index)
    {
        // Substitua por animações específicas
        Debug.LogError($"Reproduzindo animação para Trigger {index + 1}", this);
    }


}
