using System.Text;
using PBScript.ExpressionParsing;
using PBScript.ExpressionParsing.Exceptions;
using PBScript.Interfaces;

namespace PBScript.Fruity;

public static class ValueHelper
{
    public static string ReadStringLiteral(ExpressionContainer e, int nextOpIndex,
        int currentIndex, out int newIndex)
    {
        var sb = new StringBuilder();
        currentIndex++; // move behind the first " (Start of the literal)
        while (currentIndex < e.Expression.Length && e.Expression[currentIndex] != '"')
        {
            sb.Append(e.Expression[currentIndex]);
            currentIndex++;
        }
        newIndex = currentIndex+1; // +1 moves behind the ".
        return sb.ToString(); 
    }

    public static double ReadDouble(ExpressionContainer e, int nextOpIndex,
        int currentIndex, out int newIndex)
    {
        var sb = new StringBuilder();

        if (e.Expression[currentIndex] == '~')
        {
            sb.Append('-');
            currentIndex++;
        }

        // Support for numbers like .55 (instead of 0.55)
        if (e.Expression[currentIndex] == '.')
        {
            sb.Append(0);
        }
        
        while (currentIndex < nextOpIndex && NumericValues.Contains(e.Expression[currentIndex]))
        {
            sb.Append(e.Expression[currentIndex]);
            currentIndex++;
        }
        newIndex = currentIndex;

        try
        {
            return double.Parse(sb.ToString());
        }
        catch (System.Exception)
        {
            throw new ExpressionParsingException("Bad number format. Does the number contain multiple dots?", sb.ToString());
        }
    }
    
    public static readonly HashSet<char> NumericValues = new()
    {
        '.','~',
        '0','1','2','3','4','5','6','7','8','9',
    };
}