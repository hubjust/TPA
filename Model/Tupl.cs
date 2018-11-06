using System;

namespace Model
{
    // Tuple (krotka) to uporządkowany ciąg, niezmienny, stałej wielkości i różnorodnych obiektów, np. każdy obiekt określonego typu.
    // Krotka pozwala na szybkie grupowanie wielu wartości w jeden rezultat.

    public class Tupl<T1, T2, T3>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }
        public T3 Item3 { get; set; }

        public Tupl() { }

        public static implicit operator Tupl<T1, T2, T3>(Tuple<T1, T2, T3> t)
        {
            return new Tupl<T1, T2, T3>()
            {
                Item1 = t.Item1,
                Item2 = t.Item2,
                Item3 = t.Item3
            };
        }

        public static implicit operator Tuple<T1, T2, T3>(Tupl<T1, T2, T3> t)
        {
            return Tuple.Create(t.Item1, t.Item2, t.Item3);
        }


    }
}
