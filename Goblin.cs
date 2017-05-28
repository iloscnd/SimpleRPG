//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	class Goblin : Enemy {

		///nadpisany konstruktor - inne statystyki
		public Goblin() : base(){
			MaxHealth = 40;
			Speed = 12;
			Defense = 5;
		}


		public override int getAttackType(){
			return 0;
		}

		public override string ToString(){
			return "Goblińskie Biegajało";
		}

	}
}
