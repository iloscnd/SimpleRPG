//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	class Orc : Enemy {

		///nadpisany konstruktor - inne statystyki
		public Orc() : base() {
			MaxHealth = 60;
			Defense = 9;
			Speed = 6;
		}

		public override int getAttackType(){
			return 1;
		}

		public override string ToString(){
			return "Orkowy Śiepacz";
		}


	}
}
