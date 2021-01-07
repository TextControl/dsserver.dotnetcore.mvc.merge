using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSServerMerge.Models {
	public class Report {
		public List<Customer> Customers { get; set; } = new List<Customer>();
	}

	public class Customer {
		public string Name { get; set; }
		public string Firstname { get; set; }
		public string Company { get; set; }
	}
}
