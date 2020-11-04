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

    public sealed class TestCategory : CategoryAttribute
    {
        public TestCategory() : base("Test")
        { }
    }

    public sealed class NumericCategory : CategoryAttribute
    {
        public NumericCategory() : base("Numeric")
        { }
    }

}
