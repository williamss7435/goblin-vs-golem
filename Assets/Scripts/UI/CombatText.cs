using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatText : MonoBehaviour
{

    public static CombatText instance;
    public TextMeshProUGUI prefab;

    public Vector3 offset;
    public Vector3 random;
    public float timeToLive;
    private void Awake() {
        instance = this;
    }

    public void PopText(Unit unit, int value){
      StartCoroutine(PopControl(unit, value));
    }

    public void PopText(Unit unit, string value){
      StartCoroutine(PopControl(unit, value, Color.gray));
    }

    public void PopText(Unit unit, string value, Color color){
      StartCoroutine(PopControl(unit, value, color));
    }

    IEnumerator PopControl(Unit unit, int value){
        Vector3 randomPos = new Vector3(
            Random.Range(-random.x, random.x), 
            Random.Range(-random.y, random.y), 0
        );

        TextMeshProUGUI instantiated = Instantiate(
            prefab, unit.transform.position+offset+randomPos, Quaternion.identity,
            unit.transform.Find("Canvas")
        );
        instantiated.transform.SetAsLastSibling();

        if(value < 0){
            instantiated.color = Color.red;
        }else {
            instantiated.color = Color.green;
        }

        instantiated.text = ""+Mathf.Abs(value);
        Vector3 positionUp = new Vector3(instantiated.transform.position.x, instantiated.transform.position.y+0.35f, 0);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 1, 0.5f);
       LeanTween.move(instantiated.gameObject, positionUp, 0.3f);
        yield return new WaitForSeconds(timeToLive);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 0, 1).setOnComplete(() => {
            Destroy(instantiated.gameObject);
        });

    }

    IEnumerator PopControl(Unit unit, string value, Color color){
        Vector3 randomPos = new Vector3(
            Random.Range(-random.x, random.x), 
            Random.Range(-random.y, random.y), 0
        );

        TextMeshProUGUI instantiated = Instantiate(
            prefab, unit.transform.position+offset+randomPos, Quaternion.identity,
            unit.transform.Find("Canvas")
        );
        instantiated.transform.SetAsLastSibling();

        instantiated.color = color;
        instantiated.text = value;

        Vector3 positionUp = new Vector3(instantiated.transform.position.x, instantiated.transform.position.y+0.35f, 0);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 1, 0.5f);
        LeanTween.move(instantiated.gameObject, positionUp, 0.3f);
        
        yield return new WaitForSeconds(timeToLive);
        LeanTween.alphaCanvas(instantiated.GetComponent<CanvasGroup>(), 0, 1).setOnComplete(() => {
            Destroy(instantiated.gameObject);
        });
    }

    
}
