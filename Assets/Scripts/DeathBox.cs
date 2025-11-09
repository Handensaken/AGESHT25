using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DeathBox : MonoBehaviour
{
    [SerializeField] private float _force = 0.05f;
    [SerializeField] private float _mineDelay = 0.2f;
    [SerializeField] private float _startAudioTime = 1f;
    [SerializeField] private AudioSource _boom;
    public MineVFXActivation mineVFXActivation;
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void Death()
    {
        GameEventsManager.instance.PlayerDeath();
        Vector3 dir = (Movement.playerReference.transform.position - transform.position).normalized;
        Movement.playerReference.GetComponent<Rigidbody2D>().AddForce(dir * 0.05f);

        if (_boom != null)
        {
            _boom.Play();
            _boom.time = _startAudioTime;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            mineVFXActivation.Bang();
            Invoke(nameof(Death), _mineDelay);
        }
    }
}
