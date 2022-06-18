using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillVisualFX : MonoBehaviour
{
    public ParticleSystem always;
    public ParticleSystem onlyOnHit;
    [HideInInspector]
    public Unit target;
    public float delay;
    public bool didHit;
    public void VFX(){
        Invoke("VFXDelay", delay);
    }
    void VFXDelay(){
        if(always!=null) SpawnEffect(always);
        if(didHit && onlyOnHit!=null) SpawnEffect(onlyOnHit);
    }

    void SpawnEffect(ParticleSystem particleSystem){
        Instantiate(
                particleSystem, 
                target.SS.transform.position,
                Quaternion.identity, 
                target.transform.Find("Canvas")
            );
    }
}
