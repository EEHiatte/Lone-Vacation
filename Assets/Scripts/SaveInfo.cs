using UnityEngine;
using System.Collections;
using System.Text;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using System;
using System.Runtime.Serialization;
using System.Reflection;

public class SaveInfo : MonoBehaviour
{
    public GameObject player;

    public GameObject[] stockpiles;

    public GameObject[] cthulhuStatuePieces;
    public GameObject[] tikiStatuePieces;
    public GameObject[] tikiTorchPieces;
    public GameObject[] resortBarPieces;
    public GameObject[] surfboardPieces;
    public GameObject[] stoneheadPieces;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!Application.isMobilePlatform)
        {
            if (!File.Exists(Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat"))
            {
                Directory.CreateDirectory(Environment.CurrentDirectory + @"\SaveFiles");
                File.Create(Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat");
            }
        }
        else
        {
            if (!File.Exists(Application.persistentDataPath + "/Playerinfo.dat"))
            {
                File.Create(Application.persistentDataPath + "/Playerinfo.dat");
            }
        }

        if (Application.loadedLevel == (int)GameManager.LevelID.DefaultIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.CthulhuIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.VolcanoIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.LizardIsland)
        {
            if (Application.loadedLevel == (int)GameManager.LevelID.DefaultIsland)
                stockpiles = GameObject.FindGameObjectsWithTag("Stockpile");

            cthulhuStatuePieces = GameObject.FindGameObjectsWithTag("CthulhuStatue");

            tikiStatuePieces = GameObject.FindGameObjectsWithTag("TikiStatue");

            tikiTorchPieces = GameObject.FindGameObjectsWithTag("TikiTorch");

            surfboardPieces = GameObject.FindGameObjectsWithTag("Surfboard");

            stoneheadPieces = GameObject.FindGameObjectsWithTag("Stonehead");

            resortBarPieces = GameObject.FindGameObjectsWithTag("IslandBar");
        }
    }
    public void SaveAllInformation()
    {
        InventoryManager inven = player.GetComponent<InventoryManager>();
        PlayerStatManager stats = player.GetComponent<PlayerStatManager>();
        TextBoxManager startBox = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>();

        PlayerPrefs.SetInt("TWIGS", inven.numTwigs);
        PlayerPrefs.SetInt("LEAVES", inven.numLeaves);
        PlayerPrefs.SetInt("ROCKS", inven.numRocks);
        PlayerPrefs.SetInt("ROPE", inven.numRope);
        PlayerPrefs.SetInt("THORNS", inven.numThorns);
        PlayerPrefs.SetFloat("PLAYERHEALTH", stats.playerHealth);
        PlayerPrefs.SetFloat("PLAYERHUNGER", stats.playerHunger);
        PlayerPrefs.SetFloat("PLAYERTHRIST", stats.playerThirst);
        PlayerPrefs.SetInt("HAVECOMPASS", (inven.haveCompass ? 1 : 0));
        PlayerPrefs.SetInt("HAVECANTEEN", (inven.haveCanteen ? 1 : 0));
        PlayerPrefs.SetInt("HAVEBACKPACK", (inven.haveBackpack ? 1 : 0));
        PlayerPrefs.SetInt("HAVEBOOTS", (inven.haveBoots ? 1 : 0));
        PlayerPrefs.SetInt("HAVELANTERN", (inven.haveLantern ? 1 : 0));
        PlayerPrefs.SetInt("HAVEGLOVES", (inven.haveGloves ? 1 : 0));
        PlayerPrefs.SetInt("SAVEDINFO", 1);
        PlayerPrefs.SetInt("STARTBOX", (startBox.start ? 1 : 0));
        PlayerPrefs.SetInt("TEXTBOX", (startBox.textBox ? 1 : 0));

        Debug.Log("Saved All Information");
    }

    public void Save()
    {
        //if (Application.loadedLevelName != "TutorialLevel")
        ///////////
        //BUG FIX (WILL)
        //BUG #28/29
        if (Application.loadedLevel == (int)GameManager.LevelID.CthulhuIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.LizardIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.DefaultIsland ||
            Application.loadedLevel == (int)GameManager.LevelID.VolcanoIsland)
        //END BUG FIX
        ///////////
        {
            InventoryManager inven = player.GetComponent<InventoryManager>();
            PlayerStatManager stats = player.GetComponent<PlayerStatManager>();
            CoveDecorations dec = GameObject.Find("GameManager").GetComponent<CoveDecorations>();

            BinaryFormatter sf = new BinaryFormatter();

            FileStream file;
            if (!Application.isMobilePlatform)
            {
                file = File.Open(Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat", FileMode.Open);
            }
            else
            {
                file = File.Open(Application.persistentDataPath + "/Playerinfo.dat", FileMode.Open);
            }

            PlayerSaving saves = new PlayerSaving();

            //saves.island = GameObject.Find("MainMenu").GetComponent<MainMenu>().GetLevelIndex();

            saves.StartBox = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().start;
            saves.textBox = GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().textBox;

            saves.diff = PlayerPrefs.GetInt("Difficulty");

            saves.twigs = inven.numTwigs;
            saves.leaves = inven.numLeaves;
            saves.rocks = inven.numRocks;
            saves.rope = inven.numRope;
            saves.thorns = inven.numThorns;

            saves.fireInten = GameObject.Find("Fire Pit").GetComponent<FireBehavior>().fireIntensity;

            saves.health = stats.playerHealth;
            saves.hunger = stats.playerHunger;
            saves.thrist = stats.playerThirst;

            saves.x = player.transform.position.x;
            saves.y = player.transform.position.y;
            saves.z = player.transform.position.z;

            saves.compass = inven.haveCompass;
            saves.canteen = inven.haveCanteen;
            saves.backpack = inven.haveBackpack;
            saves.lantern = inven.haveLantern;
            saves.gloves = inven.haveGloves;

            saves.fire1 = PlayerPrefs.GetInt("Fire 1");
            saves.fire2 = PlayerPrefs.GetInt("Fire 2");
            saves.fire3 = PlayerPrefs.GetInt("Fire 3");
            saves.fire4 = PlayerPrefs.GetInt("Fire 4");

            saves.survival1 = PlayerPrefs.GetInt("Survival 1");
            saves.survival2 = PlayerPrefs.GetInt("Survival 2");
            saves.survival3 = PlayerPrefs.GetInt("Survival 3");
            saves.survival4 = PlayerPrefs.GetInt("Survival 4");
            saves.survival5 = PlayerPrefs.GetInt("Survival 5");

            saves.death1 = PlayerPrefs.GetInt("Death 1");
            saves.death2 = PlayerPrefs.GetInt("Death 2");
            saves.death3 = PlayerPrefs.GetInt("Death 3");
            saves.death4 = PlayerPrefs.GetInt("Death 4");
            saves.death5 = PlayerPrefs.GetInt("Death 5");
            saves.death6 = PlayerPrefs.GetInt("Death 6");
            saves.death7 = PlayerPrefs.GetInt("Death 7");

            saves.spearmantrap0 = PlayerPrefs.GetInt("Spearmantrap0");
            saves.spearmantrap1 = PlayerPrefs.GetInt("Spearmantrap1");
            saves.spearmantrap2 = PlayerPrefs.GetInt("Spearmantrap2");
            saves.spearmantrap3 = PlayerPrefs.GetInt("Spearmantrap3");
            saves.spearmantrap4 = PlayerPrefs.GetInt("Spearmantrap4");
            saves.spearmantrap5 = PlayerPrefs.GetInt("Spearmantrap5");

            saves.archertrap0 = PlayerPrefs.GetInt("Archertrap0");
            saves.archertrap1 = PlayerPrefs.GetInt("Archertrap1");
            saves.archertrap2 = PlayerPrefs.GetInt("Archertrap2");
            saves.archertrap3 = PlayerPrefs.GetInt("Archertrap3");
            saves.archertrap4 = PlayerPrefs.GetInt("Archertrap4");
            saves.archertrap5 = PlayerPrefs.GetInt("Archertrap5");

            saves.scouttrap0 = PlayerPrefs.GetInt("Scouttrap0");
            saves.scouttrap1 = PlayerPrefs.GetInt("Scouttrap1");
            saves.scouttrap2 = PlayerPrefs.GetInt("Scouttrap2");
            saves.scouttrap3 = PlayerPrefs.GetInt("Scouttrap3");
            saves.scouttrap4 = PlayerPrefs.GetInt("Scouttrap4");
            saves.scouttrap5 = PlayerPrefs.GetInt("Scouttrap5");

            saves.wolftrap0 = PlayerPrefs.GetInt("Wolftrap0");
            saves.wolftrap1 = PlayerPrefs.GetInt("Wolftrap1");
            saves.wolftrap2 = PlayerPrefs.GetInt("Wolftrap2");
            saves.wolftrap3 = PlayerPrefs.GetInt("Wolftrap3");
            saves.wolftrap4 = PlayerPrefs.GetInt("Wolftrap4");
            saves.wolftrap5 = PlayerPrefs.GetInt("Wolftrap5");

            saves.boartrap0 = PlayerPrefs.GetInt("Boartrap0");
            saves.boartrap1 = PlayerPrefs.GetInt("Boartrap1");
            saves.boartrap2 = PlayerPrefs.GetInt("Boartrap2");
            saves.boartrap3 = PlayerPrefs.GetInt("Boartrap3");
            saves.boartrap4 = PlayerPrefs.GetInt("Boartrap4");
            saves.boartrap5 = PlayerPrefs.GetInt("Boartrap5");

            saves.spearmandisable = PlayerPrefs.GetInt("SpearmanDisabled");
            saves.archerdisable = PlayerPrefs.GetInt("ArcherDisabled");
            saves.scoutdisable = PlayerPrefs.GetInt("ScoutDisabled");
            saves.wolfdisable = PlayerPrefs.GetInt("WolfDisabled");
            saves.boardisable = PlayerPrefs.GetInt("BoarDisabled");

            saves.hide1 = PlayerPrefs.GetInt("Hide 1");
            saves.hide2 = PlayerPrefs.GetInt("Hide 2");
            saves.hide3 = PlayerPrefs.GetInt("Hide 3");

            saves.collect1 = PlayerPrefs.GetInt("TikiStatue");
            saves.collect2 = PlayerPrefs.GetInt("TikiTorch");
            saves.collect3 = PlayerPrefs.GetInt("Stonehead");
            saves.collect4 = PlayerPrefs.GetInt("Cthulhu");
            saves.collect5 = PlayerPrefs.GetInt("IslandBar");
            saves.collect6 = PlayerPrefs.GetInt("Surfboard");

            saves.tutorial = PlayerPrefs.GetInt("TutorialFinished");

            saves.tikitorch = dec.tikiTorchToCollect;
            saves.tikistatue = dec.tikiStatueToCollect;
            saves.cthulu = dec.cthulhuStatueToCollect;
            saves.stonehead = dec.stoneheadToCollect;
            saves.islandbar = dec.islandBarToCollect;
            saves.surfboard = dec.surfboardToCollect;

            ///////////
            //BUG FIX (WILL)
            //BUG #28/29
            saves.island = Application.loadedLevel;
            //END BUG FIX
            /////////////

            ///////////
            //BUG FIX (WILL)
            //BUG #66/69
            saves.cthulhuStatuePieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (cthulhuStatuePieces[i] != null)
                    saves.cthulhuStatuePieces[i] = 1;
                else
                    saves.cthulhuStatuePieces[i] = 0;
            }

            saves.tikiStatuePieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (tikiStatuePieces[i] != null)
                    saves.tikiStatuePieces[i] = 1;
                else
                    saves.tikiStatuePieces[i] = 0;
            }

            saves.tikiTorchPieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (tikiTorchPieces[i] != null)
                    saves.tikiTorchPieces[i] = 1;
                else
                    saves.tikiTorchPieces[i] = 0;
            }

            saves.surfboardPieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (surfboardPieces[i] != null)
                    saves.surfboardPieces[i] = 1;
                else
                    saves.surfboardPieces[i] = 0;
            }

            saves.stoneheadPieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (stoneheadPieces[i] != null)
                    saves.stoneheadPieces[i] = 1;
                else
                    saves.stoneheadPieces[i] = 0;
            }

            saves.resortBarPieces = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (resortBarPieces[i] != null)
                    saves.resortBarPieces[i] = 1;
                else
                    saves.resortBarPieces[i] = 0;
            }

            if (Application.loadedLevel == (int)GameManager.LevelID.DefaultIsland)
            {
                saves.stockpiles = new int[30];
                for (int i = 0; i < 30; i++)
                {
                    if (stockpiles[i] != null)
                        saves.stockpiles[i] = 1;
                    else
                        saves.stockpiles[i] = 0;
                }
            }
            //END BUG FIX
            ///////////

            sf.Serialize(file, saves);

            file.Close();
        }
    }

    public void Load()
    {
        InventoryManager inven = player.GetComponent<InventoryManager>();
        PlayerStatManager stats = player.GetComponent<PlayerStatManager>();
        CoveDecorations dec = GameObject.Find("GameManager").GetComponent<CoveDecorations>();

        string path;
        if (!Application.isMobilePlatform)
        {
            path = Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat";
        }
        else
        {
            path = Application.persistentDataPath + "/Playerinfo.dat";
        }

        if (File.Exists(path))
        {
            BinaryFormatter lf = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);
            PlayerSaving loads = (PlayerSaving)lf.Deserialize(file);
            file.Close();

            ///////////
            //BUG FIX (WILL)
            //BUG #28/29
            if (loads.island == (int)GameManager.LevelID.CthulhuIsland ||
                 loads.island == (int)GameManager.LevelID.LizardIsland ||
                 loads.island == (int)GameManager.LevelID.DefaultIsland ||
                 loads.island == (int)GameManager.LevelID.VolcanoIsland)
            {

                //GameObject.Find("MainMenu").GetComponent<MainMenu>().SetLevelIndex(loads.island);

                PlayerPrefs.SetInt("island", loads.island);
                //END BUG FIX
                /////////////
                PlayerPrefs.SetInt("Difficulty", loads.diff);

                inven.numTwigs = loads.twigs;
                inven.numLeaves = loads.leaves;
                inven.numRocks = loads.rocks;
                inven.numRope = loads.rope;
                inven.numThorns = loads.thorns;

                GameObject.Find("Fire Pit").GetComponent<FireBehavior>().fireIntensity = loads.fireInten;

                stats.playerHealth = loads.health;
                stats.playerHunger = loads.hunger;
                stats.playerThirst = loads.thrist;

                Vector3 newPos = new Vector3(loads.x, loads.y, loads.z);
                player.transform.position = newPos;

                GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().start = loads.StartBox;
                GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().textBox = loads.textBox;

                inven.haveCompass = loads.compass;
                inven.haveCanteen = loads.canteen;
                inven.haveBackpack = loads.backpack;
                inven.haveLantern = loads.lantern; 
                inven.haveGloves = loads.gloves;

                PlayerPrefs.SetInt("Fire 1", loads.fire1);
                PlayerPrefs.SetInt("Fire 2", loads.fire2);
                PlayerPrefs.SetInt("Fire 3", loads.fire3);
                PlayerPrefs.SetInt("Fire 4", loads.fire4);

                PlayerPrefs.SetInt("Survival 1", loads.survival1);
                PlayerPrefs.SetInt("Survival 2", loads.survival2);
                PlayerPrefs.SetInt("Survival 3", loads.survival3);
                PlayerPrefs.SetInt("Survival 4", loads.survival4);
                PlayerPrefs.SetInt("Survival 5", loads.survival5);

                PlayerPrefs.SetInt("Death 1", loads.death1);
                PlayerPrefs.SetInt("Death 2", loads.death2);
                PlayerPrefs.SetInt("Death 3", loads.death3);
                PlayerPrefs.SetInt("Death 4", loads.death4);
                PlayerPrefs.SetInt("Death 5", loads.death5);
                PlayerPrefs.SetInt("Death 6", loads.death6);
                PlayerPrefs.SetInt("Death 7", loads.death7);

                PlayerPrefs.SetInt("Spearmantrap0", loads.spearmantrap0);
                PlayerPrefs.SetInt("Spearmantrap1", loads.spearmantrap1);
                PlayerPrefs.SetInt("Spearmantrap2", loads.spearmantrap2);
                PlayerPrefs.SetInt("Spearmantrap3", loads.spearmantrap3);
                PlayerPrefs.SetInt("Spearmantrap4", loads.spearmantrap4);
                PlayerPrefs.SetInt("Spearmantrap5", loads.spearmantrap5);

                PlayerPrefs.SetInt("Archertrap0", loads.archertrap0);
                PlayerPrefs.SetInt("Archertrap1", loads.archertrap1);
                PlayerPrefs.SetInt("Archertrap2", loads.archertrap2);
                PlayerPrefs.SetInt("Archertrap3", loads.archertrap3);
                PlayerPrefs.SetInt("Archertrap4", loads.archertrap4);
                PlayerPrefs.SetInt("Archertrap5", loads.archertrap5);

                PlayerPrefs.SetInt("Scouttrap0", loads.scouttrap0);
                PlayerPrefs.SetInt("Scouttrap1", loads.scouttrap1);
                PlayerPrefs.SetInt("Scouttrap2", loads.scouttrap2);
                PlayerPrefs.SetInt("Scouttrap3", loads.scouttrap3);
                PlayerPrefs.SetInt("Scouttrap4", loads.scouttrap4);
                PlayerPrefs.SetInt("Scouttrap5", loads.scouttrap5);

                PlayerPrefs.SetInt("Wolftrap0", loads.wolftrap0);
                PlayerPrefs.SetInt("Wolftrap1", loads.wolftrap1);
                PlayerPrefs.SetInt("Wolftrap2", loads.wolftrap2);
                PlayerPrefs.SetInt("Wolftrap3", loads.wolftrap3);
                PlayerPrefs.SetInt("Wolftrap4", loads.wolftrap4);
                PlayerPrefs.SetInt("Wolftrap5", loads.wolftrap5);

                PlayerPrefs.SetInt("Boartrap0", loads.boartrap0);
                PlayerPrefs.SetInt("Boartrap1", loads.boartrap1);
                PlayerPrefs.SetInt("Boartrap2", loads.boartrap2);
                PlayerPrefs.SetInt("Boartrap3", loads.boartrap3);
                PlayerPrefs.SetInt("Boartrap4", loads.boartrap4);
                PlayerPrefs.SetInt("Boartrap5", loads.boartrap5);

                PlayerPrefs.SetInt("SpearmanDisabled", loads.spearmandisable);
                PlayerPrefs.SetInt("ArcherDisabled", loads.archerdisable);
                PlayerPrefs.SetInt("ScoutDisabled", loads.scoutdisable);
                PlayerPrefs.SetInt("WolfDisabled", loads.wolfdisable);
                PlayerPrefs.SetInt("BoarDisabled", loads.boardisable);

                PlayerPrefs.SetInt("Hide 1", loads.hide1);
                PlayerPrefs.SetInt("Hide 2", loads.hide2);
                PlayerPrefs.SetInt("Hide 3", loads.hide3);

                PlayerPrefs.SetInt("TikiStatue", loads.collect1);
                PlayerPrefs.SetInt("TikiTorch", loads.collect2);
                PlayerPrefs.SetInt("Stonehead", loads.collect3);
                PlayerPrefs.SetInt("Cthulhu", loads.collect4);
                PlayerPrefs.SetInt("IslandBar", loads.collect5);
                PlayerPrefs.SetInt("Surfboard", loads.collect6);

                PlayerPrefs.SetInt("TutorialFinished", loads.tutorial);

                dec.tikiTorchToCollect = loads.tikitorch;
                dec.tikiStatueToCollect = loads.tikistatue;
                dec.cthulhuStatueToCollect = loads.cthulu;
                dec.stoneheadToCollect = loads.stonehead;
                dec.islandBarToCollect = loads.islandbar;
                dec.surfboardToCollect = loads.surfboard;

                ///////////
                //BUG FIX (WILL)
                //BUG #66/69
                for (int i = 0; i < 3; i++)
                {
                    if (loads.cthulhuStatuePieces[i] == 0)
                        Destroy(cthulhuStatuePieces[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (loads.tikiStatuePieces[i] == 0)
                        Destroy(tikiStatuePieces[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (loads.tikiTorchPieces[i] == 0)
                        Destroy(tikiTorchPieces[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (loads.surfboardPieces[i] == 0)
                        Destroy(surfboardPieces[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (loads.stoneheadPieces[i] == 0)
                        Destroy(stoneheadPieces[i]);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (loads.resortBarPieces[i] == 0)
                        Destroy(resortBarPieces[i]);
                }

                if (loads.island == (int)GameManager.LevelID.DefaultIsland)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        if (loads.stockpiles[i] == 0)
                            Destroy(stockpiles[i]);
                    }
                }
                //END BUG FIX
                ///////////

            }
        }
    }

    //////////
    //BUG FIX
    //BUG #48
    public static void DeleteSave()
    {
        string path;
        if (!Application.isMobilePlatform)
        {
            path = Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat";
        }
        else
        {
            path = Application.persistentDataPath + "/Playerinfo.dat";
        }

        if (File.Exists(path))
        {
            //BinaryFormatter lf = new BinaryFormatter();

            File.Delete(path);
        }
    }
    //END BUG FIX
    /////////////
}

[Serializable]
public class PlayerSaving
{
    public bool textBox { get; set; }
    public bool StartBox { get; set; }

    public int island { get; set; }

    public int diff { get; set; }

    public int twigs { get; set; }
    public int leaves { get; set; }
    public int rocks { get; set; }
    public int rope { get; set; }
    public int thorns { get; set; }

    public float fireInten { get; set; }

    public float health { get; set; }
    public float hunger { get; set; }
    public float thrist { get; set; }

    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }

    public bool compass { get; set; }
    public bool canteen { get; set; }
    public bool backpack { get; set; }
    public bool lantern { get; set; }
    public bool gloves { get; set; }

    public int fire1 { get; set; }
    public int fire2 { get; set; }
    public int fire3 { get; set; }
    public int fire4 { get; set; }

    public int survival1 { get; set; }
    public int survival2 { get; set; }
    public int survival3 { get; set; }
    public int survival4 { get; set; }
    public int survival5 { get; set; }

    public int death1 { get; set; }
    public int death2 { get; set; }
    public int death3 { get; set; }
    public int death4 { get; set; }
    public int death5 { get; set; }
    public int death6 { get; set; }
    public int death7 { get; set; }

    public int spearmantrap0 { get; set; }
    public int spearmantrap1 { get; set; }
    public int spearmantrap2 { get; set; }
    public int spearmantrap3 { get; set; }
    public int spearmantrap4 { get; set; }
    public int spearmantrap5 { get; set; }

    public int archertrap0 { get; set; }
    public int archertrap1 { get; set; }
    public int archertrap2 { get; set; }
    public int archertrap3 { get; set; }
    public int archertrap4 { get; set; }
    public int archertrap5 { get; set; }

    public int scouttrap0 { get; set; }
    public int scouttrap1 { get; set; }
    public int scouttrap2 { get; set; }
    public int scouttrap3 { get; set; }
    public int scouttrap4 { get; set; }
    public int scouttrap5 { get; set; }

    public int wolftrap0 { get; set; }
    public int wolftrap1 { get; set; }
    public int wolftrap2 { get; set; }
    public int wolftrap3 { get; set; }
    public int wolftrap4 { get; set; }
    public int wolftrap5 { get; set; }

    public int boartrap0 { get; set; }
    public int boartrap1 { get; set; }
    public int boartrap2 { get; set; }
    public int boartrap3 { get; set; }
    public int boartrap4 { get; set; }
    public int boartrap5 { get; set; }

    public int spearmandisable { get; set; }
    public int archerdisable { get; set; }
    public int scoutdisable { get; set; }
    public int wolfdisable { get; set; }
    public int boardisable { get; set; }

    public int hide1 { get; set; }
    public int hide2 { get; set; }
    public int hide3 { get; set; }

    public int collect1 { get; set; }
    public int collect2 { get; set; }
    public int collect3 { get; set; }
    public int collect4 { get; set; }
    public int collect5 { get; set; }
    public int collect6 { get; set; }

    public int tutorial { get; set; }

    public int tikitorch { get; set; }
    public int tikistatue { get; set; }
    public int cthulu { get; set; }
    public int stonehead { get; set; }
    public int islandbar { get; set; }
    public int surfboard { get; set; }

    ///////////
    //BUG FIX (WILL)
    //BUG #66/69
    public int[] stockpiles { get; set; }

    public int[] cthulhuStatuePieces { get; set; }
    public int[] tikiStatuePieces { get; set; }
    public int[] tikiTorchPieces { get; set; }
    public int[] resortBarPieces { get; set; }
    public int[] surfboardPieces { get; set; }
    public int[] stoneheadPieces { get; set; }
    //END BUG FIX
    ///////////
}