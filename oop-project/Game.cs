using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Game
	{
		public List<string> MainMenu { get; set; }
		public string NameDisplay { get; set; }
		public string Name { get; set; }
		public Hero Hero { get; set; }
		public List<Monster> AllMonsters { get; set; }
		public Game()
		{
			MainMenu = new List<string> { "Show Stats", "Show Inventory", "Equip", "Fight", "Buy Health" };
			AllMonsters = new List<Monster>
			{
				new Monster("Goblin", 12, 7, 10),
				new Monster("Skeleton", 13, 5, 15),
				new Monster("Giant Rat", 11, 3, 8),
				new Monster("Wolf", 14, 6, 12),
				new Monster("Bear", 15, 9, 15),
				new Monster("Troll", 16, 12, 20),
				new Monster("Zombie", 13, 4, 22),
				new Monster("Ghost", 16, 12, 20),
				new Monster("Ogre", 18, 15, 25),
			};
		}
		public void Start()
		{
			Console.WriteLine("Welcome hero! Enter your name to begin.");
			CreateHero();
			Console.WriteLine(NameDisplay);
			PrintMenu();

			do
			{
				string input = Console.ReadLine().ToLower();

				switch (input)
				{
					case "show stats":
						ShowStats();
						break;
					case "show inventory":
						ShowInventory();
						break;
					case "fight":
						Fight();
						break;
					case "equip":
						Equip();
						break;
					case "buy health":
						BuyHealth();
						break;
					default:
						Console.WriteLine("Invalid entry.");
						break;
				}
			} while (Hero.Alive);
		}

		private void CreateHero()
		{
			Name = Console.ReadLine();
			NameDisplay = $"*********************************************************\nHero: {Name} - What would you like to do?";
			Hero newHero = new Hero(Name);
			Hero = newHero;
		}

		private void PrintMenu()
		{
			for (int i = 0; i < MainMenu.Count; i++)
				if (i == MainMenu.Count - 1)
					Console.WriteLine(MainMenu[i]);
				else
					Console.Write(MainMenu[i] + " - ");
			Console.WriteLine("*********************************************************");
		}

		private void ShowStats()
		{
			Hero.PrintStats();
			Console.WriteLine(NameDisplay);
			PrintMenu();
		}

		private void ShowInventory()
		{
			Hero.PrintInventory();
			Console.WriteLine(NameDisplay);
			PrintMenu();
		}

		public void BuyHealth()
		{
			Console.WriteLine("Use coins to purchase health?");
			bool transactionComplete = false;

			do
			{
				string input = Console.ReadLine().ToLower();

				switch (input)
				{
					case "yes":
						int amountOfHealth = CalcHealth();
						AddHealth(amountOfHealth);
						transactionComplete = true;
						break;
					case "no":
						transactionComplete = true;
						break;
					default:
						Console.WriteLine("Invalid entry.");
						break;
				}
			} while (!transactionComplete);

			Console.WriteLine(NameDisplay);
			PrintMenu();
		}

		public void AddHealth(int amountOfHealth)
		{
			Hero.CurrentHitPoints += amountOfHealth;
			Hero.Wallet -= amountOfHealth;

			Console.ForegroundColor = ConsoleColor.Green;
			if (amountOfHealth == -1)
				Console.WriteLine("You already have full health.");
			else if (amountOfHealth == 0)
				Console.WriteLine("You have no coins.");
			else
				Console.WriteLine($"You have restored {amountOfHealth} health.");
			Console.ResetColor();
		}

		public int CalcHealth()
		{
			int result = Hero.TotalHitPoints - Hero.CurrentHitPoints;
			if (result == 0)
				return -1;
			else if (result <= Hero.Wallet)
				return result;
			else return Hero.Wallet;
		}

		private void Fight()
		{
			Monster randomMonster = RandomizeMonster();
			Fight newFight = new Fight(Hero, randomMonster, this);
			newFight.StartFight();
			if (Hero.Alive)
			{
				Console.WriteLine(NameDisplay);
				PrintMenu();
			}
		}

		private void Equip()
		{
			Console.WriteLine("Equip armor or weapon?");

			bool completedEquip = false;

			do
			{
				string input = Console.ReadLine().ToLower();

				switch (input)
				{
					case "armor":
						Console.WriteLine("What piece of armor would you like to equip?");
						string armorInput = Console.ReadLine().ToLower();
						Hero.EquipArmor(armorInput);
						completedEquip = true;
						break;
					case "weapon":
						Console.WriteLine("What weapon would you like to equip?");
						string weaponInput = Console.ReadLine().ToLower();
						Hero.EquipWeapon(weaponInput);
						completedEquip = true;
						break;
					default:
						Console.WriteLine("Invalid entry.");
						break;
				}
			} while (!completedEquip);

			Console.WriteLine(NameDisplay);
			PrintMenu();
		}

		private Monster RandomizeMonster()
		{
			int max = AllMonsters.Count;
			Random r = new Random();
			int randomIndex = r.Next(max);

			return AllMonsters[randomIndex];
		}
	}
}