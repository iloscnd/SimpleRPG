//Adam Gańczorz
//Projekt z Programowania Obiektowego
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DungeonCrawler;

namespace DungeonCrawler {


	public class Game {

		private static Hero hero;
		private static Map world;
		private static string MapNumber;
		private static Room currentRoom;
		private static Enemy currentEnemy;
		private static Item currentItem;
		private static Random rand = new Random();
		private static string[] Attacks = { " atakuje pięścią ", " atakuje mieczem ", " używa mocnego uderzenia ", " używa kuli ognia ", " czeka "};


		public static void Main(){
			Console.ForegroundColor = ConsoleColor.White;
			string choice;
			bool stillPlaying = true;

			while (stillPlaying){
				Console.Clear();
				Console.WriteLine("\n******************************** ");
				Console.WriteLine("    Game Title");
				Console.WriteLine(" ******************************** ");
				Console.WriteLine("Menu");
				Console.WriteLine("\n Wpisz numer aby wybrać komendę");
				Console.WriteLine();
				Console.WriteLine("1. Nowa gra");
				Console.WriteLine("2. Wczytaj grę");
				Console.WriteLine("3. Wyjdź");
				choice = Console.ReadLine();

				if(choice == "3")
					stillPlaying = false;
				else
					if( choice == "1"){
						
						

						bool readen = false;
						while(!readen)
						{
							Console.Clear();
							Console.WriteLine("Podaj Number Mapy (1-5) lub wpisz q żeby zakończyć");
							choice = CappedRead();

							if(choice == "q")
								break;
				        	try {  

				        		readen = readMap(choice);
				        		MapNumber = choice;
					   			hero = new Hero();
								currentRoom = world.getStart();
								hero.move(currentRoom);
								Console.Clear();
								Console.WriteLine("Wpisz h lub help w dowolnym momencie, aby uzyskać spis wszystkich komend\n");
								Console.WriteLine("Budzisz się w pomieszczeniu, którego nie poznajesz.\n" +
												  "Nie pamiętasz jak się tu znalazałeś, ani co robiłeś wczoraj.\n" +
												  "Czujesz, że nie jesteś tu bezpieczny i musisz znaleźć wyjście.");
								gameLoop();
				       		}
				        	catch
				   			{
				   				Console.WriteLine("\nCoś poszło nie tak");
				   				Console.Read();
				   			}
				        }   
					}
					else
						if( choice == "2")
						{
							bool readen = false;
							while(!readen)
							{
								Console.Clear();
								Console.WriteLine("Podaj Number Mapy (1-5) lub wpisz q żeby zakończyć");
								choice = CappedRead();

								if(choice == "q")
									break;
					        	try {  
					        		readen = readMap(choice);
				        			MapNumber = choice;
						   			Deserialize();
									currentRoom =hero.getRoom();
									gameLoop();
					       		}
					        	catch
					   			{
					   				Console.WriteLine("\nCoś poszło nie tak");
					   				Console.Read();
					   			}
					        }

						}
						else
					Console.WriteLine("\n Nierozpoznana komenda: {0}", choice);
			}
		}
/// Główna pętla gry

		static void gameLoop(){
			bool dead = false;
			while(!dead){

				if(world.isEnd(currentRoom)){
					end();
					return;
				}
				Console.WriteLine(currentRoom);
				switch(currentRoom.getAction()){
					case 1:
						currentEnemy = currentRoom.getEnemy();
						if(fight()){
							Console.WriteLine("Zginąłeś\nKoniec Gry");
							Console.Read();
							return;
						}
						break;
					case 2:
						currentItem = currentRoom.getItem();
						if(pickUp())
							return;
						break;
					default:
						if(move())
							return;
						break;
				}
			}
		}
/// Instrukcje, które są wykonywane podczas walki
		static bool fight(){
			currentEnemy = currentRoom.getEnemy();
			int enemyHP = currentEnemy.getMaxHealth();
			while( enemyHP > 0){

				
				int heroAttackType=4;
				int enemyAttackType = currentEnemy.getAttackType();
				bool done = false;
			
				while(!done){
					Console.WriteLine("Co robisz?");
					Console.WriteLine("1. Atak Pięścią");
					Console.WriteLine("2. Atak Mieczem");
					Console.WriteLine("3. Mocne Uderzenie");
					Console.WriteLine("4. Ulecz");
					Console.WriteLine("5. Uciekaj");
					string inp = CappedRead();
					done = true;
					switch(inp){
						case "1":
							heroAttackType=0;
							break;
						case "2":
							if(hero.getWeapon()==null){
								Console.WriteLine("\nNie masz broni");
								done = false;
							}
							heroAttackType = 1;
							break;
						case "3":
							if(hero.getLevel() < 4){
								Console.WriteLine("\nUmiejętność dostępna od 4 poziomu");
								done = false;
							}
							heroAttackType = 2;
							break;
						case "4":
							if(hero.getBandage()==null){
								Console.WriteLine("\nNie masz bandaży");
								done = false;
							}
							else{
								heroAttackType = 4;
								int rest = hero.restoreHealth(hero.getBandage().healthPoints());
								Console.ForegroundColor = ConsoleColor.Green;
								Console.WriteLine("Bohater używa bandaży. Ma teraz " + hero.causeDamage(0) + "punktów życia");
								Console.ForegroundColor = ConsoleColor.White;
								if (rest > 0)
									hero.pickUpBandage(new Bandage(rest));
								else
									hero.pickUpBandage(null);
							}
							break;
						case "5":
							if(rand.Next()%4 + currentEnemy.getSpeed() < hero.getSpeed()){
								Console.WriteLine("Udało Ci się uciec");
								return move();
							}
							else
								Console.WriteLine("Nie udało Ci się uciec");
							break;
						case "q":
							return true;
						default:
							done = false;
							break;
					}

				}
				
				int enemyDamage = calculateDamage(currentEnemy.getAttack(), enemyAttackType, hero.getDefense(), null);
				int heroDamage = calculateDamage(hero.getAttack(), heroAttackType, currentEnemy.getDefense(), hero.getWeapon());

				if( rand.Next()%3 + currentEnemy.getSpeed() > hero.getSpeed()){
					//przeciwnik gra pierwszy
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(currentEnemy + Attacks[enemyAttackType] + " za " + enemyDamage);
					Console.ForegroundColor = ConsoleColor.White;
					if(hero.causeDamage(enemyDamage) <= 0)
						return true;
					Console.WriteLine("Bohater" + Attacks[heroAttackType] + " za " + heroDamage);
					enemyHP -= heroDamage;

				}
				else{
					Console.WriteLine("Bohater" + Attacks[heroAttackType] + " za " + heroDamage);
					enemyHP -= heroDamage;
					if(enemyHP> 0){
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(currentEnemy + Attacks[enemyAttackType] + " za " + enemyDamage);
						Console.ForegroundColor = ConsoleColor.White;
						if(hero.causeDamage(enemyDamage) <= 0){
							return true;
						}
					}
				}


			}
			Console.WriteLine("Pokonałeś " + currentEnemy + " i dostałeś " + currentEnemy.getExp() + " punktów doświadczenia");
			if(hero.addExp(currentEnemy.getExp()))
				Console.WriteLine("NOWY POZIOM");
			currentEnemy.kill();
			return false;
		}


///Instrukcje,które są wykoywane, jeżeli w pomieszczeniu znajuduje się przedmiot
		static bool pickUp(){
			currentItem = currentRoom.getItem();

			bool done = false;
			while(!done){

				if(currentItem.getType()==1)
					if(hero.getWeapon() == null)
						Console.WriteLine("Obecnie nie posiadasz broni");
					else
						Console.WriteLine("Twoja obecna broń to " + hero.getWeapon());
				else
					if(hero.getBandage() == null)
						Console.WriteLine("Obecnie nie posiadasz bandaży");
					else
						Console.WriteLine("Twoje obecne bandaże to " + hero.getBandage());
				Console.WriteLine("Co robisz?\n1.Podnieś przedmiot(zastąpi poprzedni)\n2.Zostaw");
				string choice = CappedRead();			

				done = true;
				switch(choice){
					case "1":
						if(currentItem.getType()==1){
							hero.pickUpWeapon(currentRoom.getWeapon());
							currentItem.setTaken(true);
						}
						else{
							hero.pickUpBandage(currentRoom.getBandage());
							currentItem.setTaken(true);
						}
						break;
					case "2":
						break;
					case "q":
						return true;
					default:
						done = false;
						break;
				}


			}

			return move();
		}
///Instrukcje, które są wykonywane w pustym pomiesczeniu
		static bool move(){
			
			Console.WriteLine("\nDostępne są następujące kierunki\n(wpisz literę aby podążyć w tym kierunku)");
			if(world.isN(currentRoom))
				Console.WriteLine("N");
			if(world.isS(currentRoom))
				Console.WriteLine("S");
			if(world.isW(currentRoom))
				Console.WriteLine("W");
			if(world.isE(currentRoom))
				Console.WriteLine("E");	
			
			string choice = CappedRead();
			if(choice == "q")
				return true;
			if(choice == "h")
				return false;
			switch(choice){
				case "N":
					if(world.isN(currentRoom)){
						hero.move(world.move("N",ref currentRoom));
					}
					else
						Console.WriteLine("Podaj prawidłowy kierunek");
					break;
				case "S":
					if(world.isS(currentRoom)){
						hero.move(world.move("S",ref currentRoom));
					}
					else
						Console.WriteLine("Podaj prawidłowy kierunek");
					break;
				case "E":
					if(world.isE(currentRoom)){
						hero.move(world.move("E",ref currentRoom));
					}
					else
						Console.WriteLine("Podaj prawidłowy kierunek");
					break;
				case "W":
					if(world.isW(currentRoom)){
						hero.move(world.move("W",ref currentRoom));
					}
					else
						Console.WriteLine("Podaj prawidłowy kierunek");
					break;
				default:
					Console.WriteLine("Podaj prawidłowy kierunek");
					break;
			}

			return false;
		}


///Metoda wypisujące komendy pomocnicze
		public static void WriteHelp(){
			Console.WriteLine();
			Console.WriteLine("Komendy dostępne w każdym momencie:");
			Console.WriteLine("h lub help    - wypisz ten plik pomocy");
			Console.WriteLine("s lub stats   - pokaż statyski bohatera");
			Console.WriteLine("q lub quit    - zakończ grę i wróc do menu");
			Console.WriteLine("save          - zapisz stan gry");
			Console.WriteLine();
		}
///Metoda, która sprawda, czy wejście nie jest jedną z komend pomocniczych
		static string CappedRead(){
			string read = Console.ReadLine();
			Console.Clear();
			if( read == "h" || read == "help"){
				WriteHelp();
				return "h";
			}
			if( read == "q" || read == "quit"){
				Console.Clear();
				Console.WriteLine("Czy na pewno chcesz zakończyć grę? (Y/N)");
				read = Console.ReadLine();
				if( read == "Y" || read == "y" || read == "t" || read == "tak" || read == "Yes")
					return "q";
				else
					return "h";
			}
			if( read == "s" || read == "stats"){
				Console.WriteLine(hero);
				return "h";
			}
			if( read == "kill" || read == "k" || read == "killMe"){
				Console.WriteLine("\n\nZginąłeś");
				Console.WriteLine("Koniec Gry");
				Console.WriteLine("Wciśnij dowolny przycisk aby przejść do menu głównego.");
				Console.Read();
				return "q";
			}
			if( read == "save"){
				Serialize();
				Console.WriteLine("Zapisano");
				return "h";
			}
			return read;
		}
///Metoda, która jest wywoływana w momencia ukończania poziomu
		static void end(){
			Console.WriteLine("\n\nUdało Ci się uciec z labiryntu!");
			Console.WriteLine("Koniec Gry");
			Console.WriteLine("Wciśnij dowolny przycisk aby przejść do menu głównego.");
			Console.Read();
			Console.WriteLine();
			return;
		}
///Metoda obliczająca obrażenie na podstawie ataku, obrony i broni
		static int calculateDamage(int attack, int attackType, int defense, Weapon weapon){
			
			if(attackType == 0){
				if(rand.Next()%10 >= 4)
					return 10 + attack/defense;
				else
					return 0;
			}
			if(attackType == 1){
				double acc = 0.5;
				double attM = 0.7;
				if (weapon != null){
					acc = weapon.getAccuracy();
					attM = weapon.getAttackModifier();
				}
				if(rand.Next()%10 + 5* acc >= 9)
					return (int)((20 + attack/defense)*(1+attM));
				else
					return 0;
			}
			if(attackType == 2){
				return (int)((15 + attack/defense));
			}
			if(attackType == 4)
				return 0;
			

			return attack * 6;
		}
///Metoda zapisująca stan instancji klasy Hero
		static void Serialize() 
	    {
	        try{
		        FileStream fs = new FileStream("saves/" + MapNumber + ".sav", FileMode.Create);
		        BinaryFormatter formatter = new BinaryFormatter();
	        	formatter.Serialize(fs, hero);
	             fs.Close();
		    }catch(Exception e)
		    {
		    	Console.WriteLine("Nie można Zapisać pliku" + e);
		    	Console.Read();
		    }
	        
	    }
///Metoda wczytująca instancję klasy Hero
	    static void Deserialize() 
	    {
	        try{
		        FileStream fs = new FileStream("saves/" + MapNumber + ".sav", FileMode.Open);
		        BinaryFormatter formatter = new BinaryFormatter();
				hero = (Hero) formatter.Deserialize(fs);
		        fs.Close();
		    }catch(Exception e)
		    {
		    	Console.WriteLine("Nie można Wczytać pliku" + e);
		    	Console.Read();
		    }
	    }


//Metoda która czyta mapę z pliku tekstowego
	    static bool readMap(string choice){
	    	char[,] ker = new char[200,200];
	    	String input = File.ReadAllText( "maps/"+choice+".map" );
			int i,j;
			i = j = 0;
			foreach (var row in input.Split('\n'))
			{
			    j = 0;
			    foreach (var character in row)
			    {
			        ker[i, j] = character;
			        j++;
			    }
			    i++;
			}
   			world = new Map(ker);
	    	return true;
	    }
	}
}

