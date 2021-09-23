using System;
using Ellers_Algorithm;


namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
            Eller_Generation maze = new Eller_Generation();
            maze.Generate(20,20);
            maze.Display_Maze();
        }
    }
}
