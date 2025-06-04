using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject characterGo;
    private float moveSpeed = 5f;
    // 쿼터뷰 카메라 회전값 (카메라 기준 방향으로 벡터 회전시킴)
    private Quaternion cameraRotation = Quaternion.Euler(0, 45f, 0); // Y축 회전만 반영

    void Update()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        if (Input.anyKey == false)
        {
            if (state.IsName("idle") == false)
                animator.CrossFade("idle", 0f);
            return;
        }

        if (state.IsName("walk") == false)
            animator.CrossFade("walk", 0f);

        Vector3 inputDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            inputDir += Vector3.forward;
        if (Input.GetKey(KeyCode.DownArrow))
            inputDir += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow))
            inputDir += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow))
            inputDir += Vector3.right;

        if (inputDir.x < 0f)
            characterGo.transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (inputDir.x > 0f)
            characterGo.transform.localScale = Vector3.one;

        if (inputDir != Vector3.zero)
        {
            // Y축 회전만 반영해서 평면상 입력 벡터 회전
            Vector3 moveDir = cameraRotation * inputDir.normalized;
            transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
