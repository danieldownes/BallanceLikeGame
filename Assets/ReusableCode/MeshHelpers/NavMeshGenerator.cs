using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace ReusableCode.MeshHelpers
{
    /// <summary>
    /// NavMeshSurface allow building a NavMesh from the scene geometry at runtime.
    /// The class will also generate a Mesh from the NavMesh data to display the NavMesh in the scene.
    /// Uses URP shader.
    /// </summary>
    public class NavMeshGenerator
    {
        private float agentRadius = 0.5f;
        private float agentHeight = 0.1f; // Small height to disallow height variation
        private float agentStepHeight = 0.1f;
        private float agentMaxSlope = 0f; // Zero slope to ignore any height variation

        public Mesh Mesh => navMesh;
        private Mesh navMesh;

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

            createMesh(navMeshObject);
        }

        private void createMesh(GameObject navMeshObject)
        {
            // Get the navmesh data
            NavMeshTriangulation triangulation = NavMesh.CalculateTriangulation();

            Vector3[] vertices = triangulation.vertices;
            int[] indices = triangulation.indices;

            // Create a mesh from the navmesh data
            navMesh = new Mesh();
            navMesh.vertices = vertices;
            navMesh.triangles = indices;
            navMesh.RecalculateNormals();

            // Create a GameObject to display the navmesh
            MeshFilter meshFilter = navMeshObject.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = navMeshObject.AddComponent<MeshRenderer>();
            meshFilter.mesh = navMesh;
            meshRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            meshRenderer.material.color = new Color(0, 0.2f, 1, 0.4f);
            meshFilter.transform.Translate(0, 0.01f, 0);
        }
    }
}
