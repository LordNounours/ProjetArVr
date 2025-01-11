using UnityEngine;
using System.Collections.Generic;

public class CardPhaseHandlerMonster : MonoBehaviour
{

    public List<int> usablePhases = new List<int>();

    public int currentPhase;
    public int currentPlayer;

    private Renderer renderer;


    void Start()
    {
        usablePhases.Add(2); //MP1
        usablePhases.Add(4); //MP2
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
         renderer.material.color = IsCardUsable(currentPhase,currentPlayer) ? Color.green : Color.red;
    }

    public bool IsCardUsable(int phase,int player)
    {
        return usablePhases.Contains(phase);
    }
}
