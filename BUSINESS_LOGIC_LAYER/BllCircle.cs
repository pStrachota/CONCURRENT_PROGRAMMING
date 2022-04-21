namespace BUSINESS_LOGIC_LAYER;

public class BllCircle
{
    public BllCircle(int wide, int height)
    {
        Random random = new();
        int toMoveHorizontal = random.Next(51, 1405);
        int toMoveVertical = random.Next(275, 770 - height + 4);

        this.wide = wide;
        this.height = height;
        this.startingLeftPosition = toMoveHorizontal;
        this.startingTopPosition = toMoveVertical;
        this.leftPosition = toMoveHorizontal;
        this.topPosition = toMoveVertical;
    }

    public int height { get; set; }
    public int wide { get; set; }
    public string color { get; set; }
    public int startingLeftPosition { get; set; }
    public int startingTopPosition { get; set; }
    public int lastLeftPosition { get; set; }
    public int lastTopPosition { get; set; }
    public int leftPosition { get; set; }
    public int topPosition { get; set; }
}