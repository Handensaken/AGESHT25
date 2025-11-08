using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("MovementRelated")]
    [SerializeField] private float _upMovement;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private List<float> _addedAccelerationPerCheckPoint = new List<float>();
    private Rigidbody2D rb;
    private int _AccelerationAmount = 0;
    private float _InputX;
    [Header("Rotation")]
    [SerializeField] private GameObject _playerVisuals;
    [SerializeField] private float _setRotation = 45;

    [Header("Camera")]
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private Vector3 _distanceFromPlayer;

    private bool _IsDead = false;
    private bool _Win = false;
    private bool _GameStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning("AAHHHHHHHHHHHHHH!!!!!!!!!!!! FUCK FUCK FUCK... WE DONT HAVE A RIGIDBODY 2D!!! WHO DA FUCK IS MAKING THIS GAME!!!!");
        }

        if (_playerCamera != null)
        {
            _playerCamera.transform.parent = null;
        }
        Cursor.visible = false;
        GameEventsManager.instance.OnPlayerDeath += OnPlayerDeath;
        GameEventsManager.instance.OnWin += OnWin;
    }
    void OnDisable()
    {
        GameEventsManager.instance.OnPlayerDeath -= OnPlayerDeath;
        GameEventsManager.instance.OnWin -= OnWin;
    }
    void Update()
    {
        if (_IsDead || _Win) return;
        Move();
        MoveCamera();
        SetRotation();
    }
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        _InputX = ctx.ReadValue<Vector2>().x;
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(_InputX * _horizontalSpeed, GetSpeedUp());
    }
    private void SetRotation()
    {
        if (_playerVisuals == null) return;

        SetRotationFloat(_setRotation * -_InputX);
    }

    private void SetRotationFloat(float rotation)
    {
        _playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
    }
    private void MoveCamera()
    {
        if (_playerCamera != null)
        {
            _playerCamera.transform.position = new Vector3(0, transform.position.y + _distanceFromPlayer.y, -_distanceFromPlayer.z);
        }
    }
    private float GetSpeedUp()
    {
        if (_addedAccelerationPerCheckPoint.Count < _AccelerationAmount)
        {
            return _upMovement + _addedAccelerationPerCheckPoint[_addedAccelerationPerCheckPoint.Count];
        }
        return _upMovement + _addedAccelerationPerCheckPoint[_AccelerationAmount];
    }
    public void AddAcceleration()
    {
        _AccelerationAmount++;
    }
    private void OnPlayerDeath()
    {
        _IsDead = true;
        rb.linearVelocity = Vector3.zero;
        Cursor.visible = true;
    }
    private void OnWin()
    {
        _Win = true;
        rb.linearVelocity = Vector3.zero;
        SetRotationFloat(0);
        Cursor.visible = true;
    }
}
