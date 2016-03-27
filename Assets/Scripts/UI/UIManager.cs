using UnityEngine;
using System.Collections;
using GoatFriend.UI;
using GoatFriend.Events;

public class UIManager : MonoBehaviour {

    public Toast LoadingToast;
    public TopDrawer PauseMenu;
    public ModalDialog WinDialog;
    public ModalDialog LoseDialog;

    public GameObject  DarkBackground{get; private set;}
    void Start(){
        DarkBackground = GameObject.Find("DarkBackground");
        DarkBackground.SetActive(false);

        EventSystem.Instance.GamePaused.AddListener(new SimpleFunc(OnPause));
        EventSystem.Instance.GoatDied.AddListener(new SimpleFunc(OnDead));
        EventSystem.Instance.GoatWon.AddListener(new SimpleFunc(OnWin));
    }


    void OnPause(){
        Time.timeScale = 0.0f;
        PauseMenu.Show();

        PauseMenu.BindOnClose(() =>{
            Time.timeScale = 1.0f;
        });

    }

    void OnDead(){

        UnityTimer.Instance.CallAfterDelay(() => {
            LoseDialog.Show();

            LoseDialog.BindOnClose(()=>{
                LevelManager.Instance.ResetLevel();
            });
        }, 1.0f);

    }

    void OnWin(){

        UnityTimer.Instance.CallAfterDelay(() => {
            WinDialog.Show();

            WinDialog.BindOnClose(() => {
                LevelManager.Instance.NextLevel();
            });
        }, 1.0f);
    }


}
