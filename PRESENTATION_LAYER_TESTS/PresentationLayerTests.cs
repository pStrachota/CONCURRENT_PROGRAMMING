using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ViewModel;

namespace PRESENTATION_TESTS
{
    [TestClass]
    public class PresentationLayerTests
    {
        ViewModel.ViewModel _circleViewModel;

        [TestInitialize]
        public void Initialize()
        {
            _circleViewModel = new();
        }

        [TestMethod]
        public void CheckIf_PresentationLayerIsNotNull_AfterInitialization()
        {
            Assert.IsNotNull(_circleViewModel);

        }

        [TestMethod]
        public void CheckIf_CircleListIsNotNull_AfterCVMInitialization()
        {
            Assert.IsNotNull(_circleViewModel.Circles);
        }

        [TestMethod]
        public void CheckIf_CannotMoveCircles_IfInputDataIsWrong()
        {
            _circleViewModel.CircleRadiusMax = "incorrect text";
            Assert.IsFalse(_circleViewModel.CanMoveCirclesCommand());

        }

        [TestMethod]
        public void CheckIf_CannotRemoveCircles_WhenCirclesListCountIsEmpty()
        {
            Assert.IsFalse(_circleViewModel.CanDeleteCommand());

        }

    }
}