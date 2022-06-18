using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : MonoBehaviour
{
    public StatsEnum statsEnum;
    public float value;
    Unit host;
    public virtual void Activate(Unit unit){
        host = unit;
        host.stats[statsEnum].statModifier += Modify;
        host.UpdateStats(statsEnum);
    }

    public virtual void Deactivate(){
       host.stats[statsEnum].statModifier -= Modify;
       host.UpdateStats(statsEnum);
    }

    protected abstract void Modify(Stat stat);
}
