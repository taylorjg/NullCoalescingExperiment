using System.Collections.Generic;
using NUnit.Framework;

namespace NullCoalescingExperiment
{
    // ReSharper disable UnusedVariable
    // ReSharper disable NotAccessedField.Local

    public class Thing
    {
        public string Prop1 { get; set; }
        public int Prop2 { get; set; }
    }

    [TestFixture]
    internal class NullCoalescingTests
    {
        private static List<Thing> GetThings()
        {
            return new List<Thing>
                {
                    new Thing {Prop1 = "ABC", Prop2 = 123},
                    new Thing {Prop1 = "DEF", Prop2 = 456}
                };
        }

        private List<Thing> _things;

        [Test]
        public void DebuggerStrangenessInvolvingTheNullCoalescingOperatorAndAMemberVariable()
        {
            _things = GetThings() ?? new List<Thing>();
            var flag = true;
        }
    }
}
