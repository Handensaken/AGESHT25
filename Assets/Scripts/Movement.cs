using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("MovementRelated")]
    [SerializeField] private float _upMovement;
    //[SerializeField] private float _acceleration;
    [SerializeField] private float _speed;
    private float _InputX;
    [Header("Camera")]
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _distanceFromPlayer;

    void Start()
    {
        if (_playerCamera != null)
        {
            _playerCamera.transform.parent = null;
        }
    }
    void Update()
    {
        Move();
        MoveCamera();
    }
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        _InputX = ctx.ReadValue<Vector2>().x;
    }
    private void Move()
    {
        transform.Translate(new Vector2(_InputX, _upMovement) * _speed * Time.deltaTime);
    }
    private void MoveCamera()
    {
        if (_playerCamera != null)
        {
            _playerCamera.transform.position =  new Vector3(0, transform.position.y, -_distanceFromPlayer);
        }
    }
}
