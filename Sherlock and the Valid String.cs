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
     * Complete the 'isValid' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     * 
     * 解題策略:
     * 1.計算每個字母的出現次數
     * 2.計算每個次數的數量
     * 3.只有一種次數:代表所有字母出現次數都一樣
     * 4.出現兩種次數:檢查能否透過減去1或刪除只有一個次數為1的字母
     * 5.出現超過三種次數以上:無法透過修正符合題意
     */

    public static string isValid(string s)
    {
        Dictionary<char, int> charFrequent = new Dictionary<char, int>();
        foreach (char c in s)
        {
            if (charFrequent.ContainsKey(c))
                charFrequent[c]++;
            else
                charFrequent[c] = 1;
        }

        Dictionary<int, int> freqCount = new Dictionary<int, int>();
        foreach (int freq in charFrequent.Values)
        {
            if (freqCount.ContainsKey(freq))
                freqCount[freq]++;
            else
                freqCount[freq] = 1;
        }

        if (freqCount.Count == 1) //只有一種次數
        {
            return "YES";
        }
        if (freqCount.Count > 2) //三種次數以上
        {
            return "NO";
        }

        int[] frequent = freqCount.Keys.ToArray();
        if ((frequent[0] == 1 && freqCount[frequent[0]] == 1) || (frequent[1] == 1 && freqCount[frequent[1]] == 1)) //其中一種次數為1且只有一個字母是這個次數
        {
            return "YES";
        }

        if ((frequent[0] - frequent[1] == 1 && freqCount[frequent[0]] == 1) || (frequent[1] - frequent[0] == 1 && freqCount[frequent[1]] == 1)) //兩種次數相差1且次數比較大的字母只有一個
        {
            return "YES";
        }

        return "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.isValid(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}