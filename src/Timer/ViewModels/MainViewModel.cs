using Timer.Commands;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            ResetCommand = new RelayCommand(_ => Reset());

            Values = new List<int> { 1, 5, 10, 15, 20, 25, 30 };
            SelectedValue = 15;
            IsRunning = false;
            AlwaysVisible = true;

            WindowTitle = $"Timer - {GetVerion()}";
        }

        public string WindowTitle { get; }

        private TimeSpan _currentTime;
        public TimeSpan CurrentTime
        {
            get => _currentTime;
            set
            {
                SetField(ref _currentTime, value);
                OnPropertyChanged(nameof(CanReset));
            }
        }

        private int _selectedValue;
        public int SelectedValue
        {
            get => _selectedValue;
            set
            {
                SetField(ref _selectedValue, value);
                CurrentTime = TimeSpan.FromMinutes(value);
            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetField(ref _isRunning, value);
        }

        private bool _alwaysVisible;
        public bool AlwaysVisible
        {
            get => _alwaysVisible;
            set => SetField(ref _alwaysVisible, value);
        }

        public bool CanReset => Math.Abs(CurrentTime.TotalMinutes - SelectedValue) > 0;

        public ICollection<int> Values { get; }

        public ICommand StartCommand { get; }

        public ICommand StopCommand { get; }

        public ICommand ResetCommand { get; }

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
            _dispatcherTimer.Start();
            IsRunning = true;
        }

        private void Stop()
        {
            _dispatcherTimer.Stop();
            IsRunning = false;
        }

        private void Reset()
        {
            CurrentTime = TimeSpan.FromMinutes(SelectedValue);
        }

        private string GetVerion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
