using ReusableCode.MeshHelpers;
using ReusableCode.Sensors;
using UnityEngine;

namespace BallGame
{
    /// <summary>
    /// As per the README.md, this class generates custom quad meshes for the level, adds static barriers
    /// Generates a nav mesh to be used to select points on the nav mesh while avoiding points inside the barriers.
    /// 
    /// </summary>
    public class LevelGeneration
    {
        private PlayerController player;

        public void Init(PlayerController player)
        {
            this.player = player;
        }

        public void GenerateLevelArea()
        {
            generateQuad("LevelArea");
        }

        public void AddCollectibles(int total, NavMeshGenerator navMeshGenerator, RandomPointOnMesh random, CollectibleFactory collectibleFactory)
        {
            navMeshGenerator.GenerateNavMesh();
            random.CalcAreas(navMeshGenerator.Mesh);

            for (int i = 0; i < total; i++)
            {
                Vector3 p = random.GetRandomPointOnMesh(navMeshGenerator.Mesh);
                collectibleFactory.Add(p + Vector3.up * 0.2f, player.Ball);
            }
        }

        public void AddStaticBarriers(int total)
        {
            for (int i = 0; i < total; i++)
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
            addMaterial(barrier, Color.red);
        }

        public void AddOutOfBoundsDetection()
        {
            var outOfBounds = generateQuad("OutOfBoundsArea", 40, false);
            outOfBounds.transform.Translate(Vector3.down * 2);
            var trigger = outOfBounds.AddComponent<CollideObjectDetection>();
            trigger.CollisionObject = player.Ball;
            trigger.OnCollide += () => player.ResetPosition();
        }

        private GameObject generateQuad(string name, float size = 20, bool generateRenderer = true)
        {
            GameObject quad = new GameObject(name);
            quad.AddComponent<MeshFilter>();

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

            if (generateRenderer)
                addMaterial(quad, Color.blue);

            quad.isStatic = true;

            return quad;
        }

        private static void addMaterial(GameObject mesh, Color color)
        {
            MeshRenderer meshRenderer = mesh.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
                meshRenderer = mesh.AddComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            meshRenderer.material.color = color;
        }
    }
}