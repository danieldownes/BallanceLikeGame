using System.Collections;
using NUnit.Framework;
using ReusableCode.Sensors;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// In order to save time and demonstrate good usage of tooling,
/// this PlayMode test was LLM Generated:
/// https://chatgpt.com/share/6f9e0391-e349-4efa-b998-b06b946ce9b0
/// </summary>
public class GroundedSenseTests
{
    private GameObject testObject;
    private GroundedSense groundedSense;

    [SetUp]
    public void Setup()
    {
        testObject = new GameObject();
        var collider = testObject.AddComponent<BoxCollider>();
        groundedSense = testObject.AddComponent<GroundedSense>();

        // Create ground plane
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.position = Vector3.zero;
    }

    [TearDown]
    public void Teardown()
    {
        GameObject.DestroyImmediate(testObject);
        GameObject.DestroyImmediate(GameObject.Find("Plane"));
    }

    [UnityTest]
    public IEnumerator TestIsGrounded()
    {
        // Position the object on the ground
        testObject.transform.position = new Vector3(0, 0.5f, 0);
        yield return null; // Wait for a frame
        Assert.IsTrue(groundedSense.IsGrounded(), "The object should be grounded.");

        // Raise the object above the ground
        testObject.transform.position = new Vector3(0, 2f, 0);
        yield return null; // Wait for a frame
        Assert.IsFalse(groundedSense.IsGrounded(), "The object should not be grounded.");

        // Position the object slightly above the ground within tolerance
        testObject.transform.position = new Vector3(0, 0.5f + 0.005f, 0);
        yield return null; // Wait for a frame
        Assert.IsTrue(groundedSense.IsGrounded(), "The object should be grounded due to tolerance.");
    }
}
