using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Hero
	{
		public string Name { get; set; }
		public int Strength { get; set; }
		public int Defense { get; set; }
		public int TotalHitPoints { get; set; }
		public int CurrentHitPoints { get; set; }
		public int DamageDealt { get; set; }
		public int MonstersKilled { get; set; }
		public int Wallet { get; set; }
		public bool Alive { get; set; }
		public Weapon EquippedWeapon { get; set; }
		public Armor EquippedArmor { get; set; }
		public List<Weapon> WeaponBag { get; set; }
		public List<Armor> ArmorBag { get; set; }

		public Hero(string name)
		{
			Name = name;
			Strength = 10;
			Defense = 10;
			TotalHitPoints = 50;
			CurrentHitPoints = 50;
			WeaponBag = new List<Weapon>();
			ArmorBag = new List<Armor>();
			Alive = true;
			EquippedArmor = null;
			EquippedWeapon = null;
			MonstersKilled = 0;
			DamageDealt = 0;
			Wallet = 0;
		}

		public void PrintStats()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Strength: {Strength} - Defense: {Defense} - Hitpoints: {CurrentHitPoints}/{TotalHitPoints} - Wallet: {Wallet} coin(s)");
			Console.ResetColor();
		}

		public void PrintInventory()
		{
			Console.ForegroundColor = ConsoleColor.Green;

			if (WeaponBag.Count == 0)
				Console.WriteLine("You have no weapons.");
			else
			{
				Console.WriteLine("Weapons:");
				foreach (var weapon in WeaponBag)
					Console.WriteLine(weapon.Name);
			}

			if (ArmorBag.Count == 0)
				Console.WriteLine("You have no armor.");
			else
			{
				Console.WriteLine("Armor:");
				foreach (var armor in ArmorBag)
					Console.WriteLine(armor.Name);
			}

			Console.ResetColor();
		}

		public void EquipArmor(string armorInput)
		{
			if (EquippedArmor != null)
				Defense -= EquippedArmor.DefenseRating;

			foreach (var armor in ArmorBag)
				if (armor.Name == armorInput)
				{
					EquippedArmor = armor;
					Defense += armor.DefenseRating;

					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"{armor.Name} equipped.");
					Console.ResetColor();

					return;
				}

			Console.WriteLine("You don't have that.");
		}

		public void EquipWeapon(string weaponInput)
		{
			if (EquippedWeapon != null)
				Strength -= EquippedWeapon.StrengthRating;

			foreach (var weapon in WeaponBag)
				if (weapon.Name == weaponInput)
				{
					EquippedWeapon = weapon;
					Strength += weapon.StrengthRating;

					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"{weapon.Name} equipped.");
					Console.ResetColor();

					return;
				}

			Console.WriteLine("You don't have that.");
		}
	}
}