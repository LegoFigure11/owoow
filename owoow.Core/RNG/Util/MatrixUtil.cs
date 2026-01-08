namespace owoow.Core.RNG;

// Adapted from https://github.com/Lincoln-LM/swsh-initial-seed/blob/main/matrix_utility.py

public static class MatrixUtil
{
    public static byte[,] Resize(byte[,] matrix, int newRows, int newCols)
    {
        var matRows = matrix.GetLength(0);
        var matCols = matrix.GetLength(1);

        var newMat = new byte[newRows, newCols];

        var rows = Math.Min(matRows, newRows);
        var cols = Math.Min(matCols, newCols);

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                newMat[i, j] = matrix[i, j];
            }
        }
        return newMat;
    }

    public static (byte[,] reducedForm, byte[,] inverseForm, int rank, List<int> pivots) ReducedRowEchelonForm(byte[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);

        var reduced = (byte[,])matrix.Clone();
        var inverse = Identity(rows);

        var rank = 0;
        List<int> pivots = [];

        for (var j = 0; j < cols; j++)
        {
            for (var i = rank; i < rows; i++)
            {
                if (reduced[i, j] == 0) continue;
                // Eliminate column j
                for (var k = 0; k < rows; k++)
                {
                    if (k == i || reduced[k, j] == 0) continue;
                    XorRow(reduced, k, i);
                    XorRow(inverse, k, i);
                }

                // Swap rows i and rank
                SwapRows(reduced, i, rank);
                SwapRows(inverse, i, rank);

                pivots.Add(j);
                rank++;
                break;
            }
        }

        return (reduced, inverse, rank, pivots);
    }

    public static (byte[,] generalizedInverse, byte[,] nullBasis) GeneralizedInverse(byte[,] matrix)
    {
        var (_, inverseForm, rank, pivots) = ReducedRowEchelonForm(matrix);

        var rows = inverseForm.GetLength(0);
        var cols = inverseForm.GetLength(1);

        var sub = SubMatrix(inverseForm, rank, rows - rank, 0, cols);
        var (nullBasis, _, _, _) = ReducedRowEchelonForm(sub);

        var resizedInverse = Resize(inverseForm, matrix.GetLength(1), matrix.GetLength(0));

        for (var i = rank - 1; i >= 0; i--)
        {
            var col = pivots[i];
            SwapRows(resizedInverse, i, col);
        }

        return (resizedInverse, nullBasis);
    }

    public static byte[] IntToBitVector(int value, int bitLength)
    {
        var bits = new byte[bitLength];
        for (var i = 0; i < bitLength; i++)
        {
            bits[i] = (byte)((value >> i) & 1);
        }

        return bits;
    }

    public static ulong BitVectorToInt(byte[] vec)
    {
        ulong res = 0;
        for (var i = 0; i < Math.Min(vec.Length, 64); i++) if (vec[i] == 1) res |= (1UL << i);
        return res;
    }

    private static byte[,] Identity(int size)
    {
        var id = new byte[size, size];
        for (var i = 0; i < size; i++)
        {
            id[i, i] = 1;
        }
        return id;
    }

    private static void XorRow(byte[,] mat, int targetRow, int sourceRow)
    {
        var cols = mat.GetLength(1);
        for (var j = 0; j < cols; j++)
        {
            mat[targetRow, j] ^= mat[sourceRow, j];
        }
    }

    private static void SwapRows(byte[,] mat, int r1, int r2)
    {
        if (r1 == r2) return;

        var cols = mat.GetLength(1);
        for (var j = 0; j < cols; j++)
        {
            (mat[r2, j], mat[r1, j]) = (mat[r1, j], mat[r2, j]);
        }
    }

    private static byte[,] SubMatrix(byte[,] mat, int row, int rowCount, int col, int colCount)
    {
        var result = new byte[rowCount, colCount];
        for (var i = 0; i < rowCount; i++)
        {
            for (var j = 0; j < colCount; j++)
            {
                result[i, j] = mat[row + i, col + j];
            }
        }
        return result;
    }
}
