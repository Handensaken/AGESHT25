using System;
using UnityEditor.EditorTools;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Moray : MonoBehaviour
{
    [SerializeField] private float _playerCloseDuration = 1f;
    [SerializeField] private GameObject _visuals;
    [SerializeField] private Transform _mouthPoint;
    [SerializeField] private float _attackMovementSpeed = 4;
    private bool _haveAttacked = false;
    private bool _startTimer = false;
    private float _timer = 0;
    private bool _Attacking = false;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Update()
    {
        if (_haveAttacked) return;
        if (_Attacking)
        {
            Attacking();
        }

        if (_startTimer)
        {
            _timer += Time.deltaTime;

            if (_timer >= _playerCloseDuration)
            {
                _Attacking = true;
            }
        }

        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        Vector3 targ = Movement.playerReference.transform.position;
        Vector3 objectPos = transform.position;

        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        _visuals.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void Attacking()
    {
        if (Vector3.Distance(_mouthPoint.position, Movement.playerReference.transform.position) > 0.2)
        {
            _mouthPoint.transform.position = Vector3.MoveTowards(_mouthPoint.transform.position,
            Movement.playerReference.transform.position ,
            _attackMovementSpeed * Time.deltaTime);
        }
        else
        {
            Attack();
        }
    }
    private void Attack()
    {
        GameEventsManager.instance.PlayerDeath();
        _haveAttacked = true;
        Movement.playerReference.transform.position = _mouthPoint.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _startTimer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _startTimer = false;
            _timer = 0;
        }
    }
}
