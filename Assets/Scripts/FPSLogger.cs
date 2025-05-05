using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FPSLogger : MonoBehaviour
{
    //Class used to monitor the fps during a test sequence
    //After a test, it saves all fps values during it to an csv file,
    //  which is then used to make a graph in Excel


    private List<float> fpsList = new List<float>();
    private Coroutine currentLogSequence;

    public void StartLog(float duration, string filename)
    {
        fpsList.Clear();
        StopLog();
        currentLogSequence = StartCoroutine(FPSLogSequence(duration, filename));
    }

    public void StopLog()
    {
        if (currentLogSequence != null)
        {
            StopCoroutine(currentLogSequence);
        }
    }

    private IEnumerator FPSLogSequence(float duration, string filename)
    {
        while (duration > 0)
        {
            float fps = 1f / Time.unscaledDeltaTime;
            fpsList.Add(fps);
            duration -= Time.deltaTime;
            yield return null;
        }

        SaveToCSV(filename);
        currentLogSequence = null;
    }

    private void SaveToCSV(string filename)
    {
        string path = Application.dataPath + "/Test Results" + "/" + filename + ".csv";
        string fileContent = "[Frame] - [FPS]\n";
        for (int i = 0; i < fpsList.Count; i++)
        {
            //Write all fps values gotten during the test to a string which is then written to the csv file
            fileContent += $"{i + 1} - {(int)fpsList[i]}\n";
        }

        File.WriteAllText(path, fileContent);
    }
}
