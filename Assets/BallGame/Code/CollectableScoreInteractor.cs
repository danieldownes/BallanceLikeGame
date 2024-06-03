using UnityEngine;

internal class CollectableScoreInteractor
{
    private CollectableFactory collectableCollection;
    private ScoreModel scoreModel;

    public CollectableScoreInteractor(ScoreModel scoreModel, CollectableFactory collectableCollection)
    {
        this.scoreModel = scoreModel;
        this.collectableCollection = collectableCollection;

        collectableCollection.OnCollectableCollected += onCollectableCollected;
    }

    private void onCollectableCollected(GameObject go)
    {
        scoreModel.AddScore(1);
        GameObject.Destroy(go);
    }
}