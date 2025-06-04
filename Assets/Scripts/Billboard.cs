using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private bool freezeXZAxis;
    private Camera TargetCamera => Camera.main;

    private void Update()
    {
        if (freezeXZAxis)
            transform.rotation = Quaternion.Euler(0f, TargetCamera.transform.eulerAngles.y, 0f);
        else
            transform.rotation = TargetCamera.transform.rotation;
    }
}
