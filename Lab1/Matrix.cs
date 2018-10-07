using System;
using System.Text;

namespace Lab1
{
    public class Matrix
    {
        
        private readonly double[,] _matrix;

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public int Height => _matrix.GetLength(0);
        public int Width => _matrix.GetLength(1);

        public double this[int row, int col]
        {
            get => _matrix[row, col];
            set
            {
                if (col <= Width && row <= Height)
                    _matrix[row, col] = value;
            }
        }

        public bool IsTriangular()
        {
            for (var i = 0; i < Height; i++)
            for (var j = 0; j < i; j++)
                if (!NumericComparer.Compare(_matrix[i, j], 0))
                    return false;
            return true;
        }

        public double GetDeterminant()
        {
            if (!IsTriangular()) throw new InvalidOperationException("Матрица не треугольная");
            double determinant = 1;
            for (var i = 0; i < Height; i++) determinant *= _matrix[i, i];
            return determinant;
        }

        public static Matrix Parse(string input, int height)
        {
            var width = height + 1;
            var result = new double[height, width];

            var elements = input.Split(new[] {' ', '\t', '\n', '\r'},
                StringSplitOptions.RemoveEmptyEntries);

            if (elements.Length != width * height)
            {
                throw new ArgumentException("Неверное кол-во элементов.");
            }

            var parseError = false;
            for (var row = 0; row < height && !parseError; row++)
            for (var col = 0; col < width && !parseError; col++)
                if (!double.TryParse(elements[col + row * width], out result[row, col]))
                    parseError = true;

            if (parseError)
            {
                throw new ArgumentException("Ошибка: Не удалось перевести строку в число");
            }

            return new Matrix(result);
        }

        public void AddRow(int srcRowIndex, int destRowIndex, double coefficient)
        {
            for (var col = 0; col < Width; col++)
                this[destRowIndex, col] += this[srcRowIndex, col] * coefficient;
        }

        public decimal[] GetErrors(double[] solution)
        {
            var errorVector = new decimal[Height];
            for (var y = 0; y < Height; y++)
            {
                decimal sum = 0;

                for (var x = y; x < Width - 1; x++)
                    sum += (decimal) (this[y, x] * solution[x]);

                errorVector[y] = sum - (decimal) this[y, Width - 1];
            }

            return errorVector;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    result.Append(_matrix[i, j].ToString("0.#######"));
                    result.Append(' ');
                }

                result.Append('\n');
            }

            return result.ToString();
        }
    }
}