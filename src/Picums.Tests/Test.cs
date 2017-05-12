using System
using System.Collections.Generic;
using System.Text;
using TestContext = System.Collections.Generic.Dictionary<string, (object, System.Type)>;

namespace Picums.Tests
{

    public sealed class Test
    {
        private readonly string description;
        private readonly TestContext context;

        private Test(string description) : this(description, new TestContext()) { }
        private Test(string description, Dictionary<string, (object, Type)> context)
        {
            this.description = description;
            this.context = context;
        }

        public static Test For(string description) => new Test(description);

        public Test Given<T>(string command, Func<T> setup)
            => this.Given(command, nameof(T), setup);

        public Test Given<T>(string command, string name, Func<T> setup)
        {
            this.context[name] = (setup(), typeof(T));
            return this.Morph(this.context);
        }

        public Test When(string command, Func<TestContext, TestContext> action)
        {
            return this.Morph(action(this.context));
        }

        public Test Then(string command, Func<TestContext, TestContext> action)
        {
            return this.Morph(action(this.context));
        }

        private Test Morph(Dictionary<string, (object, Type)> context)
            => new Test(this.description, context);
    }
}
