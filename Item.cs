//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	public abstract class Item {

		private int type;
		private bool taken;
		public static Random rand = new Random();
		

		///Metody pobierające wartości 
		public int getType(){
			return type;
		}
		public void setType(int type){
			this.type = type;
		}

		public bool isTaken(){
			return taken;
		}
		///Podnosi, bądź upuszcza przedmiot
		public void setTaken(bool taken){
			this.taken = taken;
		}

	}
}

