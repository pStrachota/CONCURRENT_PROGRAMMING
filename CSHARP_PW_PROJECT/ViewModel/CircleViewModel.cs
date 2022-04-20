using CSHARP_PW_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;

namespace CSHARP_PW_PROJECT.ViewModel
{
    public class CircleViewModel : ViewModelBase
    {
        /// <summary>
        /// our main 'automatic logic' lies here
        /// dispatherTimer is used for invoking 
        /// </summary>
        readonly DispatcherTimer _gameTimer = new();
        public ObservableCollection<Circle> circleList { get; private set; }
        private string _circleNumber = "";
        private string _circleWidth = "";
        private string _circleHeight = "";
        private string _circleSpeed = "";
        public string CircleNumber
        {
            get
            {
                return _circleNumber;
            }
            set
            {
                _circleNumber = value;
                CreateCirclesCommand.NotifyCanExecuteChanged();
                ResetValuesCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged("CircleNumber");
            }
        }
        public string CircleWidth
        {
            get => _circleWidth;
            set
            {
                _circleWidth = value;
                CreateCirclesCommand.NotifyCanExecuteChanged();
                ResetValuesCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged("CircleWidth");
            }
        }
        public string CircleHeight
        {
            get => _circleHeight;
            set
            {
                _circleHeight = value;
                CreateCirclesCommand.NotifyCanExecuteChanged();
                ResetValuesCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged("CircleHeight");
            }
        }
        
        public string CircleSpeed
        {
            get => _circleSpeed;
            set
            {
                _circleSpeed = value;
                CreateCirclesCommand.NotifyCanExecuteChanged();
                ResetValuesCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged("CircleSpeed");
            }
        }
        
        private readonly List<string> colorList = new();
        
        public RelayCommand CreateCirclesCommand { get; private set; }
        public RelayCommand MoveCirclesManuallyCommand { get; private set; }
        public RelayCommand MoveCirclesAutomaticallyCommand { get; private set; }
        public RelayCommand DeleteCirclesCommand { get; private set; }
        public RelayCommand StopCirclesCommand { get; private set; }
        public RelayCommand ResetValuesCommand { get; private set; }

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
            ResetValuesCommand = new RelayCommand(OnResetValuesCommand, CanResetValuesCommand);
            _gameTimer.Tick += GameTimerEvent;
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
            string pattern2 = "^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$";
            
            return new Regex(pattern).IsMatch(_circleNumber) && new Regex(pattern).IsMatch(_circleWidth)
                                                             && new Regex(pattern).IsMatch(_circleHeight)
                                                             && new Regex(pattern2).IsMatch(_circleSpeed);
        }

        private bool CanMoveCirclesCommand()
        {
            return circleList.Count > 0 && !_gameTimer.IsEnabled;
        }

        private bool CanDeleteCirclesCommand()
        {
            return !_gameTimer.IsEnabled && circleList.Count > 0;
        }

        private bool CanStopCirclesCommand()
        {
            return _gameTimer.IsEnabled;
        }

        private void OnResetValuesCommand()
        {
            CircleNumber = "";
            CircleWidth = "";
            CircleHeight = "";
            CircleSpeed = "";
        }
        
        private void OnDeleteCirclesCommand()
        {
            this.circleList.Clear();
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
        }
        
        private bool CanResetValuesCommand()
        {
            return !_gameTimer.IsEnabled && (CircleNumber != "" || CircleWidth != "" || CircleHeight != "" || CircleSpeed != "");
        }

        private void OnMoveCirclesAutomaticallyCommand()
        {
            _gameTimer.Tick += GameTimerEvent;
            _gameTimer.Interval = TimeSpan.FromSeconds(1);
            _gameTimer.Start();
            
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
            StopCirclesCommand.NotifyCanExecuteChanged();
        }

        private void OnStopCirclesCommand()
        {
            _gameTimer.Stop();
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
            int circlesCount = int.Parse(_circleNumber);
            double circlesSpeed = double.Parse(_circleSpeed, CultureInfo.InvariantCulture);

            
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

                DoubleAnimation anim1 = new(top, toMoveVertical, TimeSpan.FromSeconds(circlesSpeed));
                DoubleAnimation anim2 = new(left, toMoveHorizontal, TimeSpan.FromSeconds(circlesSpeed));

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
            int circlesCount = int.Parse(_circleNumber);
            int circlesWidth = int.Parse(_circleWidth);
            int circlesHeight = int.Parse(_circleHeight);

            for (int i = 0; i < circlesCount; i++)
            {
                int whichColor = random.Next(0, 8);

                Circle circle = new();

                circle.leftPosition = 750;
                circle.topPosition = 0;
                circle.wide = circlesWidth;
                circle.height = circlesHeight;
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
