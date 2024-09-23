using System;
using System.Collections.Generic;

namespace TestTask3GrandMAStudio
{
    //Class that represent test case
    public class TestCase
    {
        public int ID { get; private set; }

        //Keys player have at start
        private List<int> KeysAtStart { get; set; } = new();

        private List<Chest> ChestsInTreasure { get; set; } = new();

        private string result;

        //Calculations may be too long, so this is max attemps to solve
        private long maxCalculationsAttemps = 1000000;

        public TestCase(int id, List<int> keysAtStart, List<Chest> chestsInTreasure)
        {
            ID = id;
            KeysAtStart = keysAtStart;
            ChestsInTreasure = chestsInTreasure;
            result = CalculateResult();
        }

        public string GetResult()
        {
            return result;
        }

        private string bestWayToOpen;

        //Function form answer
        private string CalculateResult()
        {
            string result = "Case#" + ID + ": ";
            if(IsPossibleToSolve(ChestsInTreasure, KeysAtStart))
            {
                if (OpenChests(ChestsInTreasure, KeysAtStart, new()))
                {
                    if(counter > maxCalculationsAttemps)
                        result += "TOO HARD CALCULATIONS";
                    else
                        result += bestWayToOpen;
                }
                else
                {
                    result += "IMPOSSIBLE";
                }
            }
            else
            {
                result += "IMPOSSIBLE";
            }
            Console.WriteLine(result);
            Console.WriteLine();
            return result;
        }

        //Check for enough keys in all treasure and start keys to open all chests. If false, this is already imposible, no need to seek solve
        private bool IsPossibleToSolve(List<Chest> chestsInTreasure, List<int> keysAtStart)
        {
            List<int> allKeys = new(keysAtStart);
            List<int> keysNeedToOpenChests = new();

            for (int counterOfChests = 0; counterOfChests < chestsInTreasure.Count; counterOfChests++)
            {
                allKeys.AddRange(chestsInTreasure[counterOfChests].KeysInside);
                keysNeedToOpenChests.Add(chestsInTreasure[counterOfChests].TypeOfKeyToOpen);
            }

            foreach (int neededKey in keysNeedToOpenChests)
            {
                if (allKeys.Contains(neededKey))
                {
                    allKeys.Remove(neededKey);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        //Function return true, if you can open chest with key, from the list
        private bool TryOpenChest(Chest chestToOpen, List<int> keysPlayerHave)
        {
            foreach (int key in keysPlayerHave)
            {
                if (chestToOpen.TypeOfKeyToOpen == key)
                {
                    return true;
                }
            }
            return false;
        }


        //Complicate recursive function realise a DFS search in "treasure" graph
        private bool stop = false;
        private int counter = 0;
        private bool OpenChests(List<Chest> closedChests, List<int> keysPlayerHas, List<int> currentWay)
        {
            if(++counter> maxCalculationsAttemps)
            {
                stop = true;
            }
            if (closedChests.Count != 0)
            {
                bool isAnyChestCanBeOpened = false;
                foreach (Chest chest in closedChests)
                {
                    if (TryOpenChest(chest, keysPlayerHas) && !stop)
                    {
                        if(currentWay.Count > (ChestsInTreasure.Count - closedChests.Count))
                        {
                            currentWay.RemoveRange(ChestsInTreasure.Count - closedChests.Count, currentWay.Count- (ChestsInTreasure.Count - closedChests.Count));
                        }
                        isAnyChestCanBeOpened = true;
                        List<Chest> newClosedChests = new(closedChests);
                        List<int> newKeysPlayerHas = new(keysPlayerHas);

                        currentWay.Add(chest.ID);

                        newKeysPlayerHas.Remove(chest.TypeOfKeyToOpen);
                        newKeysPlayerHas.AddRange(chest.KeysInside);
                        newClosedChests.Remove(chest);

                        OpenChests(newClosedChests, newKeysPlayerHas, currentWay);
                    }
                }
                if (!isAnyChestCanBeOpened)
                {
                    return false;
                }
            }
            if(currentWay.Count == ChestsInTreasure.Count)
            {
                bestWayToOpen = string.Empty;
                foreach (int way in currentWay)
                {
                    bestWayToOpen += way + " ";
                }
                stop = true;
            }
            return true;
        }
    }

}
