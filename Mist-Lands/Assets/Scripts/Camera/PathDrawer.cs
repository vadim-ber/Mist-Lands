using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PathDrawer", menuName = "ScriptableObjects/Create PathDrawer")]
public class PathDrawer : ScriptableObject
{
    [SerializeField] private GameObject _linePrefab;
    [SerializeField] private GameObject _waypointPrefab;
    private Clicker _clicker;
    private GameObject _waypoint;
    private GameObject _line;
    private SpriteRenderer _spriteRenderer;
    private LineRenderer _lineRenderer;
    private List<Vector3> _wayPoints;

    public void Initialize(Clicker clicker)
    {
        _clicker = clicker;
        _waypoint = Instantiate(_waypointPrefab, _clicker.transform);
        _waypoint.SetActive(false);
        _spriteRenderer = _waypoint.GetComponentInChildren<SpriteRenderer>();
        _line = Instantiate(_linePrefab, _clicker.transform);
        _lineRenderer = _line.GetComponent<LineRenderer>();
        _clicker.OnUnitChanged += UnitHandler;
        _lineRenderer.positionCount = 0;
    }

    public void Refresh()
    {       
        DisplayLinePath();
    }

    private void DisplayLinePath()
    {
        if (_clicker.SelectedUnit == null || _clicker.SelectedUnit.Agent.path.corners.Length < 2)
        {
            _lineRenderer.positionCount = 0;
            _waypoint.SetActive(false);
            return;
        }

        _wayPoints = new List<Vector3>();
        Vector3 lastCorner = _clicker.SelectedUnit.Agent.path.corners[0];
        _wayPoints.Add(lastCorner);

        foreach (var currentCorner in _clicker.SelectedUnit.Agent.path.corners.Skip(1))
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
        if (_clicker.SelectedUnit == null)
        {
            return;
        }
        _spriteRenderer.color = newUnit.Outline.OutlineColor;
        _lineRenderer.startColor = _spriteRenderer.color;
    }    
}
