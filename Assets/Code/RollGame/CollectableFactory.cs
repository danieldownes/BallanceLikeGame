using System;
using UnityEngine;

internal class CollectableFactory
{
    public event Action<GameObject> OnCollectableCollected;

    internal void Add(Vector3 position, GameObject triggerObject)
    {
        GameObject collectable = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        collectable.transform.localScale = Vector3.one * 0.2f;
        collectable.transform.position = position;
        collectable.GetComponent<Collider>().isTrigger = true;

        var trigger = collectable.AddComponent<TriggerObjectDetection>();
        trigger.TriggerObject = triggerObject;
        trigger.OnTrigger += () => OnCollectableCollected?.Invoke(collectable);
    }
}