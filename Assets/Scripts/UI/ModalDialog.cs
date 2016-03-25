using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GoatFriend{

    namespace UI{
        public class ModalDialogProperties{

            public static Vector2 Position = Vector2.zero;
        }

        public class ModalDialog : TopDrawer {

        

            void Start(){
                rect = GetComponent<RectTransform>();
                anim = GetComponent<Animator>();

                rect.anchoredPosition = ModalDialogProperties.Position;
                _uiMan = GameObject.Find("UIManager").GetComponent<UIManager>();
               // dBg.SetActive(false);
                this.gameObject.SetActive(false);
            }


            override public void Show(){
                this.gameObject.SetActive(true);
                _uiMan.DarkBackground.SetActive(true);
                base.Show();
            }

            public void Deactivate(){
                this.gameObject.SetActive(false);
                _uiMan.DarkBackground.SetActive(false);
                base.OnClose();
            }
        
        }
    }
}
