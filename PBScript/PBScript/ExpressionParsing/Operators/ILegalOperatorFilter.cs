using PBScript.Environment;
using PBScript.Interfaces;

namespace PBScript.ExpressionParsing.Operators;

public interface ILegalOperatorFilter
{
    public delegate bool FilterDelegate(PbsValue[] values);
    public int ValueCount { get; }
    public bool Fits(PbsValue[] values);
}

public class LegalOperatorFilter: ILegalOperatorFilter
{
    public static readonly LegalOperatorFilter TwoNumbers = LegalOperatorFilter.ForValueCount(2).Where.OnlyNumbers().Build();
    public static readonly LegalOperatorFilter TwoBooleans = LegalOperatorFilter.ForValueCount(2).Where.OnlyBooleans().Build();
    
    private readonly ILegalOperatorFilter.FilterDelegate _delegate;

    public static LegalOperatorFilterBuilder ForValueCount(int valueCount) =>
        LegalOperatorFilterBuilder.ForValueCount(valueCount);
    

    public int ValueCount { get; }
    
    public bool Fits(PbsValue[] values)
    {
        return _delegate(values);
    }


    private LegalOperatorFilter(ILegalOperatorFilter.FilterDelegate @delegate, int valueCount)
    {
        _delegate = @delegate;
        ValueCount = valueCount;
    }
    
    public class LegalOperatorFilterBuilder
    {
        public readonly int ValueCount;
        private List<ILegalOperatorFilter.FilterDelegate> Ors = new();
        private List<ILegalOperatorFilter.FilterDelegate> CurrentAnds = new();

        private LegalOperatorFilterBuilder(int valueCount)
        {
            ValueCount = valueCount;
            And = new(this);
        }

        public LegalOperatorDelegateBuilder And { get; }
        
        /// <summary>
        /// Does the same as "And", but allows for cleaner, more descriptive Filter Creation.
        /// </summary>
        public LegalOperatorDelegateBuilder Where => And;
        
        public LegalOperatorDelegateBuilder Or
        {
            get
            {
                if (CurrentAnds.Count <= 0) return new LegalOperatorDelegateBuilder(this);
                var ands = CurrentAnds;
                Ors.Add(values =>
                {
                    return ands.All(filter => filter(values));
                });
                CurrentAnds = new List<ILegalOperatorFilter.FilterDelegate>();

                return new LegalOperatorDelegateBuilder(this);
            }
        }

        public LegalOperatorFilter Build()
        {
            // Turn Ands to one statement
            if (CurrentAnds.Count > 0)
            {
                var ands = CurrentAnds;
                Ors.Add(values =>
                {
                    return ands.All(filter => filter(values));
                });
            }
            
            // turn all ors to one statement
            if (Ors.Count == 0)
            {
                return new LegalOperatorFilter(values => values.Length >= ValueCount, ValueCount);
            }

            var ors = Ors;
            return new LegalOperatorFilter(values =>
            {
                return values.Length >= ValueCount && ors.Any(filter => filter(values));
            }, ValueCount);
        }


        public static LegalOperatorFilterBuilder ForValueCount(int valueCount)
        {
            return new LegalOperatorFilterBuilder(valueCount);
        }

        private void AddAnd(ILegalOperatorFilter.FilterDelegate f) => CurrentAnds.Add(f);
        
        public class LegalOperatorDelegateBuilder
        {
            private readonly LegalOperatorFilterBuilder _builder;

            public LegalOperatorDelegateBuilder(LegalOperatorFilterBuilder builder)
            {
                _builder = builder;
            }
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="minAmount">Minimum amount of variables of the type filtered for.</param>
            /// <param name="type">Filtered Variable Type (Undefined for any)</param>
            /// <returns></returns>
            private LegalOperatorFilterBuilder MinimumVariableOfTypeAmount(int minAmount, VariableType type)
            {
                _builder.AddAnd(values =>
                {
                    // can't contain 5 strings with 4 values...
                    if (values.Length < minAmount)
                    {
                        return false;
                    }
                    
                    var count = 0;
                    foreach (var value in values)
                    {
                        if ((value.ReturnType != type && type != VariableType.Undefined) || value.IsLocked) continue;
                        count++;

                        if (count >= minAmount)
                        {
                            return true;
                        }
                    }

                    return count >= minAmount;
                });
                return _builder;
            }

            public LegalOperatorFilterBuilder MinimumVariableAmount(int minAmount) =>
                MinimumVariableOfTypeAmount(minAmount, VariableType.Undefined);

            public LegalOperatorFilterBuilder MinimumStringVariableAmount(int minAmount) =>
                MinimumVariableOfTypeAmount(minAmount, VariableType.String);

            public LegalOperatorFilterBuilder MinimumNumericVariableAmount(int minAmount) =>
                MinimumVariableOfTypeAmount(minAmount, VariableType.Number);

            public LegalOperatorFilterBuilder MinimumBooleanVariableAmount(int minAmount) =>
                MinimumVariableOfTypeAmount(minAmount, VariableType.Boolean);

            public LegalOperatorFilterBuilder FirstIsVariable()
            {
                _builder.AddAnd(v => v.Length > 0 && !v[0].IsLocked);
                return _builder;
            }

            public LegalOperatorFilterBuilder FirstIsVariableOfType(VariableType type)
            {
                _builder.AddAnd(v => v.Length > 0 && !v[0].IsLocked && v[0].ReturnType == type);
                return _builder;
            }

            private LegalOperatorFilterBuilder MinimumAmountOfType(int minAmount, VariableType type)
            {
                _builder.AddAnd(values =>
                {
                    // can't contain 5 strings with 4 values...
                    if (values.Length < minAmount)
                    {
                        return false;
                    }
                    
                    var count = 0;
                    foreach (var value in values)
                    {
                        if (value.ReturnType != type) continue;
                        count++;

                        if (count >= minAmount)
                        {
                            return true;
                        }
                    }

                    return count >= minAmount;
                });
                return _builder;
            }

            public LegalOperatorFilterBuilder MinimumStringAmount(int minAmount) =>
                MinimumAmountOfType(minAmount, VariableType.String);
            
            public LegalOperatorFilterBuilder MinimumNumberAmount(int minAmount) =>
                MinimumAmountOfType(minAmount, VariableType.Number);
            
            public LegalOperatorFilterBuilder MinimumBooleanAmount(int minAmount) =>
                MinimumAmountOfType(minAmount, VariableType.Boolean);
            
            public LegalOperatorFilterBuilder MinimumNullValueAmount(int minAmount) =>
                MinimumAmountOfType(minAmount, VariableType.Null);
            
            public LegalOperatorFilterBuilder OnlyStrings() =>
                MinimumAmountOfType(_builder.ValueCount, VariableType.String);
            
            public LegalOperatorFilterBuilder OnlyNumbers() =>
                MinimumAmountOfType(_builder.ValueCount, VariableType.Number);
            
            public LegalOperatorFilterBuilder OnlyBooleans() =>
                MinimumAmountOfType(_builder.ValueCount, VariableType.Boolean);
            
            public LegalOperatorFilterBuilder OnlyNullValues() =>
                MinimumAmountOfType(_builder.ValueCount, VariableType.Null);
        }
    }
    
    
}


