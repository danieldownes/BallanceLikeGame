using ReusableCode.MeshHelpers;
using ReusableCode.Stats;
using UnityEngine;

namespace BallGame
{
    /// <summary>
    /// This class demonstrates show the DI pattern can be implemented and acts as a SceneContext to outline the
    /// implementation classes.
    /// Although a DI framework is not added to this project, the classes are still decoupled and can be easily replaced
    /// simply by editing this class. Pattern also facilitates testing as dependencies can be easily mocked.
    /// </summary>
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private ScoreUi scoreUi;

        private void Start()
        {
            var levelGeneration = new LevelGeneration();
            var navMeshGenerator = new NavMeshGenerator();
            var randomPointOnMesh = new RandomPointOnMesh();
            var collectibleFactory = new CollectibleFactory();

            levelGeneration.Init(player);
            levelGeneration.GenerateLevelArea();
            levelGeneration.AddStaticBarriers(100);
            levelGeneration.AddCollectibles(50, navMeshGenerator, randomPointOnMesh, collectibleFactory);
            levelGeneration.AddOutOfBoundsDetection();

            Vector3 playerSpawnPoint = randomPointOnMesh.GetRandomPointOnMesh(navMeshGenerator.Mesh);
            player.ResetPosition(playerSpawnPoint + Vector3.up * 1.2f);

            ScoreModel scoreModel = new ScoreModel();
            scoreUi.Init(scoreModel);

            _ = new CollectibleScoreInteractor(scoreModel, collectibleFactory);
        }
    }
}