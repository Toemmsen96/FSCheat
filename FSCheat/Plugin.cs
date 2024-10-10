using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using FSCheat.Cheats;


namespace FSCheat
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {

        internal static string lastDisplayedMessage = string.Empty;
        private string userInput = string.Empty;
        private bool showMenu = false;
        private bool showPopup = false;
        private CustomCheat selectedCheat = null;
        private ConfigEntry<KeyCode> toggleKey;
        private GUIStyle menuStyle;
        private const string modGUID = "toemmsen.FSCheats";
        private const string modName = "FSCheats";
        private const string modVersion = "1.0.0";
        private readonly Harmony harmony = new Harmony(modGUID);
        internal static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
        private static Plugin instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;

            }

            harmony.PatchAll(typeof(Patches));
            harmony.PatchAll(typeof(Plugin));
            InitMenu();
            logger.LogWarning((object)"\r\n" +
                "  ______                                                                         \r\n" +
                " /_  __/  ____   ___    ____ ___    ____ ___    _____  ___    ____    _____      \r\n" +
                "  / /    / __ \\ / _ \\  / __ `__ \\  / __ `__ \\  / ___/ / _ \\  / __ \\  / ___/\r\n" +
                " / /    / /_/ //  __/ / / / / / / / / / / / / (__  ) /  __/ / / / / (__  )  \r\n" +
                "/_/     \\____/ \\___/ /_/ /_/_/_/ /_/ /_/ /_/_/____/  \\___/ /_/ /_/ /____/\r\n");
            logger.LogInfo(modGUID+" loaded");
        }
        private void InitMenu(){
            toggleKey = instance.Config.Bind<KeyCode>("Menu Settings", "Toggle Key", KeyCode.F7, "Key to toggle the menu");
            menuStyle = new GUIStyle();
            menuStyle.fontSize = 20;
            menuStyle.normal.textColor = Color.white;
            logger.LogInfo("Menu initialized");
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(toggleKey.Value))
            {
                showMenu = !showMenu;
            }
        }

        private void DrawModMenu()
        {
        // Create a simple mod menu box at the center of the screen
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Rect menuRect = new Rect(screenWidth / 2 - 100, 0, 200, screenHeight);

        GUI.Box(menuRect, "Mod Menu");

        float buttonHeight = 30f;
        float buttonSpacing = 8f;
        float currentYPosition = 60;
        
        foreach (var cheat in Cheats.Cheats.AllCheats)
        {
            if (cheat.Format.Split(' ').Length > 1)
            {
                if (GUI.Button(new Rect(screenWidth / 2 - 80, currentYPosition, 160, buttonHeight), cheat.Name))
                {
                    showMenu = false;
                    showPopup = true;
                    selectedCheat = cheat;
                }
            }
            else
            {
                if (GUI.Button(new Rect(screenWidth / 2 - 80, currentYPosition, 160, buttonHeight), cheat.Name))
                {
                    cheat.Execute(null);
                }
            }
        
            // Update the Y position for the next button
            currentYPosition += buttonHeight + buttonSpacing;
        }
        if (GUI.Button(new Rect(screenWidth / 2 - 80, currentYPosition, 160, buttonHeight), "Close Menu"))
        {
            showMenu = false;
        }
    }
    private void OnGUI()
    {
        // Display the current toggle key when the game starts (top left corner)
        GUI.Label(new Rect(10, 10, 300, 30), $"Press {toggleKey.Value} to toggle Mod Menu", menuStyle);
        GUI.Label(new Rect(10, 40, 300, 60), $"Last DBGMsg: {lastDisplayedMessage}", menuStyle);

        // Show mod menu if toggled
        if (showMenu)
        {
            DrawModMenu();
        }

        if (showPopup){
            ShowPopupForUserInput();
        }
    }
    // Method to show a popup and get user input
    private void ShowPopupForUserInput()
    {    
        if (selectedCheat != null)
        {
            // Create a simple popup box at the center of the screen
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            Rect popupRect = new Rect(screenWidth / 2 - 100, screenHeight / 2 - 100, 200, 200);
    
            GUI.Box(popupRect, "Enter Arguments for " + selectedCheat.Name);
    
            // Create a text field for user input
            GUI.SetNextControlName("UserInputField");
            userInput = GUI.TextField(new Rect(screenWidth / 2 - 80, screenHeight / 2 - 60, 160, 30), userInput);
            GUI.FocusControl("UserInputField");
    
            // Add a button to confirm the input
            if (GUI.Button(new Rect(screenWidth / 2 - 80, screenHeight / 2 - 20, 160, 30), "Confirm"))
            {
                string fullCommand = selectedCheat.Format.Split(' ')[0]+" "+userInput; //add command to front of arguments, not ideal
                userInput = string.Empty;
                showPopup = false;
                showMenu = true;
                selectedCheat.Execute(CheatInput.Parse(fullCommand));
                selectedCheat = null;
            }
            if (GUI.Button(new Rect(screenWidth / 2 - 80, screenHeight / 2 + 20, 160, 30), "Cancel"))
            {
                showPopup = false;
                showMenu = true;
            }
        }
    }
    }
}
