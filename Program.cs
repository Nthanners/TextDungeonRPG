using System;

namespace DungeonRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Dungeon RPG Game!\n");

            // Create a new player and set their starting values
            Player player = new Player("Hero", 100, 10);

            // Game loop
            while (player.IsAlive)
            {
                Console.WriteLine("Player Health: {0}", player.Health);
                Console.WriteLine();

                // Display menu
                Console.WriteLine("MENU:");
                Console.WriteLine("1. Explore Dungeon");
                Console.WriteLine("2. Rest");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice (1-3):");

                // Get user input
                int choice = GetInput(1, 3);
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        // Explore the dungeon
                        Console.WriteLine("You enter a dark room...");
                        Console.WriteLine("A monster appears!");

                        // Create a random enemy
                        Enemy enemy = Enemy.GenerateRandomEnemy();

                        // Battle loop
                        while (enemy.IsAlive && player.IsAlive)
                        {
                            Console.WriteLine("\nPlayer's turn:");
                            Console.WriteLine("1. Attack");
                            Console.WriteLine("2. Flee");

                            // Get the player's choice
                            int battleChoice = GetInput(1, 2);

                            if (battleChoice == 1)
                            {
                                // Player attacks the enemy
                                int damage = player.Attack;
                                Console.WriteLine("Player attacks and deals {0} damage to {1}!", damage, enemy.Name);
                                enemy.TakeDamage(damage);
                            }
                            else
                            {
                                // Player flees the battle
                                Console.WriteLine("Player flees the battle!");
                                break;
                            }

                            // Check if the enemy is still alive
                            if (enemy.IsAlive)
                            {
                                // Enemy's turn
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
                        // Rest and regain health
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

        // Helper method to get valid integer input from the user
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

    // Player class
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

    // Player attacks
    public int AttackEnemy()
    {
        return Attack;
    }

    // Player takes damage
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    // Player rests and regains health
    public void Rest()
    {
        Health = 100;
    }
}

// Enemy class
class Enemy
{
    private static Random random = new Random();

    public string Name { get; }
    public int Health { get; private set; }
    public int Attack { get; }

    public bool IsAlive { get { return Health > 0; } }

    // Constructor
    public Enemy(string name, int health, int attack)
    {
        Name = name;
        Health = health;
        Attack = attack;
    }

    // Enemy takes damage
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    // Enemy attacks
    public int AttackPlayer()
    {
        return Attack;
    }

    // Generate a random enemy
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