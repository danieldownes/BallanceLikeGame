using ReusableCode.Stats;
using UnityEngine;

namespace BallGame
{
    internal class CollectibleScoreInteractor
    {
        private ScoreModel scoreModel;
        private CollectibleFactory collectibleFactory;

        public CollectibleScoreInteractor(ScoreModel scoreModel, CollectibleFactory collectibleFactory)
        {
            this.scoreModel = scoreModel;
            this.collectibleFactory = collectibleFactory;

            collectibleFactory.OnCollectibleCollected += onCollectibleCollected;
        }

        private void onCollectibleCollected(GameObject collectible)
        {
            scoreModel.AddScore(1);
            GameObject.Destroy(collectible);
        }
    }
}