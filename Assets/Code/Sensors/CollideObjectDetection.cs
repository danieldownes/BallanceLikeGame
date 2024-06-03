using System;
using UnityEngine;

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