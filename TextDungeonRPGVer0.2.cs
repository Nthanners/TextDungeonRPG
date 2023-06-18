using System;

class Enemy
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Gold { get; private set; }

    public Enemy(string name, int health, int attack, int gold)
    {
        Name = name;
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

    public static Enemy GenerateRandomEnemy(int playerLevel)
    {
        Random random = new Random();
        int health;
        int attack;
        int gold;

        if (playerLevel % 5 == 0)
        {
            health = random.Next(40, 80);
            attack = random.Next(9, 18);
            gold = random.Next(17, 26);

            int bossMultiplier = playerLevel / 5;
            for (int i = 0; i < bossMultiplier; i++)
            {
                health *= 2;
                attack *= 2;
                gold *= 2;
            }

            return new Enemy("Boss", health, attack, gold);
        }
        else
        {
            health = random.Next(20, 35);
            attack = random.Next(4, 9);
            gold = random.Next(5, 10);

            int levelMultiplier = playerLevel / 5;
            health = (int)(health * Math.Pow(1.5, levelMultiplier));
            attack = (int)(attack * Math.Pow(1.5, levelMultiplier));
            gold = (int)(gold * Math.Pow(1.7, levelMultiplier));

            string[] names = { "Goblin", "Skeleton", "Zombie", "Slime" };
            string name = names[random.Next(names.Length)];
            return new Enemy(name, health, attack, gold);
        }
    }
}

class Player
{
    public int PlayerLevel { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Gold { get; set; }
    public int MaxHealth { get; private set; }

    public Player(int health, int attack, int gold)
    {
        PlayerLevel = 1;
        Health = health;
        Attack = attack;
        Gold = gold;
        MaxHealth = 100;
    }

    public void LevelUp()
    {
        int levelUpCost = 50;

        if (Gold >= levelUpCost)
        {
        PlayerLevel++;
        Health += 20;
        Attack += 5;
        MaxHealth += 20;
        Gold -= levelUpCost;
        
        if (PlayerLevel % 5 == 0)
        {
            Console.WriteLine("Boss level! Brace yourself for a tough fight!");
            Console.WriteLine("After the Boss is defeated the enemy is stronger.");
        }
        else
        {
            Console.WriteLine("Congratulations! You've leveled up to level {0}.", PlayerLevel);
            Console.WriteLine("Your health and attack have increased.");
        }
        }
        else
        {
            Console.WriteLine("Insufficient gold to level up.");
        }
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
        int restCost = 5;
        int maxHealAmount = PlayerLevel * 100; 
        int missingHealth = MaxHealth - Health;
        int healAmount = Math.Min(maxHealAmount, missingHealth);
    
     if (Gold >= restCost)
        {
            Health = Math.Min(MaxHealth, Health + healAmount);
            Gold -= restCost;

            Health = Math.Min(MaxHealth, Health + healAmount); 

            Console.WriteLine("You are healed for {0} health.", healAmount);
            Console.WriteLine("Your current health: {0}", Health);
        }
        else
        {
            Console.WriteLine("Insufficient gold to rest.");
        }
    }

    public void BuyItem(string item, List<string> inventory)
    {
        switch (item)
        {
            case "sword":
                BuySword(inventory);
                break;
            
            case "armor":
                BuyArmor(inventory);
                break;

            default:
                Console.WriteLine("Invalid item!");
                break;
        }
    }

    private void BuySword(List<string> inventory)
    {
        int swordCost = 70;
        if (Gold >= swordCost && !inventory.Contains("Sword"))
        {
            Gold -= swordCost;
            Attack += 5;
            inventory.Add("Sword");
            Console.WriteLine("You have bought a sword! Your attack increased by 5.");
        }
        else if (inventory.Contains("Sword"))
        {
            Console.WriteLine("You already have a sword.");
        }
        else
        {
            Console.WriteLine("You don't have enough gold to buy a sword.");
        }
    }

     private void BuyArmor(List<string> inventory)
    {
        int armorCost = 80;
        if (Gold >= armorCost && !inventory.Contains("Armor"))
        {
            Gold -= armorCost;
            Health += 100;
            MaxHealth += 100;
            inventory.Add("Armor");
            Console.WriteLine("You have bought armor! Your health increased by 100.");
        }
        else if (inventory.Contains("Armor"))
        {
            Console.WriteLine("You already have armor.");
        }
        else
        {
            Console.WriteLine("You don't have enough gold to buy armor.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Player player = new Player(100, 10, 0);
        List<string> inventory = new List<string>();

        while (true)
        {
            Console.WriteLine("Player Level: {0}", player.PlayerLevel);
            Console.WriteLine("Player Health: {0}", player.Health);
            Console.WriteLine("Player Attack: {0}", player.Attack);
            Console.WriteLine("Player Gold: {0}", player.Gold);
            Console.WriteLine("Inventory: {0}", string.Join(", ", inventory));
            Console.WriteLine();

            Console.WriteLine("MENU:");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Heal (5 Gold)");
            Console.WriteLine("3. Level Up (50 Gold)");
            Console.WriteLine("4. Shop");
            Console.WriteLine("5. Code");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Enter your choice (1-6):");


            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("You enter a dark room...");
                    Enemy enemy = Enemy.GenerateRandomEnemy(player.PlayerLevel);
                    bool fled = false;

                    Console.WriteLine("A {0} appears!", enemy.Name);

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
                    if (player.Gold >= 5)
                    {
                        Console.WriteLine("You find a safe spot to rest and regain your health.");
                        player.Rest();
                        Console.WriteLine("You feel refreshed and your health is fully restored.");
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough gold to rest.");
                    }
                    break;

                case 3:
                    player.LevelUp();
                    Console.WriteLine();
                    break;
                
                case 4:
                    Console.WriteLine("Welcome to the Shop!");
                    Console.WriteLine("What would you like to buy?");
                    Console.WriteLine("1. Sword (+5 Attack): 70 Gold");
                    Console.WriteLine("2. Armor (+100 Health): 80 Gold");
                    Console.WriteLine("3. Back to Main Menu");
                    Console.WriteLine("Enter your choice (1-3):");
                    int shopChoice = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    switch (shopChoice)
                    {
                        case 1:
                            player.BuyItem("sword", inventory);
                            break;
                        case 2:
                            player.BuyItem("armor", inventory);
                            break;
                        case 3:
                            Console.WriteLine("Returning to the menu...");
                            break;
                        default:
                            Console.WriteLine("Invalid item choice!");
                            break;
                    }
                    break;

                case 5:
                    Console.WriteLine("Enter the secret code:");
                    string secretCode = Console.ReadLine();

                    if (secretCode == "Kronii")
                    {
                        player.Gold += 5000;
                        Console.WriteLine("5000 gold has been added to your inventory!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid secret code!");
                    }
                    break;

                case 6:
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
