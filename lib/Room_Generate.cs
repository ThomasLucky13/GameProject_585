using System;
using System.Collections.Generic;
using n_Cell;
using Splitting;
using n_Room;

namespace n_Room_Generate
{
    public static class Room_Generate
    {
        // ------------------------------ Генерируем новую комнату ---------------------------- //
        private static Room GenerateRoom(int x_start, int x_end, int y_start, int y_end, int width)
        {
            Room room = new Room(width, width); // создаем новую комнату с указанными размерами
            Random rnd = new Random();

            // -------- Задаем координаты -------- //
            int x = rnd.Next(x_start, x_end+1), y = rnd.Next(y_start, y_end+1);
            room.set_x(x);
            room.set_y(y);

            return room;
        }
        //--------------------------------------------------------------------------------------//

        private static int Find_center_second(List<split_leaf> leaves)
        {

            split_leaf leaf;
            int i = 0, count_of_leafs = leaves.Count, res = 0;

            while (i<count_of_leafs)
            {
                leaf = leaves[i];
                if ((leaf.j==0) && (leaf.i>leaves[res].i))
                    res = i;
                i++;
            }
            return res;
        }

        private static int Find_center_third(List<split_leaf> leaves)
        {

            split_leaf leaf;
            int i = 0, count_of_leafs = leaves.Count, res = 0;

            while (i<count_of_leafs)
            {
                leaf = leaves[i];
                if ((leaf.i==0) && (leaf.j>leaves[res].j))
                    res = i;
                i++;
            }
            return res;
        }

        // ------------------------------ Генерация всех комнат ------------------------------- //
        public static List<Room> Generate_Rooms(List<split_leaf>[] leaves, int min_room, int mid_room, int max_room, int map_height, int map_width, int min_distance)
        {
            List<Room> rooms = new List<Room>();

            Random rnd = new Random();
            int count_of_leafs= leaves[3].Count; // количество разбиений
            split_leaf cur_leaf;   // текущее разбиение 
            int width; // размер комнаты 
            int fin_room; // комната финала - портал

            // --------------------- Базы команд первая и последняя комната --------------------- //
            cur_leaf = leaves[0][0];
            width = mid_room;
            rooms.Add(new Room(width, width, cur_leaf.j, cur_leaf.i));

            cur_leaf = leaves[3][count_of_leafs-1];
            rooms.Add(new Room(width,width, 
            cur_leaf.j+ (map_height/2) +  cur_leaf.width - width, cur_leaf.i + (map_height/2) + cur_leaf.height - width));

            
            // ------------------------- Портал - центральная комната --------------------------- //
            rooms.Add(new Room(max_room,max_room, map_width/2-max_room/2, map_height/2-max_room/2));

            
            // -------------------------- Первая часть комнат ------------------------------- //
            count_of_leafs = leaves[0].Count;
            for (int i = 1; i < count_of_leafs-1; i++)
            {
                cur_leaf = leaves[0][i];
                width = rnd.Next(min_room, mid_room+1);
                rooms.Add(GenerateRoom(cur_leaf.j+min_distance, cur_leaf.j+cur_leaf.width - width, 
                cur_leaf.i+min_distance, cur_leaf.i+cur_leaf.height-width, width));
            }

            // ------------------------- Вторая часть комнат -------------------------------- //
            count_of_leafs = leaves[1].Count;
            fin_room = Find_center_second(leaves[1]);
            for (int i = 0; i < count_of_leafs; i++)
            {
                if (i != fin_room)
                {
                    cur_leaf = leaves[1][i];
                    width = rnd.Next(min_room, mid_room+1);
                    rooms.Add(GenerateRoom(cur_leaf.j+(map_width/2)+min_distance, cur_leaf.j+ (map_width/2) +cur_leaf.width - width, 
                    cur_leaf.i+min_distance, cur_leaf.i+cur_leaf.height-width, width));
                }
            }

            // ------------------------- Третья часть комнат -------------------------------- //
            count_of_leafs = leaves[2].Count;
            fin_room = Find_center_third(leaves[2]);
            for (int i = 0; i < count_of_leafs-1; i++)
            {
                if (i != fin_room)
                {
                    cur_leaf = leaves[2][i];
                    width = rnd.Next(min_room, mid_room+1);
                    rooms.Add(GenerateRoom(cur_leaf.j+min_distance, cur_leaf.j +cur_leaf.width - width, 
                    cur_leaf.i + (map_height/2)+min_distance, cur_leaf.i + (map_height/2) +cur_leaf.height-width, width));
                }
                
            }

            // -------------------------- Последняя часть комнат ------------------------------- //
            count_of_leafs = leaves[3].Count;
            for (int i = 1; i < count_of_leafs-1; i++)
            {
                cur_leaf = leaves[3][i];
                width = rnd.Next(min_room, mid_room+1);
                rooms.Add(GenerateRoom(cur_leaf.j+(map_width/2)+min_distance, cur_leaf.j+ (map_width/2) +cur_leaf.width - width, 
                cur_leaf.i + (map_height/2)+min_distance, cur_leaf.i + (map_height/2) +cur_leaf.height-width, width));
            }

            return rooms;

        }
        //--------------------------------------------------------------------------------------//

        // --------------------------- Установка комнат на карте ------------------------------ //
        public static Cell[,] Set_Rooms(List<Room> rooms, int height, int width)
        {
            // ----------------- Инициализация клеток ------------------ //
            Cell[,] cells = new Cell[height, width];
            for (int i = 0; i< height; i++) // по строкам
            {
                for (int j = 0; j< width; j++) // по столбцам
                {
                    cells[i, j] = new Cell();  // инициализация клетки
                }
            }
            
            int i_end, j_end, x, y;
            // ------------------ Установка комнат ---------------------- //
            foreach (Room room in rooms)
            {
                i_end = room.Y() + room.Height();
                j_end = room.X() + room.Width();
                y = 0;
                for (int i = room.Y(); i < i_end; i++)
                {
                    x = 0;
                    for (int j = room.X(); j < j_end; j++)
                    {
                        
                        cells[i, j] = room.Get_Cell(x, y);
                        x++;
                    }
                    y++;
                }
            }
            return cells;
        }
        //--------------------------------------------------------------------------------------//

    }
}