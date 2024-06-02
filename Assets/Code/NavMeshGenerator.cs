using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshGenerator : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float agentRadius = 0.5f;
    public float agentHeight = 0.1f; // Small height to disallow height variation
    public float agentStepHeight = 0.1f;
    public float agentMaxSlope = 0f; // Zero slope to ignore any height variation

    public Mesh Mesh => navMesh;
    private Mesh navMesh;

    void Start()
    {
        GenerateNavMesh();
    }

    public void GenerateNavMesh()
    {
        GameObject navMeshObject = new GameObject("NavMesh");

        // Set the agent settings
        NavMeshBuildSettings buildSettings = NavMesh.CreateSettings();
        buildSettings.agentRadius = agentRadius;
        buildSettings.agentHeight = agentHeight;
        buildSettings.agentClimb = agentStepHeight;
        buildSettings.agentSlope = agentMaxSlope;

        NavMeshSurface navMeshSurface = navMeshObject.AddComponent<NavMeshSurface>();
        navMeshSurface.agentTypeID = buildSettings.agentTypeID;

        // Build the navmesh
        navMeshSurface.BuildNavMesh();

        // Get the navmesh data
        NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

        // Use the triangulation data (vertices and indices)
        Vector3[] vertices = triangulation.vertices;
        int[] indices = triangulation.indices;

        // Optionally, create a mesh to visualize the navmesh
        navMesh = new Mesh();
        navMesh.vertices = vertices;
        navMesh.triangles = indices;
        navMesh.RecalculateNormals();

        // Create a GameObject to display the navmesh
        MeshFilter meshFilter = navMeshObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = navMeshObject.AddComponent<MeshRenderer>();
        meshFilter.mesh = navMesh;
        meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        meshFilter.transform.Translate(0, 0.01f, 0);

        Debug.Log("NavMesh generated with " + vertices.Length + " vertices and " + indices.Length / 3 + " triangles.");
    }
}
