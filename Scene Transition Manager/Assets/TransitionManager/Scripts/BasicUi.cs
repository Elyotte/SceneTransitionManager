using Com.EliottTan.SceneTransitions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicUi : MonoBehaviour
{
    [SerializeField] Button playBtn, quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        playBtn.onClick.AddListener(PlayAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnimation()
    {
        TransitionManager.ChangeScene(0);
    }
}
