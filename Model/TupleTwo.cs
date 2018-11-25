using System;

namespace Model
{
    // Tuple (krotka) to uporządkowany ciąg, niezmienny, stałej wielkości i różnorodnych obiektów, np. każdy obiekt określonego typu.
    // Krotka pozwala na szybkie grupowanie wielu wartości w jeden rezultat.

    public class TupleTwo<T1, T2>
    {
        public T1 Item1 { get; set; }
        public T2 Item2 { get; set; }

        public TupleTwo() { }

        public static implicit operator TupleTwo<T1, T2>(Tuple<T1, T2> t)
        {
            return new TupleTwo<T1, T2>()
            {
                Item1 = t.Item1,
                Item2 = t.Item2,
            };
        }

        public static implicit operator Tuple<T1, T2>(TupleTwo<T1, T2> t)
        {
            return Tuple.Create(t.Item1, t.Item2);
        }


    }
}
