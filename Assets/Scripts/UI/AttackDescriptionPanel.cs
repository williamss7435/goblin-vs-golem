using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackDescriptionPanel : MonoBehaviour
{
    private Text txtAttackName;
    private Text txtDescription;
    private void Awake() {
        txtAttackName = transform.Find("txtAttackName").GetComponent<Text>();
        txtDescription = transform.Find("txtDescription").GetComponent<Text>();
    }

    public void ShowDescription(string attackName, string description){
        SetTxtAttackName(attackName);
        SetTxtDescription(description);
        Show();
    }

    public void Hide(){
        this.gameObject.SetActive(false);
    }

    public void Show(){
        this.gameObject.SetActive(true);
    }

    public void SetTxtAttackName(string value){
        txtAttackName.text = value;
    }

    public void SetTxtDescription(string value){
        txtDescription.text = value;
    }

}
