using System.Windows.Media;

namespace CSHARP_PW_PROJECT.Model
{
    public class Circle
    {
        public int height { get; set; }
        public int wide { get; set; }
        public string color { get; set; }
        public int leftPosition { get; set; }
        public int topPosition { get; set; }
        public TranslateTransform RenderTransform { get; internal set; }
    }
}
