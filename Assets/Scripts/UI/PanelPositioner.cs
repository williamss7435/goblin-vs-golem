using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPositioner : MonoBehaviour
{
    public List<PanelPosition> positons;
    RectTransform rect;
    void Awake(){
        rect = GetComponent<RectTransform>();
    }
    public void MoveTo(string positionName){
        StopAllCoroutines();
        LeanTween.cancel(this.gameObject);
        PanelPosition position = positons.Find(x=> x.name == positionName);
        StartCoroutine(Move(position));
    }

    IEnumerator Move(PanelPosition panelPosition){
        rect.anchorMax = panelPosition.anchorMax;
        rect.anchorMin = panelPosition.anchorMin;
        
        int id = LeanTween.move(GetComponent<RectTransform>(), panelPosition.position, 0.5f).id;

        while(LeanTween.descr(id)!=null)
            yield return null;

        
    }
}
