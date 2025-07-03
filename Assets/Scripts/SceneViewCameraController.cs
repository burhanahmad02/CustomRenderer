using UnityEngine;

public class SceneViewCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float boostMultiplier = 3f;
    public float lookSensitivity = 2f;

    private float yaw;
    private float pitch;
    private bool isRightMouseHeld;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Toggle mouse look
        if (Input.GetMouseButtonDown(1))
        {
            isRightMouseHeld = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRightMouseHeld = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isRightMouseHeld)
        {
            HandleMouseLook();
        }

        HandleMovement();
    }

    void HandleMouseLook()
    {
        yaw += Input.GetAxis("Mouse X") * lookSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * lookSensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.E)) input.y += 1;
        if (Input.GetKey(KeyCode.Q)) input.y -= 1;

        float currentSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1f);
        Vector3 move = transform.TransformDirection(input.normalized) * currentSpeed * Time.deltaTime;

        transform.position += move;
    }
}
