using System.Collections;
using UnityEngine;
using Dreamteck.Splines;

public class RollerCoasterControl : MonoBehaviour
{
    [Header("Configurações de Controle")]
    [SerializeField] private SplineFollower splineFollower; // Componente Spline Follower
    [SerializeField] private float acceleration = 2f; // Aceleração/desaceleração base
    [SerializeField] private float maxSpeed = 10f; // Velocidade máxima
    [SerializeField] private float minSpeed = 2f; // Velocidade mínima

    [Header("Configurações de Som")]
    [SerializeField] private AudioClip engineSoundClip; // Som do carrinho
    [SerializeField] private float soundDelay = 0f; // Atraso antes de tocar o som
    [Range(0f, 1f)][SerializeField] private float soundVolume = 1f; // Volume do som
    [SerializeField] private float minPitch = 0.8f; // Pitch mínimo
    [SerializeField] private float maxPitch = 2.0f; // Pitch máximo

    private AudioSource audioSource;

    private void Start()
    {
        // Configura o som inicial
        if (engineSoundClip != null)
        {
            StartCoroutine(SetupSound());
        }
    }

    private void Update()
    {
        // Ajusta a velocidade baseada na inclinação do spline
        Vector3 direction = splineFollower.result.forward.normalized;
        float inclination = Vector3.Dot(direction, Vector3.up);

        if (inclination > 0) // Subida
        {
            splineFollower.followSpeed = Mathf.Max(splineFollower.followSpeed - acceleration * Time.deltaTime, minSpeed);
        }
        else if (inclination < 0) // Descida
        {
            splineFollower.followSpeed = Mathf.Min(splineFollower.followSpeed + acceleration * Time.deltaTime, maxSpeed);
        }

        // Ajusta o pitch do som baseado na velocidade
        if (audioSource != null)
        {
            float normalizedSpeed = (splineFollower.followSpeed - minSpeed) / (maxSpeed - minSpeed);
            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
        }
    }

    private IEnumerator SetupSound()
    {
        yield return new WaitForSeconds(soundDelay);

        // Obtém ou cria um AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura o AudioSource
        audioSource.clip = engineSoundClip;
        audioSource.spatialBlend = 1.0f; // Som 3D
        audioSource.volume = soundVolume;
        audioSource.loop = true;
        audioSource.Play();
    }
}
