using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionsHistory
{
    private readonly int _maxSize;
    private List<Vector3> _positions = new List<Vector3>();

    public PositionsHistory(int maxSize)
    {
        _maxSize = maxSize;
    }

    public void AddPosition(Vector3 position)
    {
        _positions.Add(position);
        if (_positions.Count > _maxSize)
        {
            _positions.RemoveAt(0);
        }
    }

    public int Count()
    {
        return _positions.Count;
    }

    public float GetMaxDistance(Vector3 position)
    {
        return _positions.Max(_ => Vector3.Distance(_, position));
    }

    public void Clear()
    {
        _positions.Clear();
    }
}