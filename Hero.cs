//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	class Hero {
		private int MaxHealth;
		private int currHealth;
		private int Level;
		private int Exp;
		private int NextLevelExp;
		private int Attack;
		private int Defense;
		private int Speed;
		private Room room;
		private Weapon weapon;
		private Bandage bandage;
///Konstruktor - tworzy nowego bohatera
		public Hero(){
			//MapNumber = map;
			MaxHealth = 100;
			currHealth = 100;
			Level = 1;
			Exp = 0;
			NextLevelExp = 500;
			Attack = 10;
			Defense = 12;
			Speed = 10;
			weapon = null;
			//this.X = X;
			//this.Y = Y;
		}

///metoda, która ulepsza statyski bohatera przy awansie
		void LevelUp(){
			MaxHealth = MaxHealth + 20;
			currHealth = MaxHealth;
			Exp = Exp - NextLevelExp;
			Attack += Level;
			Defense += (Level/2);
			Speed++;
			Level++;
		}
///metoda, która dodaje punkty doświadczenia
		public bool addExp(int newExp){
			Exp = Exp + newExp;
			if (Exp>=NextLevelExp){
				LevelUp();
				return true;
			}
			return false;
		}
// metody aktualizujące bądź pobierające informacje o polach
		public void move(Room room){
			this.room = room;
		}
		public void pickUpWeapon(Weapon weapon){
			this.weapon = weapon;
		}
		public void pickUpBandage(Bandage bandage){
			this.bandage = bandage;
		}

		public Room getRoom(){
			return room;
		}

		public int getLevel(){
			return Level;
		}

		public int getAttack(){
			return Attack;
		}
		public int getDefense(){
			return Defense;
		}
		public int getSpeed(){
			return Speed;
		}
		public int causeDamage(int damage){
			currHealth = currHealth - damage;
			return currHealth;
		}
		public int restoreHealth(int health){
			currHealth = currHealth + health;
			int overflow = currHealth - MaxHealth;
			if(overflow > 0)
			{
				currHealth = currHealth - overflow;
				return overflow;
			}
			else
				return 0;
		}

		public Weapon getWeapon(){
			return weapon;
		}

		public Bandage getBandage(){
			return bandage;
		}

//metoda ToString - wywołuwana przy wczytywaniu statystyk
		public override string ToString(){
			string napis =  "\nStatystyki\n\nŻycie " + currHealth.ToString() + "/" + MaxHealth.ToString() + 
			"\nPoziom " + Level.ToString() + "\nPunkty Doświadczenia " + Exp.ToString() + "/" + NextLevelExp.ToString() +
			"\nAtak " + Attack.ToString() + "\nObrona " + Defense.ToString() + "\nSzybkość " + Speed.ToString() + "\nPrzedmioty\n";
			
			if(weapon != null)
				napis = napis + weapon.ToString();


			if(bandage != null)
				napis = napis + bandage.ToString();

			return napis;

		}


	}
}

