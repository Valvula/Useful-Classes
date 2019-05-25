using System;
using System.Collections.Generic;
using System.Text;

namespace RandomString
{
	class RandomString
	{

		public bool HasLowerChars { get; set; }
		public bool HasUpperChars { get; set; }
		public bool HasNumbers { get; set; }
		public bool HasSymbols { get; set; }
		public int PasswordLength { get; set; }

		private List<int> asciiCodes = new List<int>(); //used to save ascii code of chars
		Random nextCharRandom = new Random(); //singelton

		/// <summary>
		/// Creates an object with all values set to their default value and password size set to 8.
		/// </summary>
		public RandomString()
		{
			HasLowerChars = true;
			HasUpperChars = true;
			HasNumbers = true;
			HasSymbols = true;
			PasswordLength = 8;
		}

		/// <summary>
		/// Creates an object with all values set to true.
		/// </summary>
		/// <param name="passwordLength"> The size of characters of the final string.</param>
		public RandomString(int passwordLength) : base()
		{
			PasswordLength = passwordLength;
		}

		/// <summary>
		/// Creates an object where the user can change all the parameters.
		/// </summary>
		/// <param name="passwordLength"> Size of the final string.</param>
		/// <param name="hasLowerChars"> true to generate string with lower characters.</param>
		/// <param name="hasUpperChars">true to generate string with upper characters.</param>
		/// <param name="hasNumbers">true to generate string with numbers.</param>
		/// <param name="hasSymbols">true to generate string with symbols.</param>
		public RandomString(int passwordLength, bool hasLowerChars, bool hasUpperChars, bool hasNumbers, bool hasSymbols)
		{
			PasswordLength = passwordLength;
			HasLowerChars = hasLowerChars;
			HasUpperChars = hasUpperChars;
			HasNumbers = hasNumbers;
			HasSymbols = hasSymbols;
		}

		/// <summary>
		/// Creates a new random string.
		/// </summary>
		/// <returns>Random string</returns>
		public string Generate()
		{
			return RandomStringCreation().ToString();
		}

		/// <summary>
		/// Creates a new random string.
		/// </summary>
		/// <param name="passwordLength">Size of the string.</param>
		/// <returns>Random string</returns>
		public string Generate(int passwordLength)
		{
			PasswordLength = passwordLength;
			return RandomStringCreation().ToString();
		}

		/// <summary>
		/// Creates a new random string.
		/// </summary>
		/// <param name="passwordLength"> Size of the string.</param>
		/// <param name="hasLowerChars"> Set to true for the string have random lower characters.</param>
		/// <param name="hasUpperChars"> Set to true for the string have random upper characters.</param>
		/// <param name="hasNumbers"> Set to true for the string have random numbers.</param>
		/// <param name="hasSymbols"> Set to true for the string have random symbols.</param>
		/// <returns>Random string</returns>
		public string Generate(int passwordLength, bool hasLowerChars, bool hasUpperChars, bool hasNumbers, bool hasSymbols)
		{
			PasswordLength = passwordLength;
			HasLowerChars = hasLowerChars;
			HasUpperChars = hasUpperChars;
			HasNumbers = hasNumbers;
			HasSymbols = hasSymbols;
			return RandomStringCreation().ToString();
		}

		private StringBuilder RandomStringCreation()
		{
			var typeChar = new List<string>(); //used to choose random option from the type of chars

			if (HasUpperChars)
				typeChar.Add("hasUpperChars");
			if (HasLowerChars)
				typeChar.Add("hasLowerChars");
			if (HasNumbers)
				typeChar.Add("hasNumbers");
			if (HasSymbols)
				typeChar.Add("hasSymbols");

			for (int i = 0; i < PasswordLength; i++)
			{
				var n = nextCharRandom.Next(0, typeChar.Count);
				switch (typeChar[n])
				{
					case "hasUpperChars":
						asciiCodes.Add(nextCharRandom.Next((int)'A', (int)'Z'));
						break;
					case "hasLowerChars":
						asciiCodes.Add(nextCharRandom.Next((int)'a', (int)'z'));
						break;
					case "hasNumbers":
						asciiCodes.Add(nextCharRandom.Next((int)'0', (int)'9'));
						break;
					case "hasSymbols":
						asciiCodes.Add(nextCharRandom.Next((int)'#', (int)'.'));
						break;
					default:
						Console.WriteLine("Something went wrong!");
						break;
				}
			}

			StringBuilder password = new StringBuilder();
			foreach (var asciiCode in asciiCodes)
				password.Append((char)asciiCode);

			asciiCodes.Clear();

			return password;
		}
	}
}
