using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class AutoTiler : MonoBehaviour {

    public float SingleX;
    public float SingleY;

    public enum TileMode{
        Vertical,
        Horizontal,
        Both
    }

    public TileMode TilingMode;

    void Start(){
        _autoTile();
    }


    void Update(){
        if(Application.isEditor){
            _autoTile();
        }
    }


    private void _autoTile(){
        float xTiles = this.transform.localScale.x/SingleX;
        float yTiles = this.transform.localScale.y/SingleY;

        if ( xTiles < 1 || yTiles < 1){
            Debug.LogWarning("Texture is beinf tiled smaller than ideal size");
        }

        //Set to the material
        Material _mat = gameObject.GetComponent<Renderer>().sharedMaterial;

        if ( TilingMode == TileMode.Both ){
            _mat.mainTextureScale = new Vector2(xTiles, yTiles);
        } else if( TilingMode == TileMode.Vertical)  {
            _mat.mainTextureScale = new Vector2(1, yTiles);
        }else if( TilingMode == TileMode.Horizontal){
            _mat.mainTextureScale = new Vector2(xTiles, 1);
        }
    }

}
