using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public List<Job> jobs;
    Dictionary<string, Job> jobsDictionary;
    public bool serealizeUnits;
    public List<UnitSerealized> serealizedsUnits;
    public static MapLoader instance;
    public Unit unitPrefab;
    GameObject holder;
    public List<Alliance> alliances;
    private void Awake() {
        BuildJobsDictionary();
        instance = this;
        holder = new GameObject("Units Holder");
    }

    private void BuildJobsDictionary(){
        jobsDictionary = new Dictionary<string, Job>();
        foreach(Job j in jobs){
            jobsDictionary.Add(j.name, j);
        }
    }
    private void Start() {
        holder.transform.parent = Board.instance.transform;
        InitializeAliances();
    }


    public void InitializeAliances(){
        for (int i = 0; i < alliances.Count; i++){
            alliances[i].units = new List<Unit>();
        }
    }

    public Unit CreateUnit(UnitSerealized unitSerealized){
        TileLogic t = Board.GetTile(unitSerealized.position);
        Job job = jobsDictionary[unitSerealized.spriteModel];

        Unit unit = Instantiate(unitPrefab, t.worldPos, Quaternion.identity, holder.transform);
        unit.tile = t;
        unit.name = unitSerealized.characterName;
        unit.faction = unitSerealized.faction;
        unit.spriteModel = unitSerealized.spriteModel;
        unit.job = job;
        t.content = unit.gameObject;
        unit.playerType = unitSerealized.playerType;

        StateMachineController.instance.units.Add(unit);

        SetStats(unit.stats, job);
        unit.UpdateStats();
        Job.LevelUp(unit, unitSerealized.level-1);
        SkillBook skillBook = unit.GetComponentInChildren<SkillBook>();
        skillBook.skills = new List<Skill>();
        skillBook.skills.AddRange(job.skills);

        unit.ChangeDirection(unitSerealized.faction == 0 ? 'R' : 'L');

        return unit;
    }

    void SetStats(Stats stats, Job job){
        stats.stats = new List<Stat>();

        for(int i=0; i<job.stats.Count; i++){
            Stat stat = new Stat();
            stat.baseValue = job.stats[i].baseValue;
            stat.currentValue = job.stats[i].currentValue;
            stat.growth = job.stats[i].growth;
            stat.type = job.stats[i].type;
            stats.stats.Add(stat);
        }

        stats.stats[(int) StatsEnum.HP].baseValue = stats.stats[(int)StatsEnum.MaxHP].baseValue;
        stats.stats[(int) StatsEnum.MP].baseValue = stats.stats[(int)StatsEnum.MaxMP].baseValue;
    }

    public void CreateUnits(){
        if(serealizeUnits){
            //serealize units by Inspector
            foreach (UnitSerealized unitSerealized in serealizedsUnits)
            CreateUnit(unitSerealized);
        }else {
            //serealize units by code


            // Human Units
            CreateUnit(new UnitSerealized("Leader Goblin", "Orc", new Vector3Int(0, 4, 0), 0, 1, PlayerType.Human));

            CreateUnit(new UnitSerealized("Dark Goblin1", "Ogre", new Vector3Int(0, 5, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Dark Goblin2", "Ogre", new Vector3Int(0, 6, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Dark Goblin3", "Ogre", new Vector3Int(0, 3, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Dark Goblin4", "Ogre", new Vector3Int(0, 2, 0), 0, 1, PlayerType.Human));

            CreateUnit(new UnitSerealized("Goblin1", "Goblin", new Vector3Int(1, 5, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Goblin2", "Goblin", new Vector3Int(1, 4, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Goblin3", "Goblin", new Vector3Int(1, 3, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Goblin4", "Goblin", new Vector3Int(2, 5, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Goblin5", "Goblin", new Vector3Int(2, 4, 0), 0, 1, PlayerType.Human));
            CreateUnit(new UnitSerealized("Goblin6", "Goblin", new Vector3Int(2, 3, 0), 0, 1, PlayerType.Human));



            //Computer Units
            CreateUnit(new UnitSerealized("King Golem", "IceGolem", new Vector3Int(13, 4, 0), 1, 1, PlayerType.Computer));

            CreateUnit(new UnitSerealized("Fire Golem1", "FireGolem", new Vector3Int(13, 5, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Fire Golem2", "FireGolem", new Vector3Int(13, 6, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Fire Golem3", "FireGolem", new Vector3Int(13, 3, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Fire Golem4", "FireGolem", new Vector3Int(13, 2, 0), 1, 1, PlayerType.Computer));

            CreateUnit(new UnitSerealized("Earth Golem1", "EarthGolem", new Vector3Int(12, 5, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Earth Golem2", "EarthGolem", new Vector3Int(12, 4, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Earth Golem3", "EarthGolem", new Vector3Int(12, 3, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Earth Golem4", "EarthGolem", new Vector3Int(11, 5, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Earth Golem5", "EarthGolem", new Vector3Int(11, 4, 0), 1, 1, PlayerType.Computer));
            CreateUnit(new UnitSerealized("Earth Golem6", "EarthGolem", new Vector3Int(11, 3, 0), 1, 1, PlayerType.Computer));

            

            
        }
        
        LoadPanel.instance.Load(false);
    }
}
