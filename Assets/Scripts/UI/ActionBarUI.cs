using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarUI : MonoBehaviour
{
    public Transform ui;

    GameObject player;
    PlayerControls playerControls;

    TMP_Text actionBarKeybindText;

    KeyButton keyButton;

    Transform actionBarSlot1;
    Transform actionBarSlot2;
    Transform actionBarSlot3;
    Transform actionBarSlot4;
    Transform actionBarSlot5;
    Transform actionBarSlot6;
    Transform actionBarSlot7;
    Transform actionBarSlot8;
    Transform actionBarSlot9;
    Transform actionBarSlot10;


    string GetKeyCodeString(KeyCode keyCode) {
        switch (keyCode) {
            case KeyCode.Alpha0: { return "0"; }
            case KeyCode.Alpha1: { return "1"; }
            case KeyCode.Alpha2: { return "2"; }
            case KeyCode.Alpha3: { return "3"; }
            case KeyCode.Alpha4: { return "4"; }
            case KeyCode.Alpha5: { return "5"; }
            case KeyCode.Alpha6: { return "6"; }
            case KeyCode.Alpha7: { return "7"; }
            case KeyCode.Alpha8: { return "8"; }
            case KeyCode.Alpha9: { return "9"; }
        }
        return keyCode.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player;
        playerControls = player.GetComponent<PlayerControls>();

        actionBarSlot1 = ui.GetChild(0);
        actionBarKeybindText = actionBarSlot1.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot1.primary[0]));
        keyButton = actionBarSlot1.GetChild(0).GetComponent<KeyButton>();
        keyButton.SetBind(playerControls.controls.ActionBarSlot1);

        actionBarSlot2 = ui.GetChild(1);
        actionBarKeybindText = actionBarSlot2.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot2.primary[0]));

        actionBarSlot3 = ui.GetChild(2);
        actionBarKeybindText = actionBarSlot3.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot3.primary[0]));

        actionBarSlot4 = ui.GetChild(3);
        actionBarKeybindText = actionBarSlot4.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot4.primary[0]));

        actionBarSlot5 = ui.GetChild(4);
        actionBarKeybindText = actionBarSlot5.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot5.primary[0]));

        actionBarSlot6 = ui.GetChild(5);
        actionBarKeybindText = actionBarSlot6.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot6.primary[0]));

        actionBarSlot7 = ui.GetChild(6);
        actionBarKeybindText = actionBarSlot7.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot7.primary[0]));

        actionBarSlot8 = ui.GetChild(7);
        actionBarKeybindText = actionBarSlot8.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot8.primary[0]));

        actionBarSlot9 = ui.GetChild(8);
        actionBarKeybindText = actionBarSlot9.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot9.primary[0]));

        actionBarSlot10 = ui.GetChild(9);
        actionBarKeybindText = actionBarSlot10.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        actionBarKeybindText.SetText(GetKeyCodeString(playerControls.controls.ActionBarSlot10.primary[0]));
    }

    public void nothing() {
        Debug.Log("Pressed");
    }
}
