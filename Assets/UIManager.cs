using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI levelText;
    public float levelHeight = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int height = Mathf.FloorToInt(player.position.y);
        int level = Mathf.FloorToInt(height / levelHeight);

        heightText.text = "Height: 999";
        levelText.text = "Levels; 999";

        heightText.text = "Height: " + height;
        levelText.text = "Levels: " + level;
    }
}
