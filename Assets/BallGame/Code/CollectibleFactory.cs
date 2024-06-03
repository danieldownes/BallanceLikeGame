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
            GameObject collectible = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            collectible.transform.localScale = Vector3.one * 0.2f;
            collectible.transform.position = position;
            collectible.GetComponent<Collider>().isTrigger = true;

            MeshRenderer meshRenderer = collectible.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            meshRenderer.material.color = Color.yellow;

            TriggerObjectDetection trigger = collectible.AddComponent<TriggerObjectDetection>();
            trigger.TriggerObject = triggerObject;
            trigger.OnTrigger += () => OnCollectibleCollected?.Invoke(collectible);
        }
    }
}