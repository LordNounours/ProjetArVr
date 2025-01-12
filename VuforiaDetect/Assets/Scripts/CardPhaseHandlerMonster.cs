using UnityEngine;
using System.Collections.Generic;

public class CardPhaseHandlerMonster : MonoBehaviour
{
    public VariableManager variableManager;

    public List<int> usablePhases = new List<int>();

    int currentPhase;

    private Renderer renderer;


    void Start()
    {
        usablePhases.Add(2); //MP1
        usablePhases.Add(3); //BP
        usablePhases.Add(4); //MP2
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        currentPhase = variableManager.GetCurrentPhase();
        renderer.material.color = IsCardUsable(currentPhase) ? Color.green : Color.red;
    }

    public bool IsCardUsable(int phase)
    {
        return usablePhases.Contains(phase);
    }
}
