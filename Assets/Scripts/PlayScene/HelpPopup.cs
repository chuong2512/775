using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HelpPopup : MonoBehaviour 
{
    public Image arrow;

    void Start()
    {
        if (arrow != null)
        {
            if (LevelLoader.instance.level == 1 ||
                LevelLoader.instance.level == 2 ||
                LevelLoader.instance.level == 3 ||
                LevelLoader.instance.level == 4 ||
                LevelLoader.instance.level == 5 ||
                LevelLoader.instance.level == 6 ||
                LevelLoader.instance.level == 7 ||
                LevelLoader.instance.level == 10 ||
                LevelLoader.instance.level == 12 ||
                LevelLoader.instance.level == 15 ||
                LevelLoader.instance.level == 18 ||
                LevelLoader.instance.level == 20 ||
                LevelLoader.instance.level == 23 ||
                LevelLoader.instance.level == 25)
            {
                iTween.MoveBy(arrow.gameObject, iTween.Hash(
                    "y", -1,
                    "looptype", iTween.LoopType.loop,
                    "time", 1.5f
                ));
            }            
        }

        if (LevelLoader.instance.level == 25 && Help.instance.step ==2)
        {
            if (arrow != null)
            {
                int index = arrow.gameObject.transform.GetSiblingIndex();
                GameObject nextArrow = arrow.gameObject.transform.parent.GetChild(index + 1).gameObject;
                if (nextArrow != null)
                {
                    iTween.MoveBy(nextArrow, iTween.Hash(
                        "y", -1,
                        "looptype", iTween.LoopType.loop,
                        "time", 1.5f
                    ));
                }
            }
            
        }
    }

    #region Next

    public void NextButtonDown()
    {
        Configure.instance.touchIsSwallowed = true;
    }

    public void NextButtonUp()
    {
        Configure.instance.touchIsSwallowed = false;

        if (LevelLoader.instance.level == 1)
        {
            if (Help.instance.step == 1)
            {
                // show step 2                

                var prefab = Instantiate(Resources.Load(Configure.Level1Step2())) as GameObject;
                prefab.name = "Level 1 Step 2";

                prefab.gameObject.transform.SetParent(gameObject.transform.parent.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.step = 2;
                Help.instance.current = prefab;

                // hide step 1
                gameObject.SetActive(false);
            }
        }
        else if (LevelLoader.instance.level == 13)
        {
            if (Help.instance.step == 1)
            {
                // show step 2                

                var prefab = Instantiate(Resources.Load(Configure.Level13Step2())) as GameObject;
                prefab.name = "Level 13 Step 2";

                prefab.gameObject.transform.SetParent(gameObject.transform.parent.gameObject.transform);
                prefab.GetComponent<RectTransform>().localScale = Vector3.one;

                Help.instance.step = 2;
                Help.instance.current = prefab;

                // hide step 1
                gameObject.SetActive(false);
            }
        }
    }

    #endregion Next

    #region Skip

    public void SkipButtonDown()
    {
        Configure.instance.touchIsSwallowed = true;
    }

    public void SkipButtonUp()
    {
        Configure.instance.touchIsSwallowed = false;
        
        Help.instance.step = 0;

        Help.instance.SelfDisactive();
    }

    #endregion Skip

    #region Mask

    public void MaskDown()
    {
        Configure.instance.touchIsSwallowed = true;
    }

    public void MaskUp()
    {
        Configure.instance.touchIsSwallowed = false;
    }

    #endregion
}
