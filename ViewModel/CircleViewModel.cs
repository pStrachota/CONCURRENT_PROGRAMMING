using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;
using Presentation.Model;

namespace ViewModel
{
    public class CircleViewModel : ViewModelBase
    {
        public CircleViewModel()
        {
            MyModel = ModelLayerAbstractAPI.CreateAPI();
            Start = new RelayCommand(() => start(), CanMoveCirclesCommand);
            Stop = new RelayCommand(() => stop());
            Delete = new RelayCommand((() => delete()));
            Resume = new RelayCommand((() => resume()));

            _startButton = "Start";
        }

        private ModelLayerAbstractAPI MyModel { get; set; }

        private string _numberOfBalls = "";
        private string _circleRadiusMin = "";
        private string _circleRadiusMax = "";
        private string _circleSpeed = "";
        private int _height = 504;
        private int _width = 1445;
        private string _startButton;

        public bool CanMoveCirclesCommand()
        {
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return new Regex(pattern).IsMatch(_numberOfBalls) && new Regex(pattern).IsMatch(_circleRadiusMin) &&
                   new Regex(pattern).IsMatch(_circleSpeed) && new Regex(pattern).IsMatch(_circleRadiusMax) &&
                   int.Parse(_circleRadiusMin) < int.Parse(_circleRadiusMax) && int.Parse(_circleRadiusMin) < 240 &&
                   int.Parse(_circleRadiusMax) < 250;
        }

        public string CircleSpeed
        {
            get => _circleSpeed;
            set
            {
                _circleSpeed = value;
                Start.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string StartButton
        {
            get => _startButton;
            set
            {
                _startButton = value;
                RaisePropertyChanged("StartButton");
            }
        }

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged("Width");
            }
        }


        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged("Height");
            }
        }

        public ObservableCollection<IMLCircle> Circles
        {
            get => MyModel.Circles;
            set => MyModel.Circles = value;
        }

        //todo dodac sprawdzanie wartosci dla circleRadius i circleNumer
        //czyli tu bedzie znowu "mozliwosc wywolania komendy"
        public string CircleRadiusMin
        {
            get => _circleRadiusMin;
            set
            {
                _circleRadiusMin = value;
                Start.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string CircleRadiusMax
        {
            get => _circleRadiusMax;
            set
            {
                _circleRadiusMax = value;
                Start.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public string NumberOfBalls
        {
            get => _numberOfBalls;
            set
            {
                _numberOfBalls = value;
                Start.NotifyCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public RelayCommand Start { get; set; }
        public RelayCommand Stop { get; set; }
        public RelayCommand Delete { get; set; }
        public RelayCommand Resume { get; set; }

        public void resume()
        {
            MyModel.resumeCircles();
        }


        public void start()
        {
            MyModel.generateBallsRepresentative(Height, Width, int.Parse(_numberOfBalls), int.Parse(_circleRadiusMin), int.Parse(_circleRadiusMax),
                int.Parse(_circleSpeed));
            StartButton = "Restart";
        }

        public void stop()
        {
            MyModel.stopSimulation();
        }

        public void delete()
        {
            MyModel.Circles.Clear();
        }
    }
}