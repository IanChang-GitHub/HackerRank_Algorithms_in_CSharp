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
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     *  
     *  解題策略:
     *  1. 找每一秒狀態的規律
     *  2. 第0秒:初始狀態
     *  3. 第1秒:什麼事都沒發生，維持初始狀態
     *  4. 第2秒:炸彈填滿整個地圖
     *  5. 第3秒:第0秒的炸彈爆炸
     *  6. 第4秒:炸彈填滿整個地圖
     *  7. 第5秒:第2秒炸彈爆炸
     *  8. 第6秒:炸彈填滿整個地圖
     *  9. 第7秒:同第3秒
     *  10. 第8秒:炸彈填滿整個地圖
     *  11. 第9秒:同第5秒
     *  12. 可以歸納出偶數秒皆為炸彈填滿地圖，
     *  13. 第3,7,11...秒皆相同
     *  14. 第5,9,13...秒皆相同
     */

    public static List<string> bomberMan(int n, List<string> grid)
    {
        int row = grid.Count;
        int colume = grid[0].Length;

        if (n == 0 || n == 1) //第0秒和第1秒
        {
            return grid;
        }

        if (n % 2 == 0) //偶數秒，炸彈填滿地圖
        {
            List<string> fullGrid = new List<string>();
            string fullRow = new string('O', colume); //填滿每列炸彈
            for (int i = 0; i < row; i++)
            {
                fullGrid.Add(fullRow);
            }
            return fullGrid;
        }

        List<string> state3 = Detonate(grid);
        if (n % 4 == 3) //第3,7,11...秒
        {
            return state3;
        }

        List<string> state5 = Detonate(state3);
        if (n % 4 == 1) //第5,9,13...秒
        {
            return state5;
        }

        return grid; //完善編譯邏輯用
    }

    private static List<string> Detonate(List<string> oldGrid)
    {
        int row = oldGrid.Count;
        int colume = oldGrid[0].Length;
        List<string> result = new List<string>();

        char[][] newGrid = new char[row][];

        for (int i = 0; i < row; i++)
        {
            newGrid[i] = new string('O', colume).ToCharArray();
        }

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < colume; j++)
            {
                if (oldGrid[i][j] == 'O') //移除周圍炸彈
                {
                    newGrid[i][j] = '.'  //移除本身
                    if (i - 1 >= 0)
                        newGrid[i - 1][j] = '.'; //移除上方炸彈
                    if (i + 1 < row)
                        newGrid[i + 1][j] = '.'; //移除下方炸彈
                    if (j - 1 >= 0)
                        newGrid[i][j - 1] = '.'; //移除左方炸彈
                    if (j + 1 < colume)
                        newGrid[i][j + 1] = '.'; //移除右方炸彈

                }
            }
        }

        for (int i = 0; i < row; i++)
        {
            result.Add(new string(newGrid[i]));
        }

        return result;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}