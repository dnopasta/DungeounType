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
    [SerializeField] private float speed = 1f;
    [SerializeField] private float velocityCap = 0.2f;
    [SerializeField] private float snappedSpeed = 1f;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Coroutine _currentMovementCoroutine;
    
    public void ProcessMovementSpell(SpellDescriptionScriptableObject spell)
    {
        MoveToTargetPosition(spell.MovementTargetCoordinates, spell.MovementDuration);
    }
    
    private void MoveToTargetPosition(Vector2 targetPosition, float duration)
    {
       var newTargetPosition = new Vector2(transform.position.x, transform.position.y) + targetPosition;

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
            new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        
        while (true)
        {
            transform.position = Vector2.Lerp(transform.position, clappedPosition, snappedSpeed * Time.deltaTime);
            
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        _bodySprite.transform.position = Vector2.Lerp(_bodySprite.transform.position, transform.position,
            Time.fixedDeltaTime * skinSmoothSpeed);
    }
}
