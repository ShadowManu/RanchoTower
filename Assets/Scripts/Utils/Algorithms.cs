using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algorithms
{
  public static void reshuffle(int[] arr)
  {
    // Knuth shuffle algorithm :: courtesy of Wikipedia :)
    for (int t = 0; t < arr.Length; t++)
    {
      var tmp = arr[t];
      int r = Random.Range(t, arr.Length);
      arr[t] = arr[r];
      arr[r] = tmp;
    }
  }
}