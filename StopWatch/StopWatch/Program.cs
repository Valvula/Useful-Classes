using System;
using System.Threading;

namespace StopWatch
{
	class Program
	{
		static void Main(string[] args)
		{
			StopWatch.Start();
			Thread.Sleep(1000 * 10);
			StopWatch.Stop();
			Thread.Sleep(1000 * 5);
			StopWatch.Reset();
			Console.Read();
		}
	}
}
