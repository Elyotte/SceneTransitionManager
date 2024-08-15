using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEditor;

namespace Com.EliottTan.SceneTransitions
{
    /// <summary>
    /// This class is a Singleton used to call transitions betweens scenes and screens
    /// </summary>
    public class TransitionManager : MonoBehaviour
    {
        #region Singleton
        public static TransitionManager instance { get; private set; }
        ///<summary> TransitionUI is a singleton class used to call a transition when needed</summary>
        private TransitionManager() { }
        #endregion

        //Animations
        [SerializeField] Transition _DefaultSceneTransition;

        static Transition _DefaultTransition => Instantiate(_transitionGO).GetComponent<Transition>();
        static GameObject _transitionGO => Resources.Load("BaseTransition") as GameObject;

        private void Awake()
        {
            //SingletonCheck
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);

        }

        /// <summary> Base method to call a transition  </summary>
        /// <param name="pTransition">used to access transition event sent during ui animation</param>
        /// <returns>the transition component</returns>
        static private Transition CallTransition(Transition pTransition)
        {
            if (pTransition == null)
            {
                Debug.LogError("TRANSITION IN PARAMETER IS NULL");
                return null;
            }

            return pTransition;
        }

        /// <summary> Call a transition and invoke a method when the animation is at midlife time </summary>
        /// <param name="pTransition">the transition you want to call</param>
        /// <param name="pMethod">the method you want to call at the middle of the lifetime of the transition</param>
        static public void CallTransition(Transition pTransition, Action pMethod)
        {
            if (pTransition != null)
                CallTransition(pTransition).onAnimationLoadState += pMethod;
            else
            {
                pMethod?.Invoke();
                Debug.LogWarning($"NO TRANSITION IN CallTransition : pTransition parameters");
            }
        }

        static public void CallTransition(Action pMethod)
        {
            CallTransition(_DefaultTransition, pMethod);
        }

        /// <summary> Use to call a scene by its index using a screen transition </summary>
        /// <param name="pSceneIndex">The index of the scene in the BuildSettings</param>
        static public void ChangeScene(int pSceneIndex)
        {
            
            ChangeScene(pSceneIndex, _DefaultTransition);
        }

        /// <summary>  Use to call a scene by its name using a default screen transition </summary>
        /// <param name="pSceneName">The name of the scene in string</param>
        static public void ChangeScene(string pSceneName)
        {
            ChangeScene(pSceneName, _DefaultTransition);
        }

        /// <summary> This serve as changing scene using a custom transition if desired.
        /// Make sure to reference a Transition and not a game object in you serialized field</summary>
        /// <param name="pSceneIndex">Index of the scene you want to change</param>
        /// <param name="pTransition">Transition prefab you want to use. </param>
        static public async void ChangeScene(int pSceneIndex, Transition pTransition)
        {
            AsyncOperation lCurrentLoadingScene = SceneManager.LoadSceneAsync(pSceneIndex);
            lCurrentLoadingScene.allowSceneActivation = false;
            DontDestroyOnLoad(pTransition.gameObject);
            
            Transition lCurrentTransition = CallTransition(pTransition);
            lCurrentTransition.autoEndAnimation = false;

            //Wait while transition or scene loading isn't finished
            while (!lCurrentTransition.isStartDone && !lCurrentLoadingScene.isDone)
            {
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            //When the scene is done charging activate the scene and trigger the end animation
            lCurrentLoadingScene.allowSceneActivation = true;
            lCurrentTransition.animator.SetTrigger(lCurrentTransition.endAnimationTrigger);
        }

        /// <summary>This serve as changing scene using a custom transition if desired.
        /// Make sure to reference a Transition and not a game object in you serialized field</summary>
        /// <param name="pSceneName">The scene name you want to load</param>
        /// <param name="pTransition">Transition prefab you want to use</param>
        static public async void ChangeScene(string pSceneName, Transition pTransition)
        {
            AsyncOperation lCurrentLoadingScene = SceneManager.LoadSceneAsync(pSceneName);
            lCurrentLoadingScene.allowSceneActivation = false;
            DontDestroyOnLoad(pTransition.gameObject);


            Transition lCurrentTransition = CallTransition(pTransition);
            lCurrentTransition.autoEndAnimation = false;

            //Wait while transition or scene loading isn't finished
            while (!lCurrentTransition.isStartDone && !lCurrentLoadingScene.isDone)
            {
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            //When the scene is done charging activate the scene and trigger the end animation
            lCurrentLoadingScene.allowSceneActivation = true;
            lCurrentTransition.animator.SetTrigger(lCurrentTransition.endAnimationTrigger);
        }


        private void OnDestroy()
        {
            if (instance != null && instance == this)
            {
                instance = null;
            }
        }
    }
}
