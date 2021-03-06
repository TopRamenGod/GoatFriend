﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GoatFriend.Events;

public class LevelManager : MonoBehaviour {



    public static LevelManager Instance;

    public Transform levelTop;
    public Transform levelBottom;

    public GameObject portal;
    public GameObject lava;

    private int collectedStars;
    private float _gameHeight;


    public GoatManager goat;

    public float GameHeight{
        get{
            return _gameHeight;
        }
    }


    void Awake(){
        Instance = this;
    }

    void Start(){
        _initParams();
        collectedStars = 0;

        //bind handlers
        EventSystem.Instance.HeartCollected.AddListener(new DataFunc<int>(AddStar));
       
    }


    void _initParams(){
        _gameHeight = Mathf.Abs(levelTop.position.y - levelBottom.position.y);
    }

    public void AddStar(int stars){
        collectedStars = stars;

        if( IsLevelComplete () ){
            ActivatePortal();
        }
    }

    public bool IsLevelComplete(){
        if(collectedStars == 3) return true;
        return false;
    }


    public void ActivatePortal(){
        portal.SetActive(true);
        lava.GetComponent<Collider2D>().enabled = false;
        portal.GetComponent<Animator>().SetTrigger("spawn");

        goat.isTimeSlow = true;

        //AudioManager.instance.playPortalOpen();

        UnityTimer.Instance.CallAfterDelay(() => {
            goat.isTimeSlow = false;
        } , 1.5f);
    }

    public void ResetLevel(){
        Time.timeScale = 1.0f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(){
        Scene scene = SceneManager.GetActiveScene();
        int nextScene = scene.buildIndex;
        nextScene++;
        Debug.Log("loading scene :"+nextScene);
        SceneManager.LoadScene(nextScene);
    }

//    public void ShowDieScreen(){
//        darkScreen.gameObject.SetActive(true);
//        darkScreen.SetTrigger("Fade");
//        dieScreen.SetActive(true);
//    }
//
//    public void ShowWinScreen(){
//
//        winScreen.SetActive(true);
//        darkScreen.gameObject.SetActive(true);
//        darkScreen.SetTrigger("Fade");
//        
//    }
//
//    public void PauseGame(){
//        darkScreen.SetTrigger("fade");
//        pauseScreen.SetActive(true);
//        Time.timeScale = 0.0f;
//    }
//
//    public void ResumeGame(){
//        darkScreen.SetTrigger("unfade");
//        pauseScreen.SetActive(false);
//        Time.timeScale = 1.0f;
//    }

    public void Pause(){
        EventSystem.Instance.GamePaused.TriggerEvent();
    }

 



}
