using UnityEngine;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private NavMeshGenerator navMeshGenerator;

    [SerializeField] private ScoreUi scoreUi;
    private ScoreModel scoreModel;
    [SerializeField] private PlayerController player;

    void Start()
    {
        // Generate level
        generateLevel();

        addStaticBarriers();

        // Generate layout

        // Apply MeshNav
        navMeshGenerator.GenerateNavMesh();


        var random = FindAnyObjectByType<RandomPointOnMesh>();
        random.CalcAreas(navMeshGenerator.Mesh);

        var collectableCollection = new CollectableFactory();
        for (int i = 0; i < 50; i++)
        {
            Vector3 p = random.GetRandomPointOnMesh(navMeshGenerator.Mesh);
            collectableCollection.Add(p + Vector3.up * 0.2f, player.Ball);
        }

        scoreModel = new ScoreModel();
        scoreUi.Init(scoreModel);

        _ = new CollectableScoreInteractor(scoreModel, collectableCollection);
    }

    private void addStaticBarriers()
    {
        for (int i = 0; i < 100; i++)
        {
            addBarrier();
        }
    }

    private static void addBarrier()
    {
        GameObject barrier = GameObject.CreatePrimitive(PrimitiveType.Cube);
        barrier.transform.localScale = new Vector3(2f, 0.5f, 0.5f);
        barrier.transform.position = new Vector3(Random.Range(-15f, 15f), 0.25f, Random.Range(-15f, 15f));
        barrier.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        barrier.isStatic = true;
    }

    private void generateLevel()
    {
        // Create a new GameObject with a Mesh Renderer and Mesh Filter
        GameObject quad = new GameObject("Quad");
        MeshRenderer meshRenderer = quad.AddComponent<MeshRenderer>();
        quad.AddComponent<MeshFilter>();

        float size = 20f;

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
        quad.AddComponent<MeshCollider>();

        meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        meshRenderer.material.color = Color.blue;

        quad.isStatic = true;
    }
}
