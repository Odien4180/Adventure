using UnityEngine;

public class QuarterViewCamera : MonoBehaviour
{
    [SerializeField] private GameObject followGo;
    [SerializeField] private Vector3 offset = new Vector3(-1.5f, 5f, -6f);
    private void Update()
    {
        transform.rotation = Quaternion.Euler(30f, 45f, 0f);

        transform.position = followGo.transform.position + offset;
    }

}
