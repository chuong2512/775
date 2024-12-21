using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour 
{
    public static Help instance = null;

    [Header("Variables")]
    public int step;
    public GameObject current;

    [Header("Check")]
    public bool help;
    public bool onMap;

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
        // Map scene
        if (onMap == true)
        {
            
        }
        // Play scene
        else
        {
            // show help
            if (LevelLoader.instance.level == 1 || // match 3
                LevelLoader.instance.level == 2 || // match 4
                LevelLoader.instance.level == 3 || // T and L shapes
                LevelLoader.instance.level == 4 || // match 5
                LevelLoader.instance.level == 5 || // match 2 special treats
                LevelLoader.instance.level == 6 || // Gingerbread Man
                LevelLoader.instance.level == 7 || // Lollipop
                LevelLoader.instance.level == 9 || // Waffle
                LevelLoader.instance.level == 12 || // Rolling Pin
                LevelLoader.instance.level == 13 || // Collectible
                LevelLoader.instance.level == 15 || // Pastry Bag
                LevelLoader.instance.level == 16 || // Marshmallow
                LevelLoader.instance.level == 18 || // Magic Cookie
                LevelLoader.instance.level == 25 || // Oven Mitt
                LevelLoader.instance.level == 31 || // Chocolate
                LevelLoader.instance.level == 61 || // Cage
                LevelLoader.instance.level == 76) // Rock Candy 
            {
                help = true;
            }
            else
            {
                SelfDisactive();
            }
        }
    }

    public void Show()
    {
        // don't show help when level is passed
        if (GameData.instance.GetOpendedLevel() > LevelLoader.instance.level)
        {
            SelfDisactive();
            return;
        }

        GameObject prefab = null;

        if (LevelLoader.instance.level == 1)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level1Step1())) as GameObject;
                prefab.name = "Level 1 Step 1";

                step = 1;
            }
            else if (step == 2)
            {
                prefab = Instantiate(Resources.Load(Configure.Level1Step3())) as GameObject;
                prefab.name = "Level 1 Step 3";

                // finish
                step = 3;
            }
        }
        else if (LevelLoader.instance.level == 2)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level2Step1())) as GameObject;
                prefab.name = "Level 2 Step 1";

                step = 1;
            }
            else if (step == 1)
            {
                prefab = Instantiate(Resources.Load(Configure.Level2Step2())) as GameObject;
                prefab.name = "Level 2 Step 2";

                step = 2;
            }
            else if (step == 2)
            {
                prefab = Instantiate(Resources.Load(Configure.Level2Step3())) as GameObject;
                prefab.name = "Level 2 Step 3";

                step = 3;
            }
        }
        else if (LevelLoader.instance.level == 3)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level3Step1())) as GameObject;
                prefab.name = "Level 3 Step 1";

                step = 1;
            }
            else if (step == 1)
            {
                prefab = Instantiate(Resources.Load(Configure.Level3Step2())) as GameObject;
                prefab.name = "Level 3 Step 2";

                step = 2;
            }
            else if (step == 2)
            {
                prefab = Instantiate(Resources.Load(Configure.Level3Step3())) as GameObject;
                prefab.name = "Level 3 Step 3";

                step = 3;
            }
            else if (step == 3)
            {
                prefab = Instantiate(Resources.Load(Configure.Level3Step4())) as GameObject;
                prefab.name = "Level 3 Step 4";

                step = 4;
            }
            else if (step == 4)
            {
                prefab = Instantiate(Resources.Load(Configure.Level3Step5())) as GameObject;
                prefab.name = "Level 3 Step 5";

                step = 5;
            }
        }
        else if (LevelLoader.instance.level == 4)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level4Step1())) as GameObject;
                prefab.name = "Level 4 Step 1";

                step = 1;
            }
            else if (step == 1)
            {
                prefab = Instantiate(Resources.Load(Configure.Level4Step2())) as GameObject;
                prefab.name = "Level 4 Step 2";

                step = 2;
            }
            else if (step == 2)
            {
                prefab = Instantiate(Resources.Load(Configure.Level4Step3())) as GameObject;
                prefab.name = "Level 4 Step 3";

                step = 3;
            }
        }
        else if (LevelLoader.instance.level == 5)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level5Step1())) as GameObject;
                prefab.name = "Level 5 Step 1";

                step = 1;
            }
            else if (step == 1)
            {
                prefab = Instantiate(Resources.Load(Configure.Level5Step2())) as GameObject;
                prefab.name = "Level 5 Step 2";

                step = 2;
            }
        }
        else if (LevelLoader.instance.level == 6)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level6Step1())) as GameObject;
                prefab.name = "Level 6 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 7)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level7Step1())) as GameObject;
                prefab.name = "Level 7 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 9)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level9Step1())) as GameObject;
                prefab.name = "Level 9 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 12)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level12Step1())) as GameObject;
                prefab.name = "Level 12 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 13)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level13Step1())) as GameObject;
                prefab.name = "Level 13 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 15)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level15Step1())) as GameObject;
                prefab.name = "Level 15 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 16)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level16Step1())) as GameObject;
                prefab.name = "Level 16 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 18)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level18Step1())) as GameObject;
                prefab.name = "Level 18 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 25)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level25Step1())) as GameObject;
                prefab.name = "Level 25 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 31)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level31Step1())) as GameObject;
                prefab.name = "Level 31 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 61)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level61Step1())) as GameObject;
                prefab.name = "Level 61 Step 1";

                step = 1;
            }
        }
        else if (LevelLoader.instance.level == 76)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level76Step1())) as GameObject;
                prefab.name = "Level 76 Step 1";

                step = 1;
            }
        }

        if (prefab != null)
        {
            prefab.gameObject.transform.SetParent(gameObject.transform);
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;

            current = prefab;
        }        
    }

    public void ShowOnMap()
    {
        // don't show help when level is passed
        if (GameData.instance.GetOpendedLevel() > LevelLoader.instance.level)
        {
            return;
        }

        GameObject prefab = null;

        // Begin 5 moves
        if (LevelLoader.instance.level == 10)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level10BeginStep1())) as GameObject;
                prefab.name = "Level 10 Begin Step 1";

                step = 1;
            }
        }
        // begin Magic Cookie
        else if (LevelLoader.instance.level == 20)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level20BeginStep1())) as GameObject;
                prefab.name = "Level 20 Begin Step 1";

                step = 1;
            }
        }
        // begin Magic Bomb
        else if (LevelLoader.instance.level == 23)
        {
            if (step == 0)
            {
                prefab = Instantiate(Resources.Load(Configure.Level23BeginStep1())) as GameObject;
                prefab.name = "Level 23 Begin Step 1";

                step = 1;
            }
        }

        if (prefab != null)
        {
            prefab.gameObject.transform.SetParent(gameObject.transform);
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;

            current = prefab;
        }
    }

    public void Hide()
    {
        if (LevelLoader.instance.level == 1 || 
            LevelLoader.instance.level == 2 ||
            LevelLoader.instance.level == 3 ||
            LevelLoader.instance.level == 4 ||
            LevelLoader.instance.level == 5 ||
            LevelLoader.instance.level == 6 ||
            LevelLoader.instance.level == 7 ||
            LevelLoader.instance.level == 9 ||
            LevelLoader.instance.level == 12 ||
            LevelLoader.instance.level == 15 ||
            LevelLoader.instance.level == 18 ||
            LevelLoader.instance.level == 25)
        {
            if (current != null)
            {
                current.SetActive(false);
            }            
        }

        if (LevelLoader.instance.level == 6)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 7 && step == 2)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 9)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 12 && step == 2)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 15 && step == 2)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 16)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 18 && step == 2)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 25 && step == 2)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 31)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 61)
        {
            step = 0;
            SelfDisactive();
        }
        else if (LevelLoader.instance.level == 76)
        {
            step = 0;
            SelfDisactive();
        }
    }

    public void HideOnSwapBack()
    {
        if (LevelLoader.instance.level == 2 ||
            LevelLoader.instance.level == 3)
        {
            step = 0;
            SelfDisactive();
        }
    }

    public void SelfDisactive()
    {
        if (GameObject.Find("Board"))
        {
            GameObject.Find("Board").GetComponent<Board>().Hint();
        } 

        help = false;        

        gameObject.SetActive(false);        
    }
}
