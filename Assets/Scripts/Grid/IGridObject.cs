using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridObject
{
    // Returns the cell an object takes, first element of tuple is matrix 
    // | t t t | ma matrix that indicates objects takes 3 cells on top row, then two and then one
    // | t t f |
    // | t f f | 
    // second element should be tuple 3 3
    Tuple<bool[,], Tuple<int, int>> Space();
    Tuple<int, int> GridPosition();
    // Says to the object that it should kill and clean for itself.
    void kill();

    string Tag();

    void SetGridPosition(int x, int y);
}