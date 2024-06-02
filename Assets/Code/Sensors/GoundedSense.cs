using UnityEngine;

public class GoundedSense : MonoBehaviour
{
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}