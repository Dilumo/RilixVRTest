using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform playerTransform; // Transform do jogador que o personagem irá seguir
    [SerializeField] private Animator animator; // Referência ao Animator do personagem

    [Header("Configurações de Movimento")]
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento
    [Header("Configurações da animação")]
    [SerializeField] private string stateMove = "Walk_Forward"; // Nome do state de movimento
    [SerializeField] private string stateIdle = "idle"; // Nome do state de movimento

    private bool shouldMove = false;

    void Update()
    {
        if (shouldMove && playerTransform != null)
        {
            // Calcula a direção para o jogador
            Vector3 direction = playerTransform.position - transform.position;
            direction.Normalize();

            // Move o personagem em direção ao jogador
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Aplicar animação de andar
            if (animator != null)
            {
                animator.Play(stateMove);
            }
        }
        else if (animator != null)
        {
            // Parar animação de andar quando não estiver se movendo
            animator.Play(stateIdle);
        }
    }

    // Método para iniciar o movimento
    public void StartMovement()
    {
        shouldMove = true;
    }

    // Método para parar o movimento
    public void StopMovement()
    {
        shouldMove = false;
    }
}
