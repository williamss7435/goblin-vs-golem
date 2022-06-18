using System.Collections;
using System.Collections.Generic;

public delegate void StatModifier(Stat stat);

[System.Serializable]
public class Stat
{
    public StatsEnum type;
    public int baseValue;
    public int currentValue;
    public float growth;
    public StatModifier statModifier;
}
