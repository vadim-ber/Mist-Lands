using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 100.0f;
    [SerializeField][Range(0, 1)] private float _movemenMultipler = 0.1f;
    private Vector3 _direction;
    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    void Update()
    {
        _direction = Vector3.zero;
        var keyboard = Keyboard.current;
        if(keyboard == null)
        {
            return;
        }
        _direction = new Vector3();
        if (keyboard.wKey.isPressed)
            _direction += Vector3.forward;
        if (keyboard.sKey.isPressed)
            _direction -= Vector3.forward;
        if (keyboard.aKey.isPressed)
            _direction -= Vector3.right;
        if (keyboard.dKey.isPressed)
            _direction += Vector3.right;

        _targetPosition = transform.position + _direction * _speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _movemenMultipler);
    }
}
