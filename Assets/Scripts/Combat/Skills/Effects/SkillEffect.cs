using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffect : MonoBehaviour
{
   public abstract int Predict(Unit target);
   public abstract void Apply(Unit target);
}
