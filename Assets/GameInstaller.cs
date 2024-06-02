using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private NavMeshGenerator navMeshGenerator;

    void Start()
    {
        // Generate level
        generateLevel();
        navMeshGenerator.GenerateNavMesh();

        addStaticBarriers();

        // Generate layout

        // Apply MeshNav
        // Add NavMeshSurface and generate NavMesh
        //applyNavMesh();

        var random = FindAnyObjectByType<RandomPointOnMesh>();
        random.CalcAreas(navMeshGenerator.Mesh);

    }

    private void addStaticBarriers()
    {
        // Add small static barriers randomly on the quad

        // Add cube

        GameObject barrier = GameObject.CreatePrimitive(PrimitiveType.Cube);
        barrier.transform.localScale = new Vector3(2f, 0.5f, 0.5f);
        barrier.transform.position = new Vector3(Random.Range(-15f, 15f), 0.25f, Random.Range(-15f, 15f));
        barrier.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        barrier.isStatic = true;
    }

    private void applyNavMesh()
    {
        // Create a new GameObject for the NavMesh
        GameObject navMeshObject = new GameObject("NavMesh");
        var navmeshsettings = NavMesh.GetSettingsByIndex(0);
        //navmeshsettings.agentHeight = 0.001f;
        //navmeshsettings.buildHeightMesh = false;
        navmeshsettings.agentClimb = 0.1f;

        // Add the NavMeshSurface component to the GameObject
        NavMeshSurface navMeshSurface = navMeshObject.AddComponent<NavMeshSurface>();

        // Set the parameters for the NavMesh
        navMeshSurface.agentTypeID = navmeshsettings.agentTypeID; // Use the default agent type
        navMeshSurface.collectObjects = CollectObjects.All; // Collect all renderers in the scene
        navMeshSurface.overrideTileSize = true;
        navMeshSurface.tileSize = 512; // Set the tile size

        // Build the NavMesh
        navMeshSurface.BuildNavMesh();

    }

    private void generateLevel()
    {
        // Create a new GameObject with a Mesh Renderer and Mesh Filter
        GameObject quad = new GameObject("Quad");
        quad.AddComponent<MeshRenderer>();
        quad.AddComponent<MeshFilter>();

        float size = 30f;

        // Assign the mesh data for a quad
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(-size, 0, -size), // Bottom-left
            new Vector3(size, 0, -size),  // Bottom-right
            new Vector3(-size, 0, size),  // Top-left
            new Vector3(size, 0, size)    // Top-right
        };
        mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        quad.GetComponent<MeshFilter>().mesh = mesh;

        quad.isStatic = true;

    }
}
