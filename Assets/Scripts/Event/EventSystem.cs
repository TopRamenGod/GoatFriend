using UnityEngine;
using System.Collections;
using System;

namespace GoatFriend{

    namespace Events{

        public interface IEvent<T>{
             void AddListener(T _event);
             void TriggerEvent();
        }

        public interface IEvent<T1,T2>: IEvent<T1>{
            void TriggerEvent(T2 data);
        }

        public interface IFunc{
            void Call();
            Delegate GetFunc();
            void AddFunc(Delegate func);
            void RemoveFunc(Delegate func);
        }

        public interface IFunc<T> : IFunc{
            void Call(T data);
        }



        public struct SimpleFunc: IFunc{
            
            public delegate void SimpleDelegate();
            SimpleDelegate _func;

            public SimpleFunc(SimpleDelegate func){
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

        public struct DataFunc<T>: IFunc<T> where T: struct{
            public delegate void DataDelegate(T data);
            DataDelegate _func;

            public DataFunc(DataDelegate func){
                _func = func;
            }

            public void Call(){
                Debug.LogWarning("DataFunc needs data");
            } 

            public void Call(T data){
                this._func(data);
            }

            public Delegate GetFunc(){
                return _func;
            }

            public void AddFunc(Delegate func){
                _func+=(DataDelegate)func;
            }

            public void RemoveFunc(Delegate func){
                _func-=(DataDelegate)func;
            }
        }

        public class SimpleEvent<T>: IEvent<T> where T: IFunc{

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

        public class DataEvent<T1,T2>: IEvent<T1,T2> where T1:IFunc<T2>{
            private T1 _thisEvent;

            public void AddListener(T1 _event){
                _thisEvent.AddFunc( _event.GetFunc() );
            }

            public void TriggerEvent(){
                if( _thisEvent != null ){
                    _thisEvent.Call();
                }
            }

            public void TriggerEvent(T2 data){
                if( _thisEvent != null ){
                    _thisEvent.Call(data);
                }
            }
        }






       


        public class EventSystem : MonoBehaviour {

            public static EventSystem Instance{get; private set;}

            public IEvent<SimpleFunc> GoatDied;
            public IEvent<SimpleFunc> GoatWon;
            public IEvent<SimpleFunc> GoatReleased;
            public IEvent<DataFunc<int>, int> HeartCollected;
         
            public IEvent<SimpleFunc> GamePaused;
            public IEvent<SimpleFunc> GameResumed;

            public IEvent<SimpleFunc> GoatBounced;
            public IEvent<SimpleFunc> GoatHit;
            public IEvent<SimpleFunc> GoatTouched;

            void Awake(){
                Instance = this;
                _initEvents();
            }

            private void _initEvents(){
                GoatDied =  new SimpleEvent<SimpleFunc>();
                GoatWon =  new SimpleEvent<SimpleFunc>();
                GoatReleased =  new SimpleEvent<SimpleFunc>();
                HeartCollected =  new DataEvent<DataFunc<int>, int>();
                GamePaused =  new SimpleEvent<SimpleFunc>();
                GameResumed =  new SimpleEvent<SimpleFunc>();

                GoatBounced =  new SimpleEvent<SimpleFunc>();
                GoatHit =  new SimpleEvent<SimpleFunc>();
                GoatTouched =  new SimpleEvent<SimpleFunc>();
            }

        
           

        }




    }
}
