using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Text unitName;
    public Text lvl;
    public Text hp;
    public Text mp;
    public Text acc;
    public Text evd;
    public Text res;
    public Text ct;
    public Image portrait;
    public PanelPositioner panelPositioner;
    private void Awake() {
        panelPositioner = this.GetComponent<PanelPositioner>();
    }

    void SetValues(Unit unit){
        unitName.text = unit.name;
        //lvl.text = "Lv. " + unit.stats[StatsEnum.LVL].currentValue;
        hp.text = string.Format(
            "HP: {0}/{1}", 
            unit.stats[StatsEnum.HP].currentValue,
            unit.stats[StatsEnum.MaxHP].currentValue
        );
        mp.text = string.Format(
            "MP: {0}/{1}", 
            unit.stats[StatsEnum.MP].currentValue,
            unit.stats[StatsEnum.MaxMP].currentValue
        );
        acc.text = "ACC: " + unit.stats[StatsEnum.ACC].currentValue;
        evd.text = "EVD: " + unit.stats[StatsEnum.EVD].currentValue;
        res.text = "RES: " + unit.stats[StatsEnum.RES].currentValue;
        ct.text = "CT: " + unit.chargeTime;
        portrait.sprite = unit.job.portrait;
    }

    public void Show(Unit unit){
        SetValues(unit);
        panelPositioner.MoveTo("Show");
    }

    public void Hide(){
        panelPositioner.MoveTo("Hide");
    }
}
