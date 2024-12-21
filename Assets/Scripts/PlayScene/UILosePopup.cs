using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILosePopup : MonoBehaviour 
{
    public SceneTransition toMap;
    public Text keepCost;
    public Text coinText;
    public Text skipCost;

    public PopupOpener shopPopup;

	void Start () 
    {
        coinText.text = GameData.instance.GetPlayerCoin().ToString();
        keepCost.text = Configure.instance.keepPlayingCost.ToString();
        skipCost.text = Configure.instance.skipLevelCost.ToString();
	}

    public void ExitButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        toMap.PerformTransition();
    }

    public void ReplayButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        Configure.instance.autoPopup = LevelLoader.instance.level;

        toMap.PerformTransition();
    }

    public void SkipButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        var cost = Configure.instance.skipLevelCost;

        // enough coin
        if (cost <= GameData.instance.playerCoin)
        {
            AudioManager.instance.CoinPayAudio();

            // reduce coin
            GameData.instance.SavePlayerCoin(GameData.instance.playerCoin - cost);
            
            var board = GameObject.Find("Board").GetComponent<Board>();

            if (board)
            {
                // save info
                board.SaveLevelInfo();
            }

            // go to map with auto popup of next level
            Configure.instance.autoPopup = LevelLoader.instance.level + 1;

            toMap.PerformTransition();
        }
        else
        {
            shopPopup.OpenPopup();
        }
    }

    public void KeepButtonClick()
    {
        AudioManager.instance.ButtonClickAudio();

        var cost = Configure.instance.keepPlayingCost;

        // enough coin
        if (cost <= GameData.instance.playerCoin)
        {
            AudioManager.instance.CoinPayAudio();

            // reduce coin
            GameData.instance.SavePlayerCoin(GameData.instance.playerCoin - cost);
            
            var board = GameObject.Find("Board").GetComponent<Board>();

            if (board)
            {
                // add 5 more moves
                board.moveLeft = 5;

                // change the label
                board.UITop.Set5Moves();

                // change the game state
                board.state = GAME_STATE.WAITING_USER_SWAP;

                // reset and call hint
                board.checkHintCall = 0;
                board.Hint();
            }                    

            // close the popup
            var popup = GameObject.Find("LosePopup(Clone)");

            if (popup)
            {
                popup.GetComponent<Popup>().Close();
            }
        }
        // not enough coin
        else
        {
            shopPopup.OpenPopup();
        }
    }
}
