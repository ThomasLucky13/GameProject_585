using System;
using Ellers_Algorithm;
using n_Cell;


namespace n_Maze
{
    public class Maze
    {
        public static Cell[,] Get_map(int height, int width)
        {
            Eller_Generation maze = new Eller_Generation();
            maze.Generate(height, width);
            return maze.Maze();
        }
    }
}
