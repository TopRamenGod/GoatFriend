using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace GoatFriend{

    namespace UI{

        public class ToastProperties{

            public static float StartY = -150.0f;
            public static float StartX = 0.0f;
            public static float StartZ = 0.0f;

            public static float Width = 695.0f;
            public static float Height = 200.0f;
        }

        [RequireComponent(typeof (Animator))]
        [RequireComponent(typeof (Image))]
        [RequireComponent(typeof (RectTransform))]
        public class Toast : MonoBehaviour,IToggleableWindow  {


            RectTransform rect;
            Animator anim;

            UICallback _showFinishCB;
            UICallback _hideFinishCB;
        	// Use this for initialization
        	void Start () {
        	    
                rect = GetComponent<RectTransform>();

                rect.anchoredPosition3D = new Vector3(ToastProperties.StartX, ToastProperties.StartY, ToastProperties.StartZ);
                rect.sizeDelta = new Vector2(ToastProperties.Width, ToastProperties.Height);

                anim = GetComponent<Animator>();

        	}
        	

            public void Show(){
                anim.SetTrigger("Show");
            }

            public void Hide(){
                anim.SetTrigger("Hide");
            }

            public void Show(UICallback cb){
                _showFinishCB = cb;
                Show();
            }

            public void Hide(UICallback cb){
                _hideFinishCB = cb;
                Hide();
            }


            public void OnShowFinished(){
                if(_showFinishCB != null){
                    _showFinishCB();
                }
                Debug.Log("ToastFinished Showing");
            }

            public void OnHideFinished(){
                if(_hideFinishCB != null){
                    _hideFinishCB();
                }

                Debug.Log("ToastFinished Hiding");
            }

        }
    }
}
