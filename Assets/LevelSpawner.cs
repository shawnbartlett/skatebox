using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject launchRoomPrefab;
    public GameObject levelChuckPrefab;
    public int numberOfChunks = 100;
    public float chunkHeight = 10f;

    // Start is called before the first frame update
    void Start()
    {

        //spawn base launch room
        Vector3 spawnPos = Vector3.zero;
        Instantiate(launchRoomPrefab, spawnPos, Quaternion.identity);

        //spawn rest stacked on top
        for (int i = 0; i < numberOfChunks; i++)
        {

            Vector3 spawnPosition = new Vector3(0f, i * chunkHeight, 0f);
            Instantiate(levelChuckPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
