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
     * Complete the 'almostSorted' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     * 
     * 解題策略:
     * 1. 複製一份相同資料並排序好當標準答案
     * 2. 陸續比較將與標準答案不同的索引記錄下來 
     * 3. 若沒有不相同的索引代表已按照順序排序
     * 4. 若有兩個代表需要進行交換
     * 5. 若超過兩個則進行reverse，在跟標準答案比較
     * 6. 若以上都不行這代表無法進行排序
     */

    public static void almostSorted(List<int> arr)
    {
        List<int> sortedArray = new List<int>(arr);
        sortedArray.Sort();
        List<int> diffIndex = new List<int>();
        for (int i = 0; i < arr.Count; i++)
        {
            if (arr[i] != sortedArray[i])
            {
                diffIndex.Add(i);
            }
        }
        
        
        if (diffIndex.Count == 0)
        {
            Console.WriteLine("yes");
            return;
        }
        
        if (diffIndex.Count == 2)
        {
            Console.WriteLine("yes");
            Console.WriteLine($"swap {diffIndex[0] + 1} {diffIndex[1] + 1}");
            return;
        }
        
        int left = diffIndex[0];
        int right = diffIndex[diffIndex.Count - 1];
        bool reverse = true;
        for (int i = 0; i < right - left +1; i++)
        {
            if (arr[left + i] != sortedArray[right - i])
            {
                reverse = false;
                break;
            }
        }

        if (reverse)
        {
            Console.WriteLine("yes");
            Console.WriteLine($"reverse {left + 1} {right + 1}");
        }
        else
        {
            Console.WriteLine("no");
        }

    }

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.almostSorted(arr);
    }
}
