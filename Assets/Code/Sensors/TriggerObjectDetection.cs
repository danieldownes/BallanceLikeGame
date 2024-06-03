using System;
using UnityEngine;

public class TriggerObjectDetection : MonoBehaviour
{
    public event Action OnTrigger;
    public GameObject TriggerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerObject == other.gameObject)
            OnTrigger?.Invoke();
    }
}
