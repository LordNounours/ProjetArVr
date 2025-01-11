using UnityEngine;
using UnityEngine.UI; // Nécessaire pour utiliser UI.Button.
using Unity.VisualScripting;
using TMPro;


public class IncrementPhase : MonoBehaviour
{
    public VariableManager variableManager;
    public Button button;

    TMP_Text phaseText;
    TMP_Text playerText;

    void Start()
    {
        // On récupère le composant Button de l'objet.
        button = GetComponent<Button>();
        // On ajoute un listener pour le clic sur le bouton.
        button.onClick.AddListener(OnButtonClicked);

        phaseText = GameObject.Find("PhaseInformations").GetComponent<TMPro.TMP_Text>();
        playerText = GameObject.Find("PlayerInformations").GetComponent<TMPro.TMP_Text>();
    }

    // Méthode appelée lors du clic sur le bouton.
    void OnButtonClicked()
    {
        variableManager.IncrementPhase();

        phaseText.text = "Current Phase :\n" + variableManager.GetCurrentPhaseName();
        playerText.text = "Current Player : \n" + variableManager.GetCurrentPlayerName();
    }
}
