using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Weapon
	{
		public string Name { get; set; }
		public int StrengthRating {get; set;}

		public Weapon(string name, int rating)
		{
			Name = name;
			StrengthRating = rating;
		}
	}
}