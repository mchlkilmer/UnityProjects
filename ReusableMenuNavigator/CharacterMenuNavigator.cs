using UnityEngine;
using System.Collections;

public class CharacterMenuNavigator : MonoBehaviour {
    //script that allows users to navigate menus

    public int selector; //int to determine where user is in the menu

    public string verticalAxis; //axes used for input
    public string horizontalAxis;
    public string dpadVertical;
    public string dpadHorizontal;
    public string enterButton;
    public string backButton;

    public string dogSelected;

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
