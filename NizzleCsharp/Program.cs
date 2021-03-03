using System;

namespace NizzleCsharp
{
    class Program
    {
        static public int[,] CreateMatrix()
        {
            int[,] matrix = new int[3, 3];
            int d = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0; l < 3; l++)
                {
                    matrix[i, l] = d;
                    d++;
                }
            }
            return matrix;
        }

        static public void Show(int[,] matrix)
        {
            Console.WriteLine($"  4 5 6");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{i+1} {matrix[i, 0]}|{matrix[i, 1]}|{matrix[i, 2]}");
            }
        }

        static public int[,] MovX(int[,] matrix, int row, int direct)
        {
            row -= 1;
            if (direct >= 0)
            {
                int buffer = matrix[row, 2];
                matrix[row, 2] = matrix[row, 1];
                matrix[row, 1] = matrix[row, 0];
                matrix[row, 0] = buffer;
            }
            else if (direct < 0)
            {
                int buffer = matrix[row, 0];
                matrix[row, 0] = matrix[row, 1];
                matrix[row, 1] = matrix[row, 2];
                matrix[row, 2] = buffer;
            }
            return matrix;
        }

        static public int[,] MovY(int[,] matrix, int column, int direct)
        {
            column -= 1;
            if (direct >= 0)
            {   
                int buffer = matrix[2, column];
                matrix[2, column] = matrix[1, column];
                matrix[1, column] = matrix[0, column];
                matrix[0, column] = buffer;
            }
            else if (direct < 0)
            {
                int buffer = matrix[0, column];
                matrix[0, column] = matrix[1, column];
                matrix[1, column] = matrix[2, column];
                matrix[2, column] = buffer;
            }
            return matrix;
        }

        static public int Start()
        {
            string banner = "";
            banner += "               ,,                    ,,          \n";
            banner += "`7MN.   `7MF'  db                  `7MM          \n";
            banner += "  MMN.    M                          MM          \n";
            banner += "  M YMb   M  `7MM  M\"\"\"MMV M\"\"\"MMV   MM   .gP\"Ya \n";
            banner += "  M  `MN. M    MM  '  AMV  '  AMV    MM  ,M'   Yb\n";
            banner += "  M   `MM.M    MM    AMV     AMV     MM  8M\"\"\"\"\"\"\n";
            banner += "  M     YMM    MM   AMV  ,  AMV  ,   MM  YM.    ,\n";
            banner += ".JML.    YM  .JMML.AMMmmmM AMMmmmM .JMML. `Mbmmd'\n";
            Console.WriteLine(banner);
            Console.WriteLine("Укажите уровень сложности\n(1 - Легкий, 2 - Средний, 3 - Сложный): ");
            int level;
            string input_level;
            while (true)
            {   
                input_level = Console.ReadLine();
                if (input_level == "") {
                    level = 2;
                    break;
                }
                else if ((input_level == "1") || (input_level == "2") || (input_level == "3")) {
                    level = Int32.Parse(input_level);
                    break;
                }
                else
                {
                    Console.WriteLine("(1 - Легкий, 2 - Средний, 3 - Сложный): ");
                }
            }
            return level;
        }

        static public int[,] Generaton(int[,] matrix, int level)
        {
            level = (level + 1) * 2;
            while (level > 0) {
                Random rnd = new Random();
                int TF = rnd.Next(1, 2);
                int PM = rnd.Next(1, 2);
                int CM = rnd.Next(1, 3);
                if (TF == 0)
                {
                    MovX(matrix, CM, PM);
                }
                else if (TF == 1)
                {
                    MovY(matrix, CM, PM);
                }
                level -= 1;
            }
            return matrix;
        }

        static public bool ComparingArrays(int[,] array1, int[,] array2)
        {
            bool TF = true;
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0; l < 3; l++)
                {
                    if (array1[i, l] != array2[i, l])
                    {
                        TF = false;
                    }
                }
            }
            return TF;
        }
        static public void Game(int level)
        {
            int[,] answer = CreateMatrix();
            int[,] matrix = CreateMatrix();
            matrix = Generaton(matrix, level);
            DateTime starttime = DateTime.Now;
            while (!ComparingArrays(matrix, answer))
            {
                Console.Clear();
                Show(matrix);
                Console.WriteLine("Укажите номер поворота: ");
                string rotate_input = Console.ReadLine();
                int PM;
                int RT = Int32.Parse(rotate_input);
                while (true)
                {
                    try
                    {
                        if (RT < 0)
                        {
                            PM = 0;
                            RT = -RT;
                        }
                        else
                        {
                            PM = 1;
                        }
                        if ((RT <= 3 * 2) && (RT > 0))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Укажите корректный номер: ");
                            rotate_input = Console.ReadLine();
                        }
                    }
                    catch{
                        Console.WriteLine("Укажите корректный номер: ");
                        rotate_input = Console.ReadLine();
                    }
                }
                if (RT > 3)
                {
                    RT = RT - 3;
                    MovY(matrix, RT, PM);
                }
                else
                {
                    MovX(matrix, RT, PM);
                }      
            }
            DateTime endtime = DateTime.Now;
            Console.Clear();
            Show(matrix);
            Console.WriteLine("Поздравляем! Вы справились с головоломкой!");
            Console.WriteLine($"Потраченное время: {endtime - starttime}");
        }


        static public void Main()
        {
            int level = Start();
            Game(level);
        }
    }
}
