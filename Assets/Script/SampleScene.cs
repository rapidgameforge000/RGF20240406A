using Assets.Script.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleScene : MonoBehaviour
{
    [UnityEngine.SerializeField] private UnityEngine.Canvas canvas;
    [UnityEngine.SerializeField] private UnityEngine.Transform root;
    private StageManager stageManager;
    // Start is called before the first frame update
    void Start()
    {
        this.stageManager = new StageManager();
        this.stageManager.Initialize(this.root);
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = UnityEngine.Time.deltaTime;
        this.stageManager.Process(deltaTime);
    }
}
