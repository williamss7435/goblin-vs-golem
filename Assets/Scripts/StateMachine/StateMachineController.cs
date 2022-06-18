using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineController : MonoBehaviour
{
    public Text ActiveStateText;
    public static StateMachineController instance;
    public GameObject selector;
    State _current;
    bool busy;
    public State current{get{return _current;}}

    public TileLogic selectedTile;
    public List<Unit> units;
    [Header("ChooseActionState")]
    public List<Image> chooseActionButtons;
    public Image chooseActionSelection;
    public PanelPositioner chooseActionPanel;
    public Text txtActionState;

    [Header("SkillSelectionState")]
    public List<Image> skillSelectionButtons;
    public Image skillSelectionSelection;
    public PanelPositioner skillSelectionPanel;
    public AttackDescriptionPanel attackDescriptionPanel;
    public Sprite skillSelectionBlock;
    [Header("SkillPredictionState")]
    public SkillPredictionPanel skillPredictionPanel;
    public CharacterPanel leftcharacterPanel;
    public CharacterPanel rightcharacterPanel;
    void Awake() {
        instance = this;
    }

    private void Start() {
        ChangeTo<LoadState>();
    }

    public void ChangeTo<T>() where T: State{
        State state = GetState<T>();
        if(_current != state){
            //Debug current State
            //this.ActiveStateText.text = state.ToString();
            ChangeState(state);
        }
    }

    public T GetState<T>() where T: State {
        T target = GetComponent<T>();
        if(target==null)
            target = gameObject.AddComponent<T>();
        return target;
    }

    protected void ChangeState(State state){
        if(busy) return;
        busy = true;

        if(_current!= null){
            _current.Exit();
        }

        _current = state;
        if(_current != null){
            _current.Enter();
        }

        busy = false;
    }
}
