using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UObject = UnityEngine.Object;

namespace Com.EliottTan.SceneTransitions
{
    public static class TransitionManager 
    {
        static public Transition defaultTransition => Resources.Load<Transition>("BaseTransition");

        /// <summary>
        /// Change scene using a default transition and the scene index
        /// </summary>
        /// <param name="pSceneIndex">The scene index in build parameters </param>
        /// <param name="pLoadSceneMode">The scene mode you want to use, Single by default </param>
        static public void ChangeScene(int pSceneIndex,LoadSceneMode pLoadSceneMode = LoadSceneMode.Single) 
        {
            ChangeScene(defaultTransition, pSceneIndex, pLoadSceneMode);
        }

        /// <summary>
        /// Change scene using a default transition and the scene name
        /// </summary>
        /// <param name="pSceneName">The name of the scene you want to load</param>
        /// <param name="pLoadSceneMode">The scene mode you want to use, is in Single by default</param>
        static public void ChangeScene(string pSceneName, LoadSceneMode pLoadSceneMode = LoadSceneMode.Single)
        {
            ChangeScene(defaultTransition, pSceneName, pLoadSceneMode);
        }

        /// <summary>
        /// Change a scene using Scene index in build parameters, with a user given transition
        /// </summary>
        /// <param name="pTransition">The transitionn you want to display, reference a prefab that contained a Transition at its root</param>
        /// <param name="pSceneIndex">The index of the scene in build settings </param>
        /// <param name="pLoadSceneMode">The load scene mode is in Single by default </param>
        static public void ChangeScene(Transition pTransition, int pSceneIndex, LoadSceneMode pLoadSceneMode = LoadSceneMode.Single)
        {
            Transition currentTransition = UObject.Instantiate(pTransition);
            currentTransition.TransitionToNextScene(pSceneIndex, pLoadSceneMode);
        }

        /// <summary>
        /// Change a scene using Scene name, with a user given transition
        /// </summary>
        /// <param name="pTransition">The transitionn you want to display, reference a prefab that contained a Transition at its root</param>
        /// <param name="pSceneName">The scene name</param>
        /// <param name="pLoadSceneMode">The load scene mode is in Single by default</param>
        static public void ChangeScene(Transition pTransition, string pSceneName, LoadSceneMode pLoadSceneMode = LoadSceneMode.Single)
        {
            Transition currentTransition = UObject.Instantiate(pTransition);
            currentTransition.TransitionToNextScene(pSceneName, pLoadSceneMode);
        }

        static public void ShowTransition(Transition pTransition, Action pMethod)
        {
            Transition currentTransition = UObject.Instantiate(pTransition);
            currentTransition.onStartAnimationFinished += pMethod;
            currentTransition.TransitionToMethod();
        }

        static public void ShowTransition(Action pMethod)
        {
            ShowTransition(defaultTransition, pMethod);
        }
    }
}
