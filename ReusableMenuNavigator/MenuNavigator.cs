using UnityEngine;
using System.Collections;

public class MenuNavigator : MonoBehaviour {
    //script that allows users to navigate menus
    //Attach this script to the object that you would like to use to navigate through a menu

    public int selector; //int to determine where user is in the menu

    public string verticalAxis; //axes used for input
    public string horizontalAxis;
    public string dpadVertical;
    public string dpadHorizontal;
    public string enterButton;
    public string backButton;

    public bool canMove;
    public bool onAxis = false;
    public bool isActive = false;


    void Awake()
    {
        canMove = true;
    }
    void Start()
    {

    }

    void Update()
    {

    }

}
