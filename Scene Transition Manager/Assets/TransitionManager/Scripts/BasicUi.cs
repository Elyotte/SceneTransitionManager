using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BasicUi : MonoBehaviour
{
    [SerializeField] Button playBtn, quitBtn;
    [SerializeField] Transition transition;
    [SerializeField] GameObject toHide;

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(PlayAnimation);
        quitBtn.onClick.AddListener(QuitPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnimation()
    {
        TransitionManager.ChangeScene(1);
    }

    void QuitPressed()
    {
        TransitionManager.ShowTransition(transition,HideGraphics);
    }

    void HideGraphics()
    {
        toHide?.SetActive(false);
    }
}
