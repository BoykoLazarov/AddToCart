using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace AddToCart.Attributes
{
    public class NameAttribute : NUnitAttribute, IApplyToTest, IApplyToContext
    {
        private readonly string _name;

        public NameAttribute(string name)
        {
            _name = name;
        }

        public void ApplyToContext(TestExecutionContext context)
        {
            context.CurrentTest.Properties.Add("Name", _name);
        }

        public void ApplyToTest(Test test)
        {
            test.Properties.Add("Name", _name);
        }
    }
}