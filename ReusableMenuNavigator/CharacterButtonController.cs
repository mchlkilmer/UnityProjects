using UnityEngine;
using System.Collections;

public class CharacterButtonController : MonoBehaviour {
    //Button Controller
    //Each differnet menu should have its own buttons script

    public string dogName;
    public int number;

    public bool selected = false;

    int returnNumber()
    {
        return number;
    }

    public void setSelected()
    {
        if(selected == true)
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
