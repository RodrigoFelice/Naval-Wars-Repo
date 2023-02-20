using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSettings : MonoBehaviour
{
    [Header("Time Settings")]
    [SerializeField] float timeRemaining = 90;

    [Header("Timer Format Settings")]
    int minutes, seconds;
    string textFormat;

    [Header("Spawn Enemies Settings")]
    GameObject enemiePrefab;
    [SerializeField] float enemieRespawnTime = 15, lastEnemieRespawnedTime;


    [Header("Possible Spawn Points")]
    [SerializeField] Transform[] spawnPoints;

    
    void Start()
    {
        timeRemaining = LoadSettings("MatchTime" ,timeRemaining);
        enemieRespawnTime = LoadSettings("EnemieRespawnTime" , enemieRespawnTime);
    }

     float LoadSettings(string keyName, float value)
    {
        if (PlayerPrefs.HasKey(keyName)) value = PlayerPrefs.GetFloat(keyName);
        return value;
    }

    void Update()
    {
        DecreaseTime();
        RespawnEnemies(); 
       
    }

    void DecreaseTime()
    {
        timeRemaining -= Time.deltaTime;
        minutes = Mathf.FloorToInt(timeRemaining / 60);
        seconds = Mathf.FloorToInt(timeRemaining % 60);

        textFormat = string.Format("{0:00}:{1:00}", minutes, seconds);
        HUD_Manager.instance.timeRemaningText.text = "Time: " + textFormat;

        if(timeRemaining <= 0f) GameManager.instance.ChangeGameState("Ending");
    }

    void RespawnEnemies()
    {
        lastEnemieRespawnedTime += Time.deltaTime;

        if(lastEnemieRespawnedTime >= enemieRespawnTime)
        {
            int randomPoint = Random.Range(0, spawnPoints.Length);
            enemiePrefab = ObjectPoolingManager.instance.GetObject("Enemie");
            enemiePrefab.transform.position = spawnPoints[randomPoint].transform.position;
            enemiePrefab.transform.rotation = spawnPoints[randomPoint].transform.rotation;
            lastEnemieRespawnedTime = 0f;
        }
    }


}
