using System;
using n_Cell;

namespace n_Room
{
    public class Room
    {
        private Cell[,] cells;     // Клетки команты
        private int x, y;          // Координаты команты
        private int width, height; // Размер команты

        // ---------------- Инициализация клеток ---------------- // 
            private void Init(int _height, int _width)
            {
                bool outside_wall_right = false, outside_wall_bottom=false; // для проверки при добавлении крайних стенок
                Random rand = new Random();
                // --- Проверка ширины комнаты //
                    if (_width>0)
                    {
                        width = _width;
                    } else width = 0;

                // --- Проверка длины комнаты --- //
                    if (_height>0)
                    {
                        height = _height;
                    } else height = 0;

                // --- Обновление комнаты --- //
                    cells = new Cell[height, width];  // новые параметры
                    for (int i = 0; i< height; i++) // по строкам
                    {
                        for (int j = 0; j< width; j++) // по столбцам
                        {
                            cells[i, j] = new Cell();  // инициализация клетки
                            cells[i, j].Use();
                        }
                    }



                    /// 
                    /// КОРОЧЕ
                    /// ДАЛЬШЕ ПРОИЗОШЛА КАКАЯ-ТО ФИГНЯ
                    /// И ВМЕСТО ПРАВЫХ СТЕНОК СТАВЯТСЯ НИЖНИЕ
                    /// А ВМЕСТО НИЖНИХ - ПРАВЫЕ
                    /// ВОЗМОЖНО Я СЛИШКОМ ДОЛГО СИЖУ ЗА ЭТИМ ЧТОБЫ РАЗОБРАТЬСЯ В ЧЕМ ДЕЛО
                    /// 
                    /// НО ВСЕ ЕЩЕ ЭТО ОСТАЕТСЯ ЗАГАДКОЙ ВСЕЛЕННОЙ
                    /// 
                    /// 

                    // -------- ставим крайние правые стенки -------- //
                    for (int i = 0;  i< height; i++)
                    {
                        if ((i!=height-1)||(outside_wall_right))
                        {
                            x = rand.Next(2); // если 0 - то ставим, если 1 - то не ставим
                            // --- Ставим стенку справа --- //
                            if (x==0) 
                            {
                                cells[i,width-1].Put_BottomWall();                       
                            } else if (x==1)
                            // --- Не ставим стенку справа --- //
                            {
                                outside_wall_right = true;
                            }
                        }
                    }


                    // ----------- ставим крайние нижние стенки --------- //
                    for (int j = 0; j<width; j++)
                    {   
                        if ((j!=width-1)||(outside_wall_bottom))
                        {
                            x = rand.Next(2); // если 0 - то ставим, если 1 - то не ставим

                            // --- Ставим стенку cнизу --- //
                            if (x==0) 
                            {
                                cells[height - 1,j].Put_RightWall();             
                            } else if (x==1)
                            // --- Не ставим стенку снизу --- //
                            {
                                outside_wall_bottom = true;
                            }
                        }
                    }
                x = 0; y = 0;  // Начальные координаты
            }
        //------------------------------------------------//

        // ---------------- Конструкторы ---------------- //
            public Room()
            {
                Init(0,0);
            }

            public Room(int _height, int _width)
            {
                Init(_height,_width);
            }
            public Room(int _height, int _width, int _x, int _y)
            {
                Init(_height,_width);
                x = _x;
                y = _y;
            }
        //------------------------------------------------//

        // ------------- Координаты комнаты ------------- //
            public int X()
            {
                return x;
            }

            public void set_x(int _x)
            {
                x = _x;
            }

            public int Y()
            {
                return y;
            }

            public void set_y(int _y)
            {
                y = _y;
            }
        //------------------------------------------------//
        
        // ------------- Параметры комнаты -------------- //
            public int Width()
            {
                return width;
            }

            public int Height()
            {
                return height;
            }
        //------------------------------------------------//

            public Cell Get_Cell(int i, int j)
            {
                return cells[i,j];
            }

    }
}