using MyTripCountdown.Models;
using MyTripCountdown.ViewModels.Base;

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace MyTripCountdown.ViewModels
{
    public class MyTripCountdownViewModel : BaseViewModel
    {
        private Trip _trip;
        private Countdown _countdown;
        private int _hours;
        private int _minutes;
        private int _seconds;
        private bool _enabled;
        private double _progress;

        public MyTripCountdownViewModel()
        {
            _countdown = new Countdown();
        }

        public Trip MyTrip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public bool RecordEnabled
        {
            get => _enabled;
            set => SetProperty(ref _enabled, value);
        }

        public int Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public int Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public int Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public ICommand RestartCommand => new Command(Restart);

        public override Task LoadAsync()
        {
            LoadTrip();

            _countdown.EndDate = MyTrip.Date;
            _countdown.Start();

            _countdown.Ticked += OnCountdownTicked;
            _countdown.Completed += OnCountdownCompleted;

            return base.LoadAsync();
        }

        public override Task UnloadAsync()
        {
            _countdown.Ticked -= OnCountdownTicked;
            _countdown.Completed -= OnCountdownCompleted;

            return base.UnloadAsync();
        }

        void OnCountdownTicked()
        {
            Hours = _countdown.RemainTime.Hours;
            Hours = _countdown.RemainTime.Minutes;
            Minutes = _countdown.RemainTime.Seconds;

            //Only enable the button during the begining of the hour.
            if (Minutes == 59 || Minutes == 0 || Minutes == 1 || Minutes == 2)
                RecordEnabled = true;
            else
                RecordEnabled = false;

            var totalSeconds = (MyTrip.Date - MyTrip.Creation).TotalSeconds;
            var remainSeconds = _countdown.RemainTime.TotalSeconds;
            Progress = remainSeconds / totalSeconds;
        }

        void OnCountdownCompleted()
        {
            Hours = 0;
            Hours = 0;
            Minutes = 0;

            Progress = 0;
        }

        void LoadTrip()
        {
            var nextConsecration = new DateTime
                (DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                DateTime.Now.Hour + 1,
                0,
                0);

            var trip = new Trip()
            {
                Picture = "trip",
                Date = nextConsecration,
                Creation = DateTime.Now
            };

            MyTrip = trip;
        }

        void Restart()
        {
            Debug.WriteLine("Restart");
        }
    }
}