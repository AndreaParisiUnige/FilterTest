using System.Runtime.CompilerServices;
using MyClass;

namespace TestProject1 {

    [TestFixture]
    public class Tests {

        [Test]
        public void Test1() {
            var s = new[] { "banana", "plutocratico", "questo proprio no", "pera", "questo si", "mela" };
            var onEvenPredicate = new Predicate<string>(x => x.Length < 10);
            var onOddPredicate = new Predicate<string>(x => x.First() == 'p');
            var result = s.Filter(onEvenPredicate, onOddPredicate).ToArray();
            Assert.That(result, Is.EqualTo(new[] { "banana", "plutocratico", "pera", "questo si"}));
        }


        [TestCase(new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4, 5})]
        [TestCase(new[] {1})]
        public void Test2(int[] source) {
            if(source.Length < 2)
                Assert.Inconclusive("The lenght of the source must be at least 2");
            var onEvenPredicate = new Predicate<int>(x => true);
            var onOddPredicate = new Predicate<int>(x => false);
            var expected = new List<int>();
            for (int i = 0; i < source.Length; i++) 
                if(i%2==0) expected.Add(source[i]);
            var result = source.Filter<int>(onEvenPredicate, onOddPredicate).ToArray();
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Test3() {
            var onEvenPredicate = new Predicate<string>(x => x.Last() == 'a');
            var onOddPredicate = new Predicate<string>(x => x.Last() == 'a');
            var result = Infinite().Filter(onEvenPredicate, onOddPredicate).Take(100);
            var expected = Infinite().Where(x => x.Last() == 'a').Take(100);
            Assert.That(result, Is.EqualTo(expected));
        }

        private IEnumerable<string> Infinite() {
            char OtherChar(char c) {
                if (c == 'a') return 'b';
                return 'a';
            }
            char LastChar(string s) {
                return s.Last();
            }

            string x;
            string? y = null;
            x = y;

            var s = new List<string> { "a" }; ;
            yield return "a";
            s.Add("b");
            yield return "b";
            while (true) {
                s.Add(s.First() + OtherChar(LastChar(s.First())));
                yield return s.Last();
                s.RemoveAt(0);
            }
        }

        [Test]
        public void Test4() {
            int[]? s = null;
            var onEvenPredicate = new Predicate<int>(x => true);
            var onOddPredicate = new Predicate<int>(x => false);
            Assert.That(() => s.Filter(onEvenPredicate, onOddPredicate).Count(), Throws.TypeOf<ArgumentNullException>());
        }
    }
}