using System.ComponentModel;

namespace Neulib
{
    public sealed class RandomizeCategory : CategoryAttribute
    {
        public RandomizeCategory() : base("Randomize")
        { }
    }
    
    public sealed class LearnCategory : CategoryAttribute
    {
        public LearnCategory() : base("Learn")
        { }
    }

    public sealed class VerifyCategory : CategoryAttribute
    {
        public VerifyCategory() : base("Verify")
        { }
    }

    public sealed class NumericCategory : CategoryAttribute
    {
        public NumericCategory() : base("Numeric")
        { }
    }

}
