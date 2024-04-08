using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PathDrawer", menuName = "ScriptableObjects/Create PathDrawer")]
public class PathDrawer : ScriptableObject
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _waypointPrefab;
    private Selector _selector;
    private GameObject _waypoint;
    private GameObject _line;
    private SpriteRenderer _spriteRenderer;
    private LineRenderer _lineRenderer;
    private List<Vector3> _wayPoints;

    public void Initialize(Selector selector, Transform transform,
        GameObject linePrefab, GameObject waypointPrefab)
    {
        _selector = selector;
        _linePrefab = linePrefab;
        _waypointPrefab = waypointPrefab;
        _waypoint = Instantiate(_waypointPrefab, transform);
        _waypoint.SetActive(false);
        _spriteRenderer = _waypoint.GetComponentInChildren<SpriteRenderer>();
        _line = Instantiate(_linePrefab, transform);
        _lineRenderer = _line.GetComponent<LineRenderer>();
        _selector.OnNewUnitSelected += UnitHandler;
        _lineRenderer.positionCount = 0;
    }

    public void UpdatePath(Transform transform)
    {
        DisplayLinePath(transform);
    }

    private void DisplayLinePath(Transform transform)
    {
        if (_selector.SelectedUnit == null || _selector.SelectedUnit.Agent.path.corners.Length < 2)
        {
            _lineRenderer.positionCount = 0;
            _waypoint.SetActive(false);
            return;
        }

        _wayPoints = new List<Vector3>();
        Vector3 lastCorner = _selector.SelectedUnit.Agent.path.corners[0];
        _wayPoints.Add(lastCorner);

        foreach (var currentCorner in _selector.SelectedUnit.Agent.path.corners.Skip(1))
        {
            if (Physics.Linecast(lastCorner, currentCorner, out var hit))
            {
                _wayPoints.Add(hit.point);
            }
            _wayPoints.Add(currentCorner);
            lastCorner = currentCorner;
        }

        _lineRenderer.positionCount = _wayPoints.Count;
        for (var i = 0; i < _wayPoints.Count; i++)
        {
            _lineRenderer.SetPosition(i, _wayPoints[i]);
        }

        _waypoint.transform.position = lastCorner;
        _waypoint.SetActive(true);
    }

    private void UnitHandler(Unit newUnit)
    {
        _lineRenderer.positionCount = 0;
        if (_selector.SelectedUnit == null)
        {
            return;
        }
        _spriteRenderer.color = newUnit.Outline.OutlineColor;
        _lineRenderer.startColor = _spriteRenderer.color;
    }    
}
