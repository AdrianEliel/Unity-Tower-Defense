using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;
    private Resolution[] resolutions;
    private int currentResolutionIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height + " " + (int)resolutions[i].refreshRateRatio.value + "Hz";
            options.Add(resolutionOption);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetResolution(int num)
    {
        Resolution resolution = resolutions[num];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }
}
