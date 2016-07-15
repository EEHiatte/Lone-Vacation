using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipBehavior : MonoBehaviour
{
    public Text nameObject;
    public Text descriptionObject;
    public string nameText = "";
    public string descriptionText = "";

    void Awake()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameObject = texts[0];
        descriptionObject = texts[1];
        transform.SetParent(GameObject.Find("UI 2").transform, false);
    }

    // Use this for initialization
    void Start()
    {
        nameObject.text = nameText;
        descriptionObject.text = descriptionText;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition + new Vector3(20, -20, Input.mousePosition.z);
    }
}
