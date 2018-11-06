using System;

namespace Model
{
    // Tuple (krotka) to uporządkowany ciąg, niezmienny, stałej wielkości i różnorodnych obiektów, np. każdy obiekt określonego typu.
    // Krotka pozwala na szybkie grupowanie wielu wartości w jeden rezultat.

    public class TupleFour<T1, T2, T3, T4>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }
        public T4 Item4 { get; set; }

        public TupleFour() { }

        public static implicit operator TupleFour<T1, T2, T3, T4>(Tuple<T1, T2, T3, T4> t)
        {
            return new TupleFour<T1, T2, T3, T4>()
            {
                Item1 = t.Item1,
                Item2 = t.Item2,
                Item3 = t.Item3,
                Item4 = t.Item4
            };
        }

        public static implicit operator Tuple<T1, T2, T3, T4>(TupleFour<T1, T2, T3, T4> t)
        {
            return Tuple.Create(t.Item1, t.Item2, t.Item3, t.Item4);
        }


    }
}
