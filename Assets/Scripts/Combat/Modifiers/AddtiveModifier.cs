using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddtiveModifier : Modifier
{
    protected override void Modify(Stat stat)
    {
        stat.currentValue += (int)value;
    }
}
