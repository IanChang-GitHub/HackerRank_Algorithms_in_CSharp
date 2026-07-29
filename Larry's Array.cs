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
     * Complete the 'larrysArray' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER_ARRAY A as parameter.
     * 
     * 解題策略:
     * 1.逆序對:在一個陣列中，如果有一個較大的數字排在較小的數字前面，這兩個數字就構成一個逆序對
     * 2.由小到大排列逆序對為0
     * 3.三元素旋轉，若要能由小到大排序，逆序對必為0或2
     */

    public static string larrysArray(List<int> A)
    {
        int inversion = 0; //逆序對

        for (int i = 0; i < A.Count; i++) //計算逆序對數量
        {
            for (int j = i + 1; j < A.Count; j++)
            {
                if (A[i] > A[j])
                {
                    inversion++;
                }
            }
        }
        
        if (inversion % 2 == 0)
        {
            return "YES";
        }
        else
        {
            return "NO";
        }
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

            List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

            string result = Result.larrysArray(A);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}