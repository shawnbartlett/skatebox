using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    
    public static ScoreManager Instance;
    public GameObject scorePopupPrefab;
    public GameObject bankedPopupPrefab;
    public GameObject deathPopupPrefab;
    public GameObject comboPopupPrefab;
    public Transform skaterTransform;
    public Canvas worldCanvas;

    public float currentCombo = 0f;
    public float totalScore = 0f;

    public void ComboPoints()
    {
        //Debug.Log("+" + rounded + " (Combo now: " + currentCombo + " )");

        GameObject popup = Instantiate(comboPopupPrefab, worldCanvas.transform);
        var rt = popup.GetComponent<RectTransform>();
        rt.position = skaterTransform.position + Vector3.up * 3f;

        popup.GetComponent<TextMeshProUGUI>().text = $"+{currentCombo}";
        Destroy(popup, 0.5f);
        
    }

    public void AddPoints(float amount, Color color)
    {
        int rounded = Mathf.RoundToInt(amount);
        currentCombo += rounded;

        GameObject popup = Instantiate(scorePopupPrefab, worldCanvas.transform);
        var rt = popup.GetComponent<RectTransform>();
        rt.position = skaterTransform.position + Vector3.up * 1.5f;

        TMP_Text text = popup.GetComponent<TextMeshProUGUI>();

        text.text = $"{amount}";
        text.color = color;

        ComboPoints();

    }

    public void BankPoints()
    {
        if (currentCombo == 0) return;
        totalScore += currentCombo;
        GameObject popup = Instantiate(bankedPopupPrefab, worldCanvas.transform);
        var rt = popup.GetComponent<RectTransform>();
        rt.position = skaterTransform.position + Vector3.up * 1.5f;
        popup.GetComponent<TextMeshProUGUI>().text = $"+{currentCombo} BANKED!";
        Destroy(popup, 1f);
        currentCombo = 0f;
    }

    public void ResetCombo()
    {
        Debug.Log("Combo lost (" + currentCombo + ")");
        currentCombo = 0f;
    }

    public void PlayerDeath()
    {
        GameObject popup = Instantiate(deathPopupPrefab, worldCanvas.transform);
        var rt = popup.GetComponent<RectTransform>();
        rt.position = skaterTransform.position + Vector3.up * 1.5f;
        popup.GetComponent<TextMeshProUGUI>().text = "YOU DIED!";
        Destroy(popup, 5f);
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
