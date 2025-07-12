using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsList : MonoBehaviour
{
    public static SpellsList Instance;
    public SpellDescriptionScriptableObject[] SpellDescriptionScriptableObjects;

    private void Awake()
    {
        Instance = this;
    }

    public SpellDescriptionScriptableObject CheckMessageForSpell(string message)
    {
        foreach (var spell in SpellDescriptionScriptableObjects)
        {
            if (spell.SpellSummonText == message)
            {
                return spell;
            }
        }

        return null;
    }
}
