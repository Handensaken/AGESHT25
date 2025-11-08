using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DeathBox : MonoBehaviour
{
    [SerializeField] private float _force = 0.05f;
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
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            mineVFXActivation.Bang();
            Invoke(nameof(Death), 0.2f);
            }
    }
}
