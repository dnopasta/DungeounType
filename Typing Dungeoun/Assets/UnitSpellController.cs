using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpellController : MonoBehaviour
{
   [SerializeField] private UnitBehaviour _unit;

   public Action<SpellDescriptionScriptableObject> OnSpellActivate = delegate{};
   
   private SpellsList _spellsList;

   private void Start()
   {
      _spellsList = SpellsList.Instance;
      _unit.InputHandler.OnMessageUpdated += OnMessageUpdated;
   }

   private void OnDisable()
   {
      _unit.InputHandler.OnMessageUpdated -= OnMessageUpdated;
   }

   private void OnMessageUpdated(string message)
   {
      CheckMessageForSpellAndExecuteIt(message);
   }
   
   private void CheckMessageForSpellAndExecuteIt(string message)
   {
      var spellSO = _spellsList.CheckMessageForSpell(message);
      if (spellSO != null)
      {
         ProcessSpell(spellSO);
      }
   }
   
   private void ProcessSpell(SpellDescriptionScriptableObject spell)
   {
      OnSpellActivate?.Invoke(spell);
      
      switch (spell.SpellType)
      {
         case SpellType.Movement:
            _unit.MovementBehaviour.ProcessMovementSpell(spell);
            break;
         
         case SpellType.Attacking:
            
            break;
      }
   }
}
