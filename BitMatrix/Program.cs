using System;
using System.Collections;


namespace BitMatrixNameSpace
{
    class Testy
    {
        static void Main()
        {
    
        }
    }
    public partial class BitMatrix
    {
        private BitArray data;
        public int NumberOfRows { get; }
        public int NumberOfColumns { get; }
        public bool IsReadOnly => false;

        public BitMatrix(int numberOfRows, int numberOfColumns, int defaultValue = 0)
        {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentOutOfRangeException("Incorrect size of matrix");

            data = new BitArray(numberOfRows * numberOfColumns, BitToBool(defaultValue));
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
        }

        public static int BoolToBit(bool boolValue) => boolValue ? 1 : 0;
        public static bool BitToBool(int bit) => bit != 0;


    } //def
    public partial class BitMatrix
    {
        public override string ToString()
        {
            {
                string output = "";
                for (int i = 0; i < this.NumberOfRows; i++)
                {
                    for (int j = 0; j < this.NumberOfColumns; j++)
                    {
                        output += BoolToBit(data[i * NumberOfColumns + j]).ToString();
                    }
                    output += Environment.NewLine;
                }
                return output;
            }
        }
    } //zad1
    public partial class BitMatrix
    {
        public BitMatrix(int numberOfRows, int numberOfColumns, params int[] bits)
        {
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentOutOfRangeException("Incorrect size of matrix");

            int totalBits = numberOfRows * numberOfColumns;
            data = new BitArray(totalBits);

            if (bits != null && bits.Length > 0)
            {
                for (int i = 0; i < totalBits; i++)
                {
                    if (i < bits.Length)
                    {
                        data[i] = BitToBool(bits[i]);
                    }
                    else
                    {
                        data[i] = false;
                    }
                }
            }

            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;
        }
        public BitMatrix(int[,] bits)
        {
            if (bits == null)
            {
                throw new NullReferenceException("input array is null.");
            }

            int numberOfRows = bits.GetLength(0);
            int numberOfColumns = bits.GetLength(1);

            if (numberOfRows == 0 || numberOfColumns == 0)
            {
                throw new ArgumentOutOfRangeException("Input array has no elements.");
            }

            data = new BitArray(numberOfRows * numberOfColumns);
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    data[i * numberOfColumns + j] = BitToBool(bits[i, j]);
                }
            }
        }
        public BitMatrix(bool[,] bits)
        {
            if (bits == null)
            {
                throw new NullReferenceException("input array is null.");
            }

            int numberOfRows = bits.GetLength(0);
            int numberOfColumns = bits.GetLength(1);

            if (numberOfRows == 0 || numberOfColumns == 0)
            {
                throw new ArgumentOutOfRangeException("Input array has no elements.");
            }

            data = new BitArray(numberOfRows * numberOfColumns);
            NumberOfRows = numberOfRows;
            NumberOfColumns = numberOfColumns;

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    data[i * numberOfColumns + j] = bits[i, j];
                }
            }
        }


    } //zad2
    public partial class BitMatrix : IEquatable<BitMatrix>
    {
        public bool Equals(BitMatrix other)
        {
            if (other == null || NumberOfRows != other.NumberOfRows || NumberOfColumns != other.NumberOfColumns)
            {
                return false;
            }

            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] != other.data[i])
                {
                    return false;
                }
            }

            return true;
        }


        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BitMatrix)obj);

        }
        public override int GetHashCode()
        {
            return NumberOfRows.GetHashCode() ^ NumberOfColumns.GetHashCode();
        }
        ///

    } //zad3
    public partial class BitMatrix : IEnumerable<int>
    {
        public int this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= NumberOfRows || column < 0 || column >= NumberOfColumns)
                    throw new IndexOutOfRangeException("Invalid indices");

                return data[row * NumberOfColumns + column] ? 1 : 0;
            }
            set
            {
                if (row < 0 || row >= NumberOfRows || column < 0 || column >= NumberOfColumns)
                    throw new IndexOutOfRangeException("Invalid indices");

                data[row * NumberOfColumns + column] = (value != 0);
            }
        }
        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    yield return this[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    } //zad4
    public partial class BitMatrix : ICloneable
    {
        public object Clone()
        {
            var newData = new BitArray(data);
            return new BitMatrix(NumberOfRows, NumberOfColumns) { data = newData };
        }
    } //zad5
    public partial class BitMatrix
    {
        public static BitMatrix Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException(nameof(s));
            }

            var rows = s.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length == 0)
            {
                throw new FormatException("FormatException");
            }

            var numColumns = rows[0].Length;
            for (int i = 1; i < rows.Length; i++)
            {
                if (rows[i].Length != numColumns)
                {
                    throw new FormatException("FormatException");
                }
            }

            var numRows = rows.Length;
            var matrix = new BitMatrix(numRows, numColumns);

            for (int i = 0; i < numRows; i++)
            {
                var row = rows[i];

                for (int j = 0; j < numColumns; j++)
                {
                    var c = row[j];

                    if (c != '0' && c != '1')
                    {
                        throw new FormatException("FormatException");
                    }

                    if (c == '1')
                    {
                        matrix[i, j] = 1;
                    }
                }
            }

            int NumberOfRows = s.Split('\n').Length;
            int NumberOfColumns = 0;

            foreach (char c in s)
            {
                if (c == '0' || c == '1')
                {
                    NumberOfColumns++;
                }
            }
            NumberOfColumns = NumberOfColumns / NumberOfRows;

            s = s.Replace(Environment.NewLine, "");
            int[] bits = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                bits[i] = int.Parse(s[i].ToString());
            }


            var result = new BitMatrix(NumberOfRows, NumberOfColumns, bits);
            return result;
        }
        public static bool TryParse(string s, out BitMatrix result)
        {
            result = null;
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            var rows = s.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length == 0)
            {
                return false;
            }

            var numColumns = rows[0].Length;
            for (int i = 1; i < rows.Length; i++)
            {
                if (rows[i].Length != numColumns)
                {
                    return false;
                }
            }

            var numRows = rows.Length;
            var matrix = new BitMatrix(numRows, numColumns);

            for (int i = 0; i < numRows; i++)
            {
                var row = rows[i];

                for (int j = 0; j < numColumns; j++)
                {
                    var c = row[j];

                    if (c != '0' && c != '1')
                    {
                        return false;
                    }

                    if (c == '1')
                    {
                        matrix[i, j] = 1;
                    }
                }
            }

            int NumberOfRows = s.Split('\n').Length;
            int NumberOfColumns = 0;

            foreach (char c in s)
            {
                if (c == '0' || c == '1')
                {
                    NumberOfColumns++;
                }
            }
            NumberOfColumns = NumberOfColumns / NumberOfRows;

            s = s.Replace(Environment.NewLine, "");
            int[] bits = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                bits[i] = int.Parse(s[i].ToString());
            }

            result = new BitMatrix(NumberOfRows, NumberOfColumns, bits);
            return true;
        }

    } //zad6
    public partial class BitMatrix
    {

        public static explicit operator BitMatrix(int[,] intArray)
        {
            if (intArray == null)
                throw new NullReferenceException("NullReferenceException");

            int numberOfRows = intArray.GetLength(0);
            int numberOfColumns = intArray.GetLength(1);
            if (numberOfRows < 1 || numberOfColumns < 1)
                throw new ArgumentOutOfRangeException("ArgumentOutOfRangeException");

            BitMatrix bitMatrix = new BitMatrix(numberOfRows, numberOfColumns);

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    bitMatrix[i, j] = Convert.ToInt32(intArray[i, j] != 0);
                }
            }

            return bitMatrix;
        }
        public static explicit operator bool[,](BitMatrix bitMatrix)
        {
            if (bitMatrix == null)
                throw new NullReferenceException("NullReferenceException");

            int numberOfRows = bitMatrix.NumberOfRows;
            int numberOfColumns = bitMatrix.NumberOfColumns;
            bool[,] boolArray = new bool[numberOfRows, numberOfColumns];

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    boolArray[i, j] = BitToBool(Convert.ToInt32(bitMatrix[i, j]));
                }
            }

            return boolArray;
        }

    } //zad7 niedokonczon
}




    

    
