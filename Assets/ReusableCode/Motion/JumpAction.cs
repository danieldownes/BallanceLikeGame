using UnityEngine;

namespace ReusableCode.Motion
{
    [RequireComponent(typeof(Rigidbody))]
    public class JumpAction : MonoBehaviour
    {
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void ApplyJumpForce(float force)
        {
            rb.AddForce(Vector3.up * force, ForceMode.VelocityChange);
        }

        public void Stop()
        {
            rb.velocity = Vector3.zero;
        }
    }
}