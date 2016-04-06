using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour {

    public void StartGame(){
        Scene scene = SceneManager.GetActiveScene();
        int nextScene = scene.buildIndex;
        nextScene++;

        SceneManager.LoadScene(nextScene);
    }
}
