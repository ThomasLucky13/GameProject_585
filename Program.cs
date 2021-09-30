using System;
using n_Cell;
using n_Maze;

namespace Maze_pj
{
    class Program
    {
         // -------------- Вывод лабиринта на экран -------------- //
            public static void Display_Maze(Cell[,] cells)
            {
                int height = cells.GetLength(0);
                int width = cells.GetLength(1);

                // ------------------- Сам лабиринт ------------------- //
                for (int i = 0; i < height; i++) // проход по лабиринту по длине
                {
                    // ---- Проход по лабиринту по ширине ---- //
                    System.Console.Write("\n"); 
                    for (int j = 0; j < width; j++) // выставление правых стенок
                    {
                        cells[i, j].Display_RightWall();
                    }
                    System.Console.Write("");
                    System.Console.Write("\n"); 
                    for (int j = 0; j< width; j++) // выставление нижних стенок
                    {
                        cells[i, j].Display_BottomWall();
                    }
                    System.Console.Write("");
                }
                System.Console.Write("\n\n");
            }
        //--------------------------------------------------------//

        static void Main(string[] args)
        {
            Cell[,] maze = Maze.Get_map(20,20);
            Display_Maze(maze);
        }
    }
}
