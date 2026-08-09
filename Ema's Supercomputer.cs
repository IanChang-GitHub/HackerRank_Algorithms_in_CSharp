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
     * Complete the 'twoPluses' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING_ARRAY grid as parameter.
     * 
     * 解題策略:
     * 1.題目網格限制為15*15不大，採用暴力搜尋
     * 2.暴力搜尋找出第一個十字，並將占用的格子改為B避免第二個十字重疊
     * 3.暴力搜尋第二個十字，計算面積乘積
     * 4.恢復第一個十字占用的格子為G，繼續往下找其他十字
     */

    public static int twoPluses(List<string> gridInput)
    {
        int row = gridInput.Count;
        int colume = gridInput[0].Length;
        
        char[][] grid = new char[row][];
        for (int i = 0; i < row; i++)
        {
            grid[i] = gridInput[i].ToCharArray();
        }

        int maxProduct = 0;

        for (int row1 = 0; row1 < row; row1++) //遍歷所有以當格為中心點的十字
        {
            for (int colume1 = 0; colume1 < colume; colume1++)
            {
                int length1 = 0; 
                
                while (IsValidPlus(grid, row1, colume1, length1)) //找到當格最大的十字
                {
                    SetPlus(grid, row1, colume1, length1, 'B'); //將第一個十字佔用的格子改成B

                    for (int row2 = 0; row2 < row; row2++) //找尋第二個十字
                    {
                        for (int colume2 = 0; colume2 < colume; colume2++)
                        {
                            int length2 = 0; //第二個十字的臂長
                            
                            while (IsValidPlus(grid, row2, colume2, length2))
                            {
                                int area1 = 4 * length1 + 1; //計算面積: 4 * 臂長 + 1(中心點)
                                int area2 = 4 * length2 + 1;
                                maxProduct = Math.Max(maxProduct, area1 * area2);
                                
                                length2++; //擴大第二個十字
                            }
                        }
                    }

                    SetPlus(grid, row1, colume1, length1, 'G'); //復原網格為G
                    length1++; //擴大第一個十字
                }
            }
        }

        return maxProduct;
    }

    private static bool IsValidPlus(char[][] grid, int row, int colume, int length) //檢查是否為合法的十字
    {
        for (int i = 0; i <= length; i++)
        {
            if (row - i < 0 || row + i > grid.Length-1 || colume - i < 0 || colume + i > grid[0].Length-1) //檢查十字有無超出邊界
                return false;

            if (grid[row - i][colume] != 'G' || grid[row + i][colume] != 'G' || grid[row][colume - i] != 'G' || grid[row][colume + i] != 'G')
            {
                return false;
            }
        }
        return true;
    }

    private static void SetPlus(char[][] grid, int row, int colume, int length, char value) //將十字的所有格子改為特定字元
    {
        for (int i = 0; i <= length; i++)
        {
            grid[row - i][colume] = value; 
            grid[row + i][colume] = value; 
            grid[row][colume - i] = value; 
            grid[row][colume + i] = value;
        }
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<string> grid = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        int result = Result.twoPluses(grid);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}