namespace BUSINESS_LOGIC_LAYER;

public class BllCircle
{
    public BllCircle(int wide, int height)
    {
        Random random = new();
        int toMoveHorizontal = random.Next(55, 1414);
        int toMoveVertical = random.Next(275, 770 - height + 4);

        this.wide = wide;
        this.height = height;
        this.startingLeftPosition = toMoveHorizontal;
        this.startingTopPosition = toMoveVertical;
        this.leftPosition = toMoveHorizontal;
        this.topPosition = toMoveVertical;
    }

    private int height { get; set; }
    private int wide { get; set; }
    private string color { get; set; }
    private int startingLeftPosition { get; set; }
    private int startingTopPosition { get; set; }
    private int lastLeftPosition { get; set; }
    private int lastTopPosition { get; set; }
    private int leftPosition { get; set; }
    private int topPosition { get; set; }
}