using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class NumToString : MonoBehaviour
{
    public enum buildSetting { KOREA, GLOBAL };
    static readonly string[]    digitStringKR = { "?? ", "?? ", "?? ", "?? ", "" };
    static readonly string      digitComma = ",";
    static readonly long[]      digitNumberKR = { 10000000000000000, 1000000000000, 100000000, 10000, 1};
    static readonly long[]      digitNumberGL = { 1000000000000000, 1000000000000, 1000000000, 1000000, 1000, 0};
    static readonly int         digitCountKR = digitNumberKR.Length;
    static readonly int         digitCountGL = digitNumberGL.Length;
    public static readonly int  showNumberMax = 30;
    public static readonly int  digitCode = 0;

    public static string GetNumberString(ref StringBuilder str, long value, buildSetting code = buildSetting.KOREA) =>
        code switch
        {
            0 => NumToStringKR(ref str, value),
            _ => NumToStringGL(ref str, value)
        };

    public static string GetNumberString(ref StringBuilder str, long value, string word, buildSetting code = buildSetting.KOREA) =>
    code switch
    {
        0 => NumToStringKR(ref str, value, word),
        _ => NumToStringGL(ref str, value, word)
    };

    static string NumToStringKR(ref StringBuilder str, long value, string word = null)
    {
        str.Remove(0, str.Length);

        long num = value;
        int count = 0, range = 0;
        while (count < digitCountKR)
        {
            num = value / digitNumberKR[count];
            if (num != 0)
            {
                str.Append(num);
                str.Append(digitStringKR[count]);
                value %= digitNumberKR[count];
                if (0 < range++) break;
            }

            count++;
        }

        return str.ToString();
    }

    static string NumToStringGL(ref StringBuilder str, long value, string word = null)
    {
        str.Remove(0, str.Length);
        if (word != null) str.Append(word);
        str.Append(value.ToString());

        int count = 0;
        for(int i = 0; i < digitCountGL; i++)
        {
            if(value >= digitNumberGL[i])
            {
                count = digitCountGL - 1 - i;
                break;
            }
        }

        int gap = (str.Length) % 3;
        if (gap == 0 && str.Length > 3) gap = 3;
        for(int i = 0; i < count; i++)
        {
            str.Insert(i * 3 + i * 1 + gap, digitComma);
        }

        return str.ToString();
    }
}
