using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public static Timer Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }


    [SerializeField] TextMeshPro _text;

    [SerializeField] bool _isStarted;
    [SerializeField] float _timerTime;

    void Update()
    {
        if (!_isStarted) return;

        _text.text = _timerTime.ToString("F2");

        _timerTime -= 1 * Time.deltaTime;

        if (_timerTime <= 0)
        {
            _timerTime = 0;
            _isStarted = false;
            return;
        }


    }

    public void WaitforTime(float time)
    {
        _isStarted = true;
        _timerTime = time;
    }

    public bool IsTimeCounting() => _isStarted;


}
