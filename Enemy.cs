//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {

	[Serializable]
	abstract class Enemy {

		protected int MaxHealth;
		protected bool isDead;
		protected int Attack;
		protected int Defense;
		protected int Speed;
		protected int expGiven;
		///tworzy nowego przeciwnika
		public Enemy(){
			isDead = false;
			Attack = 8;
			Defense = 8;
			Speed = 8;
			expGiven = 100;
		}



	
		public void kill(){
			isDead = true;
		}
		public void resurrect(){
			isDead = false;
		}

		public bool IsDead(){
			return isDead;
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
		public int getMaxHealth(){
			return MaxHealth;
		}

		public int getExp(){
			return expGiven;
		}

		public abstract int getAttackType();



	}
}

