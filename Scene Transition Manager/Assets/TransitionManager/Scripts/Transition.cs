using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.EliottTan.SceneTransitions
{
    /// <summary>
    /// Component used to create transition between scenes or screens
    /// </summary>
    public class Transition : MonoBehaviour
    {
        //Animator and triggers
        public Animator animator { get; private set; }
        string startTrigger = "StartTrigger";
        string endTrigger = "TriggerEnd";

        float maxAnimationProgress = .9f;

        bool startAnimationFinished = false;

        public event Action<float> onAsyncLoadProgress;
        public event Action onStartAnimationFinished;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
            startAnimationFinished = false;
        }

        public async void TransitionToNextScene(int pSceneIndex,LoadSceneMode pMode = LoadSceneMode.Single)
        {
            animator.SetTrigger(startTrigger);

            while (!startAnimationFinished)
            {
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            AsyncOperation lNextScene = SceneManager.LoadSceneAsync(pSceneIndex,pMode);

            lNextScene.allowSceneActivation = false;
            while (lNextScene.progress < maxAnimationProgress)
            {
                onAsyncLoadProgress?.Invoke(lNextScene.progress);
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            lNextScene.allowSceneActivation = true;
            animator.SetTrigger(endTrigger);
        }

        public async void TransitionToNextScene(string pSceneName, LoadSceneMode pMode = LoadSceneMode.Single)
        {
            animator.SetTrigger(startTrigger);

            while (!startAnimationFinished)
            {
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            AsyncOperation lNextScene = SceneManager.LoadSceneAsync(pSceneName, pMode);

            lNextScene.allowSceneActivation = false;
            while (lNextScene.progress < maxAnimationProgress)
            {
                onAsyncLoadProgress?.Invoke(lNextScene.progress);
                await Awaitable.WaitForSecondsAsync(Time.deltaTime);
            }

            lNextScene.allowSceneActivation = true;
            animator.SetTrigger(endTrigger);
        }

        public void OnStartAnimationFinished()
        {
            onStartAnimationFinished?.Invoke();
            startAnimationFinished = true;
        }

        public void OnEndAnimationFinished()
        {
            Destroy(gameObject);
        }

    }
}
