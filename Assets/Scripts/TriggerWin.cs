using UnityEngine;
using UnityEngine.Playables;

public class TriggerWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsManager.instance.Win();




            collision.gameObject.GetComponent<PlayableDirector>().Play();
        }
    }
}
