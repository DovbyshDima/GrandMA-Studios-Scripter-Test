using System;

namespace TestTask1GrandMAStudio
{
    class Program
    {
        static void Main()
        {
            //Reading data from user
            Console.Write("Введіть frame_width: ");
            int frame_width = int.Parse(Console.ReadLine());
            Console.Write("Введіть frame_height: ");
            int frame_height = int.Parse(Console.ReadLine());

            Console.Write("Введіть pic_width (менше, ніж frame_width): ");
            int pic_width = int.Parse(Console.ReadLine());
            Console.Write("Введіть pic_height (менше, ніж frame_height): ");
            int pic_height = int.Parse(Console.ReadLine());

            Console.Write("Введіть pic_x: ");
            int pic_x = int.Parse(Console.ReadLine());
            Console.Write("Введіть pic_y: ");
            int pic_y = int.Parse(Console.ReadLine());

            //Create array with random content
            int[,] array = new int[frame_width, frame_height];
            Random random = new();
            for (int i = 0; i < frame_width; i++)
            {
                for (int j = 0; j < frame_height; j++)
                {
                    array[i, j] = random.Next(1, 1001); // Заповнення випадковими значеннями
                }
            }

            //Print array before changes
            Console.WriteLine("Початковий масив:");
            PrintArray(array, frame_width, frame_height);

            //Move "useful" data
            for (int i = 0; i < pic_width; i++)
            {
                for (int j = 0; j < pic_height; j++)
                {
                    array[i, j] = array[pic_x + i, pic_y + j];
                    array[pic_x + i, pic_y + j] = 0;
                }
            }

            //Print new version of array
            Console.WriteLine("Перетворений масив:");
            PrintArray(array, frame_width, frame_height);
        }

        //Function that print array
        static void PrintArray(int[,] array, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.Write(array[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
