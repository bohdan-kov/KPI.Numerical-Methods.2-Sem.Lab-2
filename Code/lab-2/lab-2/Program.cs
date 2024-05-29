using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    internal class Program
    {
        private static readonly int j;
        static void PrintSLAR(double[,] Matrix, double[,] Vector, string tmp_unk = "x")
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Math.Round(Matrix[i, j], 2) + $"{tmp_unk}({j + 1})");
                    Console.Write((j < Matrix.GetLength(0) - 1) ? " + " : "");
                }
                Console.WriteLine(" = " + Math.Round(Vector[i, 0], 2));
            }
        }

        static void PrintMatrix(double[,] Matrix)
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Math.Round(Matrix[i, j], 2) + "\t");
                }
                Console.WriteLine();
            }
        }

        static void PrintSep()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 50)));
        }

        static void PrintSepT()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
        }
        static void Main(string[] args)
        {
            PrintSep();
            Console.WriteLine("\tЛабораторна робота #2");
            Console.WriteLine("Виконав студент групи IC-31 Коваль Богдан");
            PrintSep();

            double alpha, k, beta;
            bool Flag_symmetry = true;

            k = 11 - 11;
            alpha = 0.25*k;
            beta = 0.35 * k;


            Console.WriteLine("Змiннi:");
            Console.WriteLine($"k = {k}; alpha = {alpha}; beta = {beta}");
            PrintSep();

            Console.WriteLine("\n\n\tПочаткова матриця системи А:");
            PrintSepT();


            double[,] Matrix_A =
            {
                {5.18 + alpha, 1.12, 0.95, 1.32, 0.83},
                {1.12, 4.28 - alpha, 2.12, 0.57, 0.91},
                {0.95, 2.12, 6.13 + alpha, 1.29, 1.57},
                {1.32, 0.57, 1.29, 4.57 - alpha, 1.25 },
                {0.83, 0.91, 1.57, 1.25, 5.21 + alpha },
            };

            double[,] Vector_b =
            {
                {6.19 + beta},
                {3.21},
                {4.28 - beta},
                {6.25},
                {4.95 + beta},
            };


            /*
            double[,] Matrix_A =
            {
                {2, 1, 4},
                {1, 1, 3,},
                {4, 3, 14},
            };

            double[,] Vector_b =
            {
                {16,},
                {12,},
                {52,},
            };*/


            for (int i = 0; i < Matrix_A.GetLength(0); i++) 
            {
                for (int j = 0; j < Matrix_A.GetLength(1); j++)
                {
                    Console.Write(Math.Round(Matrix_A[i, j], 2) + " | ");
                    if (Matrix_A[i, j] != Matrix_A[j, i]) Flag_symmetry = false;
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n\tПочатковий вектор правої частини b:");
            PrintSepT();
            PrintMatrix(Vector_b);


            Console.WriteLine("\n\tCЛАР:");
            PrintSepT();
            PrintSLAR(Matrix_A, Vector_b);

            Console.WriteLine();
            PrintSepT();
            Console.WriteLine((Flag_symmetry) ? "Maтриця симетрична розв’язання проводити за методом квадратних коренiв" : "Матриця не симетрична використати метод Гауса");
            PrintSepT();
            Console.WriteLine();

            double[,] Matrix_T = new double[Matrix_A.GetLength(0), Matrix_A.GetLength(1)];
            Matrix_T[0, 0] = Math.Sqrt(Matrix_A[0, 0]);
            double[,] Matrix_Ttr = new double[Matrix_A.GetLength(0), Matrix_A.GetLength(1)];

            Console.WriteLine("1.1) Знаходимо елементи t матриць-множникiв:");
            PrintSepT();

            for (int i = 0; i < Matrix_A.GetLength(0); i++)
            {
                for (int j = i; j < Matrix_A.GetLength(1); j++)
                {

                    if (j > 0)
                    {
                        Matrix_T[0, j] = Matrix_A[0, j] / Matrix_T[0, 0];
                    }
                    if (0 < i && i <= Matrix_A.GetLength(0))
                    {
                        double tmp = 0;
                        for(int h  = 0; h <= i - 1; h++)
                        {
                            tmp += (Math.Pow(Matrix_T[h, i], 2));
                        }
                        Matrix_T[i, i] = Math.Sqrt(Matrix_A[i, i] - tmp);
                    }
                    if (i < j)
                    {
                        double temp = 0;
                        for (int p = 0; p <= i - 1; p++)
                        {
                            temp += (Matrix_T[p, i] * Matrix_T[p, j]);
                        }
                        Matrix_T[i, j] = (Matrix_A[i, j] - temp) / (Matrix_T[i, i]);
                    }
                    if (i > j)
                    {
                        Matrix_T[i, j] = 0;
                    }
                }
            }


            PrintMatrix(Matrix_T);


            Console.WriteLine("\nЗнаходимо елементи t` матриць-множникiв:");
            PrintSepT();
            for (int i = 0; i < Matrix_Ttr.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix_T.GetLength(1); j++)
                {
                    Matrix_Ttr[i, j] = Matrix_T[j, i];
                    Console.Write(Math.Round(Matrix_Ttr[i, j], 2) + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\n1.2) Формуємо замiсть вихiдної системи двi наступнi системи:");
            Console.WriteLine("CЛАР T'y=b:");
            PrintSepT();
            PrintSLAR(Matrix_Ttr, Vector_b, "y");


            double[] Vector_y = new double[Matrix_T.GetLength(0)];
            double tempSum;


            Console.WriteLine('\n');
            PrintSep();
            Console.WriteLine("\n2) Зворотний хiд.\n");

            Console.WriteLine("2.1) Послiдовно знаходимо:");
            PrintSepT();

            Console.WriteLine("\n2) Розв'язання системи U^T * Y = B");
            PrintSepT();
            for (int i = 0; i < Vector_b.GetLength(0); i++)
            {
                if (i >= 1)
                {
                    tempSum = 0;
                    for (int j = 0; j <= i - 1; j++)
                    {
                        tempSum += Matrix_Ttr[i, j] * Vector_y[j];

                    }
                    Vector_y[i] = (Vector_b[i, 0] - tempSum) / Matrix_Ttr[i, i];
                } else
                {
                    Vector_y[i] = Vector_b[0, i] / Matrix_Ttr[i, i];
                }
                Console.WriteLine($"y{i + 1} = {Vector_y[i]}");
            }

            double[] Vector_x = new double[Matrix_T.GetLength(0)];
            int n = Matrix_T.GetLength(0) - 1;

            Console.WriteLine("\n3) Розв'язання системи U * X = Y");
            PrintSepT();

            for (int i = n; i >= 0; i--)
            {

                if (i < n)
                {
                    tempSum = 0;
                    for (int j = i + 1; j <= n; j++)
                    {
                        tempSum += Matrix_T[i, j] * Vector_x[j];
                                
                    }
                    Vector_x[i] = (Vector_y[i] - tempSum) / Matrix_T[i, i];
                }
                else
                {
                    Vector_x[i] = Vector_y[i] / Matrix_T[i, i];
                }
                //Console.WriteLine($"x{i + 1} = {Vector_x[i]}");
            }

            for (int i = 0; i < Vector_x.GetLength(0); i++)
            {
                Console.WriteLine($"x{i+1} = {Vector_x[i]}");
            }


            Console.WriteLine("\nРезультати перевiрок: вектор нев’язки r = b – Ax");
            PrintSepT();
            double sumElem;
            int LenghtVec = Vector_x.GetLength(0);
            double[] Vector_tmp = new double[LenghtVec];
            Console.WriteLine("1. Спершу знаходимо Ax:");
            for (int i = 0; i < LenghtVec; i++)
            {
                sumElem = 0;
                for (int j = 0; j < LenghtVec;  j++)
                {
                    sumElem += Matrix_A[i, j] * Vector_x[j];
                }
                Vector_tmp[i] = sumElem;
                Console.WriteLine(Vector_tmp[i]);
            }

            Console.WriteLine("\n2. Вiднiмаємо:");
            for (int i = 0; i <= n; i++)
            {
                Console.WriteLine(Vector_b[i, 0] - Vector_tmp[i]);
            }

            double fault;
            double tmp_suma = 0;
            int nLenght = Vector_x.GetLength(0);
            double[] Vector_xMad = new double[] { 0.7983789, 0.2245311, 0.172232, 0.9206449, 0.5109044 };

            Console.WriteLine("\nСередньоквадратичної похибки гамма:");
            for(int i = 0; i < nLenght; i++)
            {
                tmp_suma += Math.Pow((Vector_x[i] - Vector_xMad[i]), 2); 
            }
            fault = Math.Sqrt(0.25 * tmp_suma);
            Console.WriteLine(fault);
        }
    }
}
