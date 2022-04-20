using CSHARP_PW_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;

namespace CSHARP_PW_PROJECT.ViewModel
{
    public class CircleViewModel
    {
        /// <summary>
        /// our main 'automatic logic' lies here
        /// dispatherTimer is used for invoking 
        /// </summary>
        readonly DispatcherTimer gameTimer = new();
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
                CreateCirclesCommand.NotifyCanExecuteChanged();
            }
        }

        private readonly List<string> colorList = new();
        
        public RelayCommand CreateCirclesCommand { get; private set; }
        public RelayCommand MoveCirclesManuallyCommand { get; private set; }
        public RelayCommand MoveCirclesAutomaticallyCommand { get; private set; }
        public RelayCommand DeleteCirclesCommand { get; private set; }
        public RelayCommand StopCirclesCommand { get; private set; }

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

            // initialize CircleCommand with specific 'On' and 'Can' Commands

            CreateCirclesCommand = new RelayCommand(OnCreateCirclesCommand, CanCreateCirclesCommand);
            MoveCirclesManuallyCommand = new RelayCommand(OnMoveCirclesManuallyCommand, CanMoveCirclesCommand);
            MoveCirclesAutomaticallyCommand = new RelayCommand(OnMoveCirclesAutomaticallyCommand, CanMoveCirclesCommand);
            DeleteCirclesCommand = new RelayCommand(OnDeleteCirclesCommand, CanDeleteCirclesCommand);
            StopCirclesCommand = new RelayCommand(OnStopCirclesCommand, CanStopCirclesCommand);

            circleList = new ObservableCollection<Circle> { };

        }

        /// <summary>
        /// Every function, that starts with 'Can', is used 
        /// for check if condition that must be met to enable above action is true
        /// </summary>
        /// <param name="commandParameter"> is not used, but has be here (function<obj, bool>)</param>
        /// <returns></returns>
        private bool CanCreateCirclesCommand()
        {
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return new Regex(pattern).IsMatch(circleNumber);
        }

        private bool CanMoveCirclesCommand()
        {
            return circleList.Count > 0 && !gameTimer.IsEnabled;
        }

        private bool CanDeleteCirclesCommand()
        {
            return !gameTimer.IsEnabled && circleList.Count > 0;
        }

        private bool CanStopCirclesCommand()
        {
            return gameTimer.IsEnabled;
        }

        private void OnDeleteCirclesCommand()
        {
            this.circleList.Clear();
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
        }

        private void OnMoveCirclesAutomaticallyCommand()
        {
            gameTimer.Tick += GameTimerEvent;
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Start();
            
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
            StopCirclesCommand.NotifyCanExecuteChanged();
        }

        private void OnStopCirclesCommand()
        {
            gameTimer.Stop();
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
            StopCirclesCommand.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// function below can be improved
        /// now all our circles are created in one specific position
        /// becouse of that, circles cannot 'escape' from screen
        /// </summary>
        private void OnMoveCirclesBase()
        {
            Random random = new();
            int circlesCount = int.Parse(circleNumber);

            for (int i = 0; i < circlesCount; i++)
            {
                //hardcoded values for screen width and height
                int toMoveHorizontal = random.Next(40, 700);
                int toMoveVertical = random.Next(40, 450);

                //which side is for changing directions when moving horizontal
                int whichSide = random.Next(0, 200);

                if (whichSide > 100)
                {
                    toMoveHorizontal = -toMoveHorizontal;
                }

                int top = circleList.ElementAt(i).topPosition;
                int left = circleList.ElementAt(i).leftPosition;

                DoubleAnimation anim1 = new(top, toMoveVertical, TimeSpan.FromSeconds(1));
                DoubleAnimation anim2 = new(left, toMoveHorizontal, TimeSpan.FromSeconds(1));

                circleList.ElementAt(i).RenderTransform.BeginAnimation(TranslateTransform.XProperty, anim2);
                circleList.ElementAt(i).RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim1);

                circleList.ElementAt(i).topPosition = toMoveVertical;
                circleList.ElementAt(i).leftPosition = toMoveHorizontal;
            }
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            this.OnMoveCirclesBase();
        }
        private void OnMoveCirclesManuallyCommand()
        {
            this.OnMoveCirclesBase();
        }
        private void OnCreateCirclesCommand()
        {
            Random random = new();
            int circlesCount = int.Parse(circleNumber);

            for (int i = 0; i < circlesCount; i++)
            {
                int whichColor = random.Next(0, 8);

                Circle circle = new();

                circle.leftPosition = 750;
                circle.topPosition = 0;
                circle.wide = 50;
                circle.height = 50;
                circle.RenderTransform = new TranslateTransform();
                circle.color = colorList.ElementAt(whichColor);

                circleList.Add(circle);

                DeleteCirclesCommand.NotifyCanExecuteChanged();
                MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
                MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
