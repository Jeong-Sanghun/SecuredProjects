using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLogDialogPair
{
    public SceneName sceneName;
    public DialogBundle dialogBundle;

    public BackLogDialogPair(SceneName scene, DialogBundle bundle)
    {
        sceneName = scene;
        dialogBundle = bundle;
    }
}
