using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject[] instructions;
    public GameObject nextButton;

    int currentInstruction = 0;
    
    public void startInstructions()
    {
        instructions[currentInstruction].SetActive(true);
        nextButton.SetActive(true);
    }

    public void passInstruction()
    {
        instructions[currentInstruction].SetActive(false);
        currentInstruction++;
        if(currentInstruction < instructions.Length)
            instructions[currentInstruction].SetActive(true);
        else
        {
            nextButton.SetActive(false);
            currentInstruction = 0;
        }
    }
}
