using CSHARP_PW_PROJECT.Commands;
using CSHARP_PW_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;

namespace CSHARP_PW_PROJECT.ViewModel
{
    internal class CircleViewModel
    {
        public ObservableCollection<Circle> circleList { get; private set; }
        private string circleNumber = "";
        public string CircleNumber
        {
            get
            {
                return circleNumber;
            }
            set
            {
                circleNumber = value;
                _createCirclesCommand.InvokeCanExecuteChanged();
            }
        }
        private readonly List<string> colorList = new();
        private readonly CircleCommands _createCirclesCommand;
        public ICommand CreateCirclesCommand => _createCirclesCommand;

        public CircleViewModel()
        {
            colorList.Add("Black");
            colorList.Add("Yellow");
            colorList.Add("Red");
            colorList.Add("Blue");
            colorList.Add("Purple");
            colorList.Add("Orange");
            colorList.Add("Green");
            colorList.Add("Pink");
        
            _createCirclesCommand = new CircleCommands(OnCreateCirclesCommand, CanCreateCirclesCommand);         
            circleList = new ObservableCollection<Circle> { };

        }
        private bool CanCreateCirclesCommand(object commandParameter)
        {
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return new Regex(pattern).IsMatch(circleNumber);
        }

        private void OnCreateCirclesCommand(object obj)
        {
            Random random = new();
            int circlesCount = int.Parse(circleNumber);

            for (int i = 0; i < circlesCount; i++)
            {
                int whichColor = random.Next(0, 8);
                int toMoveHorizontal = random.Next(40, 1400);
                int toMoveVertical = random.Next(40, 500);

                Circle circle = new();

                circle.leftPosition = toMoveHorizontal;
                circle.topPosition = toMoveVertical;
                circle.wide = 50;
                circle.height = 50;
                circle.RenderTransform = new TranslateTransform();
                circle.color = colorList.ElementAt(whichColor);

                circleList.Add(circle);
            }
        }
    }
}
