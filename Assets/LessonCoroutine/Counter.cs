using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private WaitForSeconds _delay = new WaitForSeconds(0.5f);
    private Coroutine _jobCounter;
    private int _value;
    private bool _isCounterWorking = false;

    public event Action<int> ValueChanged;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_isCounterWorking)
            {
                _isCounterWorking = false;
                StopCounter();
            }
            else
            {
                _isCounterWorking = true;
                StartCounter();
            }
        }
    }

    private void StartCounter()
    {        
        _jobCounter = StartCoroutine(Calculate());
    }

    private void StopCounter()
    {
        if (_jobCounter != null)
            StopCoroutine(_jobCounter);
    }

    private IEnumerator Calculate()
    {
        while (_isCounterWorking)
        {
            yield return _delay;

            _value++;
            ValueChanged?.Invoke(_value);
        }
    }
}
