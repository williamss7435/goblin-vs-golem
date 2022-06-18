using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPredictionPanel : MonoBehaviour
{
    [HideInInspector]
    public Text skillName;
    [HideInInspector]
    public Text chanceToHit;
    [HideInInspector]
    public Text predictEffect;
    [HideInInspector]
    public PanelPositioner positioner;

    void Awake() {
        positioner = GetComponent<PanelPositioner>();
        skillName = transform.Find("SkillName").GetComponent<Text>();
        chanceToHit = transform.Find("ChanceToHit").GetComponent<Text>();
        predictEffect = transform.Find("PredictEffect").GetComponent<Text>();
    }

    public void SetPredictionText(){
        int toHit = 0;
        int predict = 0;
        Unit target = null;
        
        if(SelectorBoard.instance.tile.content && SelectorBoard.instance.tile.content != null)
            target = SelectorBoard.instance.tile.content.GetComponent<Unit>();

        if(target != null){
            toHit = Mathf.Clamp(Turn.skill.GetHitPrediction(target), 0 , 100);
            predict = Turn.skill.GetEffetPrediction(target);
            StateMachineController.instance.rightcharacterPanel.Show(target);
        }
        
        skillName.text = Turn.skill.skillName;
        chanceToHit.text = toHit + "%";
        predictEffect.text = predict + " hit points";
    }
}
