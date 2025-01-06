using UnityEngine;

namespace RilixHalloweenChallenge
{
    public class AnimationChangeExample : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator animator;

        [Header("Settings")]
        [SerializeField] private string stateName;

        public void ChangeAnimation()
        {
            animator.Play(stateName);
        }
    }
}