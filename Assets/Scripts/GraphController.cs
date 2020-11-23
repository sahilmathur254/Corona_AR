using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    public float WaitTime = .1f;
    public float BarScale = 5000;

    [SerializeField]
    API api;

    [SerializeField]
    TextMeshPro title;

    [SerializeField]
    List<BarBehaviour> bars = new List<BarBehaviour>();

    void Start()
    {
        api.GetTimeData(onDataRecieved);
    }

    void onDataRecieved(List<TimeData> datalist)
    {
        StartCoroutine(CycleDataRoutine(datalist));
    }

    IEnumerator CycleDataRoutine(List<TimeData> datalist)
    {
        while (true)
        {
            foreach (TimeData data in datalist)
            {
                title.text = data.date.ToString("yyyy-mm-dd");
                bars[0].SetScale(data.tested / BarScale);
                bars[1].SetScale(data.positives / BarScale);
                bars[2].SetScale(data.deaths / BarScale);

                yield return new WaitForSeconds(WaitTime);
            }

            yield return new WaitForEndOfFrame();

        }
    }

}
