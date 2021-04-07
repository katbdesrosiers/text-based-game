using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Monster
	{
		public string Name { get; set; }
		public int Strength { get; set; }
		public int Defense { get; set; }
		public int TotalHitPoints { get; set; }
		public int CurrentHitPoints { get; set; }

		public Monster(string name, int strength, int defense, int totalHitPoints)
		{
			Name = name;
			Strength = strength;
			Defense = defense;
			TotalHitPoints = totalHitPoints;
			CurrentHitPoints = totalHitPoints;
		}
	}
}