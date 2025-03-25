using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _points = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
            _points[i] = transform.GetChild(i);
    }
#endif

    public Transform[] GetPoints()
    {
        return _points;
    }
}
