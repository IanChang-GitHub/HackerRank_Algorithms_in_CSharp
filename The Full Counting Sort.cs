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
     * Complete the 'countSort' function below.
     *
     * The function accepts 2D_STRING_ARRAY arr as parameter.
     * 
     * 解題策略:
     * 1.題目限制整數範圍0~99，建立長度100的陣列，用索引來儲存對應整數的字串
     * 2.將字串用串接的維持順序stable
     */

    public static void countSort(List<List<string>> arr)
    {
        int half = arr.Count / 2;
        StringBuilder result = new StringBuilder();
        StringBuilder[] bucket = new StringBuilder[100]; //建立桶子儲存字串
        for (int i = 0; i < 100; i++)
        {
            bucket[i] = new StringBuilder();
        }
        
        for (int i = 0; i < arr.Count; i++)
        {
            int index = int.Parse(arr[i][0]);
            string str = arr[i][1];

            if (i < half) //前半段資料換成-
            {
                bucket[index].Append("- "); 
            }
            else
            {
                bucket[index].Append(str).Append(" ");
            }
        }
        
        for (int i = 0; i < 100; i++) //依序讀出每個桶子的字串
        {
            if (bucket[i].Length > 0)
            {
                result.Append(bucket[i]);
            }
        }
        
        Console.WriteLine(result.ToString().TrimEnd());
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<string>> arr = new List<List<string>>();

        for (int i = 0; i < n; i++)
        {
            arr.Add(Console.ReadLine().TrimEnd().Split(' ').ToList());
        }

        Result.countSort(arr);
    }
}
