using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TestSequencer : MonoBehaviour
{
    [Header("UI Variables")]
    [SerializeField] private TMP_Dropdown prefabDropdown;
    [SerializeField] private TMP_InputField countInput;
    [SerializeField] private TMP_InputField durationInput;
    [SerializeField] private TextMeshProUGUI testDurationText;

    [Header("Test Variables")]
    [SerializeField] private GameObject[] prefabsToSpawn;
    private GameObject currentPrefabChosen;
    [SerializeField] private int objectCount = 0;
    [SerializeField] private float testDuration = 0;
    private float testTimer = 0;

    [Header("Other Variables")]
    [SerializeField] private ObjectSpawner spawner;
    [SerializeField] private InformationTracker tracker;
    [SerializeField] private FPSLogger fpsLogger;

    [SerializeField] private int fpsCap = 60;

    private bool isTestRunning = false;

    [SerializeField] private bool canRunTest = true;

    private void Start()
    {
        SetUpDropdown();
        UpdateCurrentPrefab(prefabDropdown.value);
        UpdateCountInput(countInput.text);
        UpdateTestDuration(durationInput.text);
        countInput.onValueChanged.AddListener(UpdateCountInput);
        prefabDropdown.onValueChanged.AddListener(UpdateCurrentPrefab);
        durationInput.onValueChanged.AddListener(UpdateTestDuration);
        StopTest();
        Application.targetFrameRate = fpsCap; //Cap the framerate to keep test conditions consistent
    }

    private void Update()
    {
        if (isTestRunning)
        {
            testTimer -= Time.deltaTime;
            testDurationText.text = "Test duration: " + (int)testTimer;
            if (testTimer <= 0)
            {
                StopTest();
            }
        }
    }    

    public void UpdateCurrentPrefab(int value)
    {
        currentPrefabChosen = prefabsToSpawn[value];
    }

    public void UpdateCountInput(string value)
    {
        if (!int.TryParse(value, out int result))
        {
            Debug.LogError("ERROR: Object count input is invalid!");
            countInput.text = "NaN";
            canRunTest = false;
            return;
        }
        canRunTest = true;
        objectCount = result;
    }

    public void UpdateTestDuration(string value)
    {
        if (!int.TryParse(value, out int result))
        {
            Debug.LogError("ERROR: Test duration input is invalid!");
            durationInput.text = "NaN";
            canRunTest = false;
            return;
        }
        canRunTest = true;
        testDuration = result;
    }

    private void SetUpDropdown()
    {
        prefabDropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (GameObject arrayElement in prefabsToSpawn)
        {
            string optionText = arrayElement.name;

            if (!string.IsNullOrEmpty(optionText))
            {
                options.Add(new TMP_Dropdown.OptionData(optionText));
            }
        }
        prefabDropdown.AddOptions(options);
    }

    public void StartTest()
    {
        if (!canRunTest)
        {
            DebugMessenger.DebugMessage("One or more parameters is invalid, cannot start test.");
            return;
        }
        if (isTestRunning)
        {
            DebugMessenger.DebugMessage("Test is in progress, stopping previous test instance.");
            StopTest();
        }
        isTestRunning = true;
        testTimer = testDuration;
        spawner.SpawnObjectsForTest(currentPrefabChosen, objectCount);
        tracker.ResetStats();
        tracker.keepTrackOfFPS = true;
        testDurationText.gameObject.SetActive(true);
        fpsLogger.StartLog(testDuration);
    }

    public void StopTest()
    {
        isTestRunning = false;
        spawner.DestroyAllObjects();
        testDurationText.gameObject.SetActive(false);
        tracker.keepTrackOfFPS = false;
    }
}
