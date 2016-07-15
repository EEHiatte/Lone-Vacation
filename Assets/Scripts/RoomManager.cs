using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour 
{
    public GameObject player;
    public Texture Rarrow;
    public Texture Larrow;
    public Texture Exit;
    public GUIStyle buttonStyle;
    public GUIStyle textStyle;

    public List<AudioClip> audios;
    AudioSource audioSource;
    bool audioPlayed = true;

    public List<int> page;
    List<string> Rooms = new List<string>();
    List<bool> held = new List<bool>();

    bool textBox = false;

    bool mobile = false;
    float mobile_timer = 0;

    void Start()
    {
        Rooms.Add("\n1. Room 1.");
        Rooms.Add("1. Room 2.");
        Rooms.Add("\n2. Room 3.");
        Rooms.Add("1. Room 4.");
        Rooms.Add("\n2. Room 5.");
        Rooms.Add("1. Room 6.");
        Rooms.Add("\n2. Room 7.");
        Rooms.Add("1. Room 8.");
        Rooms.Add("\n2. Room 9.");

        held.Add(false);
        held.Add(false);
        held.Add(false);
        held.Add(false);

        audioSource = gameObject.AddComponent<AudioSource>();

        if (Application.isMobilePlatform)
        {
            mobile = true;
        }
    }

    void Update()
    {
        if (textBox)
            Time.timeScale = 0;
        if (textBox == false && GameManager.paused == false)
            Time.timeScale = 1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            held[0] = true;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            held[1] = true;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            held[2] = true;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            held[3] = true;

        if (mobile && Room_Controller.finished == true)
        {
            mobile_timer += Time.deltaTime;
        }
    }
    void OnGUI()
    {
        DoorColliding door = player.gameObject.GetComponent<DoorColliding>();

        Rect area_sz = new Rect(Screen.width / 3.5f, Screen.height / 1.6f, Screen.width / 2, Screen.height / 6);

        //GUI.TextArea(area_sz, "Test Text");
        //GUI.Button(new Rect(area_sz.xMax - 20, area_sz.yMax - 20, 20, 20), "Button");


        if (!mobile)
        {
            switch (door.currRoom)
            {
                case 0:
                    {
                        MainRoom menu = GameObject.Find("MainDoorRoom").GetComponent<MainRoom>();
                        switch (page[0])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Welcome to CannCorps main room here are some doors each one trains you to survive on a Cannibal infested island.", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                        page[0]++;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Would you like to know anything about the doors you have unlocked?" + '\n' + "1. Yes" + '\n' + "2. No", textStyle);
                                    if (held[0] == true)
                                        page[0]++;
                                    if (held[1] == true)
                                        page[0] = 20;
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0]--;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Go ahead and choose which room you would like to know about." + Rooms[0] + '\n' + "2. More Options", textStyle);
                                    if (held[0] == true)
                                        page[0] = 7;
                                    if (held[1] == true)
                                        page[0]++;
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0]--;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 3:
                                {
                                    textBox = true;

                                    if (menu.OpenDoors[1] == true && menu.OpenDoors[2] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[1] + "\n2. Back" + "\n3. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 8;
                                        if (held[1] == true)
                                            page[0]--;
                                        if (held[2] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[2] == true && menu.OpenDoors[3] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[1] + Rooms[2] + "\n3. Back" + "\n4. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 8;
                                        if (held[1] == true)
                                            page[0] = 9;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[3] == true)
                                    {
                                        GUI.TextArea(area_sz, Rooms[1] + Rooms[2] + "\n3. Back" + "\n4. More Options", textStyle);
                                        if (held[0] == true)
                                            page[0] = 8;
                                        if (held[1] == true)
                                            page[0] = 9;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0]++;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[1] == false)
                                    {
                                        GUI.TextArea(area_sz, "Need to unlock more doors\n 1. Back\n 2. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0]--;
                                        if (held[1] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                }
                                break;
                            case 4:
                                {
                                    textBox = true;

                                    if (menu.OpenDoors[3] == true && menu.OpenDoors[4] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[3] + "\n2. Back" + "\n3. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 10;
                                        if (held[1] == true)
                                            page[0]--;
                                        if (held[2] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[4] == true && menu.OpenDoors[5] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[3] + Rooms[4] + "\n3. Back" + "\n4. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 10;
                                        if (held[1] == true)
                                            page[0] = 11;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[5] == true)
                                    {
                                        GUI.TextArea(area_sz, Rooms[3] + Rooms[4] + "\n3. Back" + "\n4. More Options", textStyle);
                                        if (held[0] == true)
                                            page[0] = 10;
                                        if (held[1] == true)
                                            page[0] = 11;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0]++;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[3] == false)
                                    {
                                        GUI.TextArea(area_sz, "Need to unlock more doors\n 1. Back\n 2. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0]--;
                                        if (held[1] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                }
                                break;
                            case 5:
                                {
                                    textBox = true;

                                    if (menu.OpenDoors[5] == true && menu.OpenDoors[6] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[5] + "\n2. Back" + "\n3. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 12;
                                        if (held[1] == true)
                                            page[0]--;
                                        if (held[2] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[6] == true && menu.OpenDoors[7] == false)
                                    {
                                        GUI.TextArea(area_sz, Rooms[5] + Rooms[6] + "\n3. Back" + "\n4. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 12;
                                        if (held[1] == true)
                                            page[0] = 13;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[7] == true)
                                    {
                                        GUI.TextArea(area_sz, Rooms[5] + Rooms[6] + "\n3. Back" + "\n4. More Options", textStyle);
                                        if (held[0] == true)
                                            page[0] = 12;
                                        if (held[1] == true)
                                            page[0] = 13;
                                        if (held[2] == true)
                                            page[0]--;
                                        if (held[3] == true)
                                            page[0]++;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (menu.OpenDoors[6] == false)
                                    {
                                        GUI.TextArea(area_sz, "Need to unlock more doors\n 1. Back\n 2. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0]--;
                                        if (held[1] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                }
                                break;
                            case 6:
                                {
                                    textBox = true;

                                    if (menu.OpenDoors[7] == true)
                                    {
                                        GUI.TextArea(area_sz, Rooms[7] + "\n2. Back" + "\n3. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0] = 14;
                                        if (held[1] == true)
                                            page[0]--;
                                        if (held[2] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    //if (menu.OpenDoors[8] == true)
                                    //{
                                    //    GUI.TextArea(area_sz, Rooms[7] + Rooms[8] + "\n3. Back" + "\n4. Exit", textStyle);
                                    //    if (held[0] == true)
                                    //        page[0] = 14;
                                    //    if (held[1] == true)
                                    //        page[0] = 15;
                                    //    if (held[2] == true)
                                    //        page[0]--;
                                    //    if (held[3] == true)
                                    //        page[0] = 20;
                                    //    for (int i = 0; i < 4; i++)
                                    //        held[i] = false;
                                    //}
                                    if (menu.OpenDoors[7] == false)
                                    {
                                        GUI.TextArea(area_sz, "Need to unlock more doors\n 1. Back\n 2. Exit", textStyle);
                                        if (held[0] == true)
                                            page[0]--;
                                        if (held[1] == true)
                                            page[0] = 20;
                                        for (int i = 0; i < 4; i++)
                                            held[i] = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                }
                                break;
                            case 7:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 1 is movement, this is important overall because can't do anything if you dont move DUH.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 2;
                                    if (held[0] == true)
                                        page[0] = 2;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 8:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 2 teaches you how to collect items off the ground you needs these for an asortment of things like trap building.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 3;
                                    if (held[0] == true)
                                        page[0] = 3;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 9:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 3 shows how to maintain a fire, fire is important to be rescue because it's very noticeable from sea.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 3;
                                    if (held[0] == true)
                                        page[0] = 3;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 10:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 4 has a roleplayer in there that is an enemy, this will show you how they move.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 4;
                                    if (held[0] == true)
                                        page[0] = 4;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 11:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 5 teaches you how to build most of the traps these will stop some of the enemies but not all.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 4;
                                    if (held[0] == true)
                                        page[0] = 4;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 12:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 6 shows you things that you can hide in and also places where you can get a drink and eat which is key.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 5;
                                    if (held[0] == true)
                                        page[0] = 5;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 13:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 7 gives you a little bit of a feel on what weather is going to be out there.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 5;
                                    if (held[0] == true)
                                        page[0] = 5;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 14:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 8 shows you different terrians that may be encounter out in the wild.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 6;
                                    if (held[0] == true)
                                        page[0] = 6;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            case 15:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Room 9 is a huge room for you just to implement what you have learned.\n 1. Back", textStyle);
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                        page[0] = 6;
                                    if (held[0] == true)
                                        page[0] = 6;
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                        page[0] = 20;
                                    for (int i = 0; i < 4; i++)
                                        held[i] = false;
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (page[1])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test You will have to move to each button and press them in order to unlock the door.", textStyle);
                                    audioSource.clip = audios[0];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[1]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[1] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }

                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To move use the W,A,S,D" + '\n' + "W = Up, S = Down, A = Left, D = Right.", textStyle);
                                    audioSource.clip = audios[1];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[1]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[1]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[1] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To sprint use the spacebar and move normally." + '\n' + "Your speed will double while sprinting.", textStyle);
                                    audioSource.clip = audios[2];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[1]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[1]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[1] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 2:
                    {
                        switch (page[2])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test You will have to pickup each item to unlock the door.", textStyle);
                                    audioSource.clip = audios[3];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[2]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[2] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To collect the items just simply walk up to the item and press F or Left click.", textStyle);
                                    audioSource.clip = audios[4];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[2]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[2]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[2] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "These items will be moved to your inventory which is off to the left of the screen.", textStyle);
                                    audioSource.clip = audios[5];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[2]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[2]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[2] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 3:
                    {
                        switch (page[3])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test You will have feed the fire with all 20 sticks to unlock the door.", textStyle);
                                    audioSource.clip = audios[6];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[3]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[3] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To feed the fire simply walk up to it and press F or Left Click on it.", textStyle);
                                    audioSource.clip = audios[7];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[3]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[3]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[3] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "You will see your bar go up which is located on the right hand side.", textStyle);
                                    audioSource.clip = audios[8];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[3]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[3]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[3] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "The compass at the bottom right will always point towards the fire.", textStyle);
                                    audioSource.clip = audios[9];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[3]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[3]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[3] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 4:
                    {
                        switch (page[4])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test all you have to do is evade the enemy to get to the door and escape!", textStyle);
                                    audioSource.clip = audios[10];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[4]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[4] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "If the the enemy hits you, then you will lose a little bit of health which can be found in the upper left.", textStyle);
                                    audioSource.clip = audios[11];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[4]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[4]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[4] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Enemies will chase you till you lose them.", textStyle);
                                    audioSource.clip = audios[12];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[4]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    } if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[4]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[4] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 5:
                    {
                        switch (page[5])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test you will have to build each type of trap to unlock the door.", textStyle);
                                    audioSource.clip = audios[13];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[5]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[5] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To place a trap just press the number that it says in the build area to build the trap.", textStyle);
                                    audioSource.clip = audios[14];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[5]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[5]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[5] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Your traps are at the bottom of the screen and the materials to make them are shown.", textStyle);
                                    audioSource.clip = audios[15];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[5]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[5]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[5] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 6:
                    {
                        switch (page[6])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test you will have to take a drink and eat to unlock the door.", textStyle);
                                    audioSource.clip = audios[16];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[6]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[6] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To drink just walk up to the pond and press F or Left Click.", textStyle);
                                    audioSource.clip = audios[17];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[6]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[6]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[6] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To eat just walk up to the banana tree, berry bush, or bug rock and press F or Left Click.", textStyle);
                                    audioSource.clip = audios[18];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[6]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[6]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[6] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "To hide just walk up to any tree, rock, or bush and Press E.", textStyle);
                                    audioSource.clip = audios[19];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[6]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[6]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[6] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 7:
                    {
                        switch (page[7])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this Test you will have to experience all 3 types of weather to unlock the door.", textStyle);
                                    audioSource.clip = audios[20];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[7]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[7] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Cold Weather will make it so your thrist will go down slower as well as your stamina.", textStyle);
                                    audioSource.clip = audios[21];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[7]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[7]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[7] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Cold Weather will also make your fire go down faster and regen slower.", textStyle);
                                    audioSource.clip = audios[22];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[7]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[7]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[7] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Hot Weather will make you thristy faster and use more stamina but will make it so your fire dies out slower.", textStyle);
                                    audioSource.clip = audios[23];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[7]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[7]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[7] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 4:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Rain will make it so you get thirsty slower, but your fire will die out faster while in the rain. It will also cut the enemies vision down.", textStyle);
                                    audioSource.clip = audios[24];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[7]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[7]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[7] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 8:
                    {
                        switch (page[8])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this test, you will have to experience all 3 types of terrian to unlock the door.", textStyle);
                                    audioSource.clip = audios[25];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[8]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[8] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "The beach will decrease your stamina a little faster.", textStyle);
                                    audioSource.clip = audios[26];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[8]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[8]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[8] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 2:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "The marsh will make you move slower walking and sprinting.", textStyle);
                                    audioSource.clip = audios[27];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[8]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[8]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[8] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 3:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "Snow will make your stamina decrease even faster than the beach.", textStyle);
                                    audioSource.clip = audios[28];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[8]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[8]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[8] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
                case 9:
                    {
                        switch (page[9])
                        {
                            case 0:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "For this room you will have free roam to test out a simulation just have fun with it.", textStyle);
                                    audioSource.clip = audios[29];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[9]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[9] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            case 1:
                                {
                                    textBox = true;
                                    GUI.TextArea(area_sz, "You can leave this room anytime you want.", textStyle);
                                    audioSource.clip = audios[30];
                                    audioSource.volume = 1.0f;
                                    if (audioPlayed)
                                    {
                                        audioSource.Play();
                                        audioPlayed = false;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 10, area_sz.yMax - 10, 20, 20), Rarrow, buttonStyle))
                                    {
                                        page[9]++;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMin - 10, area_sz.yMax - 10, 20, 20), Larrow, buttonStyle))
                                    {
                                        page[9]--;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                    if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
                                    {
                                        page[9] = 10;
                                        audioSource.Stop();
                                        audioPlayed = true;
                                    }
                                }
                                break;
                            default: textBox = false; break;
                        }
                    }
                    break;
            }
        }

        if (Room_Controller.finished == true && !mobile)
        {
            textBox = true;
            GUI.TextArea(area_sz, "You have finished the tutorial level.", textStyle);

            if (GUI.Button(new Rect(area_sz.xMax - 12, area_sz.yMin - 12, 20, 20), Exit, buttonStyle))
            {
                Room_Controller.finished = false;
                textBox = false;
            }
        }

        if (Room_Controller.finished == true && mobile)
        {
            GUI.TextArea(area_sz, "You have finished the tutorial level.", textStyle);

            if (mobile_timer >= 6)
            {
                Room_Controller.finished = false;
            }
        }
    }
}
