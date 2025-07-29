using System;
using System.Collections.Generic;
using System.Threading;

namespace ProblematicProblem
{
    class Program
    {
        // Make rng static so it can be used across methods without redeclaring
        private static Random rng = new Random();
        private static bool cont = true;

        // List of activities
        private static List<string> activities = new List<string>()
        {
            "Movies", "Paintball", "Bowling", "Lazer Tag", 
            "LAN Party", "Hiking", "Axe Throwing", "Wine Tasting"
        };

        static void Main(string[] args)
        {
            Console.Write("Hello, welcome to the random activity generator! \nWould you like to generate a random activity? yes/no: ");
            cont = Console.ReadLine().Trim().ToLower() == "yes";

            if (!cont)
            {
                Console.WriteLine("Maybe next time! Goodbye.");
                return;
            }

            Console.Write("\nWe are going to need your information first! What is your name? ");
            string userName = Console.ReadLine();

            Console.Write("\nWhat is your age? ");
            if (!int.TryParse(Console.ReadLine(), out int userAge))
            {
                Console.WriteLine("Invalid age. Defaulting to 0.");
                userAge = 0;
            }

            Console.Write("\nWould you like to see the current list of activities? yes/no: ");
            bool seeList = Console.ReadLine().Trim().ToLower() == "yes";

            if (seeList)
            {
                foreach (string activity in activities)
                {
                    Console.Write($"{activity} "); // FIXED: correctly prints the activity
                    Thread.Sleep(150);
                }

                Console.WriteLine();
                Console.Write("\nWould you like to add any activities before we generate one? yes/no: ");
                bool addToList = Console.ReadLine().Trim().ToLower() == "yes";

                while (addToList)
                {
                    Console.Write("What would you like to add? ");
                    string userAddition = Console.ReadLine();
                    activities.Add(userAddition);

                    Console.WriteLine("\nHere’s the updated list:");
                    foreach (string activity in activities)
                    {
                        Console.Write($"{activity} ");
                        Thread.Sleep(150);
                    }

                    Console.WriteLine("\nWould you like to add more? yes/no: ");
                    addToList = Console.ReadLine().Trim().ToLower() == "yes";
                }
            }

            // Main loop
            while (cont)
            {
                Console.Write("\nConnecting to the database");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(200);
                }

                Console.WriteLine("\nChoosing your random activity");
                for (int i = 0; i < 5; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(200);
                }

                Console.WriteLine();

                int randomNumber = rng.Next(activities.Count);
                string randomActivity = activities[randomNumber];

                // Remove "Wine Tasting" if underage
                if (userAge < 21 && randomActivity == "Wine Tasting")
                {
                    Console.WriteLine($"\nOh no! You’re too young for {randomActivity}. Choosing something else...");
                    activities.Remove(randomActivity);
                    randomNumber = rng.Next(activities.Count);
                    randomActivity = activities[randomNumber];
                }

                Console.WriteLine($"\nGot it! Your random activity is: {randomActivity}. Are you okay with this, {userName}? (keep/redo)");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "keep")
                {
                    Console.WriteLine($"\nEnjoy your activity, {userName}!");
                    cont = false; // Exit loop
                }
            }
        }
    }
}
