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
    
    private void MoveToTargetPosition(Vector2 targetPosition, float duration)
    {
        var rigidBodyPosition = _rigidbody2D.transform.position;
        var newTargetPosition = new Vector2(rigidBodyPosition.x, rigidBodyPosition.y) + targetPosition;

        if(_currentMovementCoroutine != null)
        {
            StopCoroutine(_currentMovementCoroutine);
        }
       
        _currentMovementCoroutine = StartCoroutine(MoveToPosition(newTargetPosition, duration));
    }

    private IEnumerator MoveToPosition(Vector2 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = _rigidbody2D.transform.position;
        Vector2 previousPosition = Vector2.negativeInfinity;
        
        while (true)
        {

            elapsedTime += Time.fixedDeltaTime;

            var t = elapsedTime / duration;
            
            // f(x) = x^1.1
            t = 1 - Mathf.Pow(1 - t, 3);

            Vector2 newPosition =
                Vector2.MoveTowards(startPosition, targetPosition, t);
            _rigidbody2D.MovePosition(newPosition);

            if (elapsedTime >= duration || previousPosition == _rigidbody2D.position)
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
      //  _bodySprite.transform.position = Vector2.Lerp(_bodySprite.transform.position,_rigidbody2D.position,
       //     Time.fixedDeltaTime * skinSmoothSpeed);
    }
    
    
}
