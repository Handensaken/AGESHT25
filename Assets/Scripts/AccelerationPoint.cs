using UnityEngine;

public class AccelerationPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("AHHHHHHHHHHH!!!!!!");
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Movement>().AddAcceleration();
        }
    }
}
