using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Ctrl : MonoBehaviour
{
    Rigidbody rb;
    [Header("Animator")]
    public Animator playerAnimator;

    [Header("Rotate")]
    public float mouseSpeed;
    float yRotation;
    float xRotation;
    public Transform cam;

    [Header("Move")]
    public float moveSpeed;
    float h;
    float v;

    [Header("Jump")]
    public float jumpForce;

    [Header("Ground Check")]
    public float playerHeight;
    bool grounded;


    [Header("UI")]
    public float interactRange = 3f;         // E키 상호작용 범위
    public LayerMask interactableLayer;      // 상호작용 대상 레이어 (문 등)


    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서를 화면 안에서 고정

        //cam = Camera.main;
        rb = GetComponent<Rigidbody>();             // Rigidbody 컴포넌트 가져오기
        rb.freezeRotation = true;                   // Rigidbody의 회전을 고정하여 물리 연산에 영향을 주지 않도록 설정
        
        
    }

    void Update()
    {
        Rotate();
        Move();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // 카메라의 마우스 위치를 기준으로 레이 발사
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // interactableLayer를 사용해 필요한 오브젝트만 감지
            if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    void Rotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime;

        yRotation += mouseX;    // 마우스 X축 입력에 따라 수평 회전 값을 조정
        xRotation -= mouseY;    // 마우스 Y축 입력에 따라 수직 회전 값을 조정

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 수직 회전 값을 -90도에서 90도 사이로 제한

        
        transform.rotation = Quaternion.Euler(0, yRotation, 0);             // 플레이어 캐릭터의 회전을 조절

        // 카메라에는 수직 회전 적용 (xRotation만)
        if (cam != null)
        {
            cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal"); // 수평 이동 입력 값
        v = Input.GetAxisRaw("Vertical");   // 수직 이동 입력 값

        // 입력에 따라 이동 방향 벡터 계산
        Vector3 moveVec = transform.forward * v + transform.right * h;

        // 이동 벡터를 정규화하여 이동 속도와 시간 간격을 곱한 후 현재 위치에 더함
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;
        // 플레이어가 움직이고 있는지 확인
        if (moveVec.magnitude > 0) // 이동 벡터가 0이 아니라면 (즉, 입력이 있을 때)
        {
            playerAnimator.SetBool("Walk", true);
        }
        else
        {
            playerAnimator.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // 힘을 가해 점프
    }
}