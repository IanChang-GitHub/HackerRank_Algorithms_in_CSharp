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
     * Complete the 'absolutePermutation' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  
     *  解題策略:
     *  1. 根據題意陣列的填入的值都要滿足|P[i] - i| = k，i為index，k為差值，1~n必須剛好用完且不重複
     *  2. P[i]有兩種可能，P[i] = i+k 和 P[i] = i-k
     *  3. 若k=0，|P[i] - i| = 0，P[i] = i，故直接回傳[1, 2,... ,n]
     *  4. 若k>0,i=1,2,..., k，則P[i]必為i+k，i-k會超出1~n的範圍
     *  5. 若i=k+1,k+2,..., 2k，P[i] = i-k
     *  6. 數字必須以大小為2k的區塊為單位，進行前後半部的對調
     *  7. 一個區塊包含2k個位置，前半部(長度k)每個數字都是位置+k，後半部(長度k)每個數字都是位置-k
     *  8. 只要有人往右跳，就必須要有對應數量的人往左跳來填補空缺，這意味著他們必須是等量的兩組人才能完成交換。
     *  9. 唯一解，不需考慮最小問題
     */

    public static List<int> absolutePermutation(int n, int k)
    {
        List<int> result = new List<int>(n);

        if (k == 0)
        {
            for (int i = 1; i <= n; i++)
            {
                result.Add(i);
            }
            return result;
        }

        if (n % (2 * k) != 0) //範圍大小不是2k的單位
        {
            result.Add(-1);
            return result;
        }

        for (int i = 1; i <= n; i++)
        {
            if (((i - 1) / k) % 2 == 0) //偶數，表示前半部，(i-1)是為了讓其能用除法分組所做的平移
            {
                result.Add(i + k); 
            }
            else //奇數，表示後半部
            {
                result.Add(i - k);
            }
        }

        return result;
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
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int k = Convert.ToInt32(firstMultipleInput[1]);

            List<int> result = Result.absolutePermutation(n, k);

            textWriter.WriteLine(String.Join(" ", result));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}