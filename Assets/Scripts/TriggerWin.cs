using UnityEngine;

public class TriggerWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsManager.instance.Win();
        }
    }
}
