using UnityEngine;

/// <summary>
/// Bind User input to Player action.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public GameObject Ball;
    private RollAction roll;
    private JumpAction jump;
    private GoundedSense grounded;

    public float jumpForce = 5.0f;
    public float moveForce = 50f;
    private Vector3 spawnPosition;

    private void Awake()
    {
        roll = Ball.AddComponent<RollAction>();
        jump = Ball.AddComponent<JumpAction>();
        grounded = Ball.AddComponent<GoundedSense>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded.IsGrounded())
        {
            jump.ApplyJumpForce(jumpForce);
        }
    }

    private void FixedUpdate()
    {
        // Bind input using Input System 1, to jump

        Vector3 direction = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
        roll.ApplyTorqueForce(direction * moveForce * Time.fixedDeltaTime);

    }

    internal void Reset(Vector3 position)
    {
        spawnPosition = position;
        Reset();
    }

    internal void Reset()
    {
        Ball.transform.position = spawnPosition;
        Ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }
}
