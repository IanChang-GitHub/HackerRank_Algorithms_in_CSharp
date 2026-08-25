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
     * Complete the 'lilysHomework' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY arr as parameter.
     * 
     * 解題策略:
     * 1. 相鄰元素絕對差值之和最小:排序由小到大或由大到小
     * 2. 轉換題目成要交換幾次，才能把原陣列變成由小到大或是由大到小
     */

    public static int lilysHomework(List<int> arr)
    {
        int[] original = arr.ToArray();

        int[] ascendingOrder = arr.ToArray();
        Array.Sort(ascendingOrder); //由小到大排列

        int[] descendingOrder = arr.ToArray();
        Array.Sort(descendingOrder);
        Array.Reverse(descendingOrder); //由大到小排列

        int ascendingSwap = MinSwaps(original, ascendingOrder); //計算兩種的交換次數
        int descendingSwap = MinSwaps(original, descendingOrder);
        return Math.Min(ascendingSwap, descendingSwap);
    }

    private static int MinSwaps(int[] original, int[] target) //計算將陣列轉換為目標排序狀態所需的最少交換次數
    {
        int swap = 0; //交換次數
        int[] array = (int[])original.Clone(); //Clone回傳為object，需轉型
        Dictionary<int, int> valueToIndex = new Dictionary<int, int>(); //紀錄目前每個人的位子<:key:number, value:index>

        for (int i = 0; i < array.Length; i++)
        {
            valueToIndex[array[i]] = i;
        }

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != target[i])
            {
                swap++;
                int wrongValue = array[i]; //目前坐錯位子的人
                int targetValue = target[i]; //這個位子真正該坐的人
                int swapIndex = valueToIndex[targetValue]; //查找出真正該坐的人在哪裡

                array[i] = targetValue; //交換兩者的位子
                array[swapIndex] = wrongValue;

                valueToIndex[wrongValue] = swapIndex; //更新錯誤值的索引
            }
        }

        return swap;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        int result = Result.lilysHomework(arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}