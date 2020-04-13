using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRecap : MonoBehaviour
{
    public RectTransform recapPlane;
    public Recap recap;
    public Stats StatsManager;

    public void endRecap(){
        StatsManager.resetRecap();
        recap.resetActions();
        recap.resetCurrentText();
        recapPlane.gameObject.SetActive(false);
        recap.endTyping();
        recap.resetSkip();
    }
}
