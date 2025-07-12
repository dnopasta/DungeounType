using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells", fileName = "SpellsSO")]
public class SpellDescriptionScriptableObject : ScriptableObject
{
   public string SpellName;
   public string SpellSummonText;
   public SpellType SpellType;

   [Space(15f)]
   
   [ShowIf("IsMovementSpell")] public Vector2 MovementTargetCoordinates;
   [ShowIf("IsMovementSpell")] public float MovementDuration;
   
   private bool IsMovementSpell => SpellType == SpellType.Movement;
}


public enum SpellType
{
   Movement,
   Attacking
}