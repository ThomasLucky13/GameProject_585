using System;
using System.Collections.Generic;
using Ellers_Algorithm;
using n_Cell;
using n_Room;
using Splitting;
using n_Room_Generate;

namespace n_Maze
{
    public class Maze
    {
        public static int Get_Part_Width(Cell[,] cells, int i, int j, int map_width)
        {
            int width = 1;
            for (int jj = j+1; jj<map_width; jj++)
            {
                if (!cells[i, jj].Unused()) return width;
                width ++;
            }
            return width;
        }
        public static int Get_Part_Height(Cell[,] cells, int i, int j, int width, int map_height, int map_width)
        {
            int height = 1;
            for (int ii = i+1; ii<map_height; ii++)
            {
                if (Get_Part_Width(cells, ii, j, width)!=height) return height;
                height++;
            }
            return height;
        }
        ///
        /// height = width = 72
        /// rooms_count = 16
        /// min_room = 4, mid_room = 8, max_room = 9
        ///
        public static Cell[,] Get_map(int height, int width, int min_room, int mid_room, int max_room, int distance)
        {
            Cell[,] map, part_map;
            Eller_Generation maze = new Eller_Generation();
            int start_i, start_j, part_width, part_height, maze_i, maze_j;

            // -------- Получаем разбиение карты  --------- //
            List <split_leaf> [] leaves = new List<split_leaf> [4];
            leaves[0] = Map_Splitter.Split(height/2, width/2, mid_room+distance, mid_room+distance);
            leaves[1] = Map_Splitter.Split(height/2, width/2, mid_room+distance, mid_room+distance);
            leaves[2] = Map_Splitter.Reverse_Split(leaves[1], height/2, width/2);
            leaves[3] = Map_Splitter.Reverse_Split(leaves[0], height/2, width/2);

            // ------------ Генерируем комнаты ------------ //
            List <Room> rooms = Room_Generate.Generate_Rooms(leaves, min_room, mid_room, max_room, height, width, distance);

            // ------------ Устанавливаем комнаты на карте ------------- //
            map = Room_Generate.Set_Rooms(rooms, height, width);
            
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i,j].Unused())
                    {
                        start_i = i; start_j = j; 
                        part_width = Get_Part_Width(map, i, j, width);
                        part_height = Get_Part_Height(map, i, j, part_width, height, width);
                        maze.Generate(part_height, part_width);
                        part_map = maze.Maze();
                        maze_i = 0;
                        for (int ii = start_i; ii < start_i+part_height; ii++)
                        {
                            maze_j=0;
                            for (int jj = start_j; jj < start_j+part_width; jj++)
                            {
                                map[ii, jj] = part_map[maze_i, maze_j];
                                map[ii, jj].Use();
                                maze_j++;
                            }
                            maze_i++;
                        }
                    }
                }
            }
            
            maze.Generate(height, width);
            return map;
        }
    }
}
