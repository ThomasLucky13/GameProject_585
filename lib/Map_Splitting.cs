using System;
using System.Collections.Generic;

namespace Splitting
{
    // ---------------- Структура лист - который используется для разбиения карты -------------- // 
    public struct split_leaf
    {
        public int i, j; // начальные координаты листа на карте
        public int height, width; //размер листа

        public split_leaf(int _i, int _j, int _height, int _width)
        {
            i = _i; j = _j; 
            height = _height; width = _width;
        }
    }
    //-------------------------------------------------------------------------------------------//

    public static class Map_Splitter
    {

        public static List<split_leaf> Reverse_Split(List<split_leaf> leaves, int height, int width)
        {
            List<split_leaf> splits = new List<split_leaf>();
            int i, j;

            foreach (split_leaf split in leaves)
            {
                i = height - (split.i + split.height);
                j = width - (split.j + split.width);
                splits.Add(new split_leaf(i, j, split.height, split.width));
            }
            splits.Reverse();
            return splits;
        }
        
        private static List<split_leaf> VerticalSplit(List<split_leaf> leaves, int min_width)
        {
            List<split_leaf> splits = new List<split_leaf>();
            int i1, j1, height1, width1, i2, j2, height2, width2;
            Random rnd = new Random();
            int rand_res;

            foreach (var leaf in leaves)
            {
                if (leaf.width >= (min_width*2))
                {
                    // Если пригоден для разбиения 
                    i1 = leaf.i; j1 = leaf.j;  // начальные координаты первого листа

                    rand_res = rnd.Next(min_width, leaf.width - min_width+1);

                    i2 = leaf.i; j2 = leaf.j+rand_res; // начальные координаты второго листа 
                    height1 = leaf.height; height2 = leaf.height; // высота одинаковая 
                    width1 = rand_res; width2 = leaf.width - rand_res;

                    // ------- Добавляем новые листы ------- // 
                    splits.Add(new split_leaf(i1, j1, height1, width1));
                    splits.Add(new split_leaf(i2, j2, height2, width2));

                } else // если не пригоден для разбиения - то отправляем в новый список
                 splits.Add(new split_leaf(leaf.i, leaf.j, leaf.height, leaf.width));
            }
            return splits;
        }

        private static List<split_leaf> HorizontalSplit(List<split_leaf> leaves, int min_height)
        {
            List<split_leaf> splits = new List<split_leaf>();
            int i1, j1, height1, width1, i2, j2, height2, width2;
            Random rnd = new Random();
            int rand_res;

            foreach (var leaf in leaves)
            {
                if (leaf.height >= (min_height*2))
                {
                    
                    // Если пригоден для разбиения 
                    i1 = leaf.i; j1 = leaf.j;  // начальные координаты первого листа

                    rand_res = rnd.Next(min_height, leaf.height - min_height+1);
                        
                    j2 = leaf.j; i2 = leaf.i+rand_res; // начальные координаты второго листа 
                    width1 = leaf.width; width2 = leaf.width; // ширина одинаковая 
                    height1 = rand_res; height2 = leaf.height - rand_res;

                    // ------- Добавляем новые листы ------- // 
                    splits.Add(new split_leaf(i1, j1, height1, width1));
                    splits.Add(new split_leaf(i2, j2, height2, width2));

                } else // если не пригоден для разбиения - то отправляем в новый список
                 splits.Add(new split_leaf(leaf.i, leaf.j, leaf.height, leaf.width));
            }
            return splits;
        }

        // --------------------------------- Разбиение самой карты ---------------------------------------- //
        public static List<split_leaf> Split(int map_height, int map_width, int min_height, int min_width)
        {
            List<split_leaf> splits = new List<split_leaf>(); //Создаем массив для хранения листьев
            splits.Add(new split_leaf(0, 0, map_height, map_width)); // заносим первый лист размером со всю карту
            int cur_leaf = 1, series = 0; //присваиваем количество листьев 1

            bool vertical = true; // начинаем с вертикального деления 

            while (series < 2)
            {
                if (vertical)
                {
                    splits = VerticalSplit(splits, min_width);
                    vertical = false;
                } else
                {
                    splits = HorizontalSplit(splits, min_height);
                    vertical = true;
                }
                if (cur_leaf == splits.Count) series++;
                else cur_leaf = splits.Count;
            }
            
            return splits;
        }
        //--------------------------------------------------------------------------------------------------//

    }
} 