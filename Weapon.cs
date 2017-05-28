//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler{

	[Serializable]
	public class Weapon : Item{
	
		double AttackModifier;
		double Accuracy;
		///Tworzy nowy miecz
		public Weapon(){
			AttackModifier = (rand.Next()%60 + 40.0)/100.0;
			Accuracy = (rand.Next()%40 + 60)/100.0;
			setType(1);
			setTaken(false);
		}
		///Zwraca atak i celność broni
		public double getAttackModifier(){
			return AttackModifier;
		}

		public double getAccuracy(){
			return Accuracy;
		}

		public override string ToString(){
			return "Miecz\nAtak: " + (AttackModifier*100).ToString() + "\nCelność: " + (Accuracy*100).ToString() + "\n";
		}


	}
}