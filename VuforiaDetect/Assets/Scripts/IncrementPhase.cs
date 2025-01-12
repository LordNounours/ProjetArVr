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

    Image header;
    Image footer;

    void Start()
    {

        GameObject objHeader = GameObject.Find("Header");
        GameObject objfooter = GameObject.Find("Footer");

        if (objHeader == null || objfooter == null)
        {
            Debug.LogError("Header or Footer not found");
            return;
        }

        header = objHeader.GetComponent<Image>();
        footer = objfooter.GetComponent<Image>();

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
        int player = variableManager.GetCurrentPlayer();
        variableManager.IncrementPhase();
        if (player != variableManager.GetCurrentPlayer())
        {
            if (player == 1){
                header.color = new Color(1.0f, 0.0f, 0.0f, 0.3921f);
                footer.color = new Color(1.0f, 0.0f, 0.0f, 0.3921f);
            }else if (player == 0){
                header.color = new Color(0.0f, 0.0f, 1.0f, 0.3921f);
                footer.color = new Color(0.0f, 0.0f, 1.0f, 0.3921f);
            }
        }

        phaseText.text = "Current Phase :\n" + variableManager.GetCurrentPhaseName();
        playerText.text = "Current Player : \n" + variableManager.GetCurrentPlayerName();
    }
}
