#region License

/*
Licensed to Blue Chilli Technology Pty Ltd and the contributors under the MIT License (the "License").
You may not use this file except in compliance with the License.
See the LICENSE file in the project root for more information.
*/

#endregion

using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChilliSource.Mobile.UI
{
	//TODO: inherit directly from Label
	//TODO: add dispose method
	public class TimerLabel : ContentView
	{
		private ExtendedLabel _label;
		private Timer _timer;
		private string _time;
		private int _counter;
		private const int precisionFactor = 10;
		private DateTime _pausedTime;
		private bool _isTicking;

		public TimerLabel()
		{
			_label = new ExtendedLabel();
			Content = _label;

			_label.SetBinding(ExtendedLabel.TextProperty, nameof(Time));
			_label.BindingContext = this;

			_timer = new Timer(100);
			_timer.Elapsed += OnTimeElapsed;

			ResetDisplay();

			TimerCommand = new Command((arg) =>
		   {
			   var command = (TimerCommandType)arg;
			   switch (command)
			   {
				   case TimerCommandType.Reset:
					   {
						   ResetTimer();
						   break;
					   }
				   case TimerCommandType.Start:
					   {
						   ResetTimer();
						   _timer.Start();
						   _isTicking = true;
						   break;
					   }
				   case TimerCommandType.Stop:
					   {
						   _timer.Stop();
						   _isTicking = false;
						   break;
					   }
				   case TimerCommandType.Pause:
					   {
						   _timer.Stop();
						   _isTicking = false;
						   break;
					   }
				   case TimerCommandType.Resume:
					   {
						   _timer.Start();
						   _isTicking = true;
						   break;
					   }
			   }
		   });

		}

		public string Time
		{
			get { return _time; }
			set
			{
				_time = value;
				OnPropertyChanged(nameof(Time));
			}
		}

		public static readonly BindableProperty CustomFontProperty =
			BindableProperty.Create(nameof(CustomFont), typeof(ExtendedFont), typeof(TimerLabel), null);


		public ExtendedFont CustomFont
		{
			get { return (ExtendedFont)GetValue(CustomFontProperty); }
			set { SetValue(CustomFontProperty, value); }
		}

		public static readonly BindableProperty StartValueProperty =
			BindableProperty.Create(nameof(StartValue), typeof(int), typeof(TimerLabel), 0);


		public int StartValue
		{
			get { return (int)GetValue(StartValueProperty); }
			set { SetValue(StartValueProperty, value); }
		}

		public static readonly BindableProperty StepProperty =
			BindableProperty.Create(nameof(Step), typeof(int), typeof(TimerLabel), 1);


		public int Step
		{
			get { return (int)GetValue(StepProperty); }
			set { SetValue(StepProperty, value); }
		}

		public static readonly BindableProperty TimerCommandProperty =
			BindableProperty.Create(nameof(TimerCommand), typeof(ICommand), typeof(TimerLabel), default(ICommand));

		public ICommand TimerCommand
		{
			get { return (ICommand)GetValue(TimerCommandProperty); }
			set { SetValue(TimerCommandProperty, value); }
		}

		public static readonly BindableProperty TimerFinishedCallbackCommandProperty =
			BindableProperty.Create(nameof(TimerFinishedCallbackCommand), typeof(ICommand), typeof(TimerLabel), default(ICommand));

		public ICommand TimerFinishedCallbackCommand
		{
			get { return (ICommand)GetValue(TimerFinishedCallbackCommandProperty); }
			set { SetValue(TimerFinishedCallbackCommandProperty, value); }
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName.Equals(nameof(CustomFont)))
			{
				_label.SetBinding(ExtendedLabel.CustomFontProperty, "CustomFont");
			}
			else if (propertyName.Equals(nameof(Step)))
			{
				ResetDisplay();
			}
		}

		#region Public Methods

		public void HandleOnAppSleep()
		{
			if (_isTicking)
			{
				_timer.Stop();
				_pausedTime = DateTime.Now;
			}
		}

		public void HandleOnAppResume()
		{
			if (_isTicking)
			{
				var timeSpan = DateTime.Now - _pausedTime;
				var seconds = (int)Math.Round(timeSpan.TotalSeconds);
				_counter += Step * precisionFactor * seconds;
				if (_counter > 0)
				{
					_timer.Start();
				}
				else
				{
					_counter = 0;
					Time = "00:00";
					TimerFinishedCallbackCommand.Execute(null);
				}
			}
		}

		#endregion


		private void ResetTimer()
		{
			_timer.Stop();
			ResetDisplay();
			_counter = StartValue * precisionFactor;
		}

		private void ResetDisplay()
		{
			//TODO: use Humanizer
			var minutes = StartValue / 60;
			var seconds = StartValue % 60;
			Time = (minutes < 10 ? "0" + minutes : "" + minutes) + ":" + (seconds < 10 ? "0" + seconds : "" + seconds);
		}

		private void OnTimeElapsed(Object source, ElapsedEventArgs e)
		{
			_counter += Step;

			//TODO: use Humanizer
			var minutes = (int)(_counter / (double)precisionFactor) / 60;
			var seconds = (int)(_counter / (double)precisionFactor) % 60;
			Time = (minutes < 10 ? "0" + minutes : "" + minutes) + ":" + (seconds < 10 ? "0" + seconds : "" + seconds);

			if (_counter == 0)
			{
				_timer.Stop();
				TimerFinishedCallbackCommand.Execute(null);
			}
		}


	}
}

