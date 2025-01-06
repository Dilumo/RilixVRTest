using UnityEngine;
using UnityEngine.Events;

namespace RilixHalloweenChallenge
{
    public class CollisionTriggerExample : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform target;

        [Header("Events")]
        [SerializeField] UnityEvent onTriggerEnter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == target)
            {
                onTriggerEnter?.Invoke();
            }
        }
    }
}