using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD_Manager : MonoBehaviour
{
    public static HUD_Manager instance;

    void Awake() => instance = this;    
    

    [Header("Loading Panel Settings")]
    public GameObject loadingHud;

    [Header("Time Settings")]
    public TMP_Text timeRemaningText;


    [Header("Score Settings")]
    public TMP_Text scoreText;

    
    [Header("End HUD Settings")]
    public TMP_Text endingScoreText;
    public GameObject gameplayHUD,endingImage;
    

    public void changeScene(string nameScene)
    {
       loadingHud.SetActive(true); 
       SceneManager.LoadScene(nameScene);
    }
   
}
