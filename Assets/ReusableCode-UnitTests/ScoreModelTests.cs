using NUnit.Framework;
using ReusableCode.Stats;

namespace ReusableCode.Tests
{
    /// <summary>
    /// Unit tests for the ScoreModel class.
    /// It makes sense to generate tests to validate the provided implementation.
    /// These used to be written by hand, but now we should generate these and then review
    /// the generated code.
    /// Ref: https://chatgpt.com/share/bbbaaea8-1559-40c1-b946-ddf97e5ef9ca
    /// </summary>
    [TestFixture]
    public class ScoreModelTests
    {
        private ScoreModel scoreModel;

        [SetUp]
        public void SetUp()
        {
            scoreModel = new ScoreModel();
        }

        [Test]
        public void Score_SetScore_RaisesOnScoreChangedEvent()
        {
            bool eventRaised = false;
            int expectedScore = 10;
            scoreModel.OnScoreChanged += (score) => eventRaised = score == expectedScore;

            scoreModel.Score = expectedScore;

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void Score_SetScore_UpdatesScore()
        {
            int expectedScore = 10;
            scoreModel.Score = expectedScore;

            Assert.AreEqual(expectedScore, scoreModel.Score);
        }

        [Test]
        public void AddScore_AddsToCurrentScore()
        {
            int initialScore = 5;
            int amountToAdd = 10;
            scoreModel.Score = initialScore;

            scoreModel.AddScore(amountToAdd);

            Assert.AreEqual(initialScore + amountToAdd, scoreModel.Score);
        }

        [Test]
        public void AddScore_RaisesOnScoreChangedEvent()
        {
            bool eventRaised = false;
            int initialScore = 5;
            int amountToAdd = 10;
            scoreModel.Score = initialScore;
            int expectedScore = initialScore + amountToAdd;
            scoreModel.OnScoreChanged += (score) => eventRaised = score == expectedScore;

            scoreModel.AddScore(amountToAdd);

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void OnScoreChanged_NoListeners_DoesNotThrowException()
        {
            Assert.DoesNotThrow(() =>
            {
                scoreModel.Score = 10;
            });
        }
    }
}
