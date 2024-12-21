using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIWinPopup : MonoBehaviour 
{
    public Text levelText;
    public Text scoreText;
    public Text bonusText;
    public Image cake;
    public Image star1;
    public Image star2;
    public Image star3;
    public Text buttonText;

	void Start () 
    {
        var board = GameObject.Find("Board").GetComponent<Board>();

        var star = board.star;

        levelText.text = "Level " + LevelLoader.instance.level.ToString();

        //switch (star)
        //{
        //    case 1:
        //        star1.gameObject.SetActive(true);
        //        star2.gameObject.SetActive(false);
        //        star3.gameObject.SetActive(false);
        //        break;
        //    case 2:
        //        star1.gameObject.SetActive(true);
        //        star2.gameObject.SetActive(true);
        //        star3.gameObject.SetActive(false);
        //        break;
        //    case 3:
        //        star1.gameObject.SetActive(true);
        //        star2.gameObject.SetActive(true);
        //        star3.gameObject.SetActive(true);
        //        break;
        //    default:
        //        star1.gameObject.SetActive(false);
        //        star2.gameObject.SetActive(false);
        //        star3.gameObject.SetActive(false);
        //        break;
        //}

        // hide stars
        star1.gameObject.SetActive(false);
        star2.gameObject.SetActive(false);
        star3.gameObject.SetActive(false);

        // start coroutine to show stars
        StartCoroutine(ShowStars());

        // hide the button
        buttonText.gameObject.transform.parent.gameObject.SetActive(false);

        if (star == 1)
        {
            bonusText.text = Configure.instance.bonus1Star.ToString();
        }
        else if (star == 2)
        {
            bonusText.text = Configure.instance.bonus2Star.ToString();
        }
        else if (star == 3)
        {
            bonusText.text = Configure.instance.bonus3Star.ToString();
        }
        else
        {
            bonusText.text = "0";
        }

        scoreText.text = "Score: " + board.score.ToString();

        var name = "cake_" + LevelLoader.instance.cake + "_4";
        cake.sprite = Resources.Load<Sprite>(Configure.Cake(name));

        // the button text change from next to close when the last level
        if (LevelLoader.instance.level == Configure.instance.maxLevel)
        {
            buttonText.text = "Close";
        }

        // add 1 life
        //print("Win popup add 1 life");

        Configure.instance.life += 1;

        if (Configure.instance.life > Configure.instance.maxLife)
        {
            Configure.instance.life = Configure.instance.maxLife;
        }
	}

    public void MapAutoPopup()
    {
        Configure.instance.autoPopup = LevelLoader.instance.level + 1;
    }

    IEnumerator ShowStars()
    {
        yield return new WaitForSeconds(1f);

        var board = GameObject.Find("Board").GetComponent<Board>();
        var star = board.star;

        GameObject explosion1;
        GameObject explosion2;
        GameObject explosion3;

        switch (star)
        {
            case 1:
                star1.gameObject.SetActive(true);
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
                break;
            case 2:
                star1.gameObject.SetActive(true);
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star2.gameObject.SetActive(true);
                AudioManager.instance.Star2Audio();
                explosion2 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion2.transform.SetParent(star2.gameObject.transform, false);

                star3.gameObject.SetActive(false);
                break;
            case 3:
                star1.gameObject.SetActive(true);
                AudioManager.instance.Star1Audio();
                explosion1 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion1.transform.SetParent(star1.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star2.gameObject.SetActive(true);
                AudioManager.instance.Star2Audio();
                explosion2 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion2.transform.SetParent(star2.gameObject.transform, false);

                yield return new WaitForSeconds(0.5f);

                star3.gameObject.SetActive(true);
                AudioManager.instance.Star3Audio();
                explosion3 = Instantiate(Resources.Load(Configure.StarExplode()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                explosion3.transform.SetParent(star3.gameObject.transform, false);
                break;
            default:
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
                break;
        }

        yield return new WaitForSeconds(1f);

        board.ShowAds();

        yield return new WaitForSeconds(1f);

        buttonText.gameObject.transform.parent.gameObject.SetActive(true);
    }

    public void ShareOnFacebook()
    {
        Debug.Log("ShareOnFacebookClicked");

       
    }
}
