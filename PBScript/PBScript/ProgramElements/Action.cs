using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using PBScript.Extension;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public class Action: IPbsAction
{
        private readonly bool _alwaysFalse = false;
        public readonly string ObjectToken = "";
        private readonly string _actionToken = "";
        private readonly string _parameter = "";
        private readonly string _text;

        public Action(string actionCode)
        {
            actionCode = actionCode.Trim();
            _text = actionCode;

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

        public bool Execute(IPbsEnvironment env)
        {
            if (_alwaysFalse)
                return false;

            var value = false;

            try
            {
                var obj = env.GetObject(ObjectToken);
                var enrichedParameter = _parameter.EnrichString(env);
                var enrichedToken = _actionToken.EnrichString(env);

                value = obj?.ExecuteAction(enrichedToken, enrichedParameter, env) ?? false;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                value = false;
            }
            
            if(PbsInterpreter.Log) env.Log(_text, $" => {value}");
            
            return value;
        }
}