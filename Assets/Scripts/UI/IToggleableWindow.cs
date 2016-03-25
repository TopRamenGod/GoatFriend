using UnityEngine;
using System.Collections;

namespace GoatFriend{

    namespace UI{

        public delegate void UICallback();

        //A window which can be shown and hidden
        interface IToggleableWindow{
            void Show();
            void Hide();

            void Show(UICallback cb);
            void Hide(UICallback cb);
        }

        //A window which the user manually closes
        interface ICloseableWindow : IToggleableWindow{
            void BindOnClose(UICallback cb);
        }

    }
}