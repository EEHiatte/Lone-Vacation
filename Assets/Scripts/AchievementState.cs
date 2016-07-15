using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementState : MonoBehaviour
{
    Color Black;
    Color Greyedout;
    // Use this for initialization
    void Start()
    {
        Black = new Color(0 / 255f, 0 / 255f, 0 / 255f);
        Greyedout = new Color(136f / 255f, 136f / 255f, 136f / 255f);
        PlayerPrefs.Save();
        // use this to reset player prefs and achievements. PlayerPrefs.DeleteAll();
        if (Application.loadedLevel == 0)
        {
            /////////fire
            if (PlayerPrefs.GetInt("Fire 1") == 1)
                GameObject.Find("Fire 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 2") == 1)
                GameObject.Find("Fire 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 3") == 1)
                GameObject.Find("Fire 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 4") == 1)
                GameObject.Find("Fire 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 4").GetComponent<Text>().color = Black;
            ///////////////////////


            ////////////survival 1
            if (PlayerPrefs.GetInt("Survival 1") == 1)
                GameObject.Find("Survival 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 1").GetComponent<Text>().color = Black;
            ///2
            if (PlayerPrefs.GetInt("Survival 2") == 1)
                GameObject.Find("Survival 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 2").GetComponent<Text>().color = Black;
            ///3
            if (PlayerPrefs.GetInt("Survival 3") == 1)
                GameObject.Find("Survival 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 3").GetComponent<Text>().color = Black;
            ///4
            if (PlayerPrefs.GetInt("Survival 4") == 1)
                GameObject.Find("Survival 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 4").GetComponent<Text>().color = Black;
            ///5
            if (PlayerPrefs.GetInt("Survival 5") == 1)
                GameObject.Find("Survival 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 5").GetComponent<Text>().color = Black;
            ///////////


            ////////////Death 1
            if (PlayerPrefs.GetInt("Death 1") == 1)
                GameObject.Find("Death 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 1").GetComponent<Text>().color = Black;
            ///2
            if (PlayerPrefs.GetInt("Death 2") == 1)
                GameObject.Find("Death 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 2").GetComponent<Text>().color = Black;
            ///3
            if (PlayerPrefs.GetInt("Death 3") == 1)
                GameObject.Find("Death 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 3").GetComponent<Text>().color = Black;
            ///4
            if (PlayerPrefs.GetInt("Death 4") == 1)
                GameObject.Find("Death 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 4").GetComponent<Text>().color = Black;
            ///5
            if (PlayerPrefs.GetInt("Death 5") == 1)
                GameObject.Find("Death 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 5").GetComponent<Text>().color = Black;
            ///6
            if (PlayerPrefs.GetInt("Death 6") == 1)
                GameObject.Find("Death 6").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 6").GetComponent<Text>().color = Black;
            ///7
            if (PlayerPrefs.GetInt("Death 7") == 1)
                GameObject.Find("Death 7").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 7").GetComponent<Text>().color = Black;
            ////////////


            /////////Trapping
            if (PlayerPrefs.GetInt("Spearmantrap5") == 1 && PlayerPrefs.GetInt("Spearmantrap4") == 1 && PlayerPrefs.GetInt("Spearmantrap3") == 1 && PlayerPrefs.GetInt("Spearmantrap2") == 1 && PlayerPrefs.GetInt("Spearmantrap1") == 1 && PlayerPrefs.GetInt("Spearmantrap0") == 1)
                GameObject.Find("Trapping 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Archertrap5") == 1 && PlayerPrefs.GetInt("Archertrap4") == 1 && PlayerPrefs.GetInt("Archertrap3") == 1 && PlayerPrefs.GetInt("Archertrap2") == 1 && PlayerPrefs.GetInt("Archertrap1") == 1 && PlayerPrefs.GetInt("Archertrap0") == 1)
                GameObject.Find("Trapping 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Scouttrap5") == 1 && PlayerPrefs.GetInt("Scouttrap4") == 1 && PlayerPrefs.GetInt("Scouttrap3") == 1 && PlayerPrefs.GetInt("Scouttrap2") == 1 && PlayerPrefs.GetInt("Scouttrap1") == 1 && PlayerPrefs.GetInt("Scouttrap0") == 1)
                GameObject.Find("Trapping 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Wolftrap5") == 1 && PlayerPrefs.GetInt("Wolftrap4") == 1 && PlayerPrefs.GetInt("Wolftrap3") == 1 && PlayerPrefs.GetInt("Wolftrap2") == 1 && PlayerPrefs.GetInt("Wolftrap1") == 1 && PlayerPrefs.GetInt("Wolftrap0") == 1)
                GameObject.Find("Trapping 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 4").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Boartrap5") == 1 && PlayerPrefs.GetInt("Boartrap4") == 1 && PlayerPrefs.GetInt("Boartrap3") == 1 && PlayerPrefs.GetInt("Boartrap2") == 1 && PlayerPrefs.GetInt("Boartrap1") == 1 && PlayerPrefs.GetInt("Boartrap0") == 1)
                GameObject.Find("Trapping 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 5").GetComponent<Text>().color = Black;
            //////////


            ///////disabling
            if (PlayerPrefs.GetInt("SpearmanDisabled") == 1)
                GameObject.Find("Disabling 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("ArcherDisabled") == 1)
                GameObject.Find("Disabling 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("ScoutDisabled") == 1)
                GameObject.Find("Disabling 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("WolfDisabled") == 1)
                GameObject.Find("Disabling 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 4").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("BoarDisabled") == 1)
                GameObject.Find("Disabling 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 5").GetComponent<Text>().color = Black;
            ///

            //////////// HIDING
            if (PlayerPrefs.GetInt("Hide 1") == 1)
                GameObject.Find("Hiding 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 1").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("Hide 2") == 1)
                GameObject.Find("Hiding 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 2").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Hide 3") == 1)
                GameObject.Find("Hiding 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 3").GetComponent<Text>().color = Black;
            ///////////////

            //////////// Collectibles
            if (PlayerPrefs.GetInt("TikiStatue") == 1)
                GameObject.Find("Collecting 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 1").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("TikiTorch") == 1)
                GameObject.Find("Collecting 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 2").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Stonehead") == 1)
                GameObject.Find("Collecting 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 3").GetComponent<Text>().color = Black;
            ////////
            if (PlayerPrefs.GetInt("Cthulhu") == 1)
                GameObject.Find("Collecting 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 4").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("IslandBar") == 1)
                GameObject.Find("Collecting 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 5").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Surfboard") == 1)
                GameObject.Find("Collecting 6").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 6").GetComponent<Text>().color = Black;
            ///////////////

            /////Tutorial achievement
            if (PlayerPrefs.GetInt("TutorialFinished") == 1)
                GameObject.Find("Tutorial 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Tutorial 1").GetComponent<Text>().color = Black;
            //////////////
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevel == 1 || Application.loadedLevel == 2 || Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            /////////fire
            if (PlayerPrefs.GetInt("Fire 1") == 1)
                GameObject.Find("Fire 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 2") == 1)
                GameObject.Find("Fire 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 3") == 1)
                GameObject.Find("Fire 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Fire 4") == 1)
                GameObject.Find("Fire 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Fire 4").GetComponent<Text>().color = Black;
            ///////////////////////


            ////////////survival 1
            if (PlayerPrefs.GetInt("Survival 1") == 1)
                GameObject.Find("Survival 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 1").GetComponent<Text>().color = Black;
            ///2
            if (PlayerPrefs.GetInt("Survival 2") == 1)
                GameObject.Find("Survival 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 2").GetComponent<Text>().color = Black;
            ///3
            if (PlayerPrefs.GetInt("Survival 3") == 1)
                GameObject.Find("Survival 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 3").GetComponent<Text>().color = Black;
            ///4
            if (PlayerPrefs.GetInt("Survival 4") == 1)
                GameObject.Find("Survival 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 4").GetComponent<Text>().color = Black;
            ///5
            if (PlayerPrefs.GetInt("Survival 5") == 1)
                GameObject.Find("Survival 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Survival 5").GetComponent<Text>().color = Black;
            ///////////


            ////////////Death 1
            if (PlayerPrefs.GetInt("Death 1") == 1)
                GameObject.Find("Death 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 1").GetComponent<Text>().color = Black;
            ///2
            if (PlayerPrefs.GetInt("Death 2") == 1)
                GameObject.Find("Death 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 2").GetComponent<Text>().color = Black;
            ///3
            if (PlayerPrefs.GetInt("Death 3") == 1)
                GameObject.Find("Death 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 3").GetComponent<Text>().color = Black;
            ///4
            if (PlayerPrefs.GetInt("Death 4") == 1)
                GameObject.Find("Death 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 4").GetComponent<Text>().color = Black;
            ///5
            if (PlayerPrefs.GetInt("Death 5") == 1)
                GameObject.Find("Death 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 5").GetComponent<Text>().color = Black;
            ///6
            if (PlayerPrefs.GetInt("Death 6") == 1)
                GameObject.Find("Death 6").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 6").GetComponent<Text>().color = Black;
            ///7
            if (PlayerPrefs.GetInt("Death 7") == 1)
                GameObject.Find("Death 7").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Death 7").GetComponent<Text>().color = Black;
            ////////////


            /////////Trapping
            if (PlayerPrefs.GetInt("Spearmantrap5") == 1 && PlayerPrefs.GetInt("Spearmantrap4") == 1 && PlayerPrefs.GetInt("Spearmantrap3") == 1 && PlayerPrefs.GetInt("Spearmantrap2") == 1 && PlayerPrefs.GetInt("Spearmantrap1") == 1 && PlayerPrefs.GetInt("Spearmantrap0") == 1)
                GameObject.Find("Trapping 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Archertrap5") == 1 && PlayerPrefs.GetInt("Archertrap4") == 1 && PlayerPrefs.GetInt("Archertrap3") == 1 && PlayerPrefs.GetInt("Archertrap2") == 1 && PlayerPrefs.GetInt("Archertrap1") == 1 && PlayerPrefs.GetInt("Archertrap0") == 1)
                GameObject.Find("Trapping 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Scouttrap5") == 1 && PlayerPrefs.GetInt("Scouttrap4") == 1 && PlayerPrefs.GetInt("Scouttrap3") == 1 && PlayerPrefs.GetInt("Scouttrap2") == 1 && PlayerPrefs.GetInt("Scouttrap1") == 1 && PlayerPrefs.GetInt("Scouttrap0") == 1)
                GameObject.Find("Trapping 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Wolftrap5") == 1 && PlayerPrefs.GetInt("Wolftrap4") == 1 && PlayerPrefs.GetInt("Wolftrap3") == 1 && PlayerPrefs.GetInt("Wolftrap2") == 1 && PlayerPrefs.GetInt("Wolftrap1") == 1 && PlayerPrefs.GetInt("Wolftrap0") == 1)
                GameObject.Find("Trapping 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 4").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("Boartrap5") == 1 && PlayerPrefs.GetInt("Boartrap4") == 1 && PlayerPrefs.GetInt("Boartrap3") == 1 && PlayerPrefs.GetInt("Boartrap2") == 1 && PlayerPrefs.GetInt("Boartrap1") == 1 && PlayerPrefs.GetInt("Boartrap0") == 1)
                GameObject.Find("Trapping 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Trapping 5").GetComponent<Text>().color = Black;
            //////////


            ///////disabling
            if (PlayerPrefs.GetInt("SpearmanDisabled") == 1)
                GameObject.Find("Disabling 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 1").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("ArcherDisabled") == 1)
                GameObject.Find("Disabling 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 2").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("ScoutDisabled") == 1)
                GameObject.Find("Disabling 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 3").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("WolfDisabled") == 1)
                GameObject.Find("Disabling 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 4").GetComponent<Text>().color = Black;
            ///
            if (PlayerPrefs.GetInt("BoarDisabled") == 1)
                GameObject.Find("Disabling 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Disabling 5").GetComponent<Text>().color = Black;
            ///

            //////////// HIDING
            if (PlayerPrefs.GetInt("Hide 1") == 1)
                GameObject.Find("Hiding 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 1").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("Hide 2") == 1)
                GameObject.Find("Hiding 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 2").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Hide 3") == 1)
                GameObject.Find("Hiding 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Hiding 3").GetComponent<Text>().color = Black;
            ///////////////

            //////////// Collectibles
            if (PlayerPrefs.GetInt("TikiStatue") == 1)
                GameObject.Find("Collecting 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 1").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("TikiTorch") == 1)
                GameObject.Find("Collecting 2").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 2").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Stonehead") == 1)
                GameObject.Find("Collecting 3").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 3").GetComponent<Text>().color = Black;
            ////////
            if (PlayerPrefs.GetInt("Cthulhu") == 1)
                GameObject.Find("Collecting 4").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 4").GetComponent<Text>().color = Black;
            ///////////
            if (PlayerPrefs.GetInt("IslandBar") == 1)
                GameObject.Find("Collecting 5").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 5").GetComponent<Text>().color = Black;
            /////////
            if (PlayerPrefs.GetInt("Surfboard") == 1)
                GameObject.Find("Collecting 6").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Collecting 6").GetComponent<Text>().color = Black;
            ///////////////

            /////Tutorial achievement
            if (PlayerPrefs.GetInt("TutorialFinished") == 1)
                GameObject.Find("Tutorial 1").GetComponent<Text>().color = Greyedout;
            else
                GameObject.Find("Tutorial 1").GetComponent<Text>().color = Black;
            //////////////
        }
    }
}
