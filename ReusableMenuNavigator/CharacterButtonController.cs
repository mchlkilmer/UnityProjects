using UnityEngine;
using System.Collections;

public class CharacterButtonController : MonoBehaviour {
    //Button Controller
    //Attach this script to anything in your menu that you would like to be a button
    //Each differnet menu should have its own buttons script
    
    public int number; //Number of where the button should be in the grid

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
