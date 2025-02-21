using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Ctrl : MonoBehaviour
{
    Rigidbody rb;

    [Header("Animator")]
    public Animator playerAnimator;

    [Header("Camera Settings")]
    public float mouseSpeed = 100f;  // 마우스 감도
    public Transform cameraPivot;    // 카메라 피벗(머리 위치)
    public Transform cam;            // 실제 카메라

    private float yRotation = 0f;
    private float xRotation = 0f;

    [Header("Move")]
    public float moveSpeed = 5f;
    private float h;
    private float v;

    [Header("Jump")]
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    private bool grounded;

    [Header("UI")]
    public float interactRange = 5f;         // E키 상호작용 범위
    public LayerMask interactableLayer;      // 상호작용 대상 레이어 (문 등)

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();     // Rigidbody 가져오기
        rb.freezeRotation = true;           // Rigidbody의 회전 잠금
        Cursor.lockState = CursorLockMode.Locked;  // 마우스 커서 고정
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
            // 카메라의 정면에서 Ray 발사하여 상호작용 감지
            Ray ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;

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
        // 마우스 이동값 가져오기
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        // 수평 회전 (좌우)
        yRotation += mouseX;

        // 수직 회전 (상하) - 클램프 적용하여 일정 각도 이상 안 움직이게
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 상하 각도 제한

        // 플레이어 회전 (좌우)
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // 카메라 회전 (상하) → cameraPivot을 기준으로 회전
        if (cameraPivot != null)
        {
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
    }

    void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveVec = transform.forward * v + transform.right * h;
        transform.position += moveVec.normalized * moveSpeed * Time.deltaTime;

        if (moveVec.magnitude > 0)
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
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    
}
