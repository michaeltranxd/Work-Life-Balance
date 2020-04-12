using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordAction : MonoBehaviour
{
    public Recap recap;
    public Button btn;

    public void recordAction(){
        recap.setActions(btn.GetComponentInChildren<Text>().text + "\n");
    }
}
