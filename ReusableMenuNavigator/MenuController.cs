using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
    //Menu controller
    //Attach this script to an empty object that can be used to controll different menus
    //Each menu needs to be a square grid with buttons in each grid location

    public int playersConfirmed = 0;
    public int playersPlaying = 1; //Starts at one and goes up for more players 
    public int rows;
    public int columns;
    private int max;

    int currentRow;
    int currentColumn;

    public GameObject P1, P2, P3, P4; //Gameobjects that have a menu navigator script attached to them for user to move around
    public GameObject screen; //The UI Canvas that can be turned on and off

    CharacterButtonController[] characterButtons;

    void Awake () {
        playersPlaying = 1;
        characterButtons = GameObject.FindObjectsOfType(typeof(CharacterButtonController)) as CharacterButtonController[]; //Find all buttons for this type of menu
        max = rows * columns;
        for (int i = 0; i < max; i++)
        {
            characterButtons[i].setSelected(); //make sure nothing is selected when game starts   
        }
    }

    // Update is called once per frame
    void Update () {
        if(screen.activeSelf == true )
        {
            ChangePosition(P1);
            ChangePosition(P2);
            ChangePosition(P3);
            ChangePosition(P4);
        }
    }

    int FindRow(GameObject player) //Find the row that the user is currently on
    {
        int mySelector = player.GetComponent<MenuNavigator>().selector;
        currentRow = ((mySelector - 1) / (columns)) + 1;
        return currentRow; 
    }

    int FindColumn(GameObject player) //Find the column that the user is currently on
    {
        int mySelector = player.GetComponent<MenuNavigator>().selector;
        currentColumn = mySelector % (max / rows);
        if(currentColumn == 0)
        {
            currentColumn += columns;
        }
        return currentColumn;
    }

    public void SetPostion(GameObject player) //Set user position to the button they are on
    {
        for(int i = 0; i < max; i++)
        {
            if(player.GetComponent<MenuNavigator>().selector == characterButtons[i].number)
            {
                player.transform.position = characterButtons[i].transform.position;
            }
        }
    }

    bool ResetPosition(GameObject player) //Reset Axis Input for Xbox controller
    {
        MenuNavigator navigator = player.GetComponent<MenuNavigator>();

        string hAxis = navigator.horizontalAxis;
        string vAxis = navigator.verticalAxis;
        string hAxisDpad = navigator.dpadHorizontal;
        string vAxisDpad = navigator.dpadVertical;

        if ((Input.GetAxisRaw(hAxis) < .2f) && (Input.GetAxisRaw(hAxis) > -0.2f) && (Input.GetAxisRaw(vAxis) < 0.2f) && (Input.GetAxisRaw(vAxis) > -.2f) &&
            (Input.GetAxisRaw(hAxisDpad) < .2f) && (Input.GetAxisRaw(hAxisDpad) > -0.2f) && (Input.GetAxisRaw(vAxisDpad) < 0.2f) && (Input.GetAxisRaw(vAxisDpad) > -.2f))
        {
            return true;
        }
        else
            return false;
    }

    void ChangePosition(GameObject player)//
    {
        MenuNavigator navigator = player.GetComponent<MenuNavigator>();

        string hAxis = navigator.horizontalAxis;
        string vAxis = navigator.verticalAxis;
        string hAxisDpad = navigator.dpadHorizontal;
        string vAxisDpad = navigator.dpadVertical;
        string input = navigator.enterButton;
        string back = navigator.backButton;

        if ((navigator.onAxis == true) && ResetPosition(player) == true)
        {
            navigator.onAxis = false;
        }

        if (navigator.canMove == true && navigator.isActive == true)
        {
            if (((Input.GetAxis(vAxis) == 1) || (Input.GetAxis(vAxisDpad) == 1)) && (navigator.onAxis == false)) //Move up. If user is on top row move them to the bottom
            {
                if (FindRow(player) == 1)
                {
                    navigator.selector += (columns * (rows - 1));
                }
                else
                    navigator.selector -= columns;

                SetPostion(player);
                navigator.onAxis = true;
            }

            if (((Input.GetAxis(vAxis) == -1) || (Input.GetAxis(vAxisDpad) == -1)) && (navigator.onAxis == false)) //Move down. If user is on botton row move them to top
            {
                if (FindRow(player) == (rows))
                {
                    navigator.selector -= (columns * (rows - 1));
                }
                else
                    navigator.selector += columns;

                SetPostion(player);
                navigator.onAxis = true;
            }

            if (((Input.GetAxis(hAxis) == -1) || (Input.GetAxis(hAxisDpad) == -1)) && (navigator.onAxis == false)) //Move left. If user is in left most column move them to right most column
            {
                Debug.Log("Moving");
                if (FindColumn(player) == 1)
                {
                    navigator.selector += (columns - 1);
                }
                else
                    navigator.selector -= 1;

                SetPostion(player);
                navigator.onAxis = true;
            }

            if (((Input.GetAxis(hAxis) == 1) || (Input.GetAxis(hAxisDpad) == 1)) && (navigator.onAxis == false)) //Move right. If user is in left most column move them to right most column
            {
                Debug.Log("Moving");
                if (FindColumn(player) == columns)
                {
                    navigator.selector -= (columns - 1);
                }
                else
                    navigator.selector += 1;

                SetPostion(player);
                navigator.onAxis = true;
            }
        }

        if (Input.GetButtonDown(input)) //Get input / select button
        {
            if (navigator.isActive == true)
            {

                if (playersConfirmed == playersPlaying)
                {
                    //Move to next screen when all players are ready if needed
                }

                if (navigator.canMove == true)
                {
                    //Call button function
                }
            }

            if (navigator.isActive == false) //Used to add players that start as unactive. Ex. Multiplayer character select
            {
                player.GetComponent<MenuNavigator>().isActive = true;
                playersPlaying += 1;
                navigator.gameObject.SetActive(true); //Turn on the new players navigator
                player.GetComponent<MenuNavigator>().selector = 1; //Set player at start of the menu
                SetPostion(player);
            }
        }

        if (Input.GetButtonDown(back) && navigator.isActive == true)
        {
            if(playersConfirmed == 0)
            {
                //Go back to previous menu
            }

            if(navigator.canMove == false)
            {
                //Deselect object if necessary 
            }
        }
    }
}
