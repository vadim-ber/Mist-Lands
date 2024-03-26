using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    private Vector3 _direction;

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            _direction = new Vector3();
            if (keyboard.wKey.isPressed)
                _direction += Vector3.forward;
            if (keyboard.sKey.isPressed)
                _direction -= Vector3.forward;
            if (keyboard.aKey.isPressed)
                _direction -= Vector3.right;
            if (keyboard.dKey.isPressed)
                _direction += Vector3.right;
        }
    }

    void FixedUpdate()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
