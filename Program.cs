using System;

class Enemy
{
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Gold { get; private set; }

    public Enemy(int health, int attack, int gold)
    {
        Health = health;
        Attack = attack;
        Gold = gold;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
    }

    public bool IsAlive
    {
        get { return Health > 0; }
    }

    public static Enemy GenerateRandomEnemy()
    {
        Random random = new Random();
        int health = random.Next(30, 50);
        int attack = random.Next(5, 10);
        int gold = random.Next(5, 10);

        return new Enemy(health, attack, gold);
    }
}

class Player
{
    public int PlayerLevel { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Gold { get; set; }
    public int MaxHealth { get; private set; }

    public Player(int health, int attack, int gold, int maxHealth)
    {
        PlayerLevel = 1;
        Health = health;
        Attack = attack;
        Gold = gold;
        MaxHealth = maxHealth;
    }

    public void LevelUp()
    {
        PlayerLevel++;
        Health += 20;
        Attack += 5;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0)
            Health = 0;
    }

    public bool IsAlive
    {
        get { return Health > 0; }
    }

    public void Rest()
    {
        int maxHealAmount = PlayerLevel * 10; 
        int missingHealth = MaxHealth - Health;
        int healAmount = Math.Min(maxHealAmount, missingHealth);
    
        Health = Math.Min(MaxHealth, Health + healAmount); 

        Console.WriteLine("You are healed for {0} health.", healAmount);
        Console.WriteLine("Your current health: {0}", Health);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player(100, 10, 0, 200);

        while (true)
        {
            Console.WriteLine("Player Level: {0}", player.PlayerLevel);
            Console.WriteLine("Player Health: {0}", player.Health);
            Console.WriteLine("Player Attack: {0}", player.Attack);
            Console.WriteLine("Player Gold: {0}", player.Gold);
            Console.WriteLine();

            Console.WriteLine("MENU:");
            Console.WriteLine("1. Explore The Dungeon");
            Console.WriteLine("2. Rest");
            Console.WriteLine("3. Level Up");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Enter your choice (1-3):");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You enter a dark room...");
                    Console.WriteLine("A monster appears!");

                    Enemy enemy = Enemy.GenerateRandomEnemy();
                    bool fled = false;

                    while (enemy.IsAlive && player.IsAlive)
                    {
                        Console.WriteLine("Player Health: {0}", player.Health);
                        Console.WriteLine("Enemy Health: {0}", enemy.Health);
                        Console.WriteLine();

                        Console.WriteLine("\nPlayer's turn:");
                        Console.WriteLine("1. Attack");
                        Console.WriteLine("2. Flee");

                        int playerChoice = int.Parse(Console.ReadLine());

                        switch (playerChoice)
                        {
                            case 1:
                                Console.WriteLine("Player attacks!");
                                int damage = player.Attack;
                                enemy.TakeDamage(damage);
                                Console.WriteLine("Enemy takes {0} damage.", damage);

                                if (!enemy.IsAlive)
                                {
                                    Console.WriteLine("You defeated the enemy! You gained {0} gold.", enemy.Gold);
                                    player.Gold += enemy.Gold;
                                    break;
                                }

                                Console.WriteLine("Enemy attacks!");
                                damage = enemy.Attack;
                                player.TakeDamage(damage);
                                Console.WriteLine("Player takes {0} damage.", damage);

                                if (!player.IsAlive)
                                {
                                    Console.WriteLine("You were defeated! Game over.");
                                    return;
                                }
                                break;

                                                                if (!player.IsAlive)
                                {
                                    Console.WriteLine("You were defeated! Game over.");
                                    return;
                                }
                                break;

                            case 2:
                                Console.WriteLine("You fled from the battle!");
                                fled = true;
                                break;

                            default:
                                Console.WriteLine("Invalid choice! Please enter a valid option.");
                                break;
                        }

                        if (fled)
                        {
                            break;
                        }
                    }
                    break;

                case 2:
                    Console.WriteLine("You find a safe spot to rest and regain your health.");
                    player.Rest();
                    Console.WriteLine("You feel refreshed and your health is fully restored.");
                    break;

                case 3:
                    player.LevelUp();
                    Console.WriteLine("Congratulations! You leveled up.");
                    Console.WriteLine();
                    break;

                case 4:
                    Console.WriteLine("Exiting game...");
                    return;

                default:
                    Console.WriteLine("Invalid choice! Please enter a valid option.");
                    break;
            }

            Console.WriteLine();
        }
    }
}

