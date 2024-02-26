namespace MyClass {
    public static class Class1 {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T>? s, Predicate<T> onEvenPredicate,
            Predicate<T> onOddPredicate) {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            int count = 0;
            foreach (var el in s) {
                if ((count % 2 != 0 && onOddPredicate.Invoke(el)) || (count % 2 == 0 && onEvenPredicate.Invoke(el)))
                    yield return el;
                count++;
            }
        }
    }
}
