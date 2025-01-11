using UnityEngine;
using System.Collections.Generic;

public class CardPhaseHandlerTrap : MonoBehaviour
{

    public List<int> usablePhases = new List<int>();

    public int currentPhase;
    public int currentPlayer;

    private Renderer renderer;

    void Start()
    {
        usablePhases.Add(1); //SP
        usablePhases.Add(2); //MP1
        usablePhases.Add(3); //BP
        usablePhases.Add(4); //MP2
        usablePhases.Add(5); //EP
        renderer = GetComponent<Renderer>();    }

    void Update()
    {
         renderer.material.color = IsCardUsable(currentPhase,currentPlayer) ? Color.green : Color.red;
    }

    public bool IsCardUsable(int phase,int player)
    {
        return usablePhases.Contains(phase);
    }
}
