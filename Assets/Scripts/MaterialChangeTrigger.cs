using UnityEngine;

public class MaterialChangeTrigger : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float _smoothness = 0.97f;
    [SerializeField] private float _size = 0.72f;
    private bool _doingLerp = false;
    private float _timer = 0;
    private float _smoothnessStart;
    private float _sizeStart;
    void Update()
    {
        if (_doingLerp && _timer < 1.1f)
        {
            float a = Mathf.Lerp(_smoothnessStart, _smoothness, _timer);
            float b = Mathf.Lerp(_sizeStart, _size, _timer);

            SetMater(a, b);

            _timer += Time.deltaTime;
        }
    }
    private void SetMater(float a, float b)
    {
        material.SetFloat(Shader.PropertyToID("_Smoothness"), a);
        material.SetFloat(Shader.PropertyToID("_Size"), b);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _doingLerp = true;
            _smoothnessStart = material.GetFloat(Shader.PropertyToID("_Smoothness"));
            _sizeStart = material.GetFloat(Shader.PropertyToID("_Size"));
        }
    }
}
