using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using BUSINESS_LOGIC_LAYER;

namespace CSHARP_PW_PROJECT.Model
{
    public class ModelCircle
    {
        private readonly List<string?> _colorList = new List<string?>()
        {
            "Black",
            "Yellow",
            "Red",
            "Blue",
            "Brown",
            "White",
            "Purple",
            "Orange",
            "Green",
            "Pink",
            "Gray"
        };

        public ModelCircle() { }
    
        public ModelCircle(BllCircle bllCircle)
        {
            this.height = bllCircle.height;
            this.wide = bllCircle.wide;
            this.color = _colorList.ElementAt(new Random().Next(0, 8));
            this.startingTopPosition = bllCircle.startingTopPosition;
            this.startingLeftPosition = bllCircle.startingLeftPosition;
            this.leftPosition = bllCircle.leftPosition;
            this.topPosition = bllCircle.topPosition;
            this.RenderTransform = new TranslateTransform();
        }
    
        public int height { get; set; }
        public int wide { get; set; }
        public string? color { get; set; }
        public int startingLeftPosition { get; set; }
        public int startingTopPosition { get; set; }
        public int lastLeftPosition { get; set; }
        public int lastTopPosition { get; set; }
        public int leftPosition { get; set; }
        public int topPosition { get; set; }
        public TranslateTransform? RenderTransform { get; internal set; }
    }
}
