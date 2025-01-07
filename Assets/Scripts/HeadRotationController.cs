using System.Collections;
using UnityEngine;

public class HeadRotationController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform headTransform; // Transform da cabeça do personagem
    [SerializeField] private Transform playerTransform; // Transform do jogador que a cabeça irá seguir
    [SerializeField] private Animator animator; // Referência ao Animator

    [Header("Configurações de Rotação")]
    [SerializeField] private float rotationSpeed = 5f; // Velocidade de rotação
    [SerializeField] private float maxYRotationAngle = 90f; // Ângulo máximo de rotação em graus no eixo Y
    [SerializeField] private float rotationThreshold = 0.1f; // Tolerância para evitar atualizações desnecessárias

    private bool isRotating = false;
    private Quaternion targetRotation;

    void Start()
    {
        // Define a rotação inicial como a atual
        targetRotation = headTransform.rotation;
    }

    public void ActivateRotation()
    {
        if (!isRotating)
        {
            isRotating = true;
            StartCoroutine(RotateTowardsPlayer());
        }
    }

    public void DeactivateRotation()
    {
        isRotating = false;
        StopCoroutine(RotateTowardsPlayer());
    }

    private IEnumerator RotateTowardsPlayer()
    {
        while (isRotating && headTransform != null && playerTransform != null)
        {
            // Calcula a direção para o jogador
            Vector3 directionToPlayer = playerTransform.position - headTransform.position;
            directionToPlayer.Normalize();

            // Obtém a rotação desejada
            Quaternion calculatedRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            Vector3 targetEulerAngles = calculatedRotation.eulerAngles;

            // Limita a rotação no eixo Y dentro do máximo permitido
            targetEulerAngles.y = Mathf.Clamp(targetEulerAngles.y, -maxYRotationAngle, maxYRotationAngle);

            // Mantém a rotação atual nos eixos X e Z
            targetEulerAngles.x = headTransform.rotation.eulerAngles.x;
            targetEulerAngles.z = headTransform.rotation.eulerAngles.z;

            // Calcula a nova rotação
            targetRotation = Quaternion.Euler(targetEulerAngles);

            // Apenas aplique a rotação se necessário
            if (Quaternion.Angle(headTransform.rotation, targetRotation) > rotationThreshold)
            {
                headTransform.rotation = Quaternion.Slerp(
                    headTransform.rotation,
                    targetRotation,
                    Time.deltaTime * rotationSpeed
                );
            }

            yield return null;
        }
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator && isRotating)
        {
            // Define a posição e rotação da cabeça na animação
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(playerTransform.position);
        }
    }
}
