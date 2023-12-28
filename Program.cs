namespace Console_RPG;
using Game_Characters;
using Game_Essentials;
using Zombie;
using Weapons;
using System;
using System.Linq;
using System.Diagnostics;

class Program
{   
    static void Main(string[] args)
    {

        // Initzilaize new CMD window
        if(args.Length > 0 && args[0] == "start") 
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c dotnet run";
            startInfo.CreateNoWindow = true; // Set CreateNoWindow to true
            startInfo.UseShellExecute = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
        }

        // Initialize Game
        DisplayManager DisplayManager = new DisplayManager();
        
        DisplayManager.displayGreetings();
        // DisplayManager.getPlayerName();
        Player player = new Player(GameVariables.GameSettings.playerName, attack: 50, strength: 1.0, armor: 10, health: 100, maxHealth: 100, level: 1, experience: 0, experienceToLevelUp: 100);

        string playerWeaponChoice = DisplayManager.getPlayerWeapon();

        switch (playerWeaponChoice) {
            case "Sword":
                player.currentWeapon = new Sword(name: "Sword", attack: 12, enduranceCost: 10);
                break;
            case "Axe":
                player.currentWeapon = new Axe(name: "Axe", attack: 25, enduranceCost: 20);
                break;
            case "Mace":
                player.currentWeapon = new Mace(name: "Mace", attack: 20, enduranceCost: 15);
                break;
        }
        player.setAttackOptions();

        while(GameVariables.GameLoopBooleans.isInMenu) {

            string menuChoice = DisplayManager.displayMainMenu();

            if(GameVariables.GameLoopBooleans.isDead) {
                player.health = player.maxHealth;
                GameVariables.GameLoopBooleans.isDead = false;
                Console.WriteLine(" You have been revived!");
                break;
            }

            switch (menuChoice)
            {
                case "Enter Dungeon":
                    GameVariables.GameLoopBooleans.isInMenu = false;
                    GameVariables.GameLoopBooleans.isInFight = true;
                    DisplayManager.enterDungeon(player);
                    break;
                case "View Stats":
                    player.printStats();
                    DisplayManager.waitForInput();
                    break;
                case "View Inventory":
                    player.printInventory();
                    DisplayManager.waitForInput();
                    break;
                case "Shop":
                    GameVariables.GameLoopBooleans.isInMenu = false;
                    GameVariables.GameLoopBooleans.isInShop = true;
                    while(GameVariables.GameLoopBooleans.isInShop) {
                        DisplayManager.displayShop(player);
                        DisplayManager.waitForInput();
                    }
                    break;
            }
        }

    }
}
