using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{    
    [SerializeField] private float _speed = 100.0f;
    [SerializeField][Range(0, 1)] private float _multipler = 0.1f;
    private InputActionAsset _actionAsset;
    private InputAction _cameraMove;
    private Vector3 _targetPosition;
    private Vector3 _convertedDirection;
    private Vector2 _inputDirection;    

    private void Start()
    {
        _actionAsset = Resources.Load<InputActionAsset>("Input/Input");
        _cameraMove = _actionAsset.FindAction("CameraMove");
        _cameraMove.Enable();
        _convertedDirection = Vector3.zero;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void OnDisable()
    {
        _cameraMove.Disable();
    }

    private void MoveCamera()
    {
        _inputDirection = _cameraMove.ReadValue<Vector2>();
        _convertedDirection.x = _inputDirection.x;
        _convertedDirection.z = _inputDirection.y;
        _targetPosition = transform.position + _speed * Time.deltaTime * _convertedDirection;
        transform.position = Vector3.Slerp(transform.position, _targetPosition, _multipler);
    }
}
