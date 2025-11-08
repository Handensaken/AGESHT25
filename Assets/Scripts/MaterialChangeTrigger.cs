using UnityEngine;

public class MaterialChangeTrigger : MonoBehaviour
{
    public Material material;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hej");
            material.SetFloat("Smoothness", 0.97f);
            material.SetFloat("Size", 0.72f);
        }
    }
}
