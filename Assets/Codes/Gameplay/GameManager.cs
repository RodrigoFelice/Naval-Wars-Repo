using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() => instance = this;
    
   [Header("Game Events")]
   public UnityEvent BeginGame, PlayingGame, EndingGame;

   public enum GameStates {Waiting ,Playing, Ending}

    GameStates gameStates;

   [Header("Score Settings")]
   public int actualScore;

    void Start() => ChangeGameState("Waiting");
    

    public void ChangeGameState(string nextState)
    {
        gameStates = (GameStates)System.Enum.Parse(typeof(GameStates),nextState);
        CallManageEvents();
    } 
   
    void CallManageEvents()
    {
        switch (gameStates) {
            case GameStates.Waiting:
                BeginGame.Invoke();
                break;
            case GameStates.Playing:
                PlayingGame.Invoke();
                break;
            case GameStates.Ending:
                EndingGame.Invoke();
                break;
            default:
                Debug.Log("NO EVENT");
                break;
        }
    }


    public void changeScore(int newScore)
    {
        actualScore += newScore;
        HUD_Manager.instance.scoreText.text = "Score: " + actualScore;
        HUD_Manager.instance.endingScoreText.text = "Score: " + actualScore;
    }

}
