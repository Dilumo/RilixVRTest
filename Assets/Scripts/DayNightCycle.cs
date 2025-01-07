using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Luz Direcional")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private Gradient nightToDayGradient;
    [SerializeField] private Gradient dayToNightGradient;

    [Header("Skybox")]
    [SerializeField] private Material daySkybox;
    [SerializeField] private Material nightSkybox;

    [Header("Música de Fundo")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dayMusic;
    [SerializeField] private AudioClip nightMusic;

    [Header("Ciclo de Tempo")]
    [SerializeField] private float cycleDuration = 20f;

    private float timeCounter = 0f;
    private bool isDay = false;

    void Start()
    {
        SetCycleState(false);
    }

    void Update()
    {
        timeCounter += Time.deltaTime;

        float normalizedTime = timeCounter / cycleDuration;
        Gradient currentGradient = isDay ? dayToNightGradient : nightToDayGradient;

        directionalLight.color = currentGradient.Evaluate(normalizedTime);

        if (timeCounter >= cycleDuration)
        {
            timeCounter = 0f;
            SwitchCycle();
        }
    }

    private void SwitchCycle()
    {
        isDay = !isDay;
        StartCoroutine(TransitionSkybox(isDay ? daySkybox : nightSkybox, 2f)); // Transição suave
        audioSource.clip = isDay ? dayMusic : nightMusic;
        audioSource.Play();
    }

    private void SetCycleState(bool toDay)
    {
        isDay = toDay;
        timeCounter = 0f;

        RenderSettings.skybox = toDay ? daySkybox : nightSkybox;
        directionalLight.color = (toDay ? dayToNightGradient : nightToDayGradient).Evaluate(0f);
        DynamicGI.UpdateEnvironment();

        audioSource.clip = toDay ? dayMusic : nightMusic;
        audioSource.Play();
    }

    IEnumerator TransitionSkybox(Material targetSkybox, float duration)
    {
        Material currentSkybox = RenderSettings.skybox;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float blend = timer / duration;

            RenderSettings.skybox.SetFloat("_Exposure", Mathf.Lerp(currentSkybox.GetFloat("_Exposure"), targetSkybox.GetFloat("_Exposure"), blend));
            DynamicGI.UpdateEnvironment();
            yield return null;
        }

        RenderSettings.skybox = targetSkybox;
        DynamicGI.UpdateEnvironment();
    }
}
