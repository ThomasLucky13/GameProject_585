using System;
using n_Cell;
using System.Collections.Generic;

namespace Ellers_Algorithm
{
    class Eller_Generation
    {
        private int width, height;                 // --- Ширина и высота лабиринта в кол-ве клеток --- // 
        private Cell [,] cells = new Cell [1,1];   // --------------- Клетки лабиринта ---------------- //
        private Queue <int> void_multiplicities;   // -------------- свободные множества -------------- //
        bool check = false;                        // для проверки при добавлении нижних стенок
        int x;                                     // для хранения результатов генерации
        private static Random rand = new Random();

        // ---------------- Инициализация клеток ---------------- // 
            private void Init(int _width, int _height)  
            {
                // --- Проверка ширины лабиринта --- //
                if (_width>0)
                {
                    width = _width;
                } else width = 1;

                // --- Проверка длины лабиринта --- //
                if (_height>0)
                {
                    height = _height;
                } else height = 1;

                // --- Обновление лабиринта --- //
                cells = new Cell[height, width];  // новые параметры
                for (int i = 0; i< height; i++) // по строкам
                {
                    for (int j = 0; j< width; j++) // по столбцам
                    {
                        cells[i, j] = new Cell();  // инициализация клетки
                    }
                }

                // --- Обновление хранилища множеств --- //
                void_multiplicities = new Queue <int>();
                for (int i = 1; i <= width; ++i) void_multiplicities.Enqueue(i);
            }
        //--------------------------------------------------------//

        // -------------------- Конструкторы -------------------- //
            public Eller_Generation()
            {
                Init(1,1);
            }

            public Eller_Generation( int _height, int _width)
            {
                Init(_width, _height);
            }
        //--------------------------------------------------------//

        // ----------------- Генерация лабиринта ---------------- //
            private void Set_New_Multiplicity(int i, int j)
            {
                cells[i, j].set_multiplicity(void_multiplicities.Dequeue());
            }

            private void Set_Multiplicity(int i, int j, int _multiplicity)
            {
                int old_mult = cells[i, j].Multiplicity();
                cells[i, j].set_multiplicity(_multiplicity);
                for (int ind = 0; ind < width; ind++)
                if (cells[i, ind].Multiplicity()==old_mult) return;
                void_multiplicities.Enqueue(old_mult);
            }

            private void Generate_RightWalls(int i)
            {
                /// 
                ///Проходим по клеткам в строке.
                ///Если клетка справа принадлежить другому множеству,
                ///то смотрим, добавлять ли стенку.
                /// Если стенка не добавляется, 
                ///то клетке справа присваиваем то же множество, 
                /// что и у текущей
                ///Если клетка справа того же множества,
                ///то добавляем стенку
                ///
                for (int j = 0; j <width-1; j++)
                {
                    // --- Проверяем на несовпадение множества у клеток --- //
                    // --- Если не совпадает множество --- ///
                    if (cells[i,j].Multiplicity()!=cells[i,j+1].Multiplicity()) 
                    {
                        x = rand.Next(2); // если 0 - то ставим, если 1 - то не ставим

                       // --- Ставим стенку справа --- //
                        if (x==0) 
                        {
                            cells[i,j].Put_RightWall();                          
                        } else if (x==1)
                        // --- Не ставим стенку справа --- //
                        {
                            Set_Multiplicity(i, j+1, cells[i, j].Multiplicity());
                        }
                    } else
                    // --- Если совпадает множество --- //
                    {
                        cells[i,j].Put_RightWall(); //ставим стенку справа
                    }
                }
            }

            private void Generate_BottomWalls(int i)
            {
                ///
                /// Проходим по клеткам вправо
                /// Добавляем нижнюю стенку 
                /// В одном множестве должна быть хотя бы одна клетка без нижней стенки
                ///
                check = false;
                for (int j=0; j < width-1; j++)
                {
                    // --- Есои след.клетка другого множества --- //
                    if (cells[i, j].Multiplicity()!=cells[i,j+1].Multiplicity())
                    {
                        if (check)
                        {
                            x = rand.Next(2); // если 0 - добавляем, если 1 - то нет
                            if (x==0) cells[i, j].Put_BottomWall();
                            check = false;
                        }

                    } else 
                    // --- Если след. клетка того же множества --- //
                    {
                        x = rand.Next(2); // если 0 - добавлем, если 1 - то нет
                        if (x==0)
                        {
                            cells[i, j].Put_BottomWall();
                        } else 
                        {
                            check = true;
                        }
                    }
                }

                if (check)
                {
                    x = rand.Next(2); // если 0 - добавляем, если 1 - то нет
                    if (x==0) cells[i, width-1].Put_BottomWall();
                    check = false;
                }

            }

            private void Copy_Line(int i, int ii)
            {
                ///
                ///Копируем в нижнюю линюю верхнюю
                ///Удаляем все правые стенки
                ///Если есть нижняя стенка, то убираем из множества
                ///
                for (int j = 0; j < width; j++)
                {
                    if(cells[i,j].BottomWall()) cells[ii, j].set_multiplicity(0);
                    else cells[ii, j].set_multiplicity(cells[i, j].Multiplicity());
                }
            }

            private void Remove_AllRightWalls(int i)
            {
                for (int j = 0; j<width; j++)
                    cells[i, j].Remove_RightWall();
            }

            public void Generate(int _height, int _width)
            {
                Init(_width, _height); //инициализируем новый лабиринт

                for (int i = 0; i<height; i++)
                {
                    ///
                    /// Если клетка не принадлежит множеству, 
                    ///то присваиваем ей новое множенство
                    ///
                    for (int j = 0; j < width; j++)
                    {
                        if(cells[i, j].Multiplicity()==0) Set_New_Multiplicity(i,j);
                        
                    }

                    Generate_RightWalls(i);
                    Generate_BottomWalls(i);

                    // Если не последняя строка
                    if (i!=height-1)
                    {
                        Copy_Line(i, i+1);
                    }


                }
            }

            public void Generate()
            {
                Generate(1,1);
            }
        //--------------------------------------------------------//

        // ---------------- Возвращение лабиринта ---------------- //
            public Cell [,] Maze()
            {
                return cells;
            }
        //--------------------------------------------------------//

        // -------------- Вывод лабиринта на экран -------------- //
            public void Display_Maze()
            {
                // ------------- Верхняя строка лабиринта ------------- //
                //for (int i = 0; i <= width; i++) System.Console.Write("__");
                // ------------------- Сам лабиринт ------------------- //
                for (int i = 0; i < height; i++) // проход по лабиринту по длине
                {
                    // ---- Проход по лабиринту по ширине ---- //
                    System.Console.Write("\n|"); 
                    for (int j = 0; j < width; j++) // выставление правых стенок
                    {
                        cells[i, j].Display_RightWall();
                    }
                    System.Console.Write("|");
                    System.Console.Write("\n|"); 
                    for (int j = 0; j< width; j++) // выставление нижних стенок
                    {
                        cells[i, j].Display_BottomWall();
                    }
                    System.Console.Write("|");
                }
                System.Console.Write("\n");
                // -------------- Нижняя строка лабиринта ------------- //
                //for (int i = 0; i <= width; i++) System.Console.Write("__");
                System.Console.Write("\n\n");
            }
        //--------------------------------------------------------//
    }
}