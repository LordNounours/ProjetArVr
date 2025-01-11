using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public int currentPlayer = 0;
    public int currentPhase = 0;

    public void IncrementPhase(){
        currentPhase = (currentPhase + 1) % 6;
        if (currentPhase == 0)
        {
            currentPlayer = (currentPlayer + 1) % 2;
        }
    }

    public int GetCurrentPlayer(){
        return currentPlayer;
    }

    public int GetCurrentPhase(){
        return currentPhase;
    }

    public string GetCurrentPhaseName(){
        switch (currentPhase)
        {
            case 0:
                return "DRAW PHASE";
            case 1:
                return "STANDBY PHASE";
            case 2:
                return "MAIN PHASE 1";
            case 3:
                return "BATTLE PHASE";
            case 4:
                return "MAIN PHASE 2";
            case 5:
                return "END PHASE";
            default:
                return "Phase inconnue";
        }
    }

    public string GetCurrentPlayerName(){
        return currentPlayer == 0 ? "Player 1" : "Player 2";
    }
}