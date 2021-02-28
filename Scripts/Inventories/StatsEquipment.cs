using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEquipment : Equipment, IModifierProvider
{
    public IEnumerable<float> GetAdditiveModifier(Stat stat)
    {
        throw new System.NotImplementedException();
    }
}
