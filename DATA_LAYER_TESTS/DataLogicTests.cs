using DATA_LAYER;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DATA_LOGIC_TESTS
{
    [TestClass]
    public class DataLogicTests
    {

        DataLayerAbstractApi _dataLayerAbstractApi;

        [TestInitialize]
        public void Initialize()
        {
            _dataLayerAbstractApi = DataLayerAbstractApi.CreateLinq2DLCircles();
        }

        [TestMethod]
        public void CheckIf_DataLayerIsNotNull_AfterInitialization()
        {
            Assert.IsNotNull(_dataLayerAbstractApi);

        }

        [TestMethod]
        public void CheckIf_DllCirclesIsNotNull_AfterInitialization()
        {
            Assert.IsNotNull(_dataLayerAbstractApi.GetDllCirclesFromBox(2, 20, 50, 20));
        }

        [TestMethod]
        public void CheckIf_SeparateBallsFromBox_AreNotEqual()
        {
            var balls = _dataLayerAbstractApi.GetDllCirclesFromBox(2, 20, 50, 20);
            var ball = balls.ElementAt(0);
            var AnotherBall = balls.ElementAt(1);

            Assert.IsNotNull(ball);
            Assert.IsNotNull(AnotherBall);
            Assert.AreNotEqual(ball, AnotherBall);

        }
    }
}