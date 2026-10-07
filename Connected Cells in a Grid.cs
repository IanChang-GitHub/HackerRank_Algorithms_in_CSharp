using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'connectedCell' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
     * 
     * 解題策略:
     * 1. 採用DFS，計算當前格子加上周圍所有相連格子的總面積
     * 2. 終止條件:如果探索的座標超出了網格邊界，或者該格子為0
     * 3. 遞迴呼叫:如果當前格子是1，則面積為1+周圍8個方向的面積加總
     */

    public static int connectedCell(List<List<int>> matrix)
    {
        int maxRegion = 0;
        int row = matrix.Count;
        int colume = matrix[0].Count;

        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < colume; c++)
            {
                if (matrix[r][c] == 1)
                {
                    int Region = GetRegionSize(matrix, r, c, row, colume); //DFS
                    maxRegion = Math.Max(maxRegion, Region);
                }
            }
        }
        return maxRegion;
    }

    private static int GetRegionSize(List<List<int>> matrix, int r, int c, int row, int colume)
    {
        if (r < 0 || r >= row || c < 0 || c >= colume || matrix[r][c] == 0) //邊界檢查
        {
            return 0;
        }

        matrix[r][c] = 0; //標記為已訪問
        int size = 1; //此格面積

        for (int rowOffset = -1; rowOffset <= 1; rowOffset++) //向周圍8個方向進行探索
        {
            for (int columeOffset = -1; columeOffset <= 1; columeOffset++)
            {
                if (rowOffset == 0 && columeOffset == 0)
                    continue;
                else
                    size += GetRegionSize(matrix, r + rowOffset, c + columeOffset, row, colume);
            }
        }

        return size;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        int m = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> matrix = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
        }

        int result = Result.connectedCell(matrix);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
