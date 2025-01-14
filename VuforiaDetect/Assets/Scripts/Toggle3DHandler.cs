using UnityEngine;
using UnityEngine.InputSystem;

public class Toggle3DHandler : MonoBehaviour
{
    private bool isChecked = false;
    private Renderer renderer;
    private Camera mainCamera;

    // Input Actions reference
    private InputAction mouseClickAction;

    void Awake()
    {
        // Create a new instance of the input action or link it via your Input Actions asset
        var inputActions = new InputActionMap("Click");
        mouseClickAction = inputActions.AddAction("MouseClick", binding: "<Mouse>/leftButton");

        // Enable the input action
        mouseClickAction.Enable();
    }

    void OnEnable()
    {
        mouseClickAction.performed += OnMouseClick;
    }

    void OnDisable()
    {
        mouseClickAction.performed -= OnMouseClick;
        mouseClickAction.Disable();
    }

    void Start()
    {
        renderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
        UpdateVisualState();
    }

    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Get mouse position from Pointer.current
            Vector2 mousePosition = Pointer.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            // Perform raycast to check object hit
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isChecked = !isChecked;
                    UpdateVisualState();
                }
            }
        }
    }

    private void UpdateVisualState()
    {
        if (renderer != null)
        {
            renderer.material.color = isChecked ? Color.green : Color.red;
        }
    }
}
