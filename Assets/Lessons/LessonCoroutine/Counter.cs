using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private const int LeftMouseButton = 0;

    private WaitForSeconds _delay = new WaitForSeconds(0.5f);
    private Coroutine _job;
    private int _value;
    private bool _isWorking = false;

    public event Action<int> ValueChanged;

    private void Update()
    {
        GetMouseButton();
    }

    private void GetMouseButton()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            if (_isWorking)
            {
                _isWorking = false;
                Stop();
            }
            else
            {
                _isWorking = true;
                Begin();
            }
        }
    }

    private void Begin()
    {        
        _job = StartCoroutine(Calculate());
    }

    private void Stop()
    {
        if (_job != null)
            StopCoroutine(_job);
    }

    private IEnumerator Calculate()
    {
        while (_isWorking)
        {
            yield return _delay;

            _value++;
            ValueChanged?.Invoke(_value);
        }
    }
}
