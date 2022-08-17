using System;
using System.Collections.Generic;
using UnityEngine;

public class RewindScript : MonoBehaviour
{
    public bool _isRewind;
    private List<TransformForRecord> _statesInSpace;
    public GameObject blure;

    private void Start()
    {
        _isRewind = false;
        _statesInSpace = new List<TransformForRecord>();
        blure.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Backspace))
            StopRewind();
    }

    private void FixedUpdate()
    {
        if (_isRewind)
            Rewind();
        else Record();
    }

    private void Record()
    {
        _statesInSpace.Insert(0, 
            new TransformForRecord
            (transform.position, transform.rotation));
    }

    private void Rewind()
    {
        if(_statesInSpace.Count > 0)
        {
            transform.position = _statesInSpace[0].positionObject;
            transform.rotation = _statesInSpace[0].rotationObject;
            _statesInSpace.RemoveAt(0);
        }
        else StopRewind();
    }

    public void StartRewind()
    {
        _isRewind = true;
        blure.SetActive(true);
    }

    public void StopRewind()
    {
        _isRewind = false;
        blure.SetActive(false);
    }
}
