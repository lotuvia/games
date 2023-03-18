using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTimer : MonoBehaviour
{
    public float MaxTime;
    public bool Tick;
    public GameObject guardianTimeline;
    public GameObject workmanTimeline;
    public GameObject guardianImage;
    public GameObject workmanImage;

    private Image img;
    private float currentTime;

    void Start()
    {
        img = GetComponent<Image>();
        currentTime = MaxTime;
    }

    void Update()
    {
        Tick = false;
        currentTime += Time.deltaTime;

        if ( currentTime >= MaxTime)
        {
            Tick=true;
            currentTime = 0;  
        }
        img.fillAmount= currentTime/MaxTime;

        if (guardianImage.active)
            guardianTimeline.SetActive(true);
        if (!guardianImage.active)
            guardianTimeline.SetActive(false);

        if (workmanImage.active)
            workmanTimeline.SetActive(true);
        if (!workmanImage.active)
            workmanTimeline.SetActive(false);
    }

}
