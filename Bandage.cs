//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler{

	[Serializable]
	public class Bandage : Item{

		int healthRestore;
		///Konstruktor - tworzy nowy bandaż
		public Bandage(){
			healthRestore = rand.Next()%152;
			setType(2);
			if(healthRestore == 0)
				setTaken(true);
			else
				setTaken(false);
		}
		///Tworzy bandaż o ilości hp życia
		public Bandage(int hp){
			healthRestore = hp;
			setType(2);
			setTaken(true);
		} 		
		///zwraca liczbę hp przywracanych przez banadż
		public int healthPoints(){
			return healthRestore;
		}

		
		public override string ToString(){
			return "Bandaż\nUzdrawia: " + healthRestore.ToString() + "punktów życia"+ "\n";
		}

	}
}