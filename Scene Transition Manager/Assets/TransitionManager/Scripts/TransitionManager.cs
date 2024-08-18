using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.EliottTan.SceneTransitions
{
    public static class TransitionManager 
    {
        static public GameObject defaultTransition => Resources.Load<GameObject>("BaseTransition");

        static public void ChangeScene(int pSceneIndex,LoadSceneMode pLoadSceneMode = LoadSceneMode.Single) 
        {
            Transition currentTransition = Object.Instantiate(defaultTransition).GetComponent<Transition>();
            currentTransition.TransitionToNextScene(pSceneIndex);
        }

        static public void ChangeScene(string pSceneNName, LoadSceneMode pLoadSceneMode = LoadSceneMode.Single)
        {
            Transition currentTransition = Object.Instantiate(defaultTransition).GetComponent<Transition>();
            currentTransition.TransitionToNextScene(pSceneNName, pLoadSceneMode);
        }
    }
}
