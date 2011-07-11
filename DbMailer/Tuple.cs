namespace DbMailer
{
    internal class Tuple<T, T1>
    {
        public T Item1 { get; set; }

        public T1 Item2 { get; set; }

        public Tuple(T item1, T1 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public static Tuple<TC, TC1> Create<TC, TC1>(TC item1, TC1 item2)
        {
            return new Tuple<TC, TC1>(item1, item2);
        }
    }
}