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
     * Complete the 'steadyGene' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING gene as parameter.
     * 
     * 解題策略:
     * 1. Steady Gene:每個字元都必須剛好出現N/4次
     * 2. 統計每個字母出現次數，找出超過次數的目標
     * 3. 不需在意如何替換，只需找出一段最短的連續視窗，這個視窗裡面至少包含超出目標個數的字母。
     */

    public static int steadyGene(string gene)
    {
        int target = gene.Length / 4;
        Dictionary<char, int> count = new Dictionary<char, int> //初始化
        {
            {'A', 0}, {'C', 0}, {'G', 0}, {'T', 0}
        };
        foreach (char c in gene)
        {
            count[c]++;
        }

        if (IsSteady(count, target))
        {
            return 0;
        }

        int minLength = int.MaxValue;
        int left = 0;
        for (int right = 0; right < gene.Length; right++) //擴大視窗
        {
            count[gene[right]]--;
            while (IsSteady(count, target) && left <= right) //當視窗外的剩餘字母都沒超過次數時，代表此視窗是合法的替換區間
            {
                minLength = Math.Min(minLength, right - left + 1);
                count[gene[left]]++; //嘗試縮小視窗
                left++;
            }
        }

        return minLength;
    }

    private static bool IsSteady(Dictionary<char, int> counts, int target) //檢查視窗外字母是否有超出次數
    {
        return counts['A'] <= target &&
               counts['C'] <= target &&
               counts['G'] <= target &&
               counts['T'] <= target;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        string gene = Console.ReadLine();

        int result = Result.steadyGene(gene);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
