using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnTurnBegin();
public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats stats;
    public int faction;
    public int alliance;
    public TileLogic tile;
    public int chargeTime;
    public bool active;
    public Job job;
    public string spriteModel;

    public PlayerType playerType;
    public SpriteSwapper SS;
    [HideInInspector]
    SpriteRenderer spriteRenderer;
    [HideInInspector]
    public char direction;
    [HideInInspector]
    public AnimationController animationController;
    public OnTurnBegin onTurnBegin;
    public Image healthBar;
    [HideInInspector]
    public AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        stats = GetComponentInChildren<Stats>();
        SS = transform.Find("Sprite").GetComponent<SpriteSwapper>();
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        animationController = GetComponent<AnimationController>();
        direction = 'R';
    }

    private void Start() {
        SS.thisUnitSprites = SpriteLoader.holder.Find(spriteModel).GetComponent<SpriteLoader>();
        animationController.Idle();
    }
    public int GetStat(StatsEnum stateEnum){
        return stats.stats[(int)stateEnum].currentValue;
    }

    public void SetStat(StatsEnum stat, int value){
        if(stat == StatsEnum.HP){
            stats[stat].currentValue = ClampStat(StatsEnum.MaxHP, stats[stat].currentValue+value);
            PopCombatText(value);
            setHealthBar();
        } 
        if(stat == StatsEnum.MP)
            stats[stat].currentValue = ClampStat(StatsEnum.MaxMP, stats[stat].currentValue+value);
    }

    private void setHealthBar(){
        float maxHP = (float)GetStat(StatsEnum.MaxHP);
        float fillValue = (stats[StatsEnum.HP].currentValue*100/maxHP)/100;
        healthBar.fillAmount = fillValue;

        if(fillValue>=0.7f)
            healthBar.color = Color.green;
        else if(fillValue>=0.3f)
            healthBar.color = Color.yellow;
        else
            healthBar.color = Color.red;
    }

    private int ClampStat(StatsEnum statsEnum, int value){
        return Mathf.Clamp(value, 0, stats[statsEnum].currentValue);
    }

    public void FlipDirection(){
        ChangeDirection(this.direction == 'L' ? 'R' : 'L');
    }
    public void ChangeDirection(char direction){
        this.direction = direction;
        spriteRenderer.flipX = direction == 'L' ? true : false;
    }

    public void UpdateStats(StatsEnum statsEnum){
        Stat toUpdate = stats.stats[(int)statsEnum];
        toUpdate.currentValue = stats[statsEnum].baseValue;

        if(toUpdate.statModifier!=null)
            toUpdate.statModifier(toUpdate);
    }

    public void UpdateStats(){
        foreach(Stat s in stats.stats){
            UpdateStats(s.type);
        }
    }

    void PopCombatText(int value){
        CombatText.instance.PopText(this, value);
    }

    public void PopCombatText(string value){
        CombatText.instance.PopText(this, value);
    }

     public void PopCombatText(string value, Color textColor){
        CombatText.instance.PopText(this, value);
    }
}
