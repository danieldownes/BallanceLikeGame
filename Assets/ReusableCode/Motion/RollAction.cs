using UnityEngine;

namespace ReusableCode.Motion
{
    [RequireComponent(typeof(Rigidbody))]
    public class RollAction : MonoBehaviour
    {
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void ApplyRollDirection(Vector3 torque)
        {
            rb.AddTorque(torque, ForceMode.Force);
        }

        public void Stop()
        {
            rb.angularVelocity = Vector3.zero;
        }
    }
}