using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TutorialSystem : MonoBehaviour
{
    public static TutorialSystem Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }



    [Header("DATA")]
    [SerializeField] Transform _background;

    [SerializeField] Transform _brightMax;
    public void SetBrightMaxPanel(bool condition) => _brightMax.gameObject.SetActive(condition);

    [SerializeField] Transform _darkMax;
    public void SetDarkMaxPanel(bool condition) => _darkMax.gameObject.SetActive(condition);

    [SerializeField] Transform _hongXi;
    public void SetHONGXIPanel(bool condition) => _hongXi.gameObject.SetActive(condition);

    [SerializeField] List<Transform> _tutorialPanelList;
    public List<Transform> TutorialPanelList { get { return _tutorialPanelList; } set { _tutorialPanelList = value; } }

    [SerializeField] int _tutorialIndex;
    public int TutorialIndex { get { return _tutorialIndex; } set { _tutorialIndex = value; } }


    bool _isActive;
    public bool IsActive { get { return _isActive; } set { _isActive = value; } }

    bool _tutorialIsOver;
    public bool TutorialIsOver { get { return _tutorialIsOver; } }



    void Start()
    {
        _tutorialIndex = 0;

        _tutorialIsOver = false;

        EnergySystem.Instance.FirstTimeBrightFullevent += ShowBrigthEnergyMaxPanel;
        EnergySystem.Instance.FirstTimeDarkFullevent += ShowDarkEnergyMaxPanel;

        LevelManager.Instance.FirstHONGXIevent += ShowHONGXIPanel;

        NumSlot.StartTutorialEvent += StartTutorialEvent;




    }

    void Update()
    {

        if (!_isActive) return;

        _background.gameObject.SetActive(true);

        TutorialPanelListIndexCheck();

        ShowTutorialPanel(_tutorialIndex);

    }


    void StartTutorialEvent()
    {
        _isActive = true;
    }





    void ShowTutorialPanel(int value)
    {
        if (value == _tutorialPanelList.Count) return;
        else _tutorialPanelList[value].gameObject.SetActive(true);
    }

    void TutorialPanelListIndexCheck()
    {
        if (_tutorialIndex == _tutorialPanelList.Count)
        {
            _background.gameObject.SetActive(false);
            _isActive = false;
            _tutorialIsOver = true;
            //_tutorialIndex = 0;
        }
    }

    void ShowBrigthEnergyMaxPanel()
    {
        _brightMax.gameObject.SetActive(true);
    }
    void ShowDarkEnergyMaxPanel()
    {
        _darkMax.gameObject.SetActive(true);
    }
    void ShowHONGXIPanel()
    {
        _hongXi.gameObject.SetActive(true);
    }













}
