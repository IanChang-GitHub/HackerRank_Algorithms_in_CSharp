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
     * Complete the 'activityNotifications' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY expenditure
     *  2. INTEGER d
     *  
     *  解題策略:
     *  1. 題目限制每日消費金額最多不超過200，故每日消費金額為0~200
     *  2. 若d為奇數，中位數位子為(d+1)/2
     *  3. 若d是偶數，中位數是中間兩個數字的平均，它的兩倍剛好就是中間兩個數字相加
     */

    public static int activityNotifications(List<int> expenditure, int d)
    {
        int notifications = 0;
        int[] spend = new int[201]; //紀錄d天內金額0~200出現次數
        for (int i = 0; i < d; i++) //初始化前d天
        {
            spend[expenditure[i]]++;
        }

        for (int i = d; i < expenditure.Count; i++)
        {
            int spendToday = expenditure[i];
            int medianX2 = GetMedianX2(spend, d);

            if (spendToday >= medianX2)
            {
                notifications++;
            }

            spend[expenditure[i - d]]--; //更新前d天消費紀錄
            spend[spendToday]++;
        }

        return notifications;
    }

    private static int GetMedianX2(int[] spend, int d)
    {
        int count = 0;
        int left = -1;  
        int right = -1;

        for (int i = 0; i <= 200; i++)
        {
            count += spend[i];
            if (d % 2 != 0)
            {
                if (count >= (d + 1) / 2)
                {
                    return i * 2;
                }
            }
            else
            {
                if (left == -1 && count >= (d / 2))
                {
                    left = i;
                }
                if (right == -1 && count >= (d / 2) + 1)
                {
                    right = i;
                    return left + right; //中位數的兩倍等於left + right
                }
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

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int d = Convert.ToInt32(firstMultipleInput[1]);

        List<int> expenditure = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(expenditureTemp => Convert.ToInt32(expenditureTemp)).ToList();

        int result = Result.activityNotifications(expenditure, d);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
