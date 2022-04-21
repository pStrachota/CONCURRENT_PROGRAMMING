using BUSINESS_LOGIC_LAYER;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1;


[TestClass]
public class BusinessLogicAbstractApiTests
{
    private BusinessLogicAbstractApi _businessLogicAbstractApi;

    [TestInitialize]
    public void Initialize()
    {
        _businessLogicAbstractApi = BusinessLogicAbstractApi.CreateLayer();
    }
    
    [TestMethod]
    public void CheckIf_BusinessLogicImplementationIsNotNull_AfterBLLInitialization()
    {
        Assert.IsNotNull(_businessLogicAbstractApi);
    }

    [TestMethod]
    public void CheckIf_BllCircleIsNotNull_AfterInitialization()
    {
        BllCircle bllCircle = _businessLogicAbstractApi.CreateBllCircle(50, 50);
        
        Assert.IsNotNull(bllCircle);
    }
    
    [TestMethod]
    public void CheckIf_BllCircleChangedPosition_AfterMoved()
    {
        BllCircle bllCircle = new BllCircle(50, 50);
        int topPositionBeforeMove = bllCircle.topPosition;
        int leftPositionBeforeMove = bllCircle.leftPosition;

        _businessLogicAbstractApi.moveBllCircle(bllCircle);

        Assert.IsNotNull(bllCircle);
        Assert.AreNotEqual(topPositionBeforeMove, bllCircle.topPosition);
        Assert.AreNotEqual(leftPositionBeforeMove, bllCircle.leftPosition);
    }

}