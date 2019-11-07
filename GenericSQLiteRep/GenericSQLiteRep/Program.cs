using GenericSQLiteRep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSQLiteRep
{
	class Program
	{
		static void Main(string[] args)
		{
			//For example purposes
			repCreation();
			repInsert();
			repSelect();
			Console.ReadLine();
		}

		static void repCreation()
		{
			using (Repository<ModelExample> rep = new Repository<ModelExample>())
			{
				if (rep.IsTableCreated(typeof(ModelExample).Name))
					Console.WriteLine($"[Repository] {typeof(ModelExample).Name} exists!");
				else
					Console.WriteLine($"[Repository] {typeof(ModelExample).Name} doesn't exist!");
			}
		}

		static void repInsert()
		{
			using (Repository<ModelExample> rep = new Repository<ModelExample>())
			{
				ModelExample example = new ModelExample
				{
					Name = "Some Name",
					Age = 18,
					Address = "Some Address"
				};
				rep.Insert(example);
			}
		}

		static async void repSelect()
		{
			using (Repository<ModelExample> rep = new Repository<ModelExample>())
			{
				var _modelExample = await rep.Select();
				foreach (var data in _modelExample)
					Console.WriteLine($"Name: {data.Name}, Age: {data.Age}, Address: {data.Address}");
			}
		}
	}
}
