using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class MineVFXActivation : MonoBehaviour
{
    [SerializeField] private VisualEffect MineEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Bang()
    {
        time = 0;
        StartCoroutine(gababa());
    }
    float time = 0;
    private IEnumerator gababa()
    {
        while (time < 1)
        {
            time += Time.deltaTime * 5;
            MineEffect.SetFloat(Shader.PropertyToID("EmissionT"), time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        MineEffect.SetBool(Shader.PropertyToID("Fuckoff"), true);
        MineEffect.SendEvent(Shader.PropertyToID("Explode"));

    }
}
