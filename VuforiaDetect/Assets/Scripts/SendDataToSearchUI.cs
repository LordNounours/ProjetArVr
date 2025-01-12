using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SendDataToSearchUI : MonoBehaviour
{
    // public TMP_Text description;
    // public button owned;
    // public button usable;
    // private bool is_owned = false;
    // private bool is_usable = false;
    // private Renderer ownedRenderer;
    // private Renderer usableRenderer;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     ownedRenderer = owned.GetComponent<Renderer>();
    //     usableRenderer = usable.GetComponent<Renderer>();
    //     UpdateVisualState();

    //     owned.onClick.AddListener(OnButtonClicked);
    // }

    // void OnButtonClicked()
    // {
    //     is_owned = !is_owned;
    //     UpdateVisualState();
    // }

    // private void UpdateVisualState()
    // {
    //     if (ownedRenderer != null)
    //     {
    //         ownedRenderer.material.color = is_owned ? Color.green : Color.red;
    //     }

    //     if (usableRenderer != null)
    //     {
    //         usableRenderer.material.color = is_usable ? Color.green : Color.red;
    //     }
    // }

    void Start(){
        GameObject objSearchUI = GameObject.Find("SearchUI");

        if (objSearchUI == null)
        {
            Debug.LogError("SearchUI not found");
            return;
        }

        GameObject currentObject = this.gameObject;

        // Change le parent de l'objet actuel
        currentObject.transform.SetParent(objSearchUI.transform, false);
    }
}
