using UnityEngine;
using UnityEngine.Events;


public class TriggerUnityEvent : MonoBehaviour
{
    public UnityEvent unityAction;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            unityAction.Invoke();
        }
    }
}
