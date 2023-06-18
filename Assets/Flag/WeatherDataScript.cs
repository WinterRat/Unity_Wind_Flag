using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;

public class WeatherDataScript : MonoBehaviour
{
    private readonly string url = "http://apis.data.go.kr/1360000/VilageFcstInfoService_2.0/getUltraSrtNcst";
    private readonly string key = "Your API Key";

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }


    private IEnumerator GetWeatherData()
    {
        var now = DateTime.Now;
        var baseDate = now.ToString("yyyyMMdd");
        var baseTime = now.ToString("HH00");

        if (now.Minute <= 40)
        {
            if (now.Hour == 0)
            {
                baseDate = now.AddDays(-1).ToString("yyyyMMdd");
                baseTime = "2300";
            }
            else
            {
                baseDate = now.ToString("yyyyMMdd");
                baseTime = now.AddHours(-1).ToString("HH00");
            }
        }
        else
        {
            baseDate = now.ToString("yyyyMMdd");
            baseTime = now.ToString("HH00");
        }
        string encodedKey = UnityWebRequest.EscapeURL(key);
        string getDataUrl = $"{url}?ServiceKey={encodedKey}&base_date={baseDate}&base_time={baseTime}&nx=60&ny=126&numOfRows=30&pageNo=1&dataType=JSON";

        using (UnityWebRequest www = UnityWebRequest.Get(getDataUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    Debug.Log(jsonResult);

                    JsonData jsonData = JsonMapper.ToObject(jsonResult);
                    JsonData items = jsonData["response"]["body"]["items"]["item"];

                    float windDirection = 0.0f;
                    float windSpeed = 0.0f;

                    // Retrieve wind direction and wind speed
                    for (int i = 0; i < items.Count; i++)
                    {
                        string category = (string)items[i]["category"];
                        string obsrValue = (string)items[i]["obsrValue"];

                        if (category == "VEC")
                        {
                            float.TryParse(obsrValue, out windDirection);
                        }
                        else if (category == "WSD")
                        {
                            float.TryParse(obsrValue, out windSpeed);
                        }
                    }

                    Debug.Log("Wind Direction: " + windDirection);
                    Debug.Log("Wind Speed: " + windSpeed);

                    Vector3 wind = new Vector3(Mathf.Cos(windDirection * Mathf.Deg2Rad), 0, Mathf.Sin(windDirection * Mathf.Deg2Rad)) * windSpeed;
                    FlagController flagController = FindObjectOfType<FlagController>();
                    flagController.UpdateWind(wind);
                }
            }
        }
    }
}