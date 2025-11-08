using UnityEngine;

public class SharcMovement : MonoBehaviour
{
    [SerializeField] private Transform _startPositon;
    [SerializeField] private Transform _endPositon;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _distanceCutOff = 1f;
    private Transform _TargetPosition;
    private SpriteRenderer _SpriteRenderer;
    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();

        transform.position = _startPositon.position;
        _TargetPosition = _endPositon;

        Vector3 temp = _startPositon.position;
        _startPositon.transform.parent = null;
        _startPositon.transform.position = temp;
        temp = _endPositon.position;
        _endPositon.transform.parent = null;
        _endPositon.transform.position = temp;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _TargetPosition.position, _moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _TargetPosition.position) < _distanceCutOff)
        {
            if (_TargetPosition == _endPositon)
            {
                _TargetPosition = _startPositon;
            }
            else if (_TargetPosition == _startPositon)
            {
                _TargetPosition = _endPositon;
            }
            _SpriteRenderer.flipX = !_SpriteRenderer.flipX;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsManager.instance.PlayerDeath();
        }
    }
}
