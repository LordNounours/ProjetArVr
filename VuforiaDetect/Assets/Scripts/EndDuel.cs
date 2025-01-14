using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndDuel : MonoBehaviour
{
    // Références aux TextMeshPro-Text(UI)
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;
    public TextMeshProUGUI endText;

    public GameObject EndUI;

    // Variable pour stocker les valeurs numériques
    private int player1Value;
    private int player2Value;

    

    // Méthode appelée au début
    void Start()
    {
        if (player1Text == null || player2Text == null || endText == null || EndUI == null)
        {
            Debug.LogError("Veuillez attribuer toutes les références dans l'inspecteur.");
        }

        // Désactive l'interface de fin au départ
        EndUI.gameObject.SetActive(false);
    }

    // Méthode appelée à chaque frame
    void Update()
    {
        if (player1Text != null && player2Text != null)
        {
            // Tente de convertir les valeurs des textes en entiers
            if (int.TryParse(player1Text.text, out player1Value) && int.TryParse(player2Text.text, out player2Value))
            {
                // Vérifie si une valeur est inférieure ou égale à 0
                if (player1Value <= 0 && player2Value<=0)
                {
                    DisplayWinner("Draw !");
                }
                else if (player2Value <= 0)
                {
                    DisplayWinner("Player 1 Wins");
                }
                else if (player1Value <= 0)
                {
                    DisplayWinner("Player 2 Wins");
                }
            }
            else
            {
                Debug.LogWarning("Impossible de convertir les textes en valeurs entières.");
            }
        }
    }

    // Affiche le gagnant dans le champ endText et active EndUI
    private void DisplayWinner(string message)
    {
        endText.text = message; // Met à jour le texte de endText
        EndUI.gameObject.SetActive(true); // Active l'interface de fin
        Debug.Log(message); // Affiche un message dans la console
    }




}
