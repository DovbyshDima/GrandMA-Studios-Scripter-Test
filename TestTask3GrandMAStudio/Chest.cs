using System.Collections.Generic;

namespace TestTask3GrandMAStudio
{
    //Class that represent chest
    public class Chest
    {
        public int ID { get; private set; }
        public int TypeOfKeyToOpen { get; private set; }
        public int CountOfKeysInside { get; private set; }
        public List<int> KeysInside { get; private set; } = new();

        public Chest(int id, int type, List<int> keysIns)
        {
            ID = id;
            TypeOfKeyToOpen = type;
            KeysInside = keysIns;
            CountOfKeysInside = KeysInside.Count;
        }

        public static int GetNumberOfChestWithID(int ID, List<Chest> chests)
        {
            for(int count = 0; count< chests.Count; count++)
            {
                if(chests[count].ID == ID)
                {
                    return count;
                }
            }
            return 0;
        }
    }
}
