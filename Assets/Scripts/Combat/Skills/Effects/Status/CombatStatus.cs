using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatStatus : MonoBehaviour
{
    [HideInInspector]
    public Unit unit;
    [HideInInspector]
    public int duration;
    public abstract void Effect();
    protected virtual void OnDisable(){
        Modifier[] modifiers = GetComponents<Modifier>();
        foreach(Modifier m in modifiers){
            m.Deactivate();
        }
    }

    public void SetModifiers(int value){
        foreach(Modifier m in GetComponents<Modifier>()){
            m.value = value;
        }
    }

    public void Stack(int rcvDuration, int value){
        duration+=rcvDuration;
        foreach(Modifier m in GetComponents<Modifier>()){
            m.value += value;
        }
    }
}
