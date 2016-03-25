using UnityEngine;
using System.Collections;
using System;

namespace GoatFriend{

    namespace Events{

        public interface IEvent<T>{
             void AddListener(T _event);
             void TriggerEvent();
        }

        public interface IFunc{
            void Call();
            Delegate GetFunc();
            void AddFunc(Delegate func);
            void RemoveFunc(Delegate func);
        }



        public struct SimpleEvent: IFunc{
            
            public delegate void SimpleDelegate();
            SimpleDelegate _func;

            public SimpleEvent(SimpleDelegate func){
                _func = func;
            }

            public void Call(){
                this._func();
            } 

            public Delegate GetFunc(){
                return _func;
            }

            public void AddFunc(Delegate func){
                _func+=(SimpleDelegate)func;
            }

            public void RemoveFunc(Delegate func){
                _func-=(SimpleDelegate)func;
            }
        }

        public class GoatEvent<T>: IEvent<T> where T: IFunc{

            private T _thisEvent;

            public void AddListener(T _event){
                _thisEvent.AddFunc( _event.GetFunc() );
            }

            public void TriggerEvent(){
                if( _thisEvent != null ){
                    _thisEvent.Call();
                }
            }
        }

       


        public class EventSystem : MonoBehaviour {

            public static EventSystem Instance{get; private set;}

            public IEvent<SimpleEvent> GoatDied;
            public IEvent<SimpleEvent> GoatWon;
            public IEvent<SimpleEvent> GoatReleased;
            public IEvent<SimpleEvent> HeartCollected;
            public IEvent<SimpleEvent> GamePaused;
            public IEvent<SimpleEvent> GameResumed;

            public IEvent<SimpleEvent> GoatBounced;
            public IEvent<SimpleEvent> GoatHit;
            public IEvent<SimpleEvent> GoatTouched;

            void Awake(){
                Instance = this;
                _initEvents();
            }

            private void _initEvents(){
                GoatDied =  new GoatEvent<SimpleEvent>();
                GoatWon =  new GoatEvent<SimpleEvent>();
                GoatReleased =  new GoatEvent<SimpleEvent>();
                HeartCollected =  new GoatEvent<SimpleEvent>();
                GamePaused =  new GoatEvent<SimpleEvent>();
                GameResumed =  new GoatEvent<SimpleEvent>();

                GoatBounced =  new GoatEvent<SimpleEvent>();
                GoatHit =  new GoatEvent<SimpleEvent>();
                GoatTouched =  new GoatEvent<SimpleEvent>();
            }

        
           

        }




    }
}
