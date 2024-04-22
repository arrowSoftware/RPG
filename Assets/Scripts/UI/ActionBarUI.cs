using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionBarUI : MonoBehaviour
{
    public Transform ui;
    public Ability[] activeAbilities = new Ability[10];
 
    GameObject player;
    PlayerControls playerControls;

    TMP_Text actionBarKeybindText;

    KeyButton keyButton;

    Transform[] actionBarSlots = new Transform[10];
    ControlBinding[] slotBindings = new ControlBinding[10];

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

    void Start()
    {
        player = PlayerManager.instance.player;
        playerControls = player.GetComponent<PlayerControls>();

        slotBindings[0] = playerControls.controls.ActionBarSlot1;
        slotBindings[1] = playerControls.controls.ActionBarSlot2;
        slotBindings[2] = playerControls.controls.ActionBarSlot3;
        slotBindings[3] = playerControls.controls.ActionBarSlot4;
        slotBindings[4] = playerControls.controls.ActionBarSlot5;
        slotBindings[5] = playerControls.controls.ActionBarSlot6;
        slotBindings[6] = playerControls.controls.ActionBarSlot7;
        slotBindings[7] = playerControls.controls.ActionBarSlot8;
        slotBindings[8] = playerControls.controls.ActionBarSlot9;
        slotBindings[9] = playerControls.controls.ActionBarSlot10;

        for (int i = 0; i < 10; i++) {
            actionBarSlots[i] = ui.GetChild(i);
            TMP_Text keyBindText = actionBarSlots[i].GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
            keyBindText.SetText(GetKeyCodeString(slotBindings[i].primary[0]));
            KeyButton keyButton = actionBarSlots[i].GetChild(0).GetComponent<KeyButton>();
            keyButton.SetBind(slotBindings[i]);
        
            if (activeAbilities[i] != null) {
                actionBarSlots[i].GetChild(0).GetChild(0).GetComponent<Image>().sprite = activeAbilities[i].icon;
                actionBarSlots[i].GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
                actionBarSlots[i].GetChild(0).GetComponent<KeyButton>().ability = activeAbilities[i];
            }
        }
    }
}
