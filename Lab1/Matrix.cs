using System;

namespace GaussMethod
{
    public class Matrix
    {
        private double[,] _matrix;
        public int Width { get => _matrix.GetLength(1); }
        public int Height { get => _matrix.GetLength(0); }

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public double this[int col, int row]
        {
            get => _matrix[col, row];
            set
            {
                if (col <= Width && row <= Height)
                    _matrix[col, row] = value;
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
                        baseContents[col, row] = this[col, row];
                    }
                }

                return new Matrix(baseContents);
            }
        }

        public bool IsTriangular
        {
            get
            {  
                for (int col = 0; col < Math.Min(Width, Height) - 1; col++)
                {
                    for (int row = col + 1; row < Height; row++)
                    {
                        if (!NumericComparer.Compare(this[col, row], 0)) return false;
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
                for (int n = 0; n < Width; n++)
                    det += this[n, 0] * getMinor(n, 0) * Math.Pow(-1, n % 2);

                return det;
            }
        }

        private double getMinor(int excludedCol, int excludedRow)
        {
            var minorMatrix = new Matrix(new double[Width - 1, Width - 1]);

            int targetRow = 0, targetCol = 0;
            for (int srcRow = 0; srcRow < Width; srcRow++)
            {
                if (srcRow == excludedRow) continue;

                for (int srcCol = 0; srcCol < Height; srcCol++)
                {
                    if (srcCol == excludedCol) continue;

                    minorMatrix[targetCol, targetRow] = this[srcCol, srcRow];
                    targetCol++;
                }

                targetRow++;
                targetCol = 0;
            }

            return minorMatrix.Determinant;
        }

        public static Matrix Parse(string input, int height, out string message)
        {
            int width = height + 1;
            var result = new double[width, height];

            var elements = input.Split(new char[] { ' ', '\t', '\n'}, 
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

                    if (!double.TryParse(elements[col + row * width], out result[col, row]))
                    {
                        ParseError = true;
                    }
                }
            }

            if (ParseError)
            {
                message = "Ошибка: Не удалось перевести строку в число";
            }

            message = "";
            return new Matrix(result);
        }

        public void AddRow(int srcRowIndex, int destRowIndex, double coef)
        {
            for (int col = 0; col < Width; col++)
                this[col, destRowIndex] += this[col, srcRowIndex] * coef;
        }

        public decimal[] GetErrors(double[] solution)
        {
            var errorVector = new decimal[Height];
            for (int y = 0; y < Height; y++)
            {
                decimal sum = 0;

                for (int x = y; x < Width - 1; x++)
                    sum += (decimal)(this[x, y] * solution[x]);

                errorVector[y] = sum - (decimal)this[Width - 1, y];
            }

            return errorVector;
        }


    }
}