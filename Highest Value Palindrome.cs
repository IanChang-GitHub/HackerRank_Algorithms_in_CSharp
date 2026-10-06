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
     * Complete the 'highestValuePalindrome' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. STRING s
     *  2. INTEGER n
     *  3. INTEGER k
     *  
     *  解題策略:
     *  1.先進行第一次掃描，比較回文前後字元，如果不同則消耗1個修改額度將兩個之中比較小的數字改成比較大的數字
     *  2.若還要額度，從最外圍開始盡量把數字升級成9，在第一階段已經修改過其中一個數字，只需要再消耗一個額度，若沒有被修改過，必須同時修改左右兩邊的數字消耗兩個額度
     *  3.如果字串長度是奇數，若做完以上兩個步驟還有額度，可以直接把最正中間的那個數字改成9
     *  
     */

    public static string highestValuePalindrome(string s, int n, int k)
    {
        char[] array = s.ToCharArray(); //將字串轉為字元陣列方便修改
        bool[] change = new bool[n]; //紀錄第一次掃描是否有被修改過

        for (int i = 0; i < n / 2; i++)
        {
            int left = i;
            int right = n - 1 - i;

            if (array[left] != array[right])
            {
                char maxChar = (char)Math.Max(array[left], array[right]); //改成比較大的數字
                array[left] = maxChar;
                array[right] = maxChar;

                change[left] = true;
                k--;
            }
        }

        if (k < 0)
        {
            return "-1";
        }

        for (int i = 0; i < n / 2; i++)
        {
            int left = i;
            int right = n - 1 - i;

            if (array[left] != '9')
            {
                if (change[left]) //如果在第一階段已經改過其中一邊，只需要再花1個額度就修改兩邊
                {
                    if (k >= 1)
                    {
                        array[left] = '9';
                        array[right] = '9';
                        k -= 1;
                    }
                }
                else //如果在第一階段沒動過，要修改雙邊必須花費2個額度
                {
                    if (k >= 2)
                    {
                        array[left] = '9';
                        array[right] = '9';
                        k -= 2;
                    }
                }
            }
        }

        if (n % 2 != 0 && k > 0) //字串長度是奇數並且還有額度修改
        {
            int mid = n / 2;
            if (array[mid] != '9')
            {
                array[mid] = '9';
                k--; //可扣可不扣，最後一次修改了
            }
        }

        return new string(array);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        string s = Console.ReadLine();

        string result = Result.highestValuePalindrome(s, n, k);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}