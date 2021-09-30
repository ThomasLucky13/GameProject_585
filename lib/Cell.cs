using System;

namespace n_Cell
{
    public class Cell
    {
        private bool right_wall, left_wall;    // ------------ Правая и левая стенка ------------ //
        private bool bottom_wall, top_wall;    // ----------- Верхняя и нижняя стенки ----------- //
        private uint multiplicity;              // --- Множество к которому принадлежит клетка --- //

        // ---------------- Конструкторы ---------------- //
            public Cell()
            {     
                right_wall = false;
                bottom_wall = false;
                left_wall = false;
                top_wall = false;
                multiplicity = 0;
            }
        //------------------------------------------------//

        // ---------------- Правая стенка --------------- //
            
            // ----- Получение значения ----- //
            public bool RightWall()
            {
                return right_wall;
            }

            // ------ Поставить стенку ------ //
            public void Put_RightWall()
            {
                right_wall = true;
            }

            // -------- Убрать стенку ------- //
            public void Remove_RightWall()
            {
                right_wall = false;
            }

            // --------- Вывод стенки ------- //
            public void Display_RightWall()
            {
                if (right_wall) Console.Write($" |");
                else Console.Write($"  ");
            }
            
        //------------------------------------------------//

        // ---------------- Левая стенка ---------------- //
            
            // ----- Получение значения ----- //
            public bool LeftWall()
            {
                return left_wall;
            }

            // ------ Поставить стенку ------ //
            public void Put_LeftWall()
            {
                left_wall = true;
            }

            // -------- Убрать стенку ------- //
            public void Remove_LeftWall()
            {
                left_wall = false;
            }

            // --------- Вывод стенки ------- //
            public void Display_LeftWall()
            {
                if (left_wall) Console.Write($"| ");
                else Console.Write($"  ");
            }
        //------------------------------------------------//

        // --------------- Верхняя стенка --------------- //
            
            // ----- Получение значения ----- //
            public bool TopWall()
            {
                return top_wall;
            }

            // ------ Поставить стенку ------ //
            public void Put_TopWall()
            {
                top_wall = true;
            }

            // -------- Убрать стенку ------- //
            public void Remove_TopWall()
            {
                top_wall = false;
            }

            // --------- Вывод стенки ------- //
            public void Display_TopWall()
            {
                if (top_wall) Console.Write("__");
                else Console.Write("  ");
            }
        //------------------------------------------------//

        // ---------------- Нижняя стенка --------------- //
            
            // ----- Получение значения ----- //
            public bool BottomWall()
            {
                return bottom_wall;
            }

            // ------ Поставить стенку ------ //
            public void Put_BottomWall()
            {
                bottom_wall = true;
            }

            // -------- Убрать стенку ------- //
            public void Remove_BottomWall()
            {
                bottom_wall = false;
            }

            // --------- Вывод стенки ------- //
            public void Display_BottomWall()
            {
                if (bottom_wall) Console.Write("__");
                else Console.Write("  ");
            }
        //------------------------------------------------//

        // ----------------- Множество ------------------ //
            // ---- Получение множества ---- //
            public uint Multiplicity()
            {
                return multiplicity;
            }

            // ---- Установить множество --- //
            public void set_multiplicity(uint _multiplicity)
            {
                if (_multiplicity >= 0) multiplicity = _multiplicity;
            }
        //------------------------------------------------//
    }

}