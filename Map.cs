//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using DungeonCrawler;

namespace DungeonCrawler {



	class Map {

		Room[,] World;
		int startX, startY, endX, endY;
		/// konstruktor - tworzy Mapę z danej tablicy charów
		public Map(char[,] ker){

			World = new Room[ker.GetLength(0), ker.GetLength(1)];
		
			for(int i = 0; i<ker.GetLength(0); ++i)
				for(int j = 0; j<ker.GetLength(1); ++j)
				{
					Bandage bandage = null;
					Weapon weapon = null;
					Enemy enemy = null;
					switch (ker[i,j]) {
						case 'W':	
							weapon = new Weapon();
							World[i,j] = new Room(null,null, weapon, i,j);
							break;
						case 'B':
							bandage = new Bandage();
							World[i,j] = new Room(null,bandage,null,i,j);
							break;
						case '1':
							enemy = new Goblin();
							World[i,j] = new Room(enemy,null, null,i,j);
							break;
						case '2':
							enemy = new Orc();
							World[i,j] = new Room(enemy,null, null,i,j);
							break;
						case '3':
							enemy = new OrcMage();
							bandage = new Bandage();
							World[i,j] = new Room(enemy, bandage,null,i,j);
							break;
						case '*':
							World[i,j] = new Room(null,null, null,i,j);
							break;
						case 'S':
							startX = i;
							startY = j;
							World[i,j] = new Room(null,null, null,i,j);
							break;
						case 'E':
							endX = i;
							endY = j;
							World[i,j] = new Room(null,null, null,i,j);
							break;
						default:
							break;
					}
				}

		}
///metody zwracające informację z pól
		public int getEndX(){
			return endX;
		}
		public int getEndY(){
			return endY;
		}
		public int getStartX(){
			return startX;
		}
		public int getStartY(){
			return startY;
		}

		public Room getStart(){
			return World[startX,startY];
		}

/// metody sprawdzające, czy mozna poruszać się danym kiernku z pola
		public bool isS(Room room){
			return World[room.getX()+1, room.getY()] != null;
		}
		public bool isN(Room room){
			if(room.getX() == 0)
				return false;
			return World[room.getX()-1, room.getY()] != null;
		}
		public bool isE(Room room){
			return World[room.getX(), room.getY()+1] != null;
		}
		public bool isW(Room room){
			if (room.getY() == 0)
				return false;
			return World[room.getX(), room.getY()-1] != null;
		}
/// metoda zwracająca pokój znajdujący się w wybranym kierunku
		public Room move(string where, ref Room room){
			switch(where){
				case "N":
					room = World[room.getX()-1, room.getY()];
					break;
				case "S":
					room = World[room.getX()+1, room.getY()];
					break;
				case "E":
					room = World[room.getX(), room.getY()+1];
					break;
				case "W":
					room = World[room.getX(), room.getY()-1];
					break;
				default:
					Console.WriteLine("Podaj prawidłowy kierunek");
					break;
			}

			return room;
		}
/// sprawdza, czy dany pokój jest końcowym pokojem w grze.
		public bool isEnd(Room room){
			return room.getX()==endX && room.getY()==endY;
		}



	}
}

