using ReusableCode.Stats;
using UnityEngine;

namespace BallGame
{
    internal class CollectableScoreInteractor
    {
        private CollectibleFactory collectableCollection;
        private ScoreModel scoreModel;

        public CollectableScoreInteractor(ScoreModel scoreModel, CollectibleFactory collectableCollection)
        {
            this.scoreModel = scoreModel;
            this.collectableCollection = collectableCollection;

            collectableCollection.OnCollectibleCollected += onCollectableCollected;
        }

        private void onCollectableCollected(GameObject go)
        {
            scoreModel.AddScore(1);
            GameObject.Destroy(go);
        }
    }
}