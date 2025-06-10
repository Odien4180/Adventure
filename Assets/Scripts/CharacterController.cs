using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject characterGo;
    [SerializeField] private float moveSpeed = 10f;
    // 쿼터뷰 카메라 회전값 (카메라 기준 방향으로 벡터 회전시킴)
    private Quaternion cameraRotation = Quaternion.Euler(0, 45f, 0); // Y축 회전만 반영

    [SerializeField] private string IDLE_STATE;
    [SerializeField] private string WALK_STATE;
    [SerializeField] private string RUN_STATE;
    [SerializeField] private string ATTACK01_STATE;
    [SerializeField] private string ATTACK02_STATE;
    [SerializeField] private string ATTACK_COOLDOWN_STATE;
    private int IDLE_HASH;
    private int WALK_HASH;
    private int RUN_HASH;
    private int ATTACK01_HASH;
    private int ATTACK02_HASH;
    private int ATTACK_COOLDOWN_HASH;

    private KeyCode stackedKeycode = KeyCode.None;

    private float velocity;
    private float maxValocityPeriod = 1f;
    void Start()
    {
        IDLE_HASH = Animator.StringToHash(IDLE_STATE);
        WALK_HASH = Animator.StringToHash(WALK_STATE);
        RUN_HASH = Animator.StringToHash(RUN_STATE);
        ATTACK01_HASH = Animator.StringToHash(ATTACK01_STATE);
        ATTACK02_HASH = Animator.StringToHash(ATTACK02_STATE);
        ATTACK_COOLDOWN_HASH = Animator.StringToHash(ATTACK_COOLDOWN_STATE);
    }

    void Update()
    {
        InputProcess();
    }

    private void InputProcess()
    {
        var state = animator.GetCurrentAnimatorStateInfo(0);
        //입력 불가능한 상태 (공격 모션 중)
        if ((state.IsName(ATTACK01_STATE) || state.IsName(ATTACK02_STATE)) && state.normalizedTime < 1.0f)
        {
            velocity = 0f;
            //25%이상 공격 모션 재생되고 있을 때 공격 키 입력
            if (state.normalizedTime > 0.25f && Input.GetKeyDown(KeyCode.X))
                stackedKeycode = KeyCode.X;

            return;
        }

        //attack 01-> 02 -> cooldown 상태 (다른 입력 없을 경우)
        // 75%이상 재생된 상태에서 예약된 공격 키가 있을 경우 다음 공격 모션
        if ((state.IsName(ATTACK01_STATE) || state.IsName(ATTACK02_STATE)) && state.normalizedTime > 0.75f)
        {
            velocity = 0f;
            if (stackedKeycode == KeyCode.X)
            {
                if (state.IsName(ATTACK01_STATE))
                    animator.CrossFade(ATTACK02_STATE, 0f);
                else
                    animator.CrossFade(ATTACK01_STATE, 0f);
                stackedKeycode = KeyCode.None;
            }
            else if (state.normalizedTime > 1.0f)
            {
                animator.CrossFade(ATTACK_COOLDOWN_HASH, 0f);
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.CrossFade(ATTACK01_HASH, 0f);
            return;
        }

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

        if (inputDir == Vector3.zero)
        {
            velocity = .0f;
            if (state.IsName(ATTACK_COOLDOWN_STATE) && state.normalizedTime <= 1f)
                return;
            else if (state.IsName(IDLE_STATE) == false)
                animator.CrossFade(IDLE_HASH, 0f);
        }
        else
        {
            // Y축 회전만 반영해서 평면상 입력 벡터 회전
            Vector3 moveDir = cameraRotation * inputDir.normalized;
            velocity += moveSpeed * Time.deltaTime / maxValocityPeriod;
            velocity = Mathf.Min(velocity, moveSpeed);
            transform.Translate(moveDir * velocity * Time.deltaTime, Space.World);

            //75%이상 가속하면 run
            if (velocity >= moveSpeed * 0.75f)
            {
                if (state.IsName(RUN_STATE) == false)
                    animator.CrossFade(RUN_HASH, 0f);
            }
            else
            {
                if (state.IsName(WALK_STATE) == false)
                    animator.CrossFade(WALK_HASH, 0f);
            }
        }
    }
}
