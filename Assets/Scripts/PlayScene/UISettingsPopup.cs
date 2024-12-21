using UnityEngine;
using System.Collections;

public class UISettingsPopup : MonoBehaviour 
{
    public SceneTransition toMap;

    public void GoToMap()
    {
        AudioManager.instance.ButtonClickAudio();

        toMap.PerformTransition();
    }

    public void Replay()
    {
        AudioManager.instance.ButtonClickAudio();

        Configure.instance.autoPopup = LevelLoader.instance.level;

        toMap.PerformTransition();
    }
	
	public void ButtonClickAudio()
    {
        AudioManager.instance.ButtonClickAudio();
    }

    public void CloseButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        if (GameObject.Find("Board"))
        {
            GameObject.Find("Board").GetComponent<Board>().state = GAME_STATE.WAITING_USER_SWAP;
        }
    }
}
