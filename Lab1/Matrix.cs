using System;
using System.Text;

namespace GaussMethod
{
    public class Matrix
    {
        private double[,] _matrix;
        public int Height { get => _matrix.GetLength(0); }
        public int Width { get => _matrix.GetLength(1); }

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public double this[int row, int col]
        {
            get => _matrix[row, col];
            set
            {
                if (col <= Width && row <= Height)
                    _matrix[row, col] = value;
            }
        }
        
        public bool IsTriangular
        {
            get
            {
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (_matrix[i, j] != 0)
                            return false;
                    }
                }
                return true;
            }
        }

        public double Determinant
        {
            get
            {
                if (Width != Height) return this.BaseMatrix.Determinant;

                if (Width == 2) return (this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0]);

                double det = 0;
                for (int i = 0; i < Width; i++)
                    det += this[0, i] * getMinor(0, i) * Math.Pow(-1, i % 2);

                return det;
            }
        }

        private Matrix BaseMatrix
        {
            get
            {
                int size = Math.Min(Width, Height);
                var baseContents = new double[size, size];
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        baseContents[row, col] = this[row, col];
                    }
                }

                return new Matrix(baseContents);
            }
        }


        public static Matrix Parse(string input, int height, out string message)
        {
            int width = height + 1;
            var result = new double[height, width];

            var elements = input.Split(new char[] { ' ', '\t', '\n', '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != width * height)
            {
                message = "Ошибка: Неверное кол-во элементов.";
                return null;
            }

            bool ParseError = false;
            for (int row = 0; row < height && !ParseError; row++)
            {
                for (int col = 0; col < width && !ParseError; col++)
                {

                    if (!double.TryParse(elements[col + row * width], out result[row, col]))
                    {
                        ParseError = true;
                    }
                }
            }

            if (ParseError)
            {
                message = "Ошибка: Не удалось перевести строку в число";
                return null;
            }

            message = "";
            return new Matrix(result);
        }

        public void AddRow(int srcRowIndex, int destRowIndex, double coef)
        {
            for (int col = 0; col < Width; col++)
                this[destRowIndex, col] += this[srcRowIndex, col] * coef;
        }

        public decimal[] GetErrors(double[] solution)
        {
            var errorVector = new decimal[Height];
            for (int y = 0; y < Height; y++)
            {
                decimal sum = 0;

                for (int x = y; x < Width - 1; x++)
                    sum += (decimal)(this[y, x] * solution[x]);

                errorVector[y] = sum - (decimal)this[y, Width - 1];
            }

            return errorVector;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    result.Append(_matrix[i, j].ToString("0.#######"));
                    result.Append(' ');
                }
                result.Append('\n');
            }
            return result.ToString();
        }

        private double getMinor(int excludedRow, int excludedCol)
        {
            var minorMatrix = new Matrix(new double[Width - 1, Width - 1]);

            int targetRow = 0, targetCol = 0;
            for (int srcRow = 0; srcRow < Width; srcRow++)
            {
                if (srcRow == excludedRow) continue;

                for (int srcCol = 0; srcCol < Height; srcCol++)
                {
                    if (srcCol == excludedCol) continue;

                    minorMatrix[targetRow, targetCol] = this[srcRow, srcCol];
                    targetCol++;
                }

                targetRow++;
                targetCol = 0;
            }

            return minorMatrix.Determinant;
        }

        
    }
}