using UnityEngine;
namespace _CreativeGameAI_Scripts.Chapter04_PlayerCompanionExamples
{
public class SineWaveBob : MonoBehaviour
{
    [SerializeField]
    private float _amplitude = 0.25f;
    [SerializeField]
    private float _frequency = .5f; 

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;
    }

    void Update()
    {
        BobUpAndDown();
    }

    void BobUpAndDown()
    {
        float newY = Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.localPosition = new Vector3(_startPosition.x, _startPosition.y + newY, _startPosition.z);
    }
}
}
