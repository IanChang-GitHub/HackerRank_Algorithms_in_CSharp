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
     * Complete the 'unboundedKnapsack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER k
     *  2. INTEGER_ARRAY arr
     *  
     *  解題策略:
     *  1.目標容量從0到k，有哪些容量是能夠剛好湊出來的
     *  2.若目標target減去某個num檢查dp表可湊出(代表目標可由目前數字加上num即可湊出)， target = dp[i - num].num + num
     *  3.由k往下找出第一個能湊出的數字
     */

    public static int unboundedKnapsack(int k, List<int> arr)
    {
        if (k == 0 || arr == null || arr.Count == 0) //目標為0
            return 0;
        if (arr.Contains(1)) //陣列有1一定能湊成k 
            return k;
            
        bool[] dp = new bool[k + 1]; //0~k，dp[i]代表是否能剛好湊出總和i
        dp[0] = true; //目標0甚麼事都不用做必成功
        for(int i=1; i <= k;i++) //建立dp表
        {
            foreach (int num in arr)
            {
                if (i >= num && dp[i - num] == true)
                {
                    dp[i] = true;
                    break;
                }
            }
        }
        
        for (int i = k; i >= 0; i--) //尋找最接近目標k成立的數
        {
            if (dp[i] == true)
            {
                return i;
            }
        }
        return 0;
        
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for(int i=0; i < t; i++)
        {  
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int k = Convert.ToInt32(firstMultipleInput[1]);

            List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

            int result = Result.unboundedKnapsack(k, arr);

            textWriter.WriteLine(result);
        }
            textWriter.Flush();
            textWriter.Close();
    }
}