using System;
using System.Collections.Generic;
using Logic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Presentation.Model
{
    public class MLCircle : IMLCircle
    {
        
        private readonly List<string?> _colorList = new List<string?>()
        {
            "Black",
            "Yellow",
            "Red",
            "Blue",
            "Brown",
          //  "White",
            "Purple",
            "Orange",
            "Green",
            "Pink",
            "Gray"
        };
        
        public MLCircle(IBLCircle ball)
        {
            
            /*
             * lepsze wyjasnienie:
             * 1) w klasie ball mamy property dla X i Y polozenia
             * 2) klasa ball, poprzez klase Iball implementuje
             * interfejs INotifyPropertyChanged, wiec
             * za kazdym razem, kiedy powyzsze wlasciwosci ulegna zmianie
             * to "wystrzel w gore" i poinformuj "kazdego, kto chce wiedziec"
             * 3) i chyba ta klasa tutaj "subskrybuje" ten event (+=)
             * dodajac swojego delegata
             * 4) ten delegat ma za zadanie zaaktualizowac wartosci
             * obiektu tej klasy tutaj
             * 5) !!!! KLASA CIRCLE PRZERZUCA TE INFORMACJE DALEJ!
             * takim samym sposobem, jak opisany powyzej
             */
            
            ball.PropertyChanged += BallPropertyChanged;
            X_Center = ball.X;
            Y_Center = ball.Y;
            Radius = ball.R;
            this.color = _colorList.ElementAt(new Random().Next(0, 8));
        }
        
      

        private int x_center;
        private int y_center;
        private int radius;
             
        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                RaisePropertyChanged("Radius");
            }

        }
        public int X_Center
        {
            get => x_center;
            set
            {
                x_center = value;
                RaisePropertyChanged("X_Center");
            }
        }
        public int Y_Center
        {
            get => y_center;
            set
            {
                y_center = value;
                RaisePropertyChanged("Y_Center");
            }
        }
        public int CenterTransform { get => -1 * Radius; }
        public string color { get; }
        public int Diameter { get => 2 * Radius; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //za kazdym razem, kiedy obiekt IBall zmieni swoje polozenie
        //(a ono jest zmieniane w *update position*, to poinformuj
        //o tym ten tutaj obiekt circle, zeby on tez zmienil
        //swoje polozenie
        private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //czyli (jak nazwa wskazuje)
            //sender to jest to co wyslalo ten event
            //czyli klasa IBall (bo ball dziedziczy i IBall)
            IBLCircle b = (IBLCircle)sender;

            //a 'e' to musi byc "to co uleglo zmianie"
            //czyli jaka property zostala zmieniona
            //i w zaleznosci od tego, ktora property
            //ulegla zmianie, to zaaktualizuj jej "ekwiwalent tutaj"
            switch (e.PropertyName)
            {
                case "X":
                    X_Center = b.X;
                    break;
                case "Y":
                    Y_Center = b.Y;
                    break;
            }
        }
    }
}
