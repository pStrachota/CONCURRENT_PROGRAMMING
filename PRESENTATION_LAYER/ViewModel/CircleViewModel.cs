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

        public ObservableCollection<ModelCircle> circleList { get; private set; }
        private string _circleNumber = "";
        private string _circleWidth = "";
        private string _circleHeight = "";
        private string _circleSpeed = "";

        public string CircleNumber
        {
            get { return _circleNumber; }
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


        private ModelLayerAbstractApi _modelLayerAbstractApi;

        public RelayCommand CreateCirclesCommand { get; private set; }
        public RelayCommand MoveCirclesManuallyCommand { get; private set; }
        public RelayCommand MoveCirclesAutomaticallyCommand { get; private set; }
        public RelayCommand DeleteCirclesCommand { get; private set; }
        public RelayCommand StopCirclesCommand { get; private set; }
        public RelayCommand ResetValuesCommand { get; private set; }

        public CircleViewModel()
        {
            _modelLayerAbstractApi = ModelLayerAbstractApi.CreateAPI();
            CreateCirclesCommand = new RelayCommand(OnCreateCirclesCommand, CanCreateCirclesCommand);
            MoveCirclesManuallyCommand = new RelayCommand(OnMoveCirclesManuallyCommand, CanMoveCirclesCommand);
            MoveCirclesAutomaticallyCommand =
                new RelayCommand(OnMoveCirclesAutomaticallyCommand, CanMoveCirclesCommand);
            DeleteCirclesCommand = new RelayCommand(OnDeleteCirclesCommand, CanDeleteCirclesCommand);
            StopCirclesCommand = new RelayCommand(OnStopCirclesCommand, CanStopCirclesCommand);
            ResetValuesCommand = new RelayCommand(OnResetValuesCommand, CanResetValuesCommand);
            _gameTimer.Tick += GameTimerEvent;
            circleList = new ObservableCollection<ModelCircle> { };
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
            return !_gameTimer.IsEnabled &&
                   (CircleNumber != "" || CircleWidth != "" || CircleHeight != "" || CircleSpeed != "");
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

        private void GameTimerEvent(object sender, EventArgs e)
        {
            int circlesCount = int.Parse(_circleNumber);
            double circlesSpeed = double.Parse(_circleSpeed, CultureInfo.InvariantCulture);

            _gameTimer.Interval = TimeSpan.FromSeconds(circlesSpeed);

            for (int i = 0; i < circlesCount; i++)
            {
                _modelLayerAbstractApi.moveCircles(circleList.ElementAt(i), circlesSpeed);
            }
        }

        private void OnMoveCirclesManuallyCommand()
        {
            int circlesCount = int.Parse(_circleNumber);
            double circlesSpeed = double.Parse(_circleSpeed, CultureInfo.InvariantCulture);

            for (int i = 0; i < circlesCount; i++)
            {
                _modelLayerAbstractApi.moveCircles(circleList.ElementAt(i), circlesSpeed);
            }
        }

        private void OnCreateCirclesCommand()
        {
            Random random = new();
            int circlesCount = int.Parse(_circleNumber);
            int circlesWidth = int.Parse(_circleWidth);
            int circlesHeight = int.Parse(_circleHeight);

            for (int i = 0; i < circlesCount; i++)
            {
                circleList.Add(_modelLayerAbstractApi.CreateModelCircles(circlesWidth, circlesHeight));
            }

            _circleNumber = circleList.Count.ToString();

            DeleteCirclesCommand.NotifyCanExecuteChanged();
            MoveCirclesManuallyCommand.NotifyCanExecuteChanged();
            MoveCirclesAutomaticallyCommand.NotifyCanExecuteChanged();
        }
    }
}