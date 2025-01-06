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

    private bool shouldRotate = false;

    void Update()
    {
        if (shouldRotate && headTransform != null && playerTransform != null)
        {
            // Calcula a direção para o jogador
            Vector3 directionToPlayer = playerTransform.position - headTransform.position;
            directionToPlayer.Normalize();

            // Obtém a rotação desejada
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
            Vector3 targetEulerAngles = targetRotation.eulerAngles;

            //Limita a rotação no eixo Y dentro do máximo permitido
            targetEulerAngles.y = Mathf.Clamp(targetEulerAngles.y, -maxYRotationAngle, maxYRotationAngle);

            // Mantém a rotação atual nos eixos X e Z
            targetEulerAngles.x = headTransform.rotation.eulerAngles.x;
            targetEulerAngles.z = headTransform.rotation.eulerAngles.z;

            // Aplica a rotação suavemente usando Slerp
            headTransform.rotation = Quaternion.Slerp(
                headTransform.rotation,
                Quaternion.Euler(targetEulerAngles),
                Time.deltaTime * rotationSpeed
            );
        }
    }

    // Método para ativar a rotação via Trigger
    public void ActivateRotation()
    {
        shouldRotate = true;
    }

    // Método para desativar a rotação
    public void DeactivateRotation()
    {
        shouldRotate = false;
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            // Define a posição e rotação da cabeça na animação
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(playerTransform.position);
        }
    }
}
