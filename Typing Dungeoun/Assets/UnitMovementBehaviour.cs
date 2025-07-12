using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMovementBehaviour : MonoBehaviour
{
    [SerializeField] private float skinSmoothSpeed;
    [SerializeField] private Transform _bodySprite;
    [SerializeField] private float snappedSpeed = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Coroutine _currentMovementCoroutine;
    
    public void ProcessMovementSpell(SpellDescriptionScriptableObject spell)
    {
        MoveToTargetPosition(spell.MovementTargetCoordinates, spell.MovementDuration);
    }
    
    private void MoveToTargetPosition(Vector2 targetPosition, float speed)
    {
        var rigidBodyPosition = _rigidbody2D.transform.position;
        var newTargetPosition = new Vector2(rigidBodyPosition.x, rigidBodyPosition.y) + targetPosition;

        if(_currentMovementCoroutine != null)
        {
            StopCoroutine(_currentMovementCoroutine);
        }
       
        _currentMovementCoroutine = StartCoroutine(MoveToPosition(newTargetPosition, speed));
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition, float speed)
    {
        Vector2 previousPosition = Vector2.negativeInfinity;
        
        while (true)
        {
            Vector2 newPosition =
                Vector2.MoveTowards(_rigidbody2D.position, targetPosition, speed * Time.fixedDeltaTime);
            _rigidbody2D.MovePosition(newPosition);

            if (previousPosition == _rigidbody2D.position)
            {
                break;
            }

            previousPosition = _rigidbody2D.position;
            
            yield return new WaitForFixedUpdate();
        }

        var clappedPosition =
            new Vector2(Mathf.RoundToInt(_rigidbody2D.position.x), Mathf.RoundToInt(_rigidbody2D.position.y));
        
        while (true)
        {
            _rigidbody2D.position = Vector2.Lerp(_rigidbody2D.position, clappedPosition, snappedSpeed * Time.deltaTime);
            
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _bodySprite.transform.position = Vector2.Lerp(_bodySprite.transform.position,_rigidbody2D.position,
            Time.fixedDeltaTime * skinSmoothSpeed);
    }
}
