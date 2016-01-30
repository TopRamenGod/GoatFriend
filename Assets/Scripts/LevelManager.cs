using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {



    public static LevelManager Instance;

    public Transform levelTop;
    public Transform levelBottom;

    private float _gameHeight;

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
    }


    void _initParams(){
        _gameHeight = Mathf.Abs(levelTop.position.y - levelBottom.position.y);
    }
}
