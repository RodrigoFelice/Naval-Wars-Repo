using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    
    [Header("Match Time Settings")]
    [SerializeField] private Slider matchTimeSlider;
    [SerializeField] private TMP_Text matchTimeText;
   
   
    [Header("Enemies Respawn Time Settings")]
    [SerializeField] private Slider respawnTimeSlider;
    [SerializeField] private TMP_Text respawnTimeText;


    void Start()
    {
        ChangeMatchSliderValue(matchTimeText);
        ChangeRespawnTimeSliderValue(respawnTimeText);    
    }

    public void PlayGame(string nameScene) => SceneManager.LoadScene(nameScene);
    

    public void ChangeMatchSliderValue(TMP_Text choosenText) => choosenText.text = "" + matchTimeSlider.value + "s"; 
    

    public void ChangeRespawnTimeSliderValue(TMP_Text choosenText) => choosenText.text = "" + respawnTimeSlider.value + "s"; 
    

    public void ApplySettings()
    {
        PlayerPrefs.SetFloat("MatchTime", matchTimeSlider.value);
        PlayerPrefs.SetFloat("EnemieRespawnTime", respawnTimeSlider.value);
    }

    public void LoadSettings()
    {
        VerifySettings("MatchTime", matchTimeText, matchTimeSlider);
        VerifySettings("EnemieRespawnTime", respawnTimeText, respawnTimeSlider);
    }


    void VerifySettings(string codeValue, TMP_Text textToChange, Slider sliderToChange)
    {
        if (PlayerPrefs.HasKey(codeValue))
        {
            float previousValue = PlayerPrefs.GetFloat(codeValue);
            sliderToChange.value = previousValue;
            textToChange.text = "" + previousValue + "s";
        }
    }
}
