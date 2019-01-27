using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<A,B>
{
    public A Item1 { get; set; }
    public B Item2 { get; set; }

    public Tuple(A _item1, B _item2)
    {
        this.Item1 = _item1;
        this.Item2 = _item2;
    }
}
