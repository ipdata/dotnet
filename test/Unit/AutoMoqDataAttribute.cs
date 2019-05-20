using System;
using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace IpData.Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(CreateFixtrue)
        {
        }

        private static IFixture CreateFixtrue()
        {
            var fixture = new Fixture();

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            fixture.Customize(new AutoMoqCustomization());

            return fixture;
        }
    }
}
