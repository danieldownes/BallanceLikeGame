using System;
using ReusableCode.Sensors;
using UnityEngine;

namespace BallGame
{
    /// <summary>
    /// Collectibles are initialised here to default values.
    /// </summary>
    public class CollectibleFactory
    {
        public event Action<GameObject> OnCollectibleCollected;

        public void Add(Vector3 position, GameObject triggerObject)
        {
            GameObject collectable = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            collectable.transform.localScale = Vector3.one * 0.2f;
            collectable.transform.position = position;
            collectable.GetComponent<Collider>().isTrigger = true;

            MeshRenderer meshRenderer = collectable.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            meshRenderer.material.color = Color.yellow;

            TriggerObjectDetection trigger = collectable.AddComponent<TriggerObjectDetection>();
            trigger.TriggerObject = triggerObject;
            trigger.OnTrigger += () => OnCollectibleCollected?.Invoke(collectable);
        }
    }
}