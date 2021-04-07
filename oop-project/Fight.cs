using System;
using System.Collections.Generic;
using System.Text;

namespace oop_project
{
	class Fight
	{
		public Hero Hero { get; set; }
		public Monster Monster { get; set; }
		public string HeroDisplay { get; set; }
		public bool IsHeroTurn { get; set; }
		public bool HeroDefended { get; set; }
		public bool Run { get; set; }
		public List<Armor> AllArmor { get; set; }
		public List<Weapon> AllWeapons { get; set; }

		public Fight(Hero hero, Monster monster, Game game)
		{
			Monster = monster;
			Hero = hero;
			IsHeroTurn = true;
			HeroDefended = false;
			Run = false;
			Monster.CurrentHitPoints = Monster.TotalHitPoints;
			HeroDisplay = $"********************************************\nYour Health: {Hero.CurrentHitPoints}/{Hero.TotalHitPoints} {Monster.Name} Health: {Monster.CurrentHitPoints}/{Monster.TotalHitPoints} \nAttack - Defend - Run\n********************************************";
			AllArmor = new List<Armor>
			{
				new Armor("leather", 1),
				new Armor("studded leather", 1),
				new Armor("steel", 5),
				new Armor("dragonhide", 7),
			};
			AllWeapons = new List<Weapon>
			{
				new Weapon("dagger", 1),
				new Weapon("sword", 3),
				new Weapon("battleaxe", 5),
				new Weapon("longsword", 7),
			};
		}

		public void StartFight()
		{
			Console.WriteLine($"You have found a {Monster.Name}! It's your turn.");
			HeroTurn();
		}

		public void HeroTurn()
		{
			Console.WriteLine(HeroDisplay);

			if (HeroDefended)
			{
				Hero.Defense -= 5;
				HeroDefended = false;
			}

			do
			{
				string input = Console.ReadLine().ToLower();

				switch (input)
				{
					case "attack":
						int damage = Hero.Strength - Monster.Defense > 0 ? Hero.Strength - Monster.Defense : 0;
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine($"You hit the {Monster.Name} for {damage}!");
						Monster.CurrentHitPoints -= damage;
						Console.ResetColor();
						Hero.DamageDealt += damage;
						IsHeroTurn = false;
						break;
					case "defend":
						Hero.Defense += 5;
						IsHeroTurn = false;
						HeroDefended = true;
						break;
					case "run":
						IsHeroTurn = false;
						AttemptRun();
						break;
					default:
						Console.WriteLine("Invalid entry.");
						break;
				}
			} while (IsHeroTurn);

			if (Run) return;
			else if (Hero.CurrentHitPoints <= 0 && Monster.CurrentHitPoints <= 0 || Hero.CurrentHitPoints <= 0)
				Lose();
			else if (Hero.CurrentHitPoints > 0 && Monster.CurrentHitPoints > 0)
			{
				HeroDisplay = $"********************************************\nYour Health: {Hero.CurrentHitPoints}/{Hero.TotalHitPoints} {Monster.Name} Health: {Monster.CurrentHitPoints}/{Monster.TotalHitPoints} \nThe {Monster.Name} is attacking!\n********************************************";
				MonsterTurn();
			}
			else if (Monster.CurrentHitPoints <= 0)
				Win();
		}

		public void MonsterTurn()
		{
			Console.WriteLine(HeroDisplay);

			int damage = Monster.Strength - Hero.Defense > 0 ? Monster.Strength - Hero.Defense : 0;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"The {Monster.Name} hit you for {damage}!");
			Hero.CurrentHitPoints -= damage;
			Console.ResetColor();

			if (Hero.CurrentHitPoints > 0 && Monster.CurrentHitPoints > 0)
			{
				HeroDisplay = $"********************************************\nYour Health: {Hero.CurrentHitPoints}/{Hero.TotalHitPoints} {Monster.Name} Health: {Monster.CurrentHitPoints}/{Monster.TotalHitPoints} \nAttack - Defend - Run\n********************************************";
				HeroTurn();
			}
			else if (Hero.CurrentHitPoints <= 0)
				Lose();
			else if (Monster.CurrentHitPoints <= 0)
				Win();
		}

		public void AttemptRun()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Random r = new Random();
			double attemptRun = r.NextDouble();
			if (attemptRun > 0.5)
			{

				Console.WriteLine("You escaped!");
				Run = true;
				Console.ResetColor();

			}
			else
			{
				Console.WriteLine("You couldn't get away!");
				Console.ResetColor();
				return;
			}
		}

		public void Win()
		{
			Hero.MonstersKilled++;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"You defeated the {Monster.Name}!");
			RandomizeLoot();
			Hero.Wallet += RandomizeCoins();
			Console.ResetColor();
		}

		public void Lose()
		{
			Hero.Alive = false;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("--- Game Over ---");
			Console.WriteLine($"You killed {Hero.MonstersKilled} monsters and dealt {Hero.DamageDealt} damage!");
			Console.ResetColor();
		}

		public int RandomizeCoins()
		{
			Random r = new Random();
			int randomCoins = r.Next(10) + 1;
			Console.WriteLine($"You found {randomCoins} coin(s)!");
			return randomCoins;
		}

		public void RandomizeLoot()
		{
			Random r1 = new Random();
			double checkForLoot = r1.NextDouble();
			if (checkForLoot > 0.5)
			{
				Random r2 = new Random();
				double armorOrWeapon = r2.NextDouble();
				if (armorOrWeapon > 0.5)
				{
					int max = AllArmor.Count;
					Random r3 = new Random();
					int randomArmor = r3.Next(max);
					Hero.ArmorBag.Add(AllArmor[randomArmor]);
					Console.WriteLine($"You have recived {AllArmor[randomArmor].Name} armor. It has been added to your inventory.");
				}
				else
				{
					int max = AllWeapons.Count;
					Random r3 = new Random();
					int randomWeapon = r3.Next(max);
					Hero.WeaponBag.Add(AllWeapons[randomWeapon]);
					Console.WriteLine($"You have recived a {AllWeapons[randomWeapon].Name}. It has been added to your inventory.");
				}
			}
		}
	}
}