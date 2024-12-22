using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _moveX;
    public float MoveX { get { return _moveX; } }
    private float _moveZ;
    public float MoveZ { get { return _moveZ; } }

    private Vector3 _mousePosition;
    public Vector3 MousePosition { get { return _mousePosition; } }

    private void Update()
    {
        _mousePosition = Input.mousePosition;

        _moveX = Input.GetAxisRaw("Horizontal");
        _moveZ = Input.GetAxisRaw("Vertical");
    }
}
