using System.Windows.Media;

namespace CSHARP_PW_PROJECT.Model
{
    public class Circle
    {
        /// <summary>
        /// Model Circle Class
        /// Contains info, which is used for binding:
        /// one circle height, wide, color
        /// but also leftPosition and topPosition
        /// Render transform - is used for animate road between random positions
        /// </summary>
        public int height { get; set; }
        public int wide { get; set; }
        public string color { get; set; }
        public int leftPosition { get; set; }
        public int topPosition { get; set; }
        public TranslateTransform RenderTransform { get; internal set; }
    }
}
