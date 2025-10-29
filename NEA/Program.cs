using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        const int Max = 5;
        static double[][,] matrices = new double[6][,]; // 6 slots 1-3 normal matrices 4-6 inverse

        static void Main(string[] args)
        {
            Console.WriteLine("Matrix Calculator Press Enter to Continue!!");

            while (true)
            {
                Console.ReadLine();
                Console.Clear();
                Console.WriteLine("\nHere is what we can do for you:");
                Console.WriteLine("1. Input");
                Console.WriteLine("2. Addition");
                Console.WriteLine("3. Subtraction");
                Console.WriteLine("4. Multiplication");
                Console.WriteLine("5. Inverse of Matrix");
                Console.WriteLine("6. Determinant of Matrix");
                Console.WriteLine("7. Show Matrices");
                Console.Write("Choose option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Input(); break;
                    case "2": Add(); break;
                    case "3": Subtract(); break;
                    case "4": Multiply(); break;
                    case "5": Special(choice); break;
                    case "6": Special(choice); break;
                    case "7": Display(); break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void Input()
        {
            Console.Write("Select matrix (1-6): ");
            if (!int.TryParse(Console.ReadLine(), out int matrixNumber) || matrixNumber < 1 || matrixNumber > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            matrixNumber--;

            Console.Write("Enter rows (Maximum of 5): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows < 1 || rows > Max)
            {
                Console.WriteLine("Invalid row size.");
                return;
            }

            Console.Write("Enter columns (Maximum of 5): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols < 1 || cols > Max)
            {
                Console.WriteLine("Invalid column size.");
                return;
            }

            double[,] matrix = new double[rows, cols];
            Console.WriteLine($"Enter elements for Matrix{matrixNumber + 1}:");
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"[Value at {i},{j}]: ");
                    if (!double.TryParse(Console.ReadLine(), out double value))
                    {
                        Console.WriteLine("Invalid number. Please enter a valid number.");
                        j--;
                        continue;
                    }
                    matrix[i, j] = value;
                }
            }

            matrices[matrixNumber] = matrix;
            Console.WriteLine($"Matrix{matrixNumber + 1} has been stored.");
        }

        static void Display()
        {
            for (int i = 0; i < matrices.Length; i++)
            {
                if (matrices[i] == null)
                {
                    Console.WriteLine($"Matrix{i + 1} has not been set.");
                    continue;
                }

                string matrixType = (i >= 3) ? " (Inverse)" : ""; 
                Console.WriteLine($"\nMatrix{i + 1}{matrixType} ({matrices[i].GetLength(0)}x{matrices[i].GetLength(1)}):");
                PrintMatrix(matrices[i]);
            }
        }

        //update to allow 6
        static bool MatrixValidity(int matrixNumber)
        {
            if (matrixNumber < 0 || matrixNumber > 5 || matrices[matrixNumber] == null) 
            {
                Console.WriteLine("Matrix does not exist!");
                return false;
            }
            return true;
        }

        static void Add()
        {
            Console.Write("Select first matrix (1-6): "); 
            if (!int.TryParse(Console.ReadLine(), out int m1) || m1 < 1 || m1 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m1--;

            Console.Write("Select second matrix (1-6): "); 
            if (!int.TryParse(Console.ReadLine(), out int m2) || m2 < 1 || m2 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m2--;

            if (!MatrixValidity(m1) || !MatrixValidity(m2)) return;

            var A = matrices[m1];
            var B = matrices[m2];

            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            {
                Console.WriteLine("Addition not possible (mismatch in size).");
                return;
            }

            double[,] result = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    result[i, j] = A[i, j] + B[i, j];

            Console.WriteLine("\nResults:");
            PrintMatrix(result);
        }

        static void Subtract()
        {
            Console.Write("Select first matrix (1-6): "); // Updated to 6
            if (!int.TryParse(Console.ReadLine(), out int m1) || m1 < 1 || m1 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m1--;

            Console.Write("Select second matrix (1-6): "); // Updated to 6
            if (!int.TryParse(Console.ReadLine(), out int m2) || m2 < 1 || m2 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m2--;

            if (!MatrixValidity(m1) || !MatrixValidity(m2)) return;

            var A = matrices[m1];
            var B = matrices[m2];

            if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
            {
                Console.WriteLine("Subtraction not possible (mismatch in size).");
                return;
            }

            double[,] result = new double[A.GetLength(0), A.GetLength(1)];
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                    result[i, j] = A[i, j] - B[i, j];

            Console.WriteLine("\nResults:");
            PrintMatrix(result);
        }

        static void Multiply()
        {
            Console.Write("Select first matrix (1-6): "); // Updated to 6
            if (!int.TryParse(Console.ReadLine(), out int m1) || m1 < 1 || m1 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m1--;

            Console.Write("Select second matrix (1-6): "); // Updated to 6
            if (!int.TryParse(Console.ReadLine(), out int m2) || m2 < 1 || m2 > 6)
            {
                Console.WriteLine("Invalid matrix number.");
                return;
            }
            m2--;

            if (!MatrixValidity(m1) || !MatrixValidity(m2)) return;

            var A = matrices[m1];
            var B = matrices[m2];

            if (A.GetLength(1) != B.GetLength(0))
            {
                Console.WriteLine("Multiplication not possible.");
                return;
            }

            double[,] result = new double[A.GetLength(0), B.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < result.GetLength(1); j++)
                    for (int k = 0; k < A.GetLength(1); k++)
                        result[i, j] += A[i, k] * B[k, j];

            Console.WriteLine("\nResult of Multiplication:");
            PrintMatrix(result);
        }

        static void PrintMatrix(double[,] mat)
        {
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                    Console.Write($"{mat[i, j]:F2}\t");
                Console.WriteLine();
            }
        }

        static void Special(string choice)
        {
            Console.Write("Select matrix (1-3): "); // Only  matrices 1-3 for "special operations"
            if (!int.TryParse(Console.ReadLine(), out int m1) || m1 < 1 || m1 > 3)
            {
                Console.WriteLine("Invalid matrix number. Only matrices 1-3 can be used for this operation.");
                return;
            }
            m1--;

            if (!MatrixValidity(m1)) return;

            var A = matrices[m1];

            if (A.GetLength(0) != A.GetLength(1))
            {
                Console.WriteLine("Operation not possible because it is not a square matrix.");
                return;
            }

            switch (choice)
            {
                case "5":
                    Inverse(A);
                    break;
                case "6":
                    double det = Determinant(A);
                    Console.WriteLine($"\nDeterminant: {det:F2}");
                    break;
            }
        }

        static void Inverse(double[,] matrix)
        {
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            double det = Determinant(matrix);
            if (Math.Abs(det) <= 0)
            {
                Console.WriteLine("Matrix determinant = 0. Inverse cannot be calculated.");
                return;
            }

            // Creating clones to avoid changing original matrix
            double[,] CloneMatrix = (double[,])matrix.Clone();
            double[,] id = new double[row, row];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (i == j)
                        id[i, j] = 1;
                    else
                        id[i, j] = 0;
                }
            }

            for (int i = 0; i < row; i++)
            {
                double pivot = CloneMatrix[i, i];
                if (pivot != 0)
                {
                    // Use workingMatrix instead of matrix
                    for (int j = 0; j < col; j++)
                    {
                        CloneMatrix[i, j] /= pivot;
                        id[i, j] /= pivot;
                    }

                    for (int j = 0; j < row; j++)
                    {
                        if (i != j)
                        {
                            double multiplier = CloneMatrix[j, i];
                            for (int k = 0; k < col; k++)
                            {
                                CloneMatrix[j, k] -= CloneMatrix[i, k] * multiplier;
                                id[j, k] -= id[i, k] * multiplier;
                            }
                        }
                    }
                }
                else //pivot = 0 row swap
                {
                    for (int j = 0; j < col; j++)
                    {
                        double temp = CloneMatrix[i, j];
                        CloneMatrix[i, j] = CloneMatrix[i + 1, j];
                        CloneMatrix[i + 1, j] = temp;
                        CloneMatrix[i, j] /= pivot;
                        id[i, j] /= pivot;
                    }

                    for (int j = 0; j < row; j++)
                    {
                        if (i != j)
                        {
                            double multiplier = CloneMatrix[j, i];
                            for (int k = 0; k < col; k++)
                            {
                                CloneMatrix[j, k] -= CloneMatrix[i, k] * multiplier;
                                id[j, k] -= id[i, k] * multiplier;
                            }
                        }
                    }
                }
            }

            double[,] inverse = new double[row, row];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < row; j++)
                    inverse[i, j] = id[i, j];

            Console.WriteLine("\nInverse Matrix Result: ");
            PrintMatrix(inverse);

            // Store in matrices 4, 5, or 6
            Console.Write("\nWould you like to store this inverse matrix? (y/n): ");
            string Choice = Console.ReadLine();
            if (Choice.ToLower() == "y")
            {
                Console.Write("Select matrix slot to store inverse (4-6): ");
                if (int.TryParse(Console.ReadLine(), out int slot) && slot >= 4 && slot <= 6)
                {
                    matrices[slot - 1] = inverse;
                    Console.WriteLine($"Inverse matrix stored in Matrix{slot}");
                }
                else
                {
                    Console.WriteLine("Invalid slot selection. Please choose 4, 5, or 6.");
                }
            }
        }

        static double Determinant(double[,] A)
        {
            int n = A.GetLength(0);

            if (n == 1)
            {
                return A[0, 0];
            }

            if (n == 2)
            {
                return A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
            }

            double det = 0;
            for (int j = 0; j < n; j++)
            {
                double sign = ((0 + j) % 2 == 0) ? 1 : -1;
                det += sign * A[0, j] * Determinant(GetMinor(A, 0, j));
            }
            return det;
        }

        static double[,] GetMinor(double[,] matrix, int row, int col)
        {
            int n = matrix.GetLength(0);
            double[,] minor = new double[n - 1, n - 1];

            int tempi = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == row) continue;

                int tempj = 0;
                for (int j = 0; j < n; j++)
                {
                    if (j == col) continue;
                    minor[tempi, tempj] = matrix[i, j];
                    tempj++;
                }
                tempi++;
            }
            return minor;
        }
    }
}