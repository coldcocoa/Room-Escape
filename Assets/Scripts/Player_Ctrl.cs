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
    public float mouseSpeed = 100f;  // ���콺 ����
    public Transform cameraPivot;    // ī�޶� �ǹ�(�Ӹ� ��ġ)
    public Transform cam;            // ���� ī�޶�

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
    public float interactRange = 5f;         // EŰ ��ȣ�ۿ� ����
    public LayerMask interactableLayer;      // ��ȣ�ۿ� ��� ���̾� (�� ��)

    [Header("Puzzle Interaction")]
    public LayerMask puzzleLayer; // ���� ��ȣ�ۿ� ���̾�

    public bool isPuzzleModeActive = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();     // Rigidbody ��������
        rb.freezeRotation = true;           // Rigidbody�� ȸ�� ���
        Cursor.lockState = CursorLockMode.Locked;  // ���콺 Ŀ�� ����
        Cursor.visible = false;
    }

    void Update()
    {
        Rotate();
        Move();
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.2f + 0.1f);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPuzzleModeActive)
            {
                TogglePuzzleMode(false);
            }
            else
            {
                Ray ray = new Ray(cam.position, cam.forward);
                RaycastHit hit;
                // �켱 ���� ���� Ȯ��
                if (Physics.Raycast(ray, out hit, interactRange, puzzleLayer))
                {
                    TogglePuzzleMode(true);
                }
                // ���� ������ �ƴ϶�� �Ϲ� ��ȣ�ۿ� ��ü ó��
                else if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }

    void Rotate()
    {
        if (isPuzzleModeActive)
            return;
        // ���콺 �̵��� ��������
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        // ���� ȸ�� (�¿�)
        yRotation += mouseX;

        // ���� ȸ�� (����) - Ŭ���� �����Ͽ� ���� ���� �̻� �� �����̰�
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // ���� ���� ����

        // �÷��̾� ȸ�� (�¿�)
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // ī�޶� ȸ�� (����) �� cameraPivot�� �������� ȸ��
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

    void TogglePuzzleMode(bool activate)
    {
        isPuzzleModeActive = activate;
        if (activate)
        {
            // ���� ��� Ȱ��ȭ: Ŀ�� ���̰�, ��� ���� �� �κ��丮 UI Ȱ��ȭ
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            /*if (Inventory.Instance != null)
                Inventory.Instance.ShowInventory(true);*/
        }
        else
        {
            // ���� ��� ��Ȱ��ȭ: Ŀ�� ����� �ٽ� ����, �κ��丮 UI ����
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            /*if (Inventory.Instance != null)
                Inventory.Instance.ShowInventory(false);*/
        }
    }

}
