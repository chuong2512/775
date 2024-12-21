using UnityEngine;
using System.Collections;

public class UISettings : MonoBehaviour 
{
    public Board board;
    public PopupOpener settingsPopup;

    public void SettingsClick()
    {
        if (board.state == GAME_STATE.WAITING_USER_SWAP)
        {
            AudioManager.instance.ButtonClickAudio();

            settingsPopup.OpenPopup();

            board.state = GAME_STATE.OPENING_POPUP;

            // ads
        }        
    }
}
