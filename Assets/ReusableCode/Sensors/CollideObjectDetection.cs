using System;
using UnityEngine;

namespace ReusableCode.Sensors
{
    public class CollideObjectDetection : MonoBehaviour
    {
        public event Action OnCollide;
        public GameObject CollisionObject;

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionObject == collision.gameObject)
                OnCollide?.Invoke();
        }
    }
}