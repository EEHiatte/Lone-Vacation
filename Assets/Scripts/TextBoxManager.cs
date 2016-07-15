using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextBoxManager : MonoBehaviour 
{
    public Texture Rarrow;
    public Texture Exit;
    public GUIStyle buttonStyle;
    public GUIStyle textStyle;

    public bool textBox = true;
    public bool start = true;

    public int page;

    void Update()
    {
        if (textBox)
            Time.timeScale = 0;
        else if (textBox == false && GameManager.paused == false)
        {
            Time.timeScale = 1;
        }
    }

    void OnGUI()
    {
        Rect area_sz = new Rect(Screen.width / 3.5f, Screen.height / 1.6f, Screen.width / 2, Screen.height / 6);
        if (start)
        {
            switch (page)
            {
                case 0:
                    {
                        textBox = true;
                        GUI.TextArea(area_sz, "What happen? \n Am I shipwrecked?", textStyle);
                        GUI.TextArea(new Rect(area_sz.xMin, area_sz.yMin - 20, 60, 20), "Derrick", textStyle);
                        if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                            page++;
                        if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                            page = 20;
                    }
                    break;
                case 1:
                    {
                        textBox = true;
                        GUI.TextArea(area_sz, "Theres a fire here! It looks weak, I should probably feed it with sticks.", textStyle);
                        GUI.TextArea(new Rect(area_sz.xMin, area_sz.yMin - 20, 60, 20), "Derrick", textStyle);
                        if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                            page++;
                        if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                            page = 20;
                    }
                    break;
                case 2:
                    {
                        textBox = true;
                        GUI.TextArea(area_sz, "I bet, the more I build it up, the higher the chance of rescue!", textStyle);
                        GUI.TextArea(new Rect(area_sz.xMin, area_sz.yMin - 20, 60, 20), "Derrick", textStyle);
                        if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                            page++;
                        if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                            page = 20;
                    }
                    break;
                case 3:
                    {
                        textBox = true;
                        GUI.TextArea(area_sz, "Time to go explore the island and find some resources.", textStyle);
                        GUI.TextArea(new Rect(area_sz.xMin, area_sz.yMin - 20, 60, 20), "Derrick", textStyle);
                        if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                            page++;
                        if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                            page = 20;
                    }
                    break;
                default: { start = false; textBox = false; } break;
            }
        }
    }
}
