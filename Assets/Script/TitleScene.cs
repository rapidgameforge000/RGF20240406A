using Assets.Script.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    [UnityEngine.SerializeField] private UnityEngine.UI.Button startButton;
    private StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        this.startButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        });
    }
}

