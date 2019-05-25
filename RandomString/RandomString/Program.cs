using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomString
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Creating 10 passwords:");

			RandomString rs = new RandomString();
			for (int i = 0; i < 10; i++)
			{
				Console.WriteLine(rs.Generate());
			}


			Console.ReadLine();

		}
	}
}
