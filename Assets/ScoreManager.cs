using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public float currentCombo = 0f;
    public float totalScore = 0f;

    public void AddPoints(float amount)
    {
        currentCombo += amount;
        Debug.Log("+" + amount + " (Combo now: " + currentCombo + " )");
    }

    public void BankPoints()
    {
        totalScore += currentCombo;
        Debug.Log("BANKED" + currentCombo + " (Total now: " + totalScore + " )");
        currentCombo = 0f;
    }

    public void ResetCombo()
    {
        Debug.Log("Combo lost (" + currentCombo + ")");
        currentCombo = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
