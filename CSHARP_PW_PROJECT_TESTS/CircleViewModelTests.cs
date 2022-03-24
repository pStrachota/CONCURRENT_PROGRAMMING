using CSHARP_PW_PROJECT.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CSHARP_PW_PROJECT_TESTS
{
    [TestClass]
    public class CircleViewModelTests
    {
        readonly CircleViewModel cvm = new();

        [TestMethod]
        public void CheckIf_CircleListIsNotNull_AfterCVMInitialization()
        {
            Assert.IsNotNull(cvm.circleList);
        }

        [TestMethod]
        public void CheckIf_CircleNumberIsEmpty_AfterCVMInitialization()
        {
            Assert.AreEqual("", cvm.CircleNumber);
        }

        [TestMethod]
        public void CheckIf_CircleListContainGivenNumberOfCircles()
        {
            cvm.CircleNumber = "50";

            cvm.CreateCirclesCommand.Execute(cvm.CircleNumber);

            Assert.AreEqual(50, cvm.circleList.Count);
        }

        [TestMethod]
        public void CheckIf_CircleChangedPosition_AfterManuallyMoved()
        {
            cvm.CircleNumber = "1";
            cvm.CreateCirclesCommand.Execute(cvm.CircleNumber);
            var circle = cvm.circleList.First();    
            int topPositionBeforeMove = circle.topPosition;
            int leftPositionBeforeMove = circle.leftPosition;

            cvm.MoveCirclesManuallyCommand.Execute(cvm.CircleNumber);

            Assert.IsNotNull(circle);
            Assert.AreNotEqual(topPositionBeforeMove, circle.topPosition);
            Assert.AreNotEqual(leftPositionBeforeMove, circle.leftPosition);
        }
    }
}