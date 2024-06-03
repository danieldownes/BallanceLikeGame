using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GroundedSenseTests
{
    private GameObject testObject;
    private GroundedSense groundedSense;

    [SetUp]
    public void Setup()
    {
        // Setup the environment
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
        // Cleanup
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
