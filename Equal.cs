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
     * Complete the 'equal' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY arr as parameter.
     * 
     * 解題策略:
     * 1. 目標:每個人擁有相同顆數的巧克力等價每個人相差0
     * 2. 題目轉換:給除了A以外的所有人增加1,2,5顆巧克力→只從A身上扣除1,2,5顆巧克力(每個人之間差距相同)
     * 3. 直覺想法將所有人扣除到與最低顆顆數的人(Min)相同(需扣除Max-Min顆)
     * 4. 需額外考量湊合優勢，如:差4顆可以2+2需兩次，差5步只需一次，故目標除Min外還需考慮(Min-1)~(Min-4)
     * 5. 整體需移除顆數考慮(Max-Min)~(Max-(Min-4))
     */

    public static int equal(List<int> arr)
    {
        if (arr == null || arr.Count == 0) 
            return 0;

        int minValue = arr.Min();
        int maxValue = arr.Max();
        
        int maxDiff = maxVal - (minVal - 4); //最大需移除顆數

        int[] dp = new int[maxDiff + 1]; //dp[i]代表湊出差值i最少需要的操作次數

        dp[0] = 0; //差值為0，需要0步 

        for (int i = 1; i <= maxDiff; i++) //i:目前差值，建立dp表
        {
            dp[i] = int.MaxValue - 1; //用最大值表式步數無限大達不到，-1避免溢位

            if (i >= 1)  //計算出達到差值i的最小步數，有3種操作方式(取1,2,5顆)
                dp[i] = Math.Min(dp[i], dp[i - 1] + 1);
            if (i >= 2) 
                dp[i] = Math.Min(dp[i], dp[i - 2] + 1);
            if (i >= 5) 
                dp[i] = Math.Min(dp[i], dp[i - 5] + 1);
        }

        int minTotalOperation = int.MaxValue; 

        for (int offset = 0; offset <= 4; offset++) //計算5種調整目標:(Min)~(Min-4)的步數
        {
            int target = minVal - offset;
            int totalOperation = 0;

            foreach (int chocolateNum in arr) //針對每一個人查dp表
            {
                int diff = chocolateNum - target; //差值
                totalOperation += dp[diff]; //查表
            }

            minTotalOperations = Math.Min(minTotalOperation, totalOperation);//決定哪一個目標步數最低
        }

        return minTotalOperation;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

            int result = Result.equal(arr);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}