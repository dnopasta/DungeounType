using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private UnitSpellController _unitSpellController;
    [SerializeField] private UnitMovementBehaviour _unitMovementBehaviour;

    public PlayerInputHandler InputHandler => _inputHandler;

    public UnitMovementBehaviour MovementBehaviour => _unitMovementBehaviour;

    private void Awake()
    {
        _unitSpellController.OnSpellActivate += OnSpellActivate;
    }

    private void OnDisable()
    {
        _unitSpellController.OnSpellActivate -= OnSpellActivate;
    }

    private void OnSpellActivate(SpellDescriptionScriptableObject spellSO)
    {
        _inputHandler.ClearMessage();
    }
}
