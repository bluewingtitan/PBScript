using System.Data;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using PBScript.Interfaces;

namespace PBScript.Environment;

public class VariableObject : IPbsObject
{
    /// <summary>
    /// Experimental: CSharpScript is quite expensive on performance (as it compiles and executes as C#), but *might* work
    /// </summary>
    private const bool UseCSharpScript = false;
    public object Value { get; private set; }
    public VariableType Type { get; private set; }

        
    private delegate bool CommandDelegate(string parameter);
    private readonly Dictionary<string, CommandDelegate> _commands = new Dictionary<string, CommandDelegate>();

    public VariableObject(object value, VariableType type)
    {
        Value = value;
        Type = type;

        _commands["="] = AssignValue;
        _commands["+="] = AddValue;
        _commands["-="] = RemoveValue;
        _commands["*="] = MultiplyValue;
        _commands["/="] = DivideValue;
        _commands["++"] = PlusPlusValue;
        _commands["--"] = MinusMinusValue;

        _commands["=="] = Equals;
        _commands[">"] = BiggerThen;
        _commands["<"] = SmallerThen;
    }

    #region Actions
        
    private bool SmallerThen(string args)
    {
        args = args.Trim().Replace("\\%"," ");
        if (IsEquation(args) && Type != VariableType.String)
        {
            var value = ResolveNumberCalculation(args);

            return (double) Value < value;
        }
        else
        {
            return args.Contains((string) Value);
        }
    }
        
    private bool BiggerThen(string args)
    {
        args = args.Trim().Replace("\\%"," ");
        if (IsEquation(args) && Type != VariableType.String)
        {
            var value = ResolveNumberCalculation(args);

            return (double) Value > value;
        }
        else
        {
            return ((string) Value).Contains(args);
        }
    }
        
    private bool Equals(string args)
    {
        args = args.Trim().Replace("\\%"," ");
        if (IsEquation(args) && Type != VariableType.String)
        {
            var value = ResolveNumberCalculation(args);

            return Math.Abs(((double) Value) - value) < 0.01;
        }
        else
        {
            return ((string) Value).Equals(args);
        }
    }
        
        
    private bool AssignValue(string args)
    {
        args = args.Trim().Replace("\\%"," ");
        if (IsEquation(args))
        {
            Value = ResolveNumberCalculation(args);
            Type = VariableType.Number;
        }
        else
        {
            Value = args;
            Type = VariableType.String;
        }
            
        return true;
    }
    private bool AddValue(string args)
    {
        args = args.Trim().Replace("\\%"," ");
        if (IsEquation(args) && Type != VariableType.String)
        {
            var newVal = ResolveNumberCalculation(args);
                
            if(Type == VariableType.Undefined)
            {
                Value = newVal;
                Type = VariableType.Number;
            }
            else
            {
                var typedVal = (double)Value;
                Value = typedVal + newVal;
            }
        }
        else
        {
            
            
            if (Type == VariableType.Undefined)
            {
                Value = args;
                Type = VariableType.String;
            }
            else
            {
                var typedVal = (string)Value;
                Value = typedVal + args;
            }
        }
        return true;
    }
    
    private bool DivideValue(string args)
    {
        args = args.Trim();
        if (!IsEquation(args) || Type == VariableType.String) return false;
        var newVal = ResolveNumberCalculation(args);

        // no division by 0!
        if (newVal == 0)
            return false;
        
        if(Type == VariableType.Undefined)
        {
            // as undefined is handled as 0 and 0/x is still always 0.
            Value = 0;
            Type = VariableType.Number;
        }
        else
        {
            var typedVal = (double)Value;
            Value = typedVal / newVal;
        }
            
        return true;
    }
    
    private bool MultiplyValue(string args)
    {
        args = args.Trim();
        if (!IsEquation(args) || Type == VariableType.String) return false;
        var newVal = ResolveNumberCalculation(args);
                
        if(Type == VariableType.Undefined)
        {
            // as undefined is handled as 0 and 0*x is still always 0.
            Value = 0;
            Type = VariableType.Number;
        }
        else
        {
            var typedVal = (double)Value;
            Value = typedVal * newVal;
        }
            
        return true;
    }
    
    private bool RemoveValue(string args)
    {
        args = args.Trim();
        if (!IsEquation(args) || Type == VariableType.String) return false;
        var newVal = ResolveNumberCalculation(args);
                
        if(Type == VariableType.Undefined)
        {
            Value = -newVal;
            Type = VariableType.Number;
        }
        else
        {
            var typedVal = (double)Value;
            Value = typedVal - newVal;
        }
            
        return true;
    }
    private bool PlusPlusValue(string args)
    {
        if (Type == VariableType.String) return false;
            
        if(Type == VariableType.Undefined)
        {
            Value = 1;
            Type = VariableType.Number;
        }
        else
        {
            var typedVal = (double)Value;
            Value = typedVal + 1;
        }
            
        return true;
    }
    private bool MinusMinusValue(string args)
    {
        if (Type == VariableType.String) return false;
            
        if(Type == VariableType.Undefined)
        {
            Value = -1;
            Type = VariableType.Number;
        }
        else
        {
            var typedVal = (double)Value;
            Value = typedVal - 1;
        }
            
        return true;
    }
        
    #endregion

    public string GetStringValue()
    {
        return Value.ToString();
    }
        

    #region Helpers

    private static double ResolveNumberCalculation(string part)
    {
        if (UseCSharpScript)
        {
            var v = CSharpScript.EvaluateAsync<double>(part).Result;
            return v;
        }
            
        var dt = new DataTable();
        var value = dt.Compute(part, "").ToString();
        return string.IsNullOrEmpty(value) ? 0f : double.Parse(value);
    }

    private static bool IsEquation(string args) =>
        Regex.Matches(args, "[A-Za-z]").Count < 1 && Regex.IsMatch(args, @"\d");

    #endregion
        
    public bool ExecuteAction(string command, string parameter, IPbsEnvironment env)
    {
        // guarantee spaces around first number to allow for things like x-=1
        var first = true;
        var fullCmd = $"{command} {parameter ?? ""}";
        var parts = Regex.Replace(fullCmd, @"(\d+(\.\d+)?)|(\.\d+)", match =>
        {
            if (!first)
                return match.Value;

            first = false;
            
            var result = match.Value;
            if (!char.IsWhiteSpace(fullCmd[match.Index - 1]))
            {
                result = " " + result;
            }

            var lastChar = match.Index + match.Length;
            if (fullCmd.Length <= lastChar) return result;
            
            if (!char.IsWhiteSpace(fullCmd[lastChar]))
            {
                result = result + "";
            }

            return result;
        }).Split(" ",2);
            
        command = parts[0].Trim();
        parameter = parts[1].Trim();
            
        return _commands.ContainsKey(command) && _commands[command](parameter);
    }
}

public enum VariableType
{
    Number,
    String,
    Undefined,
}