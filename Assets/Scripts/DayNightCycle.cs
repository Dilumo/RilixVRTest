using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Luz Direcional")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Gradient nightToDayGradient; // Gradiente de noite para dia
    [SerializeField] private Gradient dayToNightGradient; // Gradiente de dia para noite

    [Header("Skybox")]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    [Header("Música de Fundo")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dayMusic;
    [SerializeField] private AudioClip nightMusic;

    [Header("Ciclo de Tempo")]
    [SerializeField] private float cycleDuration = 20f; // Duração total (dia ou noite)

    private float timeCounter = 0f;
    private bool isDay = false;

    void Start()
    {
        // Configura o estado inicial
        SetCycleState(false); // Começa como noite
    }

    void Update()
    {
        // Avança o contador de tempo
        timeCounter += Time.deltaTime;

        // Normaliza o tempo (0 a 1) baseado no ciclo atual (dia ou noite)
        float normalizedTime = timeCounter / cycleDuration;

        // Seleciona o gradiente correto
        Gradient currentGradient = isDay ? dayToNightGradient : nightToDayGradient;

        // Atualiza a cor da luz de acordo com o gradiente atual
        directionalLight.color = currentGradient.Evaluate(normalizedTime);

        // Troca o Skybox e reinicia o ciclo quando o tempo terminar
        if (timeCounter >= cycleDuration)
        {
            timeCounter = 0f;
            SwitchCycle();
        }
    }

    private void SwitchCycle()
    {
        // Alterna o estado do ciclo
        isDay = !isDay;

        // Troca o Skybox
        RenderSettings.skybox = isDay ? daySkybox : nightSkybox;
        DynamicGI.UpdateEnvironment();

        // Troca a música
        audioSource.clip = isDay ? dayMusic : nightMusic;
        audioSource.Play();
    }

    private void SetCycleState(bool toDay)
    {
        isDay = toDay;
        timeCounter = 0f;

        // Configura estado inicial
        RenderSettings.skybox = toDay ? daySkybox : nightSkybox;
        directionalLight.color = (toDay ? dayToNightGradient : nightToDayGradient).Evaluate(0f);
        DynamicGI.UpdateEnvironment();

        // Configura música inicial
        audioSource.clip = toDay ? dayMusic : nightMusic;
        audioSource.Play();
    }
}
