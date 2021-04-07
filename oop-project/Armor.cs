using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Armor
	{
		public string Name { get; set; }
		public int DefenseRating { get; set; }

		public Armor(string name, int rating)
		{
			Name = name;
			DefenseRating = rating;
		}
	}
}