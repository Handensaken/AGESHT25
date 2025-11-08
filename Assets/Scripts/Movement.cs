using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static GameObject playerReference;

    [Header("MovementRelated")]
    [SerializeField] private float _upMovement;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private List<float> _addedAccelerationPerCheckPoint = new List<float>();
    private Rigidbody2D rb;
    private int _AccelerationAmount = 0;
    private float _InputX;
    [Header("Rotation")]
    [SerializeField] private GameObject _playerVisuals;
    [SerializeField] private GameObject _acualVisuals;
    [SerializeField] private float _setRotation = 45;
    [SerializeField] private float _rotationSpeed = 10;

    [Header("Camera")]
    [SerializeField] private GameObject _playerCamera;
    [SerializeField] private Vector3 _distanceFromPlayer;
    [Header("Canvas")]
    [SerializeField] private GameObject _restartCanvas;
    [SerializeField] private float _canvasDelay = 10f;


    private bool _IsDead = false;
    private bool _Win = false;
    private bool _GameStarted = false;
    float t = 0;


    void Start()
    {
        playerReference = this.gameObject;

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
        if (!_GameStarted) return;
        if (_IsDead) return;
        if (_Win)
        {
            PostWinMoveCamera();
        }
        else
        {
            MoveCamera();
            Move();
            LerpRotation(_setRotation * -_InputX);
        }
    }
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        _InputX = ctx.ReadValue<Vector2>().x;
    }
    public void StartGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _GameStarted = true;
        }
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(_InputX * _horizontalSpeed, GetSpeedUp());
    }
    private void LerpRotation(float rotation)
    {
        if (_playerVisuals == null) return;

        Quaternion quaternionRot = Quaternion.Euler(new Vector3(0, 0, rotation));

        _playerVisuals.transform.rotation = Quaternion.RotateTowards(_playerVisuals.transform.rotation, quaternionRot, _rotationSpeed * Time.deltaTime);
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
    private void PostWinMoveCamera()
    {
        _playerCamera.transform.position = new Vector3(_acualVisuals.transform.position.x, _acualVisuals.transform.position.y + _distanceFromPlayer.y, -_distanceFromPlayer.z);
    }
    private float GetSpeedUp()
    {
        t += Time.deltaTime;
        if (_addedAccelerationPerCheckPoint.Count < _AccelerationAmount)
        {
            return Mathf.Lerp(rb.linearVelocityY, _upMovement + _addedAccelerationPerCheckPoint[_addedAccelerationPerCheckPoint.Count], t);
        }
        return _upMovement + _addedAccelerationPerCheckPoint[_AccelerationAmount];
    }
    public void AddAcceleration()
    {
        _AccelerationAmount++;
        t = 0;
    }
    private void OnPlayerDeath()
    {
        _IsDead = true;
        rb.linearVelocity = Vector3.zero;
        SetRotationFloat(0);
        Cursor.visible = true;

        if (_restartCanvas == null) return;
        _restartCanvas.SetActive(true);
    }
    private void OnWin()
    {
        _Win = true;
        rb.linearVelocity = Vector3.zero;
        SetRotationFloat(0);
        Cursor.visible = true;
        Invoke(nameof(SetCanvas), _canvasDelay);
    }
    private void SetCanvas()
    {
        if (_restartCanvas == null) return;
        _restartCanvas.SetActive(true);
    }
}
