using UnityEngine;
using System.Collections;

public class CharacterSelectController : MonoBehaviour {
    //Menu controller
    //Each menu needs to be a square grid with buttons in each grid location

    public int playersConfirmed = 0;
    public int playersPlaying;
    public int rows;
    public int columns;
    private int max;

    int currentRow;
    int currentColumn;

    public GameObject P1, P2, P3, P4;
    public GameObject screen;

    public MainMenuController controller;

    CharacterButtonController[] characterButtons;

    void Awake () {
        playersPlaying = 1;
        characterButtons = GameObject.FindObjectsOfType(typeof(CharacterButtonController)) as CharacterButtonController[]; //Find all buttons for this type of menu
        max = rows * columns;
        controller = GameObject.Find("Main Menu Controller").GetComponent<MainMenuController>(); // this script should be attached to this game object to turn off various screens, ex. main menu, options, ect.
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

    int FindRow(GameObject player)
    {
        int mySelector = player.GetComponent<CharacterMenuNavigator>().selector;
        currentRow = ((mySelector - 1) / (columns)) + 1;
        return currentRow; 
    }

    int FindColumn(GameObject player)
    {
        int mySelector = player.GetComponent<CharacterMenuNavigator>().selector;
        currentColumn = mySelector % (max / rows);
        if(currentColumn == 0)
        {
            currentColumn += columns;
        }
        return currentColumn;
    }

    public void SetPostion(GameObject player)
    {
        for(int i = 0; i < max; i++)
        {
            if(player.GetComponent<CharacterMenuNavigator>().selector == characterButtons[i].number)
            {
                player.transform.position = characterButtons[i].transform.position;
            }
        }
    }

    void SelectDog(GameObject player)
    {
        for (int i = 0; i < max; i++)
        {
            if (player.GetComponent<CharacterMenuNavigator>().selector == characterButtons[i].number)
            {
                if (characterButtons[i].selected == false)
                {
                    player.GetComponent<CharacterMenuNavigator>().dogSelected = characterButtons[i].dogName;
                    player.GetComponent<CharacterMenuNavigator>().canMove = false;
                    characterButtons[i].selected = true;
                    characterButtons[i].setSelected();
                    playersConfirmed += 1;
                }
                else
                   break;
            }
        }
    }
    

    void DeselectDog(GameObject player)
    {
        for (int i = 0; i < max; i++)
        {
            if (player.GetComponent<CharacterMenuNavigator>().selector == characterButtons[i].number)
            {
                player.GetComponent<CharacterMenuNavigator>().dogSelected = "";
                player.GetComponent<CharacterMenuNavigator>().canMove = true;
                characterButtons[i].selected =false;
                characterButtons[i].setSelected();
            }
        }
    }

    bool ResetPosition(GameObject player) //Reset Axis Input for Xbox controller
    {
        CharacterMenuNavigator navigator = player.GetComponent<CharacterMenuNavigator>();

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

    void ChangePosition(GameObject player)
    {
        CharacterMenuNavigator navigator = player.GetComponent<CharacterMenuNavigator>();

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
            if (((Input.GetAxis(vAxis) == 1) || (Input.GetAxis(vAxisDpad) == 1)) && (navigator.onAxis == false)) //up
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

            if (((Input.GetAxis(vAxis) == -1) || (Input.GetAxis(vAxisDpad) == -1)) && (navigator.onAxis == false)) //down
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

            if (((Input.GetAxis(hAxis) == -1) || (Input.GetAxis(hAxisDpad) == -1)) && (navigator.onAxis == false)) //left
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

            if (((Input.GetAxis(hAxis) == 1) || (Input.GetAxis(hAxisDpad) == 1)) && (navigator.onAxis == false)) //right
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

        if (Input.GetButtonDown(input))
        {
            if (navigator.isActive == true)
            {

                if (playersConfirmed == playersPlaying)
                {
                    GameObject gameMaster = GameObject.Find("GameMaster");
                    gameMaster.GetComponent<GlobalControl>().P1_Character = P1.GetComponent<CharacterMenuNavigator>().dogSelected;
                    gameMaster.GetComponent<GlobalControl>().P2_Character = P2.GetComponent<CharacterMenuNavigator>().dogSelected;
                    gameMaster.GetComponent<GlobalControl>().P3_Character = P3.GetComponent<CharacterMenuNavigator>().dogSelected;
                    gameMaster.GetComponent<GlobalControl>().P4_Character = P4.GetComponent<CharacterMenuNavigator>().dogSelected;
                    gameMaster.GetComponent<GlobalControl>().players = playersPlaying;
                    controller.CharacterSelect.SetActive(false);
                    StartCoroutine(controller.GetComponent<LevelSelectController>().beginCountdown());
                    controller.LevelSelect.SetActive(true);
                }

                if (navigator.canMove == true)
                {
                    SelectDog(player);
                }
            }

            if (navigator.isActive == false)
            {
                player.GetComponent<CharacterMenuNavigator>().isActive = true;
                playersPlaying += 1;
                navigator.gameObject.SetActive(true);
                player.GetComponent<CharacterMenuNavigator>().selector = 1;
                SetPostion(player);
            }
            Debug.Log("A pressed");
        }

        if (Input.GetButtonDown(back) && navigator.isActive == true)
        {
            if(playersConfirmed == 0)
            {
                controller.CharacterSelect.SetActive(false);
                controller.MainMenu.SetActive(true);
            }

            if(navigator.canMove == false)
            {
                DeselectDog(player);
                playersConfirmed -= 1;
            }
            Debug.Log("B pressed");
        }
    }
}
