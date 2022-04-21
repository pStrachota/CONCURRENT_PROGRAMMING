using CSHARP_PW_PROJECT.Model;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;

namespace CSHARP_PW_PROJECT.ViewModel
{
    public class CircleViewModel : ViewModelBase
    {
        private DispatcherTimer _gameTimer = new();

        private ModelLayerAbstractApi _modelLayerAbstractApi;
        public ObservableCollection<ModelCircle> CircleList { get; private set; }
        
        private string _circleNumber = "";
        private string _circleWidth = "";
        private string _circleHeight = "";
        private string _circleSpeed = "";
        
        public RelayCommand CreateCirclesCommand { get; }
        public RelayCommand MoveCirclesManuallyCommand { get; }
        public RelayCommand MoveCirclesAutomaticallyCommand { get; }
        public RelayCommand DeleteCirclesCommand { get; }
        public RelayCommand StopCirclesCommand { get; }

        public string CircleNumber
        {
            get => _circleNumber;
            set
            {
                _circleNumber = value;
                CreateCirclesCommand.NotifyCanExecuteChanged();
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
                 RaisePropertyChanged("CircleSpeed");
            }
        }
        
        

        public CircleViewModel()
        {
            _modelLayerAbstractApi = ModelLayerAbstractApi.CreateApi();
            CreateCirclesCommand = new RelayCommand(OnCreateCirclesCommand, CanCreateCirclesCommand);
            MoveCirclesManuallyCommand = new RelayCommand(OnMoveCirclesManuallyCommand, CanMoveCirclesCommand);
            MoveCirclesAutomaticallyCommand =
                new RelayCommand(OnMoveCirclesAutomaticallyCommand, CanMoveCirclesCommand);
            DeleteCirclesCommand = new RelayCommand(OnDeleteCirclesCommand, CanDeleteCirclesCommand);
            StopCirclesCommand = new RelayCommand(OnStopCirclesCommand, CanStopCirclesCommand);
            _gameTimer.Tick += GameTimerEvent;
            CircleList = new ObservableCollection<ModelCircle>();
        }

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
            return CircleList.Count > 0 && !_gameTimer.IsEnabled;
        }

        private bool CanDeleteCirclesCommand()
        {
            return !_gameTimer.IsEnabled && CircleList.Count > 0;
        }

        private bool CanStopCirclesCommand()
        {
            return _gameTimer.IsEnabled;
        }

        private void OnDeleteCirclesCommand()
        {
            CircleList.Clear();
            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
        }

        private void OnMoveCirclesAutomaticallyCommand()
        {
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

        private void GameTimerEvent(object? sender, EventArgs e)
        {
            int circlesCount = int.Parse(_circleNumber);
            double circlesSpeed = double.Parse(_circleSpeed, CultureInfo.InvariantCulture);

            _gameTimer.Interval = TimeSpan.FromSeconds(circlesSpeed);

            for (int i = 0; i < circlesCount; i++)
            {
                _modelLayerAbstractApi.MoveCircles(CircleList.ElementAt(i), circlesSpeed);
            }
        }

        private void OnMoveCirclesManuallyCommand()
        {
            int circlesCount = int.Parse(_circleNumber);
            double circlesSpeed = double.Parse(_circleSpeed, CultureInfo.InvariantCulture);

            for (int i = 0; i < circlesCount; i++)
            {
                _modelLayerAbstractApi.MoveCircles(CircleList.ElementAt(i), circlesSpeed);
            }
        }

        private void OnCreateCirclesCommand()
        {
            int circlesCount = int.Parse(_circleNumber);
            int circlesWidth = int.Parse(_circleWidth);
            int circlesHeight = int.Parse(_circleHeight);

            for (int i = 0; i < circlesCount; i++)
            {
                CircleList.Add(_modelLayerAbstractApi.CreateModelCircles(circlesWidth, circlesHeight));
            }

            _circleNumber = CircleList.Count.ToString();

            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
        }
    }
}