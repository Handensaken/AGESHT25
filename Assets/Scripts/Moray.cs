using System;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class Moray : MonoBehaviour
{
    [SerializeField] private float _playerCloseDuration = 1f;
    [SerializeField] private GameObject _visuals;
    [SerializeField] private GameObject _player;
    private bool _startTimer = false;
    private float _timer = 0;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Update()
    {
        if (_startTimer)
        {
            _timer += Time.deltaTime;

            if (_timer >= _playerCloseDuration)
            {
                Attack();
            }
        }

        LookAtPlayer();
    }
    private void LookAtPlayer()
    {
        if (_player == null) return;

        Vector3 targ = _player.transform.position;
        Vector3 objectPos = transform.position;

        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void Attack()
    {

        Debug.Log("attack");
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
