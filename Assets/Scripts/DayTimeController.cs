using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DayTimeController : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f;
    const float phasesInDay = 96f;

    [SerializeField] Color nightLightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayLightColor = Color.white;

   float time = 3600f * 6;

    [SerializeField] float timeScale = 60f;

   [SerializeField] Text text;
   [SerializeField] UnityEngine.Rendering.Universal.Light2D globalLight;
   private int days;


    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    public void Subscribe(TimeAgent timeAgent)
    {
        agents.Add(timeAgent);
    }

    public void Unsubscribe(TimeAgent timeAgent)
    {
        agents.Remove(timeAgent);
    }
   
   float Hours
    {
         get
         {
              return time / 3600f;
         }
    }

    float Minutes
    {
        get
        {
            return time % 3600f / 60f;
        }
    }

   private void Update()
   {
       time += Time.deltaTime * timeScale;

       TimeValueCalculation();
       DayLight();

       float v = nightTimeCurve.Evaluate(Hours);
       Color c = Color.Lerp(dayLightColor, nightLightColor, v);
       globalLight.color = c;

       if (time > secondsInDay)
       {
           NextDay();
       }

       TimeAgents();
   }

   private void TimeValueCalculation()
   {
        int hh = (int)Hours;
        int mm = (int)Minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
   }

   private void DayLight()
   {
       float v = nightTimeCurve.Evaluate(Hours);
       Color c = Color.Lerp(dayLightColor, nightLightColor, v);
       globalLight.color = c;
   }

   int oldPhase = 0;

   private void TimeAgents()
   {
        int currentPhase = (int)(time / phaseLength);

        if (oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
   }

   private void NextDay()
   {
       time = 0;
       days += 1;
   }

}
