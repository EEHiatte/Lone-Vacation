using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateIsland : MonoBehaviour 
{
    //enum IslandType {Lizard, Volcano, Cthulhu, Default};

    public GameObject rotateObject;
    public Vector3 rotatePoint;

    //public GameObject lizardIsland;
    //public GameObject volcanoIsland;
    //public GameObject cthulhuIsland;
    //public GameObject defaultIsland;

    public GameObject island;
    float degreesToRotate = 0.085f;//0.1f;//0.035f;

    //GameObject[] islands = new GameObject[4];
    //Image fadePanel;
    //float islandChangeTimer = 0.0f;
    //float maxChangeTime = 5.0f;

    float defIslZPos = 1100;
    //float cthIslZPos = 600;//600;
    //float lizIslZPos = 700;//800;
    //float volIslZPos = 800;//700;

    float defIslYPos = 300;//320;
    //float cthIslYPos = 300;//800;
    //float lizIslYPos = 300;//600;
    //float volIslYPos = 300;//600;

    float defIslXPos;
    //float cthIslXPos;
    //float lizIslXPos;
    //float volIslXPos;

    //IslandType currIsland;
    //bool subtractFadePanel = false;

	// Use this for initialization
	void Start () 
    {
        Time.timeScale = 1;

        rotatePoint = rotateObject.transform.position;
        //cthIslXPos = defIslXPos = volIslXPos = lizIslXPos = rotatePoint.x;
        defIslXPos = rotatePoint.x;

        //cthIslZPos = rotatePoint.z - cthIslZPos;
        //volIslZPos = rotatePoint.z - volIslZPos;
        //lizIslZPos = rotatePoint.z - lizIslZPos;
        defIslZPos = rotatePoint.z - defIslZPos;

        transform.position = new Vector3(defIslXPos, defIslYPos, defIslZPos);
        rotatePoint.y -= 700;//900;//800;

        //fadePanel = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Image>();

        //islands = GameObject.FindGameObjectsWithTag("Terrain");
        //islands[0] = lizardIsland;
        //islands[1] = volcanoIsland;
        //islands[2] = cthulhuIsland;
        //islands[3] = defaultIsland;
        //for (int i = 0; i < islands.Length; i++)
        //{
        //    islands[i].SetActive(false);
        //}
        //islands[(int)IslandType.Default].SetActive(true);
        //currIsland = IslandType.Default;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.RotateAround(rotatePoint, transform.up, degreesToRotate);
        transform.LookAt(rotatePoint);

        //islandChangeTimer += Time.deltaTime;
        //
        //if(islandChangeTimer >= maxChangeTime)
        //{
        //    if (!subtractFadePanel)
        //    {
        //        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadePanel.color.a + 0.01f);
        //        if (fadePanel.color.a >= 1.0f)
        //        {
        //            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1.0f);
        //            islands[(int)currIsland].SetActive(false);
        //
        //            if (currIsland == IslandType.Cthulhu)
        //            {
        //                cthIslZPos = transform.position.z;
        //                cthIslXPos = transform.position.x;
        //                cthIslYPos = transform.position.y;
        //            }
        //            else if (currIsland == IslandType.Lizard)
        //            {
        //                lizIslZPos = transform.position.z;
        //                lizIslXPos = transform.position.x;
        //                lizIslYPos = transform.position.y;
        //            }
        //            else if (currIsland == IslandType.Volcano)
        //            {
        //                volIslZPos = transform.position.z;
        //                volIslXPos = transform.position.x;
        //                volIslYPos = transform.position.y;
        //            }
        //            else
        //            {
        //                defIslZPos = transform.position.z;
        //                defIslXPos = transform.position.x;
        //                defIslYPos = transform.position.y;
        //            }
        //
        //            if (currIsland != IslandType.Default)
        //            {
        //                currIsland = (IslandType)((int)currIsland + 1);
        //                islands[(int)currIsland].SetActive(true);
        //            }
        //            else
        //            {
        //                currIsland = (IslandType)(0);
        //                islands[(int)currIsland].SetActive(true);
        //            }
        //
        //            if (currIsland == IslandType.Cthulhu)
        //                transform.position = new Vector3(cthIslXPos, cthIslYPos, cthIslZPos);
        //            else if (currIsland == IslandType.Lizard)
        //                transform.position = new Vector3(lizIslXPos, lizIslYPos, lizIslZPos);
        //            else if (currIsland == IslandType.Volcano)
        //                transform.position = new Vector3(volIslXPos, volIslYPos, volIslZPos);
        //            else
        //                transform.position = new Vector3(defIslXPos, defIslYPos, defIslZPos);
        //
        //            subtractFadePanel = true;
        //        }
        //    }
        //    else
        //    {
        //        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadePanel.color.a - 0.01f);
        //        if(fadePanel.color.a <= 0.0f)
        //        {
        //            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0.0f);
        //            islandChangeTimer = 0.0f;
        //            subtractFadePanel = false;
        //        }
        //    }
        //}
	}
}