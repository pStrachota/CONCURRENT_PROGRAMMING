﻿using CSHARP_PW_PROJECT.Commands;
using CSHARP_PW_PROJECT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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

        private readonly CircleCommands _moveCirclesManuallyCommand;

        private readonly CircleCommands _deleteCirclesCommand;
        public ICommand CreateCirclesCommand => _createCirclesCommand;
        public ICommand MoveCirclesManuallyCommand => _moveCirclesManuallyCommand;

        public ICommand DeleteCirclesCommand => _deleteCirclesCommand;

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
            _moveCirclesManuallyCommand = new CircleCommands(OnMoveCirclesManuallyCommand, CanMoveCirclesCommand);
            _deleteCirclesCommand = new CircleCommands(OnDeleteCirclesCommand, CanDeleteCirclesCommand);
            circleList = new ObservableCollection<Circle> { };

        }
        private bool CanCreateCirclesCommand(object commandParameter)
        {
            string pattern = @"^[0-9]*[1-9][0-9]*$";
            return new Regex(pattern).IsMatch(circleNumber);
        }

        private bool CanMoveCirclesCommand(object commandParameter)
        {
            return circleList.Count > 0;
        }

        private bool CanDeleteCirclesCommand(object commandParameter)
        {
            return circleList.Count > 0;
        }

        private void OnDeleteCirclesCommand(object obj)
        {
            this.circleList.Clear();
            _deleteCirclesCommand.InvokeCanExecuteChanged();
            _moveCirclesManuallyCommand.InvokeCanExecuteChanged();
        }

        private void OnMoveCirclesManuallyCommand(object obj)
        {
            Random random = new();
            int circlesCount = int.Parse(circleNumber);

            for (int i = 0; i < circlesCount; i++)
            {
                int toMoveHorizontal = random.Next(40, 700);
                int toMoveVertical = random.Next(40, 450);

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
        private void OnCreateCirclesCommand(object obj)
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

                _moveCirclesManuallyCommand.InvokeCanExecuteChanged();
                _deleteCirclesCommand.InvokeCanExecuteChanged();
            }
        }
    }
}
