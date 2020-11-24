using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class API : MonoBehaviour
{

    //const string ENDPOINT = "https://covid.ourworldindata.org/data/owid-covid-data.csv";

    const string ENDPOINT = "https://covid19.who.int/WHO-COVID-19-global-data.csv";


    public void GetTimeData(UnityAction<List<TimeData>> callback)
    {
        StartCoroutine(GetTimeDataRoutine(callback));
    }

    IEnumerator GetTimeDataRoutine(UnityAction<List<TimeData>> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(ENDPOINT);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("Network error");
        }else
        {
            callback( ParseData(request.downloadHandler.text));
        }
    }

    List<TimeData> ParseData(string data)
    {
        //Date_reported,Country_code,Country,WHO_region,New_cases,Cumulative_cases,New_deaths,Cumulative_death
        List<string> lines = data.Split('\n').ToList();
        lines.RemoveAt(0);
        lines.RemoveAt(lines.Count - 1);

        List<TimeData> dataList = new List<TimeData>();

        foreach (string line in lines)
        { 
        List<string> linesData = line.Split(',').ToList();
            TimeData timeData = new TimeData
            {
                date = DateTime.Parse(linesData[0]),
                tested = int.Parse(linesData[5]),
                positives = int.Parse(linesData[6]),
                deaths = int.Parse(linesData[7]),

            };
            dataList.Add(timeData);

    }
        return dataList;

    }

}
