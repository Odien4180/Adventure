using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public TMP_Text Text => text;
    [SerializeField] private Transform floatingTransform;
    [SerializeField] private Vector3 floatingVector = Vector3.up;
    [SerializeField] private float floatingSpeedPerSec;
    [SerializeField] private float floatingTime;

    private float startTime;

    public static FloatingText Create(string text, Vector3 position, float radius = 0f, Transform parent = null)
    {
        var floatingText = Instantiate(Resources.Load<FloatingText>("Prefabs/FloatingText"));

        floatingText.transform.position = radius > 0f ? position.RandomPositionInSphere(radius) : position;
        floatingText.Text.text = text;

        return floatingText;
    }

    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        //지정된 floating 시간 도달
        if (Time.realtimeSinceStartup - startTime >= floatingTime)
        {
            Destroy(this.gameObject);
            return;
        }

        var moveVector = floatingVector * floatingSpeedPerSec * Time.deltaTime;
        floatingTransform.Translate(moveVector);
    }
}
