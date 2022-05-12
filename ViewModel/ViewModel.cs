using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;
using Model;

namespace ViewModel
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            MyModel = ModelAbstractApi.CreateAPI();
            StartCommand = new RelayCommand(() => Start(), CanMoveCirclesCommand);
            StopCommand = new RelayCommand(() => Stop());
            DeleteCommand = new RelayCommand((() => Delete()), CanDeleteCommand);
        }

        private ModelAbstractApi MyModel { get; set; }

        private string _numberOfBalls = "";
        private string _circleRadiusMin = "";
        private string _circleRadiusMax = "";
        private string _circleSpeed = "";

        public bool CanMoveCirclesCommand()
        {
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return new Regex(pattern).IsMatch(_numberOfBalls) && new Regex(pattern).IsMatch(_circleRadiusMin) &&
                   new Regex(pattern).IsMatch(_circleSpeed) && new Regex(pattern).IsMatch(_circleRadiusMax) &&
                   int.Parse(_circleRadiusMin) < int.Parse(_circleRadiusMax) && int.Parse(_circleRadiusMin) < 240 &&
                   int.Parse(_circleRadiusMax) < 250;
        }

        public bool CanDeleteCommand()
        {
            return MyModel.Circles.Count > 0;
        }

        public string CircleSpeed
        {
            get => _circleSpeed;
            set
            {
                _circleSpeed = value;
                StartCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
 
        public ObservableCollection<IMLCircle> Circles
        {
            get => MyModel.Circles;
            set => MyModel.Circles = value;
        }

        public string CircleRadiusMin
        {
            get => _circleRadiusMin;
            set
            {
                _circleRadiusMin = value;
                StartCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string CircleRadiusMax
        {
            get => _circleRadiusMax;
            set
            {
                _circleRadiusMax = value;
                StartCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        public string NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                StartCommand.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }
        public RelayCommand StartCommand { get; set; }
        public RelayCommand StopCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public void Start()
        {
            MyModel.CreateCirclesForPresentation(int.Parse(_numberOfBalls), int.Parse(_circleRadiusMin), int.Parse(_circleRadiusMax),
                int.Parse(_circleSpeed));
            DeleteCommand.NotifyCanExecuteChanged();
        }

        public void Stop()
        {
            MyModel.StopCirclesMove();
        }

        public void Delete()
        {
            MyModel.Circles.Clear();
        }
    }
}