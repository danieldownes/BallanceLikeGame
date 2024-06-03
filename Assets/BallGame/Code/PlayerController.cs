using ReusableCode.Motion;
using ReusableCode.Sensors;
using UnityEngine;

/// <summary>
/// Bind User input to Player actions.
/// </summary>
namespace BallGame
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject Ball;
        private RollAction roll;
        private JumpAction jump;
        private GroundedSense grounded;

        public float jumpForce = 5.0f;
        public float moveForce = 50f;
        private Vector3 spawnPosition;

        private void Awake()
        {
            roll = Ball.AddComponent<RollAction>();
            jump = Ball.AddComponent<JumpAction>();
            grounded = Ball.AddComponent<GroundedSense>();
        }

        private void Update()
        {
            // Bind jump action
            if (Input.GetButtonDown("Jump") && grounded.IsGrounded())
                jump.ApplyJumpForce(jumpForce);
        }

        private void FixedUpdate()
        {
            // Bind roll action
            Vector3 direction = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
            roll.ApplyRollDirection(direction * moveForce * Time.fixedDeltaTime);
        }

        internal void ResetPosition(Vector3 position)
        {
            spawnPosition = position;
            ResetPosition();
        }

        internal void ResetPosition()
        {
            Ball.transform.position = spawnPosition;
            jump.Stop();
            roll.Stop();
        }
    }
}
