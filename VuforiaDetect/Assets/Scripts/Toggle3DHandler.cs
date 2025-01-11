using UnityEngine;

public class Toggle3DHandler : MonoBehaviour
{
    private bool isChecked = false; //Possession
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        UpdateVisualState();
    }

    void OnMouseDown()
    {
        isChecked = !isChecked;
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (renderer != null)
        {
            renderer.material.color = isChecked ? Color.green : Color.red;
        }
    }
}
