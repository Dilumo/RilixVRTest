using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator animator;

    [Header("Configurações de Movimento")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stopDistance = 1.5f;

    [Header("Configurações de Animação")]
    [SerializeField] private string stateMove = "Walk_Forward";
    [SerializeField] private string stateIdle = "idle";

    private bool isMoving = false;

    public void StartMovement()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveToPlayer());
        }
    }

    public void StopMovement()
    {
        isMoving = false;

        // Parar animação de movimento
        if (animator != null)
        {
            animator.Play(stateIdle);
        }

        StopAllCoroutines();
    }

    private IEnumerator MoveToPlayer()
    {
        while (isMoving && playerTransform != null)
        {
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0;

            if (direction.magnitude <= stopDistance)
            {
                isMoving = false;

                // Tocar animação de idle
                if (animator != null)
                {
                    animator.Play(stateIdle);
                }
                yield break;
            }

            // Move o NPC
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;

            // Tocar animação de movimento
            if (animator != null)
            {
                animator.Play(stateMove);
            }

            yield return null; // Espera até o próximo frame
        }
    }
}
