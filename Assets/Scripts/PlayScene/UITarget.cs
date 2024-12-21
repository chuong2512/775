using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITarget : MonoBehaviour 
{
    public Image target1;
    public Text target1Amount;
    public Image target1Tick;

    public Image target2;
    public Text target2Amount;
    public Image target2Tick;

    public Image target3;
    public Text target3Amount;
    public Image target3Tick;

    public Image target4;
    public Text target4Amount;
    public Image target4Tick;

	void Start () 
    {
        for (int i = 1; i <= 4; i++)
        {
            Image target = null;
            Text targetAmount = null;
            Image targetTick = null;

            GameObject prefab = null;

            TARGET_TYPE type = TARGET_TYPE.NONE;
            int amount = 0;
            int color = 0;

            switch (i)
            {
                case 1:
                    target = target1;
                    targetAmount = target1Amount;
                    targetTick = target1Tick;

                    type = LevelLoader.instance.target1Type;
                    amount = LevelLoader.instance.target1Amount;
                    color = LevelLoader.instance.target1Color;
                    break;
                case 2:
                    target = target2;
                    targetAmount = target2Amount;
                    targetTick = target2Tick;

                    type = LevelLoader.instance.target2Type;
                    amount = LevelLoader.instance.target2Amount;
                    color = LevelLoader.instance.target2Color;
                    break;
                case 3:
                    target = target3;
                    targetAmount = target3Amount;
                    targetTick = target3Tick;

                    type = LevelLoader.instance.target3Type;
                    amount = LevelLoader.instance.target3Amount;
                    color = LevelLoader.instance.target3Color;
                    break;
                case 4:
                    target = target4;
                    targetAmount = target4Amount;
                    targetTick = target4Tick;

                    type = LevelLoader.instance.target4Type;
                    amount = LevelLoader.instance.target4Amount;
                    color = LevelLoader.instance.target4Color;
                    break;
            }
            // 1 - score : currently not support
            // 2 - cookie
            if (type == TARGET_TYPE.COOKIE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                switch (color)
                {
                    case 1:
                        prefab = Resources.Load(Configure.Cookie1()) as GameObject;
                        break;
                    case 2:
                        prefab = Resources.Load(Configure.Cookie2()) as GameObject;
                        break;
                    case 3:
                        prefab = Resources.Load(Configure.Cookie3()) as GameObject;
                        break;
                    case 4:
                        prefab = Resources.Load(Configure.Cookie4()) as GameObject;
                        break;
                    case 5:
                        prefab = Resources.Load(Configure.Cookie5()) as GameObject;
                        break;
                    case 6:
                        prefab = Resources.Load(Configure.Cookie6()) as GameObject;
                        break;
                }

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 3 - marshmallow
            else if (type == TARGET_TYPE.MARSHMALLOW)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.Marshmallow()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 4 -waffle
            else if (type == TARGET_TYPE.WAFFLE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.Waffle1()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0);
            }
            // 5 - collectible
            else if (type == TARGET_TYPE.COLLECTIBLE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                switch (color)
                {
                    case 1:
                        prefab = Resources.Load(Configure.Collectible1()) as GameObject;
                        break;
                    case 2:
                        prefab = Resources.Load(Configure.Collectible2()) as GameObject;
                        break;
                    case 3:
                        prefab = Resources.Load(Configure.Collectible3()) as GameObject;
                        break;
                    case 4:
                        prefab = Resources.Load(Configure.Collectible4()) as GameObject;
                        break;
                    case 5:
                        prefab = Resources.Load(Configure.Collectible5()) as GameObject;
                        break;
                    case 6:
                        prefab = Resources.Load(Configure.Collectible6()) as GameObject;
                        break;
                    case 7:
                        prefab = Resources.Load(Configure.Collectible7()) as GameObject;
                        break;
                    case 8:
                        prefab = Resources.Load(Configure.Collectible8()) as GameObject;
                        break;
                    case 9:
                        prefab = Resources.Load(Configure.Collectible9()) as GameObject;
                        break;
                    case 10:
                        prefab = Resources.Load(Configure.Collectible10()) as GameObject;
                        break;
                    case 11:
                        prefab = Resources.Load(Configure.Collectible11()) as GameObject;
                        break;
                    case 12:
                        prefab = Resources.Load(Configure.Collectible12()) as GameObject;
                        break;
                    case 13:
                        prefab = Resources.Load(Configure.Collectible13()) as GameObject;
                        break;
                    case 14:
                        prefab = Resources.Load(Configure.Collectible14()) as GameObject;
                        break;
                    case 15:
                        prefab = Resources.Load(Configure.Collectible15()) as GameObject;
                        break;
                    case 16:
                        prefab = Resources.Load(Configure.Collectible16()) as GameObject;
                        break;
                    case 17:
                        prefab = Resources.Load(Configure.Collectible17()) as GameObject;
                        break;
                    case 18:
                        prefab = Resources.Load(Configure.Collectible18()) as GameObject;
                        break;
                    case 19:
                        prefab = Resources.Load(Configure.Collectible19()) as GameObject;
                        break;
                    case 20:
                        prefab = Resources.Load(Configure.Collectible20()) as GameObject;
                        break;
                }

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(1, 1, 0);
            }
            // 6 - col_row_breaker
            else if (type == TARGET_TYPE.COLUMN_ROW_BREAKER)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.ColumnRowBreaker()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 7 - bomb_breaker
			else if (type == TARGET_TYPE.BOMB_BREAKER) 
			{
				target.gameObject.SetActive(true);
				targetAmount.gameObject.SetActive(true);
				targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configure.GenericBombBreaker()) as GameObject;

				if (prefab != null)
				{
					target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				targetAmount.text = amount.ToString();
			}
            // 8 - x_breaker
			else if (type == TARGET_TYPE.X_BREAKER)
			{
				target.gameObject.SetActive(true);
				targetAmount.gameObject.SetActive(true);
				targetTick.gameObject.SetActive(false);

				prefab = Resources.Load(Configure.GenericXBreaker()) as GameObject;

				if (prefab != null)
				{
					target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				targetAmount.text = amount.ToString();
			}
            // 9 - cage
            else if (type == TARGET_TYPE.CAGE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.Cage1()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();

                target.rectTransform.localScale = new Vector3(0.75f, 0.75f, 0);
            }
            // 10 - rainbow
            else if (type == TARGET_TYPE.RAINBOW)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.CookieRainbow()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 11 - gingerbread
            else if (type == TARGET_TYPE.GINGERBREAD)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.GingerbreadGeneric()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 12 - chocolate
            else if (type == TARGET_TYPE.CHOCOLATE)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.Chocolate1()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            // 13 - rock candy
            else if (type == TARGET_TYPE.ROCK_CANDY)
            {
                target.gameObject.SetActive(true);
                targetAmount.gameObject.SetActive(true);
                targetTick.gameObject.SetActive(false);

                prefab = Resources.Load(Configure.RockCandyGeneric()) as GameObject;

                if (prefab != null)
                {
                    target.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                targetAmount.text = amount.ToString();
            }
            else
            {
                target.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
	}

    public void UpdateTargetAmount(int target)
    {
        if (target == 1)
        {
            target1Amount.text = (int.Parse(target1Amount.text) - 1).ToString();

            if (int.Parse(target1Amount.text) <= 0)
            {
                target1Amount.gameObject.SetActive(false);
                target1Tick.gameObject.SetActive(true);
            }
        }
        else if (target == 2)
        {
            target2Amount.text = (int.Parse(target2Amount.text) - 1).ToString();

            if (int.Parse(target2Amount.text) <= 0)
            {
                target2Amount.gameObject.SetActive(false);
                target2Tick.gameObject.SetActive(true);
            }
        }
        else if (target == 3)
        {
            target3Amount.text = (int.Parse(target3Amount.text) - 1).ToString();

            if (int.Parse(target3Amount.text) <= 0)
            {
                target3Amount.gameObject.SetActive(false);
                target3Tick.gameObject.SetActive(true);
            }
        }
        else if (target == 4)
        {
            target4Amount.text = (int.Parse(target4Amount.text) - 1).ToString();

            if (int.Parse(target4Amount.text) <= 0)
            {
                target4Amount.gameObject.SetActive(false);
                target4Tick.gameObject.SetActive(true);
            }
        }
    }
}
