using UnityEngine;
using UnityEngine.UI;

public class StartDuel : MonoBehaviour
{
    public VariableManager variableManager;
    public Button button;

    Canvas MainUI;
    public Canvas DuelUI;

    void Start()
    {
        button = GetComponent<Button>();
        // On récupère le composant Button de l'objet.
        Canvas MainUI = GameObject.Find("MainUI").GetComponent<Canvas>();
        // DuelUI = GameObject.Find("DuelUI").GetComponent<Canvas>();
        // On ajoute un listener pour le clic sur le bouton.
        button.onClick.AddListener(OnButtonClicked);
    }

    // Méthode appelée lors du clic sur le bouton.
    void OnButtonClicked()
    {
        // On démarre le duel.
        variableManager.StartDuel();
        // On désactive le bouton.
        button.interactable = false;
        // On active le canvas.
        if (DuelUI.gameObject.activeSelf)
            MainUI.gameObject.SetActive(false);
        DuelUI.gameObject.SetActive(true);

    }
}
