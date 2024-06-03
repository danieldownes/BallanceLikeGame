using UnityEngine;

namespace ReusableCode.Sensors
{
    public class GroundedSense : MonoBehaviour
    {
        private const float tolerance = 0.01f;
        private float height;
        private void Awake()
        {
            var collider = gameObject.GetComponent<Collider>();
            height = collider.bounds.max.y;
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, height + tolerance);
        }
    }
}