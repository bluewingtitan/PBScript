using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;
using Flee.PublicTypes;
using PBScript.Extension;
using PBScript.Interfaces;
using PBScript.Interpretation;

namespace PBScript.ProgramElements;

public interface IPbsValueExpressionPart
{
    public string GetValue(IPbsEnvironment env);
}
public class PbsStringValue : IPbsValueExpressionPart
{
    private readonly string _value;

    public PbsStringValue(string value)
    {
        _value = value;
    }

    public int Usages { get; private set; } = 0;

    public string GetValue(IPbsEnvironment env)
    {
        return _value.EnrichString(env);
    }
}

public class PbsAction : IPbsAction, IPbsValueExpressionPart
{
    #region ExpressionPart Implementation

    public string GetValue(IPbsEnvironment env)
    {
        return Execute(env).AsString();
    }

    #endregion


    /// <summary>
    /// Trades higher consistent memory usage use for (on the long run) quicker execution times and way lower memory allocation
    /// by caching already generated Actions. More performant from the point on, in which you run a script at least twice.
    /// </summary>
    public static bool UseActionCache = true;

    /// <summary>
    /// Clear the internal caches.
    /// </summary>
    public static void ClearCache()
    {
        ActionCache.Clear();
    }


    private static readonly Regex ActionRegex = new($@"{PbsInterpreter.TokenRegex}([ ]+({PbsInterpreter.TokenRegex}))*",
        RegexOptions.Compiled);

    private static readonly Regex ReservedTokenRegex =
        new($@"([\W\s]|^)(and|or|not)([\W\s]|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private static readonly ConcurrentDictionary<string, PbsAction> ActionCache = new();

    private readonly ExpressionContext _ctx = new();
    private readonly bool _alwaysFalse = false;
    public bool AlwaysFalse => _proxyAction?._alwaysFalse ?? _alwaysFalse;
    public string ObjectToken => _objectToken ?? _proxyAction?.ObjectToken ?? "";
    private readonly string? _objectToken;
    private readonly string? _actionToken;
    private readonly string _parameter = "";
    private readonly string _text;
    private readonly bool _isValueExpression = false;
    private readonly IPbsValueExpressionPart[] _parts = Array.Empty<IPbsValueExpressionPart>();

    private readonly PbsAction? _proxyAction;

    public PbsAction(string actionCode)
    {
        _ctx = new()
        {
            Options =
            {
                IntegersAsDoubles = true
            }
        };


        _text = actionCode.Trim();

        if (UseActionCache)
        {
            if (ActionCache.ContainsKey(_text))
            {
                _proxyAction = ActionCache[_text];
                return;
            }
        }

        if (_text.StartsWith("("))
        {
            _isValueExpression = true;
            var stringParts = _text.Split('"');

            var partList = new List<IPbsValueExpressionPart>();

            for (int i = 0; i < stringParts.Length; i++)
            {
                if (i % 2 == 0)
                {
                    var remains = stringParts[i]
                        .Replace("==", " = ")
                        .Replace("&&", " AND ")
                        .Replace("||", " OR ")
                        .Replace("!", " NOT ")
                        .Replace("$", "");

                    var matches = ActionRegex.Matches(remains);

                    remains = ActionFy(remains, matches, partList);

                    partList.Add(new PbsStringValue(remains));
                }
                else
                {
                    partList.Add(new PbsStringValue(" \"" + stringParts[i] + "\" "));
                }
            }

            _parts = partList.ToArray();

            if (UseActionCache)
            {
                ActionCache[_text] = this;
            }

            return;
        }

        var objectMatches = Regex.Matches(_text, PbsInterpreter.TokenRegex);

        if (objectMatches.Count < 1)
        {
            if (UseActionCache)
            {
                ActionCache[_text] = this;
            }

            _alwaysFalse = true;
            return;
        }

        var p1 = _text.Split(objectMatches[0].Value, 2);

        _objectToken = objectMatches[0].Value.Trim();

        var parts = p1[1].Trim().Split(" ", 2);

        _actionToken = parts.Length > 0 ? parts[0].Trim() : "";
        _parameter = parts.Length > 1 ? parts[1].Trim() : "";

        if (UseActionCache)
        {
            ActionCache[_text] = this;
        }
    }

    private void RegisterActionsAndReservedTokens(string remains, MatchCollection matches,
        IList<IPbsValueExpressionPart> partList)
    {
        foreach (Match match in matches)
        {
            var _p = remains.Split(match.Value, 2);

            if (!string.IsNullOrWhiteSpace(_p[0]))
            {
                partList.Add(new PbsAction(_p[0]));
            }

            partList.Add(new PbsStringValue(match.Value));

            if (_p.Length > 1 && !string.IsNullOrEmpty(_p[1]))
            {
                remains = _p[1];
            }
            else
            {
                remains = "";
            }
        }

        if (!string.IsNullOrEmpty(remains))
        {
            partList.Add(new PbsAction(remains));
        }
    }

    private string ActionFy(string remains, MatchCollection matches, IList<IPbsValueExpressionPart> partList)
    {
        foreach (Match match in matches)
        {
            var _p = remains.Split(match.Value, 2);

            if (!string.IsNullOrEmpty(_p[0]))
            {
                partList.Add(new PbsStringValue(_p[0]));
            }

            // stk pop and stk pop
            var rm = match.Value;
            RegisterActionsAndReservedTokens(rm, ReservedTokenRegex.Matches(rm), partList);

            if (_p.Length > 1 && !string.IsNullOrEmpty(_p[1]))
            {
                remains = _p[1];
            }
        }

        return remains;
    }


    public IPbsValue Execute(IPbsEnvironment env)
    {
        if (UseActionCache && _proxyAction != null)
        {
            return _proxyAction.Execute(env);
        }


        if (_alwaysFalse)
            return PbsValue.Null;

        IPbsValue value;
        if (_isValueExpression)
        {
            if (PbsInterpreter.Log)
            {
                env.Log("action<" + _text + ">", "is value expression");
            }

            var newStr = new StringBuilder();

            foreach (var part in _parts)
            {
                var v = part.GetValue(env);
                newStr.Append(v);
            }

            var expressionString = newStr.ToString();

            if (PbsInterpreter.Log)
            {
                env.Log("action<" + _text + ">", "-> " + expressionString);
            }

            try
            {
                var expr = _ctx.CompileDynamic(expressionString);

                var result = expr.Evaluate();

                var r = new PbsValue(result);

                if (PbsInterpreter.Log)
                {
                    env.Log("action<" + _text + ">", "==> " + (r.ObjectValue?.ToString() ?? "null"));
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


        if (PbsInterpreter.Log) env.Log("action<" + _text + ">", $"=> {value.AsString()}");

        return value;
    }
}