using System;
using System.Collections.Generic;
using System.IO;

namespace TestTask3GrandMAStudio
{
    class Program
    {
        private static string[] fileName = new string[] { "Treasure_1_Small.in", "Treasure_2_Large.in" };
        private static List<TestCase> testCasesInFile = new();

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Номер файлу для огляду (1 та 2 (-1 для виходу))");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": ReadFile(fileName[0]); break;
                    case "2": ReadFile(fileName[1]); break;
                    case "-1": return;
                }
            }
        }

        private static void ReadFile(string file)
        {
            using (StreamReader reader = new(file))
            {
                //First line – amount of test cases
                int amountOfTestCase = int.Parse(reader.ReadLine());

                //Loop to read all test cases in file
                for(int idOfTestCase = 1; idOfTestCase <= amountOfTestCase; idOfTestCase++)
                {
                    //Second line – amount of keys player has and amount of chests in treasure 
                    List<int> amountOfKeysAndChests = ConvertLineToList(reader.ReadLine());

                    //Third line – keys player has
                    List<int> keysPlayerHave = ConvertLineToList(reader.ReadLine());

                    //ListOfAllTreasuresInChest
                    List<Chest> chestsInTreasure = new();
                    for(int idOfChestInTreasure = 1; idOfChestInTreasure<=amountOfKeysAndChests[1]; idOfChestInTreasure++)
                    {
                        List<int> chestInfo = ConvertLineToList(reader.ReadLine());
                        Chest newChest = new(idOfChestInTreasure, chestInfo[0], chestInfo.GetRange(2, chestInfo.Count-2));
                        chestsInTreasure.Add(newChest);
                    }
                    testCasesInFile.Add(new TestCase(idOfTestCase, keysPlayerHave, chestsInTreasure));
                }
            }
        }

        //Function that convert line to list of int
        private static List<int> ConvertLineToList(string line)
        {
            string[] numbersSplit = line.Split(' ');
            List<int> result = new();
            foreach (string number in numbersSplit)
            {
                result.Add(int.Parse(number));
            }
            return result;
        }
    }
}
