//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	class Room {
		
		private Bandage bandage;
		private Weapon weapon;
		private Enemy enemy;
		private Item item;
		private int x;
		private int y;
		/// konstruktor - tworzy pokój zawierający przedmioty podane jako argument
		public Room (Enemy enemy, Bandage bandage, Weapon weapon, int x, int y){
			this.bandage = bandage;
			this.item = bandage;
			if(weapon != null)
				this.item =weapon;
			this.weapon = weapon;
			this.enemy = enemy;
			this.x = x;
			this.y = y;
		}
		/// Sprawdza co znajduje się w danym pokoju
		public int getAction(){
			if(enemy != null)
				if(!enemy.IsDead())
					return 1;
			if(item != null)
				if(!item.isTaken())
					return 2;
			return 0;
		}
		///zwraca referencję na obiekty znajdujące się w tym pomieszczeniu
		public Enemy getEnemy(){
			return enemy;
		}

		public Item getItem(){
			return item;
		}

		public Weapon getWeapon(){
			return weapon;
		}
		public Bandage getBandage(){
			return bandage;
		}

		public int getX(){
			return x;
		}

		public int getY(){
			return y;
		}


		//metoda ToString - zwraca informację wypisywaną przy wejściu do pokoju.
		public override string ToString(){
			if(enemy != null)
				if(!enemy.IsDead())
					return "W pomieszczeniu znajduje się potwór:\n" + enemy.ToString();
			if(item != null)
				if(!item.isTaken())
					return "W pomieszczeniu znajduje się przedmiot:\n" + item.ToString();
			return "Znajdujesz się w pustym pomieszczeniu";
		}


	}
}

	