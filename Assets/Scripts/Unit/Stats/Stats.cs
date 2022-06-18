using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public List<Stat> stats;
    public Stat this[StatsEnum s]{
        get{return stats[(int)s];}
    }
}
