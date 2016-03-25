using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GoatFriend{

    namespace UI{
            
        public class TopDrawerProperties{

            public static float MaxHeight = 875.0f;
            public static float StartY = 450.0f;
        }

        [RequireComponent ( typeof (Animator))]
        [RequireComponent ( typeof (RectTransform))]
        public class TopDrawer : MonoBehaviour, ICloseableWindow {


            protected RectTransform rect;
            protected Animator anim;
 

            protected UICallback _onShown;
            protected UICallback _onClose;
     
            protected UIManager _uiMan;

            void Start(){
                rect = GetComponent<RectTransform>();
                anim = GetComponent<Animator>();
                _uiMan = GameObject.Find("UIManager").GetComponent<UIManager>();
         
               // _dBg.SetActive(false);

                rect.anchoredPosition = new Vector2(0, TopDrawerProperties.StartY);

                //Clamp the maximum height of the drawer
                if( rect.sizeDelta.y > TopDrawerProperties.MaxHeight){
                    Vector2 currDim = rect.sizeDelta;
                    rect.sizeDelta = new Vector2(currDim.x, TopDrawerProperties.MaxHeight);
                }

            }

            virtual public void Show(){
                anim.SetTrigger("Show");
                _uiMan.DarkBackground.SetActive(true);

            }

            //Hide cannot be called externally for a top drawer, clicking on the close button calls it manually
            void IToggleableWindow.Hide(){


            }
            void IToggleableWindow.Hide(UICallback cb){
                ((IToggleableWindow)this).Hide();
            }


            virtual public void Show(UICallback cb){
                this._onShown = cb;
                Show();
            }


           

            virtual public void BindOnClose(UICallback cb){
                this._onClose = cb;
            }

            virtual public void Close(){
                anim.SetTrigger("Hide");
                ((IToggleableWindow)this).Hide();
            }

            virtual public void OnClose(){
                if (this._onClose != null){
                    this._onClose();
                }
                _uiMan.DarkBackground.SetActive(false);
            }


            virtual public void OnShowFinished(){
                if( this._onShown != null){
                    this._onShown();
                }
               
                Debug.Log("Top Drawer SHown");
            }

        }
    }
}
