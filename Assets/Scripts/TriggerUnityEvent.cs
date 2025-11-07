using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;


public class TriggerUnityEvent : MonoBehaviour
{
    public UnityEvent unityAction;
    public void OnTrggerEnter2D(Collision collision)
    {
        unityAction.Invoke();
    }
}
