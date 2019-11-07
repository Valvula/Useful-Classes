using System;
using System.Threading;

namespace StopWatch
{
	public static class StopWatch
	{

		public static int minutes = 0;
		public static int seconds = 0;
		private static Timer _timer = new Timer(new TimerCallback(Tick));

		private static void Tick(object state)
		{
			if (seconds == 59)
			{
				seconds = 0;
				minutes++;
			}
			else
				seconds++;
			ShowOnConsole();
		}

		public static void Start()
		{
			_timer.Change(0, 1000);
		}

		public static void Stop()
		{
			_timer.Change(Timeout.Infinite, 1000);
		}

		public static void Restart()
		{
			Reset();
			Start();
		}

		public static void Reset()
		{
			minutes = 0;
			seconds = 0;
			ShowOnConsole();
		}

		public static void ShowOnConsole()
		{
			Console.Clear();
			Console.WriteLine($"{minutes.ToString("D2")}:{seconds.ToString("D2")}");
		}
	}
}
