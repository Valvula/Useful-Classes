using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericSQLiteRep.Models
{
	class ModelExample : BaseModel
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Address { get; set; }
	}
}
