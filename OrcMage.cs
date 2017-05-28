//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	class OrcMage : Enemy {


		public static Random rand = new Random();
		///nadpisany konstruktor - inne statystyki
		public OrcMage() : base(){
			MaxHealth = 100;
			Attack = 7;
			expGiven = 250;
		}


		public override int getAttackType(){
			return rand.Next()%5;
		}

		public override string ToString(){
			return "Ośfiecony ork";
		}


	}
}
