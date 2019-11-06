using UnityEngine;

public class NumberSystem : MonoBehaviour
{
    public static string Output(double number)
    {
        string output;
        output = NiceCurrencyFormat.ConvertToString(number);
        return output;
    }

    public static string OutputTwoNumbers(double number)
    {
        string output;
        output = NiceCurrencyFormat.ConvertToStringTwoNumbers(number);
        return output;
    }

}
