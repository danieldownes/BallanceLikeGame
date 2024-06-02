using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollAction : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyTorqueForce(Vector3 torque)
    {
        rb.AddTorque(torque, ForceMode.Force);
    }
}
