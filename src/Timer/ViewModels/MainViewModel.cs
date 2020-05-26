using Timer.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Threading;

namespace Timer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DispatcherTimer _dispatcherTimer;

        public MainViewModel()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += OnDispatcherTimerTick;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);

            StartCommand = new RelayCommand(_ => Start(), _ => !IsRunning);
            StopCommand = new RelayCommand(_ => Stop(), _ => IsRunning);

            Values = new List<int>
            {
                1, 5, 10, 15, 20, 25, 30
            };

            _selectedValue = 15;
            _isRunning = false;
            _alwaysVisible = true;
        }

        private TimeSpan _currentTime;
        public TimeSpan CurrentTime
        {
            get => _currentTime;
            set => SetField(ref _currentTime, value, nameof(CurrentTime));
        }

        private int _selectedValue;
        public int SelectedValue
        {
            get => _selectedValue;
            set => SetField(ref _selectedValue, value, nameof(SelectedValue));
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetField(ref _isRunning, value, nameof(IsRunning));
        }

        private bool _alwaysVisible;
        public bool AlwaysVisible
        {
            get => _alwaysVisible;
            set => SetField(ref _alwaysVisible, value, nameof(AlwaysVisible));
        }

        public ICollection<int> Values { get; set; }

        public ICommand StartCommand { get; set; }

        public ICommand StopCommand { get; set; }

        private void OnDispatcherTimerTick(object sender, EventArgs e)
        {
            if (CurrentTime.TotalSeconds > 0)
            {
                CurrentTime = CurrentTime.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                Stop();
            }
        }

        private void Start()
        {
            CurrentTime = TimeSpan.FromMinutes(SelectedValue);
            _dispatcherTimer.Start();
            IsRunning = true;
        }

        private void Stop()
        {
            _dispatcherTimer.Stop();
            IsRunning = false;
        }
    }
}
