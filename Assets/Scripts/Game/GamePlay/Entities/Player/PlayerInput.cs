using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float _moveX;
    public float MoveX { get { return _moveX; } }
    private float _moveZ;
    public float MoveZ { get { return _moveZ; } }
    private bool _isKeyDownR;
    public bool IsKeyDownR { get => _isKeyDownR; }

    private Vector3 _mousePosition;
    public Vector3 MousePosition { get { return _mousePosition; } }
    private bool _isLeftMouseButtonHeldDown;
    public bool IsLeftMouseButtonHeldDown { get => _isLeftMouseButtonHeldDown; }
    

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _isLeftMouseButtonHeldDown = Input.GetMouseButton(0);

        _moveX = Input.GetAxisRaw("Horizontal");
        _moveZ = Input.GetAxisRaw("Vertical");
        _isKeyDownR = Input.GetKeyDown(KeyCode.R);
    }
}
