using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {



    public static LevelManager Instance;

    public Transform levelTop;
    public Transform levelBottom;

    public GameObject portal;
    public GameObject lava;

    private int collectedStars;
    private float _gameHeight;


    public Animator darkScreen;
    public GameObject dieScreen;
    public GameObject winScreen;

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
    }


    void _initParams(){
        _gameHeight = Mathf.Abs(levelTop.position.y - levelBottom.position.y);
    }

    public void AddStar(){
        collectedStars++;

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

        UnityTimer.Instance.CallAfterDelay(() => {
            goat.isTimeSlow = false;
        } , 1.5f);
    }

    public void ResetLevel(){
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void NextLevel(){
        Scene scene = SceneManager.GetActiveScene();
        int nextScene = scene.buildIndex;
        nextScene++;

        SceneManager.LoadScene(nextScene);
    }

    public void ShowDieScreen(){
        darkScreen.gameObject.SetActive(true);
        darkScreen.SetTrigger("Fade");
        dieScreen.SetActive(true);
    }

    public void ShowWinScreen(){

        winScreen.SetActive(true);
        darkScreen.gameObject.SetActive(true);
        darkScreen.SetTrigger("Fade");
        
    }
}
