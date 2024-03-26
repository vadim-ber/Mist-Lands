using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Clicker : MonoBehaviour
{
    [SerializeField] private PathDrawer _pathDrawer;
    private Vector3 _clickedPosition;
    private Camera _camera;
    private Unit _selectedUnit;
    public event Action<Unit> OnUnitChanged;
    public event Action<Vector3> OnNewClickPosition;
    public Vector3 ClickedPosition
    {
        get => _clickedPosition;
    }
    public Unit SelectedUnit
    {
        get => _selectedUnit;
    }

    private void Start()
    {
        _camera = Camera.main;
        _pathDrawer.Initialize(this);
    }

    private void Update()
    {
        MouseLeftButtonHandler();
        MouseRightButtonHandler();
        _pathDrawer.Refresh();
    }

    private void MouseLeftButtonHandler()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var unit = hit.collider.gameObject.GetComponent<Unit>();
                if (unit)
                {
                    _selectedUnit = unit;
                    OnUnitChanged?.Invoke(_selectedUnit);
                }
                else
                {    
                    if (_selectedUnit == null)
                    {
                        return;
                    }
                    int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
                    if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, SelectedUnit.Agent.height, walkableMask))
                    {                        
                        _clickedPosition = navMeshHit.position;
                        OnNewClickPosition?.Invoke(_clickedPosition);
                    }
                }
            }
        }
    }

    private void MouseRightButtonHandler()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            _selectedUnit = null;
            OnUnitChanged?.Invoke(_selectedUnit);
        }
    }
}
