using System;

namespace DungeonRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dungeon RPG Game!\n");

            Player player = new Player("Hero", 100, 10);

            while (player.IsAlive)
            {
                Console.WriteLine("Player Health: {0}", player.Health);
                Console.WriteLine();

                Console.WriteLine("MENU:");
                Console.WriteLine("1. Explore Dungeon");
                Console.WriteLine("2. Rest");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice (1-3):");

                int choice = GetInput(1, 3);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("You enter a dark room...");
                        Console.WriteLine("A monster appears!");

                        Enemy enemy = Enemy.GenerateRandomEnemy();

                        while (enemy.IsAlive && player.IsAlive)
                        {
                            Console.WriteLine("\nPlayer's turn:");
                            Console.WriteLine("1. Attack");
                            Console.WriteLine("2. Flee");

                            int battleChoice = GetInput(1, 2);

                            if (battleChoice == 1)
                            {
                                int damage = player.Attack;
                                Console.WriteLine("Player attacks and deals {0} damage to {1}!", damage, enemy.Name);
                                enemy.TakeDamage(damage);
                            }
                            else
                            {
                                Console.WriteLine("Player flees the battle!");
                                break;
                            }

                            if (enemy.IsAlive)
                            {
                                Console.WriteLine("\n{0}'s turn:", enemy.Name);
                                int enemyDamage = enemy.Attack;
                                Console.WriteLine("{0} attacks and deals {1} damage to the player!", enemy.Name, enemyDamage);
                                player.TakeDamage(enemyDamage);
                            }
                        }

                        if (enemy.IsAlive)
                        {
                            Console.WriteLine("\n{0} has defeated you!", enemy.Name);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("\nYou have defeated {0}!", enemy.Name);
                            Console.WriteLine("You found a treasure chest and gained 50 gold.");
                            player.Gold += 50;
                        }

                        break;

                    case 2:
                        Console.WriteLine("You find a safe spot to rest and regain your health.");
                        player.Rest();
                        Console.WriteLine("You feel refreshed and your health is fully restored.");
                        break;

                    case 3:
                        Console.WriteLine("Exiting game...");
                        return;
                }
            }
        }

        static int GetInput(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine("Invalid input. Please enter a number between {0} and {1}.", min, max);
            }
            return choice;
        }
    }

     class Player
    {
        public string Name { get; }
        public int Health { get; private set; }
        public int Attack { get; private set; }
        public int Gold { get; set; }

        public bool IsAlive { get { return Health > 0; } }

    public Player(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
        Gold = 0;
    }

    public int AttackEnemy()
    {
        return Attack;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public void Rest()
    {
        Health = 100;
    }
}

class Enemy
{
    private static Random random = new Random();

    public string Name { get; }
    public int Health { get; private set; }
    public int Attack { get; }

    public bool IsAlive { get { return Health > 0; } }

    public Enemy(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
    }


    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public int AttackPlayer()
    {
        return Attack;
    }

    public static Enemy GenerateRandomEnemy()
    {
        string[] names = { "Goblin", "Skeleton", "Orc", "Troll" };
        string name = names[random.Next(names.Length)];
        int health = random.Next(50, 101);
        int attack = random.Next(5, 16);
        return new Enemy(name, health, attack);
    }
}
}
