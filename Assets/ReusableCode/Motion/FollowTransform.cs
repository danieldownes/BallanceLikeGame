using UnityEngine;

namespace ReusableCode.Motion
{
    public class FollowTransform : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        void Update()
        {
            transform.position = target.position + offset;
        }
    }
}