using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : MonoBehaviour
{
    [Header("Configurações de Som")]
    public AudioClip soundClip;
    public float soundDelay = 0f; // Atraso antes do som
    [Range(0f, 1f)] public float volume = 1f; // Volume do som (0.0 a 1.0)

    [Header("Configurações de Animação")]
    public Animator animator;
    public string animationState; // Nome do trigger no Animator
    public float animationDelay = 0f;
    public string initialAnimationState; // Nome do estado inicial do Animator

    [Header("Outras Ações Personalizadas")]
    public List<TriggerAction> actions;

    private bool isReturningToInitialState = false;

    private void Start()
    {
        // Armazena o estado inicial se não foi configurado manualmente
        if (animator != null && string.IsNullOrEmpty(initialAnimationState))
        {
            var currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            initialAnimationState = currentAnimatorStateInfo.IsName("")
                ? "idle"
                : currentAnimatorStateInfo.shortNameHash.ToString();
        }
    }

    // Método chamado pelo trigger
    public void Activate()
    {
        // Tocar som
        if (soundClip != null)
        {
            StartCoroutine(PlaySound());
        }

        // Disparar animação
        if (animator != null && !string.IsNullOrEmpty(animationState))
        {
            StartCoroutine(PlayAnimation());
        }

        // Executar ações personalizadas
        foreach (var action in actions)
        {
            StartCoroutine(ExecuteAction(action));
        }
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(soundDelay);

        // Adiciona ou usa um AudioSource existente
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = soundClip;
        audioSource.spatialBlend = 1.0f; // Som 3D
        audioSource.volume = volume; // Aplica o volume especificado
        audioSource.Play();
    }

    private IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(animationDelay);
        animator.Play(animationState);

        // Aguarda o tempo da animação antes de retornar ao estado inicial
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;

        yield return new WaitForSeconds(animationDuration);

        // Volta ao estado inicial, se necessário
        if (!string.IsNullOrEmpty(initialAnimationState) && !isReturningToInitialState)
        {
            animator.Play(initialAnimationState);
        }
    }

    private IEnumerator ExecuteAction(TriggerAction action)
    {
        yield return new WaitForSeconds(action.delay);
        action.Execute(gameObject);
    }
}

[System.Serializable]
public class TriggerAction
{
    public string actionName;
    public float delay = 0f; // Atraso antes da execução

    public void Execute(GameObject target)
    {
        Debug.Log($"Ação '{actionName}' executada no objeto {target.name}");
        // Adicione aqui a lógica para ações específicas.
    }
}
