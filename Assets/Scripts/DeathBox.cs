using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class DeathBox : MonoBehaviour
{
    void Start()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsManager.instance.PlayerDeath();
        }
    }
}
