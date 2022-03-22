using System.Text.RegularExpressions;
using Flee.PublicTypes;
using PBScript.Extension;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class PbsAction: IPbsAction
{
    
        private readonly ExpressionContext _ctx = new ExpressionContext();
    
        private readonly bool _alwaysFalse = false;
        public readonly string ObjectToken = "";
        private readonly string _actionToken = "";
        private readonly string _parameter = "";
        private readonly string _text;
        private readonly bool _isValueExpression = false;

        public PbsAction(string actionCode)
        {
            actionCode = actionCode.Trim();
            _text = actionCode;

            if (actionCode.StartsWith("("))
            {
                _isValueExpression = true;
                return;
            }
            
            var objectMatches = Regex.Matches(actionCode, PbsInterpreter.TokenRegex);

            if (objectMatches.Count < 1)
            {
                _alwaysFalse = true;
                return;
            }

            var p1 = actionCode.Split(objectMatches[0].Value,2);

            if (p1.Length < 1)
            {
                _alwaysFalse = true;
                return;
            }
            
            ObjectToken = objectMatches[0].Value.Trim();
                
            var parts = p1[1].Trim().Split(" ",2);
            
            _actionToken = parts.Length > 0 ? parts[0].Trim() : "";
            _parameter = parts.Length > 1 ? parts[1].Trim() : "";
        }

        private static List<string> ReservedLogicWords = new List<string>()
        {
            "and","or","not"
        };

        public IPbsValue Execute(IPbsEnvironment env)
        {
            if (_alwaysFalse)
                return PbsValue.Null;

            IPbsValue value;

            try
            {
                if (_isValueExpression)
                {
                    if (PbsInterpreter.Log)
                    {
                        env.Log("action<" + _text + ">", "is value expression");
                    }
                    
                    var str = _text.EnrichString(env);

                    var parts = str.Split('"');

                    var newStr = "";
                    
                    // Handles parts inside/outside of string literals differently.
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (i%2==0)
                        {
                            // we are outside of a string literal!
                            newStr += Regex.Replace(parts[i], $@"{PbsInterpreter.TokenRegex}([ ]+({PbsInterpreter.TokenRegex}))*", match =>
                            {
                                var bMatch = match.Value.ToLower();
                                if (ReservedLogicWords.Contains(bMatch))
                                {
                                    return match.Value;
                                }
                                var newA = new PbsAction(match.Value);
                                var v = newA.Execute(env);
                                return v.AsString();
                            }).Replace("=="," = ").Replace("&&"," AND ").Replace("||"," OR ");
                        }
                        else
                        {
                            newStr += $" \"{parts[i]}\" ";
                        }
                    }
                    
                    if (PbsInterpreter.Log)
                    {
                        env.Log("action<" + _text + ">", "-> " + newStr);
                    }
                    
                    try
                    {
                        var expr = _ctx.CompileDynamic(newStr);

                        var result = expr.Evaluate();

                        var r = new PbsValue(result);
                        
                        if (PbsInterpreter.Log)
                        {
                            env.Log("action<" + _text + ">", "==> " + (r.ObjectValue?.ToString()??"null"));
                        }

                        return r;
                    }
                    catch (System.Exception _)
                    {
                        Console.WriteLine(_);
                        return PbsValue.Null;
                    }
                }
                
                
                if (PbsInterpreter.Log)
                {
                    env.Log("action<" + _text + ">", "is action expression");
                }
                
                var obj = env.GetObject(ObjectToken);
                var enrichedParameter = _parameter.EnrichString(env);
                var enrichedToken = _actionToken.EnrichString(env);

                value = obj?.ExecuteAction(enrichedToken, enrichedParameter, env) ?? PbsValue.Null;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                value = PbsValue.Null;
            }
            
            if(PbsInterpreter.Log) env.Log("action<" + _text + ">", $"=> {value.AsString()}");
            
            return value;
        }
}