using CSHARP_PW_PROJECT.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using CSHARP_PW_PROJECT.Model;

namespace CSHARP_PW_PROJECT_TESTS
{
   [TestClass]
    public class CircleViewModelTests
    {
        private CircleViewModel _circleViewModel = new();

        [TestMethod]
        public void CheckIf_CircleListIsNotNull_AfterCVMInitialization()
        {
            Assert.IsNotNull(_circleViewModel.CircleList);
        }

        [TestMethod]
        public void CheckIf_CircleNumberIsEmpty_AfterCVMInitialization()
        {
            Assert.AreEqual("", _circleViewModel.CircleNumber);
        }

        [TestMethod]
        public void CheckIf_CircleListContainGivenNumberOfCircles()
        {
            _circleViewModel.CircleNumber = "50";
            _circleViewModel.CircleHeight = "50";
            _circleViewModel.CircleWidth = "50";
            _circleViewModel.CircleSpeed = "0.2";


            _circleViewModel.CreateCirclesCommand.Execute(null);

            Assert.AreEqual(50, _circleViewModel.CircleList.Count);
        }


        [TestMethod]
        public void CheckIf_CircleChangedPosition_AfterManuallyMoved()
        {
            _circleViewModel.CircleNumber = "1";
            _circleViewModel.CircleHeight = "50";
            _circleViewModel.CircleWidth = "50";
            _circleViewModel.CircleSpeed = "0.2";
            
            _circleViewModel.CreateCirclesCommand.Execute(null);
            ModelCircle circle = _circleViewModel.CircleList.First();
            int topPositionBeforeMove = circle.topPosition;
            int leftPositionBeforeMove = circle.leftPosition;

            _circleViewModel.MoveCirclesManuallyCommand.Execute(null);

            Assert.IsNotNull(circle);
            Assert.AreNotEqual(topPositionBeforeMove, circle.topPosition);
            Assert.AreNotEqual(leftPositionBeforeMove, circle.leftPosition);
        }

        [TestMethod]
        public void CheckIf_CircleListIsEmpty_AfterClear()
        {
            _circleViewModel.CircleNumber = "50";
            _circleViewModel.CircleHeight = "50";
            _circleViewModel.CircleWidth = "50";
            _circleViewModel.CircleSpeed = "0.2";

            _circleViewModel.CreateCirclesCommand.Execute(null);

            Assert.AreEqual(50, _circleViewModel.CircleList.Count);

            _circleViewModel.DeleteCirclesCommand.Execute(null);

            Assert.AreEqual(0, _circleViewModel.CircleList.Count);
        }

        [TestMethod]
        public void CheckIf_CircleChangedPosition_AfterAutomaticallyMoved()
        {
            _circleViewModel.CircleNumber = "1";
            _circleViewModel.CircleHeight = "50";
            _circleViewModel.CircleWidth = "50";
            _circleViewModel.CircleSpeed = "0.2";

            
            _circleViewModel.CreateCirclesCommand.Execute(null);
            var circle = _circleViewModel.CircleList.First();
            int topPositionBeforeMove = circle.lastTopPosition;
            int leftPositionBeforeMove = circle.lastLeftPosition;

            _circleViewModel.MoveCirclesAutomaticallyCommand.Execute(null);
            _circleViewModel.StopCirclesCommand.Execute(null);

            Assert.IsNotNull(circle);
            Assert.AreNotEqual(topPositionBeforeMove, circle.topPosition);
            Assert.AreNotEqual(leftPositionBeforeMove, circle.leftPosition);
        }
    }
}