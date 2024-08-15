using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.EliottTan.SceneTransitions
{
    /// <summary>
    /// Component used to create transition between scenes or screens
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Transition : MonoBehaviour
    {
        public event Action onAnimationLoadState;
        public Animator animator { get; private set; }
        public string endAnimationTrigger { get; private set; } = "EndAnimation";

        public bool autoEndAnimation = true;
        public bool isStartDone { get; private set; } = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Is sent by animator when the screen is fully hidden by the transition
        /// It is used for transition between screen, the canva will be set to true when EmitLoadState is called
        /// </summary>
        public void EmitLoadState()
        {
            onAnimationLoadState?.Invoke();
            isStartDone = true; 
            if (autoEndAnimation)
                animator.SetTrigger(endAnimationTrigger);
                
        }

        /// <summary>
        /// Destroy the game object when the animation is done
        /// </summary>
        public void DestroyGameObject()
        {
            Destroy(gameObject);
        }

        //Remove every callback of possibles instance that will not be destroyed
        private void OnDestroy()
        {
            onAnimationLoadState = null;
        }
    }
}
