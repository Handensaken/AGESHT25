using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] private Material _material;
    private float _Smoothness;
    private float _Size;
    void Start()
    {
        _Smoothness = _material.GetFloat(Shader.PropertyToID("_Smoothness"));
        _Size = _material.GetFloat(Shader.PropertyToID("_Size"));
    }
    void OnDisable()
    {
        _material.SetFloat(Shader.PropertyToID("_Smoothness"), _Smoothness);
        _material.SetFloat(Shader.PropertyToID("_Size"), _Size);
    }
}
