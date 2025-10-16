using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private SkateControllerFSM skater;
    public static ScoreManager Instance;
    public GameObject scorePopupPrefab;
    public GameObject bankedPopupPrefab;
    public GameObject deathPopupPrefab;
    public Transform skaterTransform;
    public Canvas worldCanvas;

    public float currentCombo = 0f;
    public float totalScore = 0f;

    public void AddPoints(float amount)
    {
        int rounded = Mathf.RoundToInt(amount);
        currentCombo += rounded;
        Debug.Log("+" + rounded + " (Combo now: " + currentCombo + " )");

        GameObject popup = Instantiate(scorePopupPrefab, worldCanvas.transform);
        var rt = popup.GetComponent<RectTransform>();
        rt.position = skaterTransform.position + Vector3.up * 1.5f;

        popup.GetComponent<TextMeshProUGUI>().text = $"+{currentCombo}";
        //Destroy(popup, 0.4f);
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
