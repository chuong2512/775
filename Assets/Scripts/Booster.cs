using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Booster : MonoBehaviour 
{
    public static Booster instance = null;

    [Header("Board")]
    public Board board;

    [Header("Booster")]
    public GameObject singleBooster;
    public GameObject rowBooster;
    public GameObject columnBooster;
    public GameObject rainbowBooster;
    public GameObject ovenBooster;

    [Header("Active")]
    public GameObject singleActive;
    public GameObject rowActive;
    public GameObject columnActive;
    public GameObject rainbowActive;
    public GameObject ovenActive;

    [Header("Amount")]
    public Text singleAmount;
    public Text rowAmount;
    public Text columnAmount;
    public Text rainbowAmount;
    public Text ovenAmount;

    [Header("Popup")]
    public PopupOpener singleBoosterPopup;
    public PopupOpener rowBoosterPopup;
    public PopupOpener columnBoosterPopup;
    public PopupOpener rainbowBoosterPopup;
    public PopupOpener ovenBoosterPopup;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

	void Start () 
    {
        singleBooster.SetActive(false);
        rowBooster.SetActive(false);
        columnBooster.SetActive(false);
        rainbowBooster.SetActive(false);
        ovenBooster.SetActive(false);

        // single breaker
        if (LevelLoader.instance.level >= 7)
        {
            singleBooster.SetActive(true);
            singleAmount.text = GameData.instance.GetSingleBreaker().ToString();
        }
        
        // row breaker
        if (LevelLoader.instance.level >= 12)
        {
            rowBooster.SetActive(true);
            rowAmount.text = GameData.instance.GetRowBreaker().ToString();
        }

        // column breaker
        if (LevelLoader.instance.level >= 15)
        {
            columnBooster.SetActive(true);
            columnAmount.text = GameData.instance.GetColumnBreaker().ToString();
        }

        // rainbow breaker
        if (LevelLoader.instance.level >= 18)
        {
            rainbowBooster.SetActive(true);
            rainbowAmount.text = GameData.instance.GetRainbowBreaker().ToString();
        }

        // oven breaker
        if (LevelLoader.instance.level >= 25)
        {
            ovenBooster.SetActive(true);
            ovenAmount.text = GameData.instance.GetOvenBreaker().ToString();
        }

        // change help order in the hierarchy
        if (LevelLoader.instance.level == 7)
        {
            Help.instance.gameObject.transform.SetParent(Booster.instance.gameObject.transform);
            Help.instance.gameObject.transform.SetSiblingIndex(0);
        }
        else if (LevelLoader.instance.level == 12)
        {
            Help.instance.gameObject.transform.SetParent(Booster.instance.gameObject.transform);
            Help.instance.gameObject.transform.SetSiblingIndex(1);
        }
        else if (LevelLoader.instance.level == 15)
        {
            Help.instance.gameObject.transform.SetParent(Booster.instance.gameObject.transform);
            Help.instance.gameObject.transform.SetSiblingIndex(2);
        }
        else if (LevelLoader.instance.level == 18)
        {
            Help.instance.gameObject.transform.SetParent(Booster.instance.gameObject.transform);
            Help.instance.gameObject.transform.SetSiblingIndex(3);
        }
        else if (LevelLoader.instance.level == 25)
        {
            Help.instance.gameObject.transform.SetParent(Booster.instance.gameObject.transform);
            Help.instance.gameObject.transform.SetSiblingIndex(4);
        }
    }

    #region Single

    public void SingleBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        board.dropTime = 1;

        // hide help
        if (LevelLoader.instance.level == 7)
        {
            // hide step 1
            Help.instance.Hide();

            // show step 2
            if (Help.instance.step == 1)
            {
                var prefab = Instantiate(Resources.Load(Configure.Level7Step2())) as GameObject;
                prefab.name = "Level 7 Step 2";

                prefab.gameObject.transform.SetParent(Help.instance.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.current = prefab;

                Help.instance.step = 2;
            }
        }

        // check amount
        
        if (GameData.instance.GetSingleBreaker() <= 0)
        {
            // show booster popup
            ShowPopup(BOOSTER_TYPE.SINGLE_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.SINGLE_BREAKER);
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);
        }
    }

    #endregion

    #region Row

    public void RowBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        board.dropTime = 1;

        // hide help
        if (LevelLoader.instance.level == 12)
        {
            // hide step 1
            Help.instance.Hide();

            // show step 2
            if (Help.instance.step == 1)
            {
                var prefab = Instantiate(Resources.Load(Configure.Level12Step2())) as GameObject;
                prefab.name = "Level 12 Step 2";

                prefab.gameObject.transform.SetParent(Help.instance.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.current = prefab;

                Help.instance.step = 2;
            }
        }

        // check amount

        if (GameData.instance.GetRowBreaker() <= 0)
        {
            // show booster popup
            ShowPopup(BOOSTER_TYPE.ROW_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.ROW_BREAKER);
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.ROW_BREAKER);
        }
    }

    #endregion

    #region Column

    public void ColumnBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        board.dropTime = 1;

        // hide help
        if (LevelLoader.instance.level == 15)
        {
            // hide step 1
            Help.instance.Hide();

            // show step 2
            if (Help.instance.step == 1)
            {
                var prefab = Instantiate(Resources.Load(Configure.Level15Step2())) as GameObject;
                prefab.name = "Level 15 Step 2";

                prefab.gameObject.transform.SetParent(Help.instance.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.current = prefab;

                Help.instance.step = 2;
            }
        }

        // check amount

        if (GameData.instance.GetColumnBreaker() <= 0)
        {
            // show booster popup
            ShowPopup(BOOSTER_TYPE.COLUMN_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.COLUMN_BREAKER);
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);
        }
    }

    #endregion

    #region Rainbow

    public void RainbowBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        board.dropTime = 1;

        // hide help
        if (LevelLoader.instance.level == 18)
        {
            // hide step 1
            Help.instance.Hide();

            // show step 2
            if (Help.instance.step == 1)
            {
                var prefab = Instantiate(Resources.Load(Configure.Level18Step2())) as GameObject;
                prefab.name = "Level 18 Step 2";

                prefab.gameObject.transform.SetParent(Help.instance.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.current = prefab;

                Help.instance.step = 2;
            }
        }

        // check amount

        if (GameData.instance.GetRainbowBreaker() <= 0)
        {
            // show booster popup
            ShowPopup(BOOSTER_TYPE.RAINBOW_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);
        }
    }

    #endregion

    #region Oven

    public void OvenBoosterClick()
    {
        if (board.state != GAME_STATE.WAITING_USER_SWAP || board.lockSwap == true)
        {
            return;
        }

        AudioManager.instance.ButtonClickAudio();

        board.dropTime = 0;

        // hide help
        if (LevelLoader.instance.level == 25)
        {
            // hide step 1
            Help.instance.Hide();

            // show step 2
            if (Help.instance.step == 1)
            {
                var prefab = Instantiate(Resources.Load(Configure.Level25Step2())) as GameObject;
                prefab.name = "Level 25 Step 2";

                prefab.gameObject.transform.SetParent(Help.instance.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.current = prefab;

                Help.instance.step = 2;
            }
        }

        // check amount

        if (GameData.instance.GetOvenBreaker() <= 0)
        {
            // show booster popup
            ShowPopup(BOOSTER_TYPE.OVEN_BREAKER);

            return;
        }

        if (board.booster == BOOSTER_TYPE.NONE)
        {
            ActiveBooster(BOOSTER_TYPE.OVEN_BREAKER);
        }
        else
        {
            CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);
        }
    }

    #endregion

    #region Complete

    public void BoosterComplete()
    {
        if (board.booster == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.SINGLE_BREAKER);

            // reduce amount

            if (GameData.instance.GetSingleBreaker() > 0)
            {
                var amount = GameData.instance.GetSingleBreaker() - 1;
                GameData.instance.SaveSingleBreaker(amount);

                // change text

                singleAmount.text = amount.ToString();
            }
        }
        else if (board.booster == BOOSTER_TYPE.ROW_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.ROW_BREAKER);

            // reduce amount

            if (GameData.instance.GetRowBreaker() > 0)
            {
                var amount = GameData.instance.GetRowBreaker() - 1;
                GameData.instance.SaveRowBreaker(amount);

                // change text

                rowAmount.text = amount.ToString();
            }
        }
        else if (board.booster == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.COLUMN_BREAKER);

            // reduce amount

            if (GameData.instance.GetColumnBreaker() > 0)
            {
                var amount = GameData.instance.GetColumnBreaker() - 1;
                GameData.instance.SaveColumnBreaker(amount);

                // change text

                columnAmount.text = amount.ToString();
            }
        }
        else if (board.booster == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.RAINBOW_BREAKER);

            // reduce amount

            if (GameData.instance.GetRainbowBreaker() > 0)
            {
                var amount = GameData.instance.GetRainbowBreaker() - 1;
                GameData.instance.SaveRainbowBreaker(amount);

                // change text

                rainbowAmount.text = amount.ToString();
            }
        }
        else if (board.booster == BOOSTER_TYPE.OVEN_BREAKER)
        {
            CancelBooster(BOOSTER_TYPE.OVEN_BREAKER);

            // reduce amount

            if (GameData.instance.GetOvenBreaker() > 0)
            {
                var amount = GameData.instance.GetOvenBreaker() - 1;
                GameData.instance.SaveOvenBreaker(amount);

                // change text

                ovenAmount.text = amount.ToString();
            }
        }
    }

    #endregion

    #region Popup

    public void ShowPopup(BOOSTER_TYPE check)
    {
        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            singleBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            rowBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            columnBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            rainbowBoosterPopup.OpenPopup();
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            board.state = GAME_STATE.OPENING_POPUP;

            ovenBoosterPopup.OpenPopup();
        }
    }

    #endregion

    #region Booster

    public void ActiveBooster(BOOSTER_TYPE check)
    {
        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            board.booster = BOOSTER_TYPE.SINGLE_BREAKER;

            singleActive.SetActive(true);

            // interactable
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            board.booster = BOOSTER_TYPE.ROW_BREAKER;

            rowActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            board.booster = BOOSTER_TYPE.COLUMN_BREAKER;

            columnActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            board.booster = BOOSTER_TYPE.RAINBOW_BREAKER;

            rainbowActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;            
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            board.booster = BOOSTER_TYPE.OVEN_BREAKER;

            ovenActive.SetActive(true);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = false;
        }
    }

    public void CancelBooster(BOOSTER_TYPE check)
    {
        board.booster = BOOSTER_TYPE.NONE;

        if (check == BOOSTER_TYPE.SINGLE_BREAKER)
        {
            singleActive.SetActive(false);

            // interactable
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.ROW_BREAKER)
        {
            rowActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.COLUMN_BREAKER)
        {
            columnActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.RAINBOW_BREAKER)
        {
            rainbowActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;            
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            ovenActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
        else if (check == BOOSTER_TYPE.OVEN_BREAKER)
        {
            ovenActive.SetActive(false);

            singleActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            columnActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
            rainbowActive.transform.parent.GetComponent<AnimatedButton>().interactable = true;
        }
    }

    #endregion
}
