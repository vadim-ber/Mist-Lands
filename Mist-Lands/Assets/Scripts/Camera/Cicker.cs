using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Clicker", menuName = "ScriptableObjects/create clicker")]
public class Clicker : Selector
{
    [SerializeField] private PathDrawer _pathDrawer;
    private Camera _camera;

    public override void Initialize(Transform transform)
    {
        _camera = Camera.main;
        _pathDrawer.Initialize(this, transform);
    }

    public override void UpdateSelector(Transform transform)
    {
        HandleLeftClick();
        HandleRightClick();
        _pathDrawer.Refresh(transform);
    }

    private void HandleLeftClick()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider == null)
        {
            return;
        }
        var unit = hit.collider.gameObject.GetComponent<Unit>();
        if (unit)
        {
            _selectedUnit = unit;
            InvokeOnUnitSelected(_selectedUnit);
            return;
        }
        if (_selectedUnit == null)
        {
            return;
        }
        int walkableMask = 1 << NavMesh.GetAreaFromName("Walkable");
        if (NavMesh.SamplePosition(hit.point, out NavMeshHit navMeshHit, SelectedUnit.Agent.height, walkableMask))
        {
            _selectedPosition = navMeshHit.position;
           InvokeOnPositionSelected(_selectedPosition);
        }
    }

    private void HandleRightClick()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }
        _selectedUnit = null;
        InvokeOnUnitSelected(_selectedUnit);
    }
}
