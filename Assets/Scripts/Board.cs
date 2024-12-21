using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
// Unity Ads
using UnityEngine.Advertisements;
#endif

public class Board : MonoBehaviour 
{
    // public
    [Header("Nodes")]
    public List<Node> nodes = new List<Node>();

    [Header("Board variables")]    
    public GAME_STATE state;
    public bool lockSwap;
    public int moveLeft;
    public int dropTime;
    public int score;
    public int star;
    public int target1Left;
    public int target2Left;
    public int target3Left;
    public int target4Left;

    [Header("Booster")]
    public BOOSTER_TYPE booster;
    public List<Item> boosterItems = new List<Item>();
    public Item ovenTouchItem;

    [Header("Check")]
    public int destroyingItems;
    public int droppingItems;
    public int flyingItems;
    public int matching;
    [Header("")]
    public bool movingGingerbread;
    public bool generatingGingerbread;
    public bool skipGenerateGingerbread;
    public bool showingInspiringPopup;
    public int skipGingerbreadCount;
    
    [Header("Item Lists")]
    public List<Item> changingList;
    public List<Item> sameColorList;

    [Header("Swap")]
    public Item touchedItem;
    public Item swappedItem;

    // UI
    [Header("UI")]
    public GameObject target1;
    public GameObject target2;
    public GameObject target3;
    public GameObject target4;
    public UITarget UITarget;
    public UITop UITop;

    // Popup
    [Header("Popup")]
    public PopupOpener targetPopup;
    public PopupOpener completedPopup;
    public PopupOpener winPopup;
    public PopupOpener losePopup;
    public PopupOpener noMatchesPopup;
    public PopupOpener plus5MovesPopup;
    public PopupOpener amazingPopup;
    public PopupOpener excellentPopup;
    public PopupOpener greatPopup;

    // hint
    [Header("Hint")]
    public int checkHintCall;
    public int showHintCall;
    public List<Item> hintItems = new List<Item>();

    // private
    Vector3 firstNodePosition;

    void Awake()
    {
        // debug
        if (LevelLoader.instance.level == 0)
        {
            LevelLoader.instance.LoadLevel(true);
        }            
    }

	void Start () 
    {
        state = GAME_STATE.PREPARING_LEVEL;
        moveLeft = LevelLoader.instance.moves;
        target1Left = LevelLoader.instance.target1Amount;
        target2Left = LevelLoader.instance.target2Amount;
        target3Left = LevelLoader.instance.target3Amount;
        target4Left = LevelLoader.instance.target4Amount;

        GenerateBoard();

        BeginBooster();

        // reset color of the random cookie to make sure there is no match in the original board
        GenerateNoMatches();

        // open target popup
        TargetPopup();
	}
	
	void Update () 
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        if (state == GAME_STATE.WAITING_USER_SWAP && lockSwap == false && moveLeft > 0)
        {
            if (Configure.instance.touchIsSwallowed == true && GameObject.Find("Help") != null && GameObject.Find("Help").activeSelf == true)
            {
                return;
            }

            // no booster
            if (booster == BOOSTER_TYPE.NONE)
            {
                // mouse down
                if (Input.GetMouseButtonDown(0))
                {
                    // hit the collier
                    Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    if (hit != null)
                    {
                        Item item = hit.gameObject.GetComponent<Item>();
                        if (item != null)
                        {
                            if (item.Movable())
                            {
                                item.drag = true;
                                item.mousePostion = item.GetMousePosition();
                                item.deltaPosition = Vector3.zero;

                                movingGingerbread = false;
                                generatingGingerbread = false;
                                skipGenerateGingerbread = false;
                            }
                        }
                    }
                }
                // mouse up
                else if (Input.GetMouseButtonUp(0))
                {
                    Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    if (hit != null)
                    {
                        var item = hit.gameObject.GetComponent<Item>();
                        if (item != null)
                        {
                            item.drag = false;
                            item.swapDirection = Vector3.zero;
                        }
                    }
                }
            }
            // use booster
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Collider2D hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    if (hit != null)
                    {
                        var item = hit.gameObject.GetComponent<Item>();
                        if (item != null)
                        {
                            DestroyBoosterItems(item);
                        }
                    }
                }

            }

        } // if state is WAITING_USER_SWAP

        // fix freezing board
        if (state == GAME_STATE.DESTROYING_ITEMS)
        {
            if (lockSwap == false && destroyingItems == 0 && droppingItems == 0 && flyingItems == 0 && matching == 0)
            {
                state = GAME_STATE.WAITING_USER_SWAP;
            }
        }
    }

    #region Board

    void GenerateBoard()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                GameObject node = Instantiate(Resources.Load(Configure.NodePrefab())) as GameObject;
                node.transform.SetParent(gameObject.transform, false);
                node.name = "Node " + order;
                node.GetComponent<Node>().board = this;
                node.GetComponent<Node>().i = i;
                node.GetComponent<Node>().j = j;

                nodes.Add(node.GetComponent<Node>());
            } // end for j
        } // end for i

        GenerateTileLayer();
        GenerateTileBorder();

        GenerateWaffleLayer();

        GenerateItemLayer();

        GenerateCageLayer();

        GenerateCollectibleBoxByColumn();
        GenerateCollectibleBoxByNode();
    }

    void GenerateTileLayer()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var tileLayerData = LevelLoader.instance.tileLayerData;

                GameObject tile = null;

                switch (tileLayerData[order])
                {
                    case TILE_TYPE.NONE:
                        tile = Instantiate(Resources.Load(Configure.NoneTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.PASS_THROUGH:
                        tile = Instantiate(Resources.Load(Configure.NoneTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.LIGHT_TILE:
                        tile = Instantiate(Resources.Load(Configure.LightTilePrefab())) as GameObject;
                        break;
                    case TILE_TYPE.DARD_TILE:
                        tile = Instantiate(Resources.Load(Configure.DarkTilePrefab())) as GameObject;
                        break;
                }

                if (tile)
                {
                    tile.transform.SetParent(nodes[order].gameObject.transform);
                    tile.name = "Tile";
                    tile.transform.localPosition = NodeLocalPosition(i, j);
                    tile.GetComponent<Tile>().type = tileLayerData[order];
                    tile.GetComponent<Tile>().node = nodes[order];

                    //if (tile.GetComponent<SpriteRenderer>()) tile.GetComponent<SpriteRenderer>().enabled = false;

                    nodes[order].tile = tile.GetComponent<Tile>();
                }

            } // end for j
        } // end for i
    }

    void GenerateTileBorder()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                nodes[order].tile.SetBorder();
            }
        }
    }
    
    // waffle
    void GenerateWaffleLayer()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var waffleLayerData = LevelLoader.instance.waffleLayerData;

                GameObject waffle = null;

                switch (waffleLayerData[order])
                {
                    case WAFFLE_TYPE.WAFFLE_1:
                        waffle = Instantiate(Resources.Load(Configure.Waffle1())) as GameObject;
                        break;
                    case WAFFLE_TYPE.WAFFLE_2:
                        waffle = Instantiate(Resources.Load(Configure.Waffle2())) as GameObject;
                        break;
                    case WAFFLE_TYPE.WAFFLE_3:
                        waffle = Instantiate(Resources.Load(Configure.Waffle3())) as GameObject;
                        break;
                }

                if (waffle)
                {
                    waffle.transform.SetParent(nodes[order].gameObject.transform);
                    waffle.name = "Waffle";
                    waffle.transform.localPosition = NodeLocalPosition(i, j);
                    waffle.GetComponent<Waffle>().type = waffleLayerData[order];
                    waffle.GetComponent<Waffle>().node = nodes[order];

                    nodes[order].waffle = waffle.GetComponent<Waffle>();
                }
            }
        }
    }

    void GenerateItemLayer()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var itemLayerData = LevelLoader.instance.itemLayerData;

                if (nodes[order].CanStoreItem())
                {
                    nodes[order].GenerateItem(itemLayerData[order]);

                    // add mask
                    var mask = Instantiate(Resources.Load(Configure.Mask())) as GameObject;
                    mask.transform.SetParent(nodes[order].transform);
                    mask.transform.localPosition = NodeLocalPosition(i, j);
                    mask.name = "Mask";
                }
            }
        }
    }

    void GenerateCageLayer()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                var cageLayerData = LevelLoader.instance.cageLayerData;

                GameObject cage = null;

                switch (cageLayerData[order])
                {
                    case CAGE_TYPE.CAGE_1:
                        cage = Instantiate(Resources.Load(Configure.Cage1())) as GameObject;
                        break;
                }

                if (cage)
                {
                    cage.transform.SetParent(nodes[order].gameObject.transform);
                    cage.name = "Cage";
                    cage.transform.localPosition = NodeLocalPosition(i, j);
                    cage.GetComponent<Cage>().type = cageLayerData[order];
                    cage.GetComponent<Cage>().node = nodes[order];

                    nodes[order].cage = cage.GetComponent<Cage>();
                }
            }
        }
    }

    void GenerateCollectibleBoxByColumn()
    {
        if (LevelLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return;
        }

        var row = LevelLoader.instance.row;

        foreach (var column in LevelLoader.instance.collectibleCollectColumnMarkers)
        {
            var node = GetNode(row - 1, column);

            if (node != null && node.CanStoreItem() == true)
            {
                var box = Instantiate(Resources.Load(Configure.CollectibleBox())) as GameObject;

                if (box)
                {
                    box.transform.SetParent(node.gameObject.transform);
                    box.name = "Box";
                    box.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0, -1 * NodeSize() + 0.2f, 0);
                }
            }
        }
    }

    void GenerateCollectibleBoxByNode()
    {
        if (LevelLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return;
        }

        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var order = NodeOrder(i, j);

                if (LevelLoader.instance.collectibleCollectNodeMarkers.Contains(order))
                {
                    var node = GetNode(i, j);

                    if (node != null)
                    {
                        var box = Instantiate(Resources.Load(Configure.CollectibleBox())) as GameObject;

                        if (box)
                        {
                            box.transform.SetParent(node.gameObject.transform);
                            box.name = "Box";
                            box.transform.localPosition = NodeLocalPosition(node.i, node.j) + new Vector3(0, -1 * NodeSize() + 0.2f, 0);
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region Begin

    void BeginBooster()
    {
        if (Configure.instance.beginFiveMoves == true)
        {
            //Configure.instance.beginFiveMoves = false;

            GameData.instance.SaveBeginFiveMoves(GameData.instance.GetBeginFiveMoves() - 1);

            moveLeft += Configure.instance.plusMoves;
        }

        if (Configure.instance.beginRainbow == true)
        {
            Configure.instance.beginRainbow = false;

            GameData.instance.SaveBeginRainbow(GameData.instance.GetBeginRainbow() - 1);

            var items = GetListItems();
            var cookies = new List<Item>();

            foreach(var item in items)
            {
                if (item != null && item.IsCookie() && item.Movable())
                {
                    cookies.Add(item);
                }
            }

            var cookie = cookies[Random.Range(0, cookies.Count - 1)];

            cookie.ChangeToRainbow();
        }

        if (Configure.instance.beginBombBreaker == true)
        {
            Configure.instance.beginBombBreaker = false;

            GameData.instance.SaveBeginBombBreaker(GameData.instance.GetBeginBombBreaker() - 1);

            var items = GetListItems();
            var cookies = new List<Item>();

            foreach (var item in items)
            {
                if (item != null && item.IsCookie() && item.Movable())
                {
                    cookies.Add(item);
                }
            }

            var cookie = cookies[Random.Range(0, cookies.Count - 1)];

            cookie.ChangeToBombBreaker();
        }
    }

    #endregion

    #region Utility
    Vector3 CalculateFirstNodePosition()
    {
        var width = NodeSize();
        var height = NodeSize();
        var column = LevelLoader.instance.column;
        var row = LevelLoader.instance.row;

        var offset = new Vector3(0, -1, 0);

        return (new Vector3(-((column - 1) * width / 2), (row - 1) * height / 2, 0) + offset);
    }

    public float NodeSize()
    {
        return 0.96f;
    }

    public Vector3 NodeLocalPosition(int i, int j)
    {
        var width = NodeSize();
        var height = NodeSize();

        if (firstNodePosition == Vector3.zero)
        {
            firstNodePosition = CalculateFirstNodePosition();
        }

        var x = firstNodePosition.x + j * width;
        var y = firstNodePosition.y - i * height;

        return new Vector3(x, y, 0);
    }

    public int NodeOrder(int i, int j)
    {
        return (i * LevelLoader.instance.column + j);
    }

    public Node GetNode(int row, int column)
    {
        if (row < 0 || row >= LevelLoader.instance.row || column < 0 || column >= LevelLoader.instance.column)
        {
            return null;
        }
        return nodes[row * LevelLoader.instance.column + column];
    }  

    Vector3 ColumnFirstItemPosition(int i, int j)
    {
        Node node = GetNode(i, j);

        if (node != null)
        {
            var item = node.item;

            if (item != null)
            {
                return item.gameObject.transform.position;
            }
            else
            {
                return ColumnFirstItemPosition(i + 1, j);
            }
        }
        else
        {
            return Vector3.zero;
        }
    }

    // return a list of items
    public List<Item> GetListItems()
    {
        var items = new List<Item>();

        foreach (var node in nodes)
        {
            if (node != null)
            {
                items.Add(node.item);
            }
        }

        return items;
    }

    #endregion

    #region Match

    // re-generate the board to make sure there is no "pre-matches"
    void GenerateNoMatches()
    {
        //Debug.Log("Start generating matches");

        var combines = GetMatches();

        do
        {
            foreach (var combine in combines)
            {
                int i = 0;
                foreach (var item in combine)
                {
                    if (item != null)
                    {
                        // only re-generate color for random item
                        if (item.OriginCookieType() == ITEM_TYPE.COOKIE_RAMDOM)
                        {
                            item.GenerateColor(item.color + i);
                            i++;
                        }
                    }
                }
            }
            combines = GetMatches();

        } while (combines.Count > 0);

        //Debug.Log("End generating matches");
    }

    // return the list of matches on the board
    public List<List<Item>> GetMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 3)
    {
        var combines = new List<List<Item>>();

        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                if (GetNode(i, j) != null)
                {
                    List<Item> combine = GetNode(i, j).FindMatches(direction, matches);

                    // combine can be null
                    if (combine != null)
                    {
                        if (combine.Count >= matches)
                        {
                            combines.Add(combine);
                        }
                    }
                }
            }
        }

        return combines;
    }

    public void FindMatches()
    {
        //print("find matches");

        StartCoroutine(DestroyMatches());
    }

    // destroy the matches on the board
    IEnumerator DestroyMatches()
    {
        matching++;

        while (true)
        {
            var combines = GetMatches();
            
            //Debug.Log("Number of combines: " + combines.Count);

            foreach (var combine in combines)
            {
                //Debug.Log("Combine count: " + combine.Count);

                if (combine.Count == 3 && combines.Count > 3)
                {
                    // item in match-3 can be a bomb-breaker/x-breaker
                    SetBombBreakerOrXBreakerCombine(GetMatches(FIND_DIRECTION.ROW));
                }
                else if (combine.Count == 4)
                {
                    SetColRowBreakerCombine(combine);
                }
                else if (combine.Count >= 5)
                {
                    SetRainbowCombine(combine);
                }

                foreach (var item in combine)
                {
                    item.Destroy();
                }

            } // end foreach combines

            // wait until item destroy animation finish
            while (destroyingItems > 0)
            {
                //Debug.Log("Destroying items");
                yield return new WaitForSeconds(0.1f);
            }

            // IMPORTANT: as describe in document Destroy is always delayed (but executed within the same frame).
            // So There is case destroyingItems = 0 BUT the item still exist that causes the GenerateNewItems function goes wrong
            yield return new WaitForEndOfFrame();

            // new items
            Drop();

            while (droppingItems > 0)
            {
                //Debug.Log("Dropping items");
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForEndOfFrame();

            // check if collect collectible

            if (GetMatches().Count <= 0 && CollectCollectible() == false)
            {
                break;
            }

            // increase dropTime
            dropTime++;

        } // end while

        // wait until all flying items fly to top bar
        while (flyingItems > 0)
        {
            //Debug.Log("Flying items");
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForEndOfFrame();

        if (matching > 1)
        {
            matching--;
            yield break;
        }

        // check if level complete
        if (state == GAME_STATE.WAITING_USER_SWAP)
        {
            if (moveLeft > 0)
            {
                if (IsLevelCompleted())
                {
                    StartCoroutine(PreWinAutoPlay());
                }
                else
                {
                    if (MoveGingerbread() == true)
                    {
                        yield return new WaitForSeconds(Configure.instance.swapTime);

                        yield return new WaitForSeconds(0.2f);

                        FindMatches();
                    }
                    
                    if (GenerateGingerbread() == true)
                    {
                        yield return new WaitForSeconds(0.2f);

                        FindMatches();
                    }

                    if (Help.instance.help == false)
                    {
                        StartCoroutine(CheckHint());
                    }
                    else
                    {
                        Help.instance.Show();
                    }
                }
            }
            else if (moveLeft == 0)
            {
                if (IsLevelCompleted())
                {
                    SaveLevelInfo();

                    // show win popup
                    state = GAME_STATE.OPENING_POPUP;

                    winPopup.OpenPopup();

                    AudioManager.instance.PopupWinAudio();

                    ShowAds();
                }
                else
                {
                    // show lose popup
                    state = GAME_STATE.OPENING_POPUP;

                    losePopup.OpenPopup();

                    AudioManager.instance.PopupLoseAudio();

                    ShowAds();
                }
            }
        }

        matching--;

        // if dropTime >= 3 we should show some text like: grate, amazing, etc.
        if (dropTime >= Configure.instance.encouragingPopup && state == GAME_STATE.WAITING_USER_SWAP && showingInspiringPopup == false)
        {
            ShowInspiringPopup();
        }

        // when finish function we can swap again
        //yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.2f);
        lockSwap = false;
    }

    #endregion

    #region Drop

    void Drop()
    {
        SetDropTargets();

        GenerateNewItems(true, Vector3.zero);

        Move();

        DropItems();
    }

    // set drop target to the remain items
    void SetDropTargets()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            //need to enumerate rows from bottom to top
            for (int i = row - 1; i >= 0; i--)
            {
                Node node = GetNode(i, j);

                if (node != null)
                {
                    Item item = node.item;

                    if (item != null)
                    {
                        // start calculating new target for the node
                        if (item.Movable())
                        {
                            Node target = node.BottomNeighbor();

                            if (target != null && target.CanGoThrough())
                            {
                                if (target.item == null)
                                {
                                    // check rows below at this time GetNode(i + 1, j) = target
                                    for (int k = i + 2; k < row; k++)
                                    {
                                        if (GetNode(k, j) != null)
                                        {
                                            if (GetNode(k, j).item == null)
                                            {
                                                if (GetNode(k, j).CanStoreItem() == true)
                                                {
                                                    target = GetNode(k, j);
                                                }
                                            }

                                            // if a node can not go through we do not need to check bellow
                                            if (GetNode(k, j).CanGoThrough() == false)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                if (GetNode(k, j).item != null)
                                                {
                                                    if (GetNode(k, j).item.Movable() == false)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // after have the target we swap items on nodes
                                if (target.item == null && target.CanStoreItem() == true)
                                {
                                    target.item = item;
                                    target.item.gameObject.transform.SetParent(target.gameObject.transform);
                                    target.item.node = target;

                                    node.item = null;
                                }
                            } // end if target != null
                        } // end item dropable
                    } // end item != null
                } // end node != null
            } // end for i
        } // end for j
    }

    // after destroy and drop items then we generate new items
    void GenerateNewItems(bool IsDrop, Vector3 pos)
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        var marshmallowGenerated = false;

        for (int j = 0; j < column; j++) 
        {
            var space = -1;

            var itemPos = Vector3.zero;

            for (int i = row - 1; i >= 0; i--) 
            {
                if (GetNode(i, j) != null)
                {
                    if (GetNode(i, j).item == null && GetNode(i, j).CanGenerateNewItem() == true)
                    {
                        // if target is collectible the new item can be a collectible
                        var collectible = false;

                        // collectible is only generated on the highest row
                        if (i == 0)
                        {
                            // check if need to generate new collectible
                            if (CheckGenerateCollectible() != null && 
                                CheckGenerateCollectible().Count > 0 && 
                                (LevelLoader.instance.collectibleGenerateMarkers.Contains(j) || LevelLoader.instance.collectibleGenerateMarkers.Count == 0))
                            {
                                collectible = true;
                            }                            
                        }

                        // check if need to generate a new marshmallow
                        var marshmallow = false;

                        if (CheckGenerateMarshmallow() == true)
                        {
                            marshmallow = true;
                        }

                        if (pos != Vector3.zero)
                        {
                            itemPos = pos + Vector3.up * NodeSize();
                        }
                        else
                        {
                            // calculate position of the new item
                            if (i > space)
                            {
                                space = i;
                            }

                            // can pass through node
                            var pass = 0;

                            for (int k = 0; k < row; k++)
                            {
                                var node = GetNode(k, j);

                                if (node != null && node.tile != null && node.tile.type == TILE_TYPE.PASS_THROUGH)
                                {
                                    pass++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            itemPos = NodeLocalPosition(i, j) + Vector3.up * (space - pass + 1) * NodeSize();
                        }

                        //print("COOKIE: Generate new item");

                        // if target is collectible then generate a new collectible item
                        if (collectible == true && Random.Range(0, 2) == 1)
                        {
                            GetNode(i, j).GenerateItem(CheckGenerateCollectible()[Random.Range(0, CheckGenerateCollectible().Count)]);
                        }
                        // generate a marshmallow
                        else if (marshmallow == true && Random.Range(0, 2) == 1 && marshmallowGenerated == false)
                        {
                            marshmallowGenerated = true;

                            GetNode(i, j).GenerateItem(ITEM_TYPE.MARSHMALLOW);
                        }
                        // generate a new random cookie
                        else
                        {
                            GetNode(i, j).GenerateItem(ITEM_TYPE.COOKIE_RAMDOM);
                        }

                        // set position

                        var newItem = GetNode(i, j).item;

                        if (newItem != null)
                        {
                            if (IsDrop == true)
                            {
                                newItem.gameObject.transform.position = itemPos;
                            }
                            else
                            {
                                newItem.gameObject.transform.position = NodeLocalPosition(i, j);
                            }
                        }
                    }
                }
            }
        }
    }

    // move item to neighbor empty node
    void Move()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = row - 1; i >= 0; i--)
        {
            //need to enumerate rows from bottom to top
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);

                if (node != null)
                {
                    if (node.item == null && node.CanStoreItem())
                    {
                        Node source = node.GetSourceNode();

                        if (source != null)
                        {
                            //Debug.Log("source node: " + source.name);

                            // new item position
                            var pos = ColumnFirstItemPosition(0, source.j);
                            
                            //print(pos);

                            // calculate move path
                            List<Vector3> path = node.GetMovePath();

                            if (source.transform.position != NodeLocalPosition(source.i, source.j))
                            {
                                // if source item is just generated
                                path.Add(NodeLocalPosition(source.i, source.j));
                            }

                            node.item = source.item;
                            node.item.gameObject.transform.SetParent(node.gameObject.transform);
                            node.item.node = node;

                            source.item = null;

                            if (path.Count > 1)
                            {
                                path.Reverse();

                                node.item.dropPath = path;
                            }

                            SetDropTargets();

                            GenerateNewItems(true, pos);
                            
                        } // end if source node != null
                    }
                } // end if node != null
            } // for j
        } // for i
    }

    // drop item to new position
    void DropItems()
    {
        //print("COOKIE: Drop items");

        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            for (int i = row - 1; i >= 0; i--)
            {
                if (GetNode(i, j) != null)
                {
                    if (GetNode(i, j).item != null)
                    {
                        GetNode(i, j).item.Drop();
                    }
                }
            }
        }
    }

    #endregion

    #region Item

    // this function check all the items and set them to be bomb-breaker/x-breaker
    public void SetBombBreakerOrXBreakerCombine(List<List<Item>> lists)
    {
        foreach (List<Item> list in lists)
        {
            foreach (Item item in list)
            {
                if (item != null && item.node != null)
                {
                    //print(item.node.name);

                    if (item.node.FindMatches(FIND_DIRECTION.COLUMN).Count > 2)
                    {
                        if (item.next == ITEM_TYPE.NONE)
                        {
                            var match = item.node.FindMatches(FIND_DIRECTION.COLUMN);

                            Node top = item.node.TopNeighbor();
                            Node bottom = item.node.BottomNeighbor();

                            if (top != null && bottom != null)
                            {
                                // - - o -
                                // o o - o
                                // - - o -
                                if (top.item != null && bottom.item != null)
                                {
                                    if (match.Contains(top.item) && match.Contains(bottom.item))
                                    {
                                        //print("T shape");
                                        item.next = item.GetXBreaker(item.type);
                                        return;
                                    }
                                }

                                var topTop = top.TopNeighbor();
                                var bottomBottom = bottom.BottomNeighbor();

                                // - - o
                                // - - o
                                // o o - o
                                if (topTop != null)
                                {
                                    if (top.item != null && topTop.item != null)
                                    {
                                        if (match.Contains(top.item) && match.Contains(topTop.item))
                                        {
                                            var left = item.node.LeftNeighbor();
                                            var right = item.node.RightNeighbor();

                                            if (left != null && right != null)
                                            {
                                                if (left.item != null && right.item != null)
                                                {
                                                    if (list.Contains(left.item) && list.Contains(right.item))
                                                    {
                                                        //print("T shape (top)");
                                                        item.next = item.GetXBreaker(item.type);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }                                    
                                }

                                // o o - o
                                // - - o
                                // - - o
                                if (bottomBottom != null)
                                {
                                    if (bottom.item != null && bottomBottom.item != null)
                                    {
                                        if (match.Contains(bottom.item) && match.Contains(bottomBottom.item))
                                        {
                                            var left = item.node.LeftNeighbor();
                                            var right = item.node.RightNeighbor();

                                            if (left != null && right != null)
                                            {
                                                if (left.item != null && right.item != null)
                                                {
                                                    if (list.Contains(left.item) && list.Contains(right.item))
                                                    {
                                                        //print("T shape (bottom)");
                                                        item.next = item.GetXBreaker(item.type);
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            } // end check T shape

                            // L shape = bomb breaker
                            item.next = item.GetBombBreaker(item.type);

                        } // item.next = none
                    } // count > 2
                }
            }
        }
    }

    public void SetColRowBreakerCombine(List<Item> combine)
    {
        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        // next type is normal (drop then match) get first item in the combine
        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        first.next = first.GetColumnBreaker(first.type);
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = first.GetRowBreaker(first.type);
                        break;
                    }
                }
            }
        } // not swap
    }

    public void SetRainbowCombine(List<Item> combine)
    {

        bool isSwap = false;

        foreach (Item item in combine)
        {
            if (item.next != ITEM_TYPE.NONE)
            {
                isSwap = true;

                break;
            }
        }

        if (!isSwap)
        {
            Item first = null;

            foreach (Item item in combine)
            {
                if (first == null)
                {
                    first = item;
                }
                else
                {
                    if (item.node.OrderOnBoard() < first.node.OrderOnBoard())
                    {
                        first = item;
                    }
                }
            }

            foreach (Item item in combine)
            {
                if (first.node.RightNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.RightNeighbor().OrderOnBoard())
                    {
                        combine[2].next = ITEM_TYPE.COOKIE_RAINBOW;
                        break;
                    }
                }

                if (first.node.BottomNeighbor())
                {
                    if (item.node.OrderOnBoard() == first.node.BottomNeighbor().OrderOnBoard())
                    {
                        first.next = ITEM_TYPE.COOKIE_RAINBOW;
                        break;
                    }
                }
            }
        }
    }

    // return 9 items around
    public List<Item> ItemAround(Node node)
    {
        List<Item> items = new List<Item>();

        for (int i = node.i - 1; i <= node.i + 1; i++)
        {
            for (int j = node.j - 1; j <= node.j + 1; j++)
            {
                if (GetNode(i, j) != null)
                {
                    items.Add(GetNode(i, j).item);
                }
            }
        }

        return items;
    }

    public List<Item> XCrossItems(Node node)
    {
        var items = new List<Item>();

        var row = LevelLoader.instance.row;

        for (int i = 0; i < row; i++)
        {
            if (i < node.i)
            {
                var crossLeft = GetNode(i, node.j - (node.i - i));
                var crossRight = GetNode(i, node.j + (node.i - i));

                if (crossLeft != null)
                {
                    if (crossLeft.item != null)
                    {
                        items.Add(crossLeft.item);
                    }
                }

                if (crossRight != null)
                {
                    if (crossRight.item != null)
                    {
                        items.Add(crossRight.item);
                    }
                }
            }
            else if (i == node.i)
            {
                if (node.item != null)
                {
                    items.Add(node.item);
                }
            }
            else if (i > node.i)
            {
                var crossLeft = GetNode(i, node.j - (i - node.i));
                var crossRight = GetNode(i, node.j + (i - node.i));

                if (crossLeft != null)
                {
                    if (crossLeft.item != null)
                    {
                        items.Add(crossLeft.item);
                    }
                }

                if (crossRight != null)
                {
                    if (crossRight.item != null)
                    {
                        items.Add(crossRight.item);
                    }
                }
            }
        }

        return items;
    }

    // return list of items in a column
    public List<Item> ColumnItems(int column)
    {
        var items = new List<Item>();

        var row = LevelLoader.instance.row;

        for (int i = 0; i < row; i++)
        {
            if (GetNode(i, column) != null)
            {
                items.Add(GetNode(i, column).item);
            }
        }

        return items;
    }

    // return list of items in a row
    public List<Item> RowItems(int row)
    {
        var items = new List<Item>();

        var column = LevelLoader.instance.column;

        for (int j = 0; j < column; j++)
        {
            if (GetNode(row, j) != null)
            {
                items.Add(GetNode(row, j).item);
            }
        }

        return items;
    }

    #endregion

    #region Destroy

    // destroy the whole board when swap 2 rainbow
    public void DoubleRainbowDestroy()
    {
        StartCoroutine(DestroyWholeBoard());
    }

    IEnumerator DestroyWholeBoard()
    {
        var column = LevelLoader.instance.column;

        for (int i = 0; i < column; i++)
        {
            List<Item> items = ColumnItems(i);

            foreach (var item in items)
            {
                if (item != null && item.Destroyable() == true)
                {
                    //item.type = item.GetCookie(item.type);

                    GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RainbowExplosion()) as GameObject);

                    if (explosion != null) explosion.transform.position = item.transform.position;

                    item.Destroy();
                }
            }

            yield return new WaitForSeconds(0.2f);
        }

        FindMatches();
    }

    // destroy all items of changing list
    public void DestroyChangingList()
    {
        StartCoroutine(StartDestroyChangingList());
    }

    IEnumerator StartDestroyChangingList()
    {
        //print("Start destroy items in the list");

        var originalState = state;

        state = GAME_STATE.DESTROYING_ITEMS;
        
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < changingList.Count; i++)
        {
            var item = changingList[i];

            if (item != null)
            {
                item.Destroy();
            }

            while (destroyingItems > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForEndOfFrame();

            Drop();

            while (droppingItems > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForEndOfFrame();
        }

        changingList.Clear();

        state = originalState;

        FindMatches();
    }

    public void DestroySameColorList()
    {
        StartCoroutine(StartDestroySameColorList());
    }

    IEnumerator StartDestroySameColorList()
    {
        //print("Start destroy items in the same color list");

        var originalState = state;

        state = GAME_STATE.DESTROYING_ITEMS;

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < sameColorList.Count; i++)
        {
            var item = sameColorList[i];

            if (item != null && item.destroying == false)
            {
                GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RainbowExplosion()) as GameObject);

                if (explosion != null) explosion.transform.position = item.transform.position;

                item.Destroy();

                yield return new WaitForSeconds(0.1f);
            }
        }

        sameColorList.Clear();

        state = originalState;

        FindMatches();
    }

    public void DestroyNeighborItems(Item item)
    {
        DestroyMarshmallow(item);

        DestroyChocolate(item);

        DestroyRockCandy(item);
    }

    public void DestroyMarshmallow(Item item)
    {
        if (item.IsMarshmallow() == true || 
            item.IsCollectible() == true || 
            item.IsGingerbread() == true || 
            item.IsChocolate() == true || 
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }

        var marshmallows = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsMarshmallow() == true)
        {
            marshmallows.Add(item.node.LeftNeighbor().item);
        }

        foreach (var marshmallow in marshmallows)
        {
            marshmallow.Destroy();
        }
    }

    public void DestroyChocolate(Item item)
    {
        if (item.IsMarshmallow() == true ||
            item.IsCollectible() == true ||
            item.IsGingerbread() == true ||
            item.IsChocolate() == true ||
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }
        
        var chocolates = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsChocolate() == true)
        {
            chocolates.Add(item.node.LeftNeighbor().item);
        }

        foreach (var chocolate in chocolates)
        {
            chocolate.Destroy();
        }
    }

    public void DestroyRockCandy(Item item)
    {
        if (item.IsMarshmallow() == true ||
            item.IsCollectible() == true ||
            item.IsGingerbread() == true ||
            item.IsChocolate() == true ||
            item.IsRockCandy() == true)
        {
            return;
        }

        if (state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            return;
        }

        var rocks = new List<Item>();

        if (item.node.TopNeighbor() != null && item.node.TopNeighbor().item != null && item.node.TopNeighbor().item.IsRockCandy() == true && item.node.TopNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.TopNeighbor().item);
        }

        if (item.node.RightNeighbor() != null && item.node.RightNeighbor().item != null && item.node.RightNeighbor().item.IsRockCandy() == true && item.node.RightNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.RightNeighbor().item);
        }

        if (item.node.BottomNeighbor() != null && item.node.BottomNeighbor().item != null && item.node.BottomNeighbor().item.IsRockCandy() == true && item.node.BottomNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.BottomNeighbor().item);
        }

        if (item.node.LeftNeighbor() != null && item.node.LeftNeighbor().item != null && item.node.LeftNeighbor().item.IsRockCandy() == true && item.node.LeftNeighbor().item.color == item.color)
        {
            rocks.Add(item.node.LeftNeighbor().item);
        }

        foreach (var rock in rocks)
        {
            rock.Destroy();
        }
    }

    #endregion

    #region Collect

    // if item is the target to collect
    public void CollectItem(Item item)
    {
        GameObject flyingItem = null;
        var order = 0;

        // cookie
        if (item.IsCookie())
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.COOKIE && LevelLoader.instance.target1Color == item.color && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.COOKIE && LevelLoader.instance.target2Color == item.color && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.COOKIE && LevelLoader.instance.target3Color == item.color && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else  if (LevelLoader.instance.target4Type == TARGET_TYPE.COOKIE && LevelLoader.instance.target4Color == item.color && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Cookie";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = null;

                switch (item.color)
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
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // gingerbread
        else if (item.IsGingerbread())
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.GINGERBREAD && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.GINGERBREAD && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.GINGERBREAD && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.GINGERBREAD && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Gingerbread";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.GingerbreadGeneric()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }    
        // marshmallow
        else if (item.IsMarshmallow())
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.MARSHMALLOW && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.MARSHMALLOW && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.MARSHMALLOW && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.MARSHMALLOW && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Marshmallow";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.Marshmallow()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // chocolate
        else if (item.IsChocolate())
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.CHOCOLATE && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.CHOCOLATE && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.CHOCOLATE && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.CHOCOLATE && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Chocolate";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.Chocolate1()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // column_row_breaker
        else if (item.IsColumnBreaker(item.type) == true || item.IsRowBreaker(item.type) == true)
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.COLUMN_ROW_BREAKER && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Column Row Breaker";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.ColumnRowBreaker()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
		// generic bomb breaker
		else if (item.IsBombBreaker(item.type) == true)
		{
			if (LevelLoader.instance.target1Type == TARGET_TYPE.BOMB_BREAKER && target1Left > 0)
			{
				target1Left--;
				flyingItem = new GameObject();
				order = 1;
			}
			else if (LevelLoader.instance.target2Type == TARGET_TYPE.BOMB_BREAKER && target2Left > 0)
			{
				target2Left--;
				flyingItem = new GameObject();
				order = 2;
			}
			else if (LevelLoader.instance.target3Type == TARGET_TYPE.BOMB_BREAKER && target3Left > 0)
			{
				target3Left--;
				flyingItem = new GameObject();
				order = 3;
			}
			else if (LevelLoader.instance.target4Type == TARGET_TYPE.BOMB_BREAKER && target4Left > 0)
			{
				target4Left--;
				flyingItem = new GameObject();
				order = 4;
			}

			if (flyingItem != null)
			{
				flyingItem.transform.position = item.transform.position;
				flyingItem.name = "Flying Bomb Breaker";
				flyingItem.layer = LayerMask.NameToLayer("On Top UI");

				SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

				GameObject prefab = Resources.Load(Configure.GenericBombBreaker()) as GameObject;

				if (prefab != null)
				{
					spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				StartCoroutine(CollectItemAnim(flyingItem, order));
			}
		}
		// generic x_breaker
		else if (item.IsXBreaker(item.type) == true)
		{
			if (LevelLoader.instance.target1Type == TARGET_TYPE.X_BREAKER && target1Left > 0)
			{
				target1Left--;
				flyingItem = new GameObject();
				order = 1;
			}
			else if (LevelLoader.instance.target2Type == TARGET_TYPE.X_BREAKER && target2Left > 0)
			{
				target2Left--;
				flyingItem = new GameObject();
				order = 2;
			}
			else if (LevelLoader.instance.target3Type == TARGET_TYPE.X_BREAKER && target3Left > 0)
			{
				target3Left--;
				flyingItem = new GameObject();
				order = 3;
			}
			else if (LevelLoader.instance.target4Type == TARGET_TYPE.X_BREAKER && target4Left > 0)
			{
				target4Left--;
				flyingItem = new GameObject();
				order = 4;
			}

			if (flyingItem != null)
			{
				flyingItem.transform.position = item.transform.position;
				flyingItem.name = "Flying X Breaker";
				flyingItem.layer = LayerMask.NameToLayer("On Top UI");

				SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

				GameObject prefab = Resources.Load(Configure.GenericXBreaker()) as GameObject;

				if (prefab != null)
				{
					spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				}

				StartCoroutine(CollectItemAnim(flyingItem, order));
			}
		}
        // rainbow
        else if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.RAINBOW && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.RAINBOW && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.RAINBOW && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.RAINBOW && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Rainbow";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.CookieRainbow()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
        // rock candy
        else if (item.IsRockCandy() == true)
        {
            if (LevelLoader.instance.target1Type == TARGET_TYPE.ROCK_CANDY && target1Left > 0)
            {
                target1Left--;
                flyingItem = new GameObject();
                order = 1;
            }
            else if (LevelLoader.instance.target2Type == TARGET_TYPE.ROCK_CANDY && target2Left > 0)
            {
                target2Left--;
                flyingItem = new GameObject();
                order = 2;
            }
            else if (LevelLoader.instance.target3Type == TARGET_TYPE.ROCK_CANDY && target3Left > 0)
            {
                target3Left--;
                flyingItem = new GameObject();
                order = 3;
            }
            else if (LevelLoader.instance.target4Type == TARGET_TYPE.ROCK_CANDY && target4Left > 0)
            {
                target4Left--;
                flyingItem = new GameObject();
                order = 4;
            }

            if (flyingItem != null)
            {
                flyingItem.transform.position = item.transform.position;
                flyingItem.name = "Flying Rock Candy";
                flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                GameObject prefab = Resources.Load(Configure.RockCandyGeneric()) as GameObject;

                if (prefab != null)
                {
                    spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                }

                StartCoroutine(CollectItemAnim(flyingItem, order));
            }
        }
    }

    public void CollectWaffle(Waffle waffle)
    {
        GameObject flyingItem = null;
        var order = 0;

        if (LevelLoader.instance.target1Type == TARGET_TYPE.WAFFLE && target1Left > 0)
        {
            target1Left--;
            flyingItem = new GameObject();
            order = 1;
        }
        else if (LevelLoader.instance.target2Type == TARGET_TYPE.WAFFLE && target2Left > 0)
        {
            target2Left--;
            flyingItem = new GameObject();
            order = 2;
        }
        else if (LevelLoader.instance.target3Type == TARGET_TYPE.WAFFLE && target3Left > 0)
        {
            target3Left--;
            flyingItem = new GameObject();
            order = 3;
        }
        else if (LevelLoader.instance.target4Type == TARGET_TYPE.WAFFLE && target4Left > 0)
        {
            target4Left--;
            flyingItem = new GameObject();
            order = 4;
        }

        if (flyingItem != null)
        {
            flyingItem.transform.position = waffle.transform.position;
            flyingItem.name = "Flying Waffle";
            flyingItem.layer = LayerMask.NameToLayer("On Top UI");
            flyingItem.transform.localScale = new Vector3(0.75f, 0.75f, 0);

            SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

            GameObject prefab = Resources.Load(Configure.Waffle1()) as GameObject; ;

            spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;

            StartCoroutine(CollectItemAnim(flyingItem, order));
        }
    }

    public void CollectCage(Cage cage)
    {
        GameObject flyingItem = null;

        var order = 0;

        if (LevelLoader.instance.target1Type == TARGET_TYPE.CAGE && target1Left > 0)
        {
            target1Left--;
            flyingItem = new GameObject();
            order = 1;
        }
        else if (LevelLoader.instance.target2Type == TARGET_TYPE.CAGE && target2Left > 0)
        {
            target2Left--;
            flyingItem = new GameObject();
            order = 2;
        }
        else if (LevelLoader.instance.target3Type == TARGET_TYPE.CAGE && target3Left > 0)
        {
            target3Left--;
            flyingItem = new GameObject();
            order = 3;
        }
        else if (LevelLoader.instance.target4Type == TARGET_TYPE.CAGE && target4Left > 0)
        {
            target4Left--;
            flyingItem = new GameObject();
            order = 4;
        }

        if (flyingItem != null)
        {
            flyingItem.transform.position = cage.transform.position;
            flyingItem.name = "Flying Cage";
            flyingItem.layer = LayerMask.NameToLayer("On Top UI");
            flyingItem.transform.localScale = new Vector3(0.75f, 0.75f, 0);

            SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

            GameObject prefab = Resources.Load(Configure.Cage1()) as GameObject; ;

            spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;

            StartCoroutine(CollectItemAnim(flyingItem, order));
        }
    }

    // if the collectible
    bool CollectCollectible()
    {
        if (LevelLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return false;
        }

        var items = GetListItems();

        foreach (var item in items)
        {
            bool collectable = false;

            // check each item in last row and in column which can collect and item is collectible
            if (item != null &&
                (item.node.i == LevelLoader.instance.row - 1) &&
                LevelLoader.instance.collectibleCollectColumnMarkers.Contains(item.node.j) &&
                item.IsCollectible() == true)
            {
                collectable = true;
            }
            
            // collectible marker by node
            if (item != null && 
                LevelLoader.instance.collectibleCollectNodeMarkers.Contains(NodeOrder(item.node.i, item.node.j)) &&
                item.IsCollectible() == true)
            {
                collectable = true;
            }
            
            if (collectable == true)
            {
                GameObject flyingItem = null;
                var order = 0;

                if (LevelLoader.instance.target1Type == TARGET_TYPE.COLLECTIBLE && LevelLoader.instance.target1Color == item.color && target1Left > 0)
                {
                    target1Left--;
                    flyingItem = new GameObject();
                    order = 1;
                }
                else if (LevelLoader.instance.target2Type == TARGET_TYPE.COLLECTIBLE && LevelLoader.instance.target2Color == item.color && target2Left > 0)
                {
                    target2Left--;
                    flyingItem = new GameObject();
                    order = 2;
                }
                else if (LevelLoader.instance.target3Type == TARGET_TYPE.COLLECTIBLE && LevelLoader.instance.target3Color == item.color && target3Left > 0)
                {
                    target3Left--;
                    flyingItem = new GameObject();
                    order = 3;
                }
                else if (LevelLoader.instance.target4Type == TARGET_TYPE.COLLECTIBLE && LevelLoader.instance.target4Color == item.color && target4Left > 0)
                {
                    target4Left--;
                    flyingItem = new GameObject();
                    order = 4;
                }

                if (flyingItem != null)
                {
                    flyingItem.transform.position = item.transform.position;
                    flyingItem.name = "Flying Collectible";
                    flyingItem.layer = LayerMask.NameToLayer("On Top UI");

                    SpriteRenderer spriteRenderer = flyingItem.AddComponent<SpriteRenderer>();

                    GameObject prefab = null;

                    switch (item.color)
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
                            prefab = Resources.Load(Configure.Collectible7()) as GameObject;
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
                        spriteRenderer.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    }

                    StartCoroutine(CollectItemAnim(flyingItem, order));

                    item.Destroy(true);

                    return true;
                }
            }
        }

        return false;
    }

    // item fly to target
    IEnumerator CollectItemAnim(GameObject item, int order)
    {
        yield return new WaitForFixedUpdate();

        GameObject target = null;

        switch (order)
        {
            case 1:
                target = target1;
                break;
            case 2:
                target = target2;
                break;
            case 3:
                target = target3;
                break;
            case 4:
                target = target4;
                break;
        }

        flyingItems++;

        AnimationCurve curveX = new AnimationCurve(new Keyframe(0, item.transform.localPosition.x), new Keyframe(0.4f, target.transform.position.x));
        AnimationCurve curveY = new AnimationCurve(new Keyframe(0, item.transform.localPosition.y), new Keyframe(0.4f, target.transform.position.y));
        curveX.AddKey(0.2f, item.transform.localPosition.x + UnityEngine.Random.Range(-2f, 2f));
        curveY.AddKey(0.2f, item.transform.localPosition.y + UnityEngine.Random.Range(-1f, 0f));

        float startTime = Time.time;
        float speed = 2.0f + flyingItems * 0.25f;
        float distCovered = 0;
        while (distCovered < 0.4f)
        {
            distCovered = (Time.time - startTime) / speed;
            item.transform.localPosition = new Vector3(curveX.Evaluate(distCovered), curveY.Evaluate(distCovered), 0);

            yield return new WaitForFixedUpdate();
        }

        AudioManager.instance.CollectTargetAudio();

        switch (order)
        {
            case 1:
                UITarget.UpdateTargetAmount(1);
                break;
            case 2:
                UITarget.UpdateTargetAmount(2);
                break;
            case 3:
                UITarget.UpdateTargetAmount(3);
                break;
            case 4:
                UITarget.UpdateTargetAmount(4);
                break;
        }

        Destroy(item);
        
        flyingItems--;
    }

    #endregion

    #region Popup

    void TargetPopup()
    {
        StartCoroutine(StartTargetPopup());
    }

    IEnumerator StartTargetPopup()
    {
        state = GAME_STATE.OPENING_POPUP;

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PopupTargetAudio();

        targetPopup.OpenPopup();

        yield return new WaitForSeconds(1.0f);

        var popup = GameObject.Find("TargetPopup(Clone)");

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }

        yield return new WaitForSeconds(0.5f);

        state = GAME_STATE.WAITING_USER_SWAP;

        if (Help.instance.help == false)
        {
            StartCoroutine(CheckHint());
        }
        else
        {
            Help.instance.Show();
        }

        // Plus 5 moves popup
        if (Configure.instance.beginFiveMoves == true)
        {
            StartCoroutine(Plus5MovesPopup());
        }
    }

    IEnumerator Plus5MovesPopup()
    {
        Configure.instance.beginFiveMoves = false;

        plus5MovesPopup.OpenPopup();

        yield return new WaitForSeconds(1.0f);

        var popup = GameObject.Find("Plus5MovesPopup(Clone)");

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }
    }

    void ShowInspiringPopup()
    {
        //print("Excellent!, Amazing!, Great!, Nice!");

        int encouraging = Random.Range(0, 3);

        switch (encouraging)
        {
            case 0:
                StartCoroutine(InspiringPopup(amazingPopup, encouraging));
                // sound
                AudioManager.instance.amazingAudio();
                break;
            case 1:
                StartCoroutine(InspiringPopup(excellentPopup, encouraging));
                // sound
                AudioManager.instance.exellentAudio();
                break;
            case 2:
                StartCoroutine(InspiringPopup(greatPopup, encouraging));
                // sound
                AudioManager.instance.greatAudio();
                break;            
        }
    }

    IEnumerator InspiringPopup(PopupOpener popupOpener, int encouraging)
    {
        // prevent multiple call
        if (showingInspiringPopup == false) showingInspiringPopup = true;
        else yield return null;

        popupOpener.OpenPopup();

        yield return new WaitForSeconds(1.0f);

        GameObject popup = null;

        switch (encouraging)
        {
            case 0:
                popup = GameObject.Find("AmazingPopup(Clone)");
                break;
            case 1:
                popup = GameObject.Find("ExcellentPopup(Clone)");
                break;
            case 2:
                popup = GameObject.Find("GreatPopup(Clone)");
                break;            
        }

        if (popup)
        {
            popup.GetComponent<Popup>().Close();
        }

        yield return new WaitForSeconds(1f);

        showingInspiringPopup = false;
    }

    #endregion

    #region Complete

    bool IsLevelCompleted()
    {
        if (target1Left == 0 && target2Left == 0 && target3Left == 0 && target4Left == 0)
        {
            return true;
        }

        return false;
    }

    // auto play the left moves when target is reached
    IEnumerator PreWinAutoPlay()
    {
        HideHint();

        // reset drop time
        dropTime = 1;

        state = GAME_STATE.OPENING_POPUP;

        yield return new WaitForSeconds(0.5f);

        // completed popup
        completedPopup.OpenPopup();

        AudioManager.instance.PopupCompletedAudio();

        yield return new WaitForSeconds(1.0f);

        if (GameObject.Find("CompletedPopup(Clone)"))
        {
            GameObject.Find("CompletedPopup(Clone)").GetComponent<Popup>().Close();
        }

        yield return new WaitForSeconds(0.5f);

        state = GAME_STATE.PRE_WIN_AUTO_PLAYING;

        var items = GetRandomItems(moveLeft);

        foreach (var item in items)
        {
            item.SetRandomNextType();
            item.nextSound = false;

            moveLeft--;
            UITop.DecreaseMoves(true);

            var prefab = Instantiate(Resources.Load(Configure.StarGold())) as GameObject;
            prefab.transform.position = UITop.GetComponent<UITop>().movesText.gameObject.transform.position;

            var startPosition = prefab.transform.position;
            var endPosition = item.gameObject.transform.position;
            var bending = new Vector3(1, 1, 0);
            var timeToTravel = 0.2f;
            var timeStamp = Time.time;

            while (Time.time < timeStamp + timeToTravel) {
                var currentPos = Vector3.Lerp(startPosition, endPosition, (Time.time - timeStamp)/timeToTravel);
         
                currentPos.x += bending.x * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);
                currentPos.y += bending.y * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);
                currentPos.z += bending.z * Mathf.Sin(Mathf.Clamp01((Time.time - timeStamp)/timeToTravel) * Mathf.PI);

                prefab.transform.position = currentPos;
         
                yield return null;
            }

            Destroy(prefab);

            item.Destroy();

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        while (GetAllSpecialItems().Count > 0)
        {
            while (GetAllSpecialItems().Count > 0)
            {
                var specials = GetAllSpecialItems();

                var item = specials[UnityEngine.Random.Range(0, specials.Count)];

                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    item.DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();

                while (destroyingItems > 0)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForEndOfFrame();

                Drop();

                while (droppingItems > 0)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForEndOfFrame();
            }

            yield return StartCoroutine(DestroyMatches());
        }

        while (destroyingItems > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForEndOfFrame();

        while (droppingItems > 0)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(0.5f);

        state = GAME_STATE.OPENING_POPUP;

        SaveLevelInfo();

        AudioManager.instance.PopupWinAudio();

        winPopup.OpenPopup();

        //ShowAds();
    }

    List<Item> GetRandomItems(int number)
    {
        var avaiableItems = new List<Item>();
        var returnItems = new List<Item>();

        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.node != null)
                {
                    if (item.IsCookie())
                    {
                        avaiableItems.Add(item);
                    }
                }
            }
        }

        while (returnItems.Count < number && avaiableItems.Count > 0)
        {
            var item = avaiableItems[Random.Range(0, avaiableItems.Count)];

            returnItems.Add(item);

            avaiableItems.Remove(item);
        }

        return returnItems;
    }

    List<Item> GetAllSpecialItems()
    {
        var specials = new List<Item>();

        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW || item.IsColumnBreaker(item.type) || item.IsRowBreaker(item.type) || item.IsBombBreaker(item.type) || item.IsXBreaker(item.type))
                {
                    specials.Add(item);
                }
            }
        }

        return specials;
    }

    public void SaveLevelInfo()
    {
        // level star
        if (score < LevelLoader.instance.score1Star)
        {
            star = 0;
        }
        else if (LevelLoader.instance.score1Star <= score && score < LevelLoader.instance.score2Star)
        {
            star = 1;
        }
        else if (LevelLoader.instance.score2Star <= score && score < LevelLoader.instance.score3Star)
        {
            star = 2;
        }
        else if (score >= LevelLoader.instance.score2Star)
        {
            star = 3;
        }

        // score and star
        GameData.instance.SaveLevelStatistics(LevelLoader.instance.level, score, star);

        // open next level
        int openedLevel = GameData.instance.GetOpendedLevel();

        if (LevelLoader.instance.level == openedLevel)
        {
            if (openedLevel < Configure.instance.maxLevel)
            {
                GameData.instance.SaveOpendedLevel(openedLevel + 1);
            }            
        }

        // add bonus coin
        int coin = GameData.instance.GetPlayerCoin();

        if (star == 1)
        {
            GameData.instance.SavePlayerCoin(coin + Configure.instance.bonus1Star);
        }
        else if (star == 2)
        {
            GameData.instance.SavePlayerCoin(coin + Configure.instance.bonus2Star);
        }
        else if (star == 3)
        {
            GameData.instance.SavePlayerCoin(coin + Configure.instance.bonus3Star);
        }

        // Post score to server
        StartCoroutine(PostLevelScoretoServer());
    }

    #endregion

    #region Ads

    public void ShowAds()
    {
        StartCoroutine(ShowPopupAds());
    }

    IEnumerator ShowPopupAds()
    {
        yield return new WaitForSeconds(0.1f);

        // TODO: check if allow to show ads
        var allowShowAds = true;

        if (allowShowAds == true)
        {
        }
    }

    #endregion

    #region Hint

    public void Hint()
    {
        StartCoroutine(CheckHint());
    }

    public IEnumerator CheckHint()
    {
        //Debug.Log("CheckHint()");

        // prevent multiple call this function only last call is triggered
        checkHintCall++;

        if (checkHintCall > 1)
        {
            checkHintCall--;

            //Debug.Log("Cancel checking hint because of multiple hint call");

            yield break;
        }

        if (Configure.instance.showHint == false)
        {
            yield break;
        }

        if (moveLeft <= 0)
        {
            yield break;
        }

        // put delay here user also need to wait when the is no matches
        //yield return new WaitForSeconds(Configure.instance.hintDelay);

        // need to call hide hint here in case items destroy after no matches generate
        HideHint();

        while (state != GAME_STATE.WAITING_USER_SWAP)
        {
            //Debug.Log("Wait for checking hint because of game state");

            yield return new WaitForSeconds(0.1f);
        }

        while (lockSwap == true)
        {
            //Debug.Log("Wait for checking hint because of lock swap");

            yield return new WaitForSeconds(0.1f);
        }

        //Debug.Log("Start checking hint");

        // check for rainbow item / breaker / color
        if (GetHintByRainbowItem() == true || GetHintByBreaker() == true || GetHintByColor() == true)
        {
            StartCoroutine(ShowHint());

            checkHintCall--;

            yield break;
        }
        // if reach this code that mean there is no matches
        else
        {
            // prevent multiple call
            if (!GameObject.Find("NoMatchesdPopup(Clone)"))
            {
                state = GAME_STATE.NO_MATCHES_REGENERATING;

                lockSwap = true;

                AudioManager.instance.PopupNoMatchesAudio();

                // show and close popup
                noMatchesPopup.OpenPopup();

                yield return new WaitForSeconds(1.0f);

                if (GameObject.Find("NoMatchesdPopup(Clone)"))
                {
                    GameObject.Find("NoMatchesdPopup(Clone)").GetComponent<Popup>().Close();
                }

                yield return new WaitForSeconds(0.5f);

                var position = Camera.main.aspect * Camera.main.orthographicSize * 2;

                // hide board
                iTween.MoveTo(gameObject, iTween.Hash(
                    "x", -position,
                    "easeType", iTween.EaseType.easeOutBack,
                    "time", 0.5
                ));

                yield return new WaitForSeconds(0.5f);

                NoMoveRegenerate();

                while (GetHintByColor() == false)
                {
                    NoMoveRegenerate();

                    yield return new WaitForEndOfFrame();
                }

                // show board
                iTween.MoveTo(gameObject, iTween.Hash(
                    "x", 0,
                    "easeType", iTween.EaseType.easeOutBack,
                    "time", 0.5
                ));

                yield return new WaitForSeconds(0.5f);

                state = GAME_STATE.WAITING_USER_SWAP;

                FindMatches();
            }

            checkHintCall--;
        }
    }

    public IEnumerator ShowHint()
    {
        //Debug.Log("ShowHint()");

        showHintCall++;

        // prevent multiple call
        if (showHintCall > 1)
        {
            //Debug.Log("Cancel showing hint because of multiple call");

            showHintCall--;

            yield break;
        }

        if (Configure.instance.showHint == false)
        {
            yield break;
        }
        
        yield return new WaitForSeconds(Configure.instance.hintDelay);

        while (state != GAME_STATE.WAITING_USER_SWAP)
        {
            yield return new WaitForSeconds(0.1f);
        }

        while (lockSwap == true)
        {
            yield return new WaitForSeconds(0.1f);
        }

        //Debug.Log("Start showing hint");

        // make sure only items in hint list run animation
        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (!hintItems.Contains(item))
                {
                    iTween.StopByName(item.gameObject, "HintAnimation");
                    item.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
        }

        foreach (var item in hintItems)
        {
            if (item != null)
            {
                //Debug.Log("Show hint: " + item.node.name);

                iTween.ShakeRotation(item.gameObject, iTween.Hash(
                    "name", "HintAnimation",
                    "amount", new Vector3(0f, 0f, 50f),
                    "easetype", iTween.EaseType.easeOutBack,
                    //"looptype", iTween.LoopType.pingPong,
                    "oncomplete", "OnCompleteShowHint",
                    "oncompletetarget", gameObject,
                    "oncompleteparams", new Hashtable() { { "item", item } },
                    //"delay", 2f,
                    "time", 1f
                ));
            }
        }

        // only wait if hint items run animation
        // if there is no item that mean the hint list is clean in clear hint function
        if (hintItems.Count > 0)
        {
            yield return new WaitForSeconds(1.5f);
        }
        
        showHintCall--;

        StartCoroutine(CheckHint());
    }

    // when shake a item it is not in good place we need to reset it
    public void OnCompleteShowHint(Hashtable args)
    {
        var item = (Item)args["item"];

        //Debug.Log("On Complete Shake Item: " + item.node.name);

        iTween.RotateTo(item.gameObject, iTween.Hash(
            "rotation", Vector3.zero,
            "time", 0.2f
        ));
    }

    public void HideHint()
    {
        //Debug.Log("Hide Hint");

        foreach (var item in hintItems)
        {
            if (item != null)
            {
                //print("Hide item in node: " + item.node.name);

                iTween.StopByName(item.gameObject, "HintAnimation");

                iTween.RotateTo(item.gameObject, iTween.Hash(
                    "rotation", Vector3.zero,
                    "time", 0.2f
                ));
            }
        }

        // clear hint list to make sure show hint function do not runs animation
        hintItems.Clear();
    }

    List<int> Shuffle(List<int> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }

    void CheckHintNode(Node node, int color, bool needMove = false)
    {
        if (node != null)
        {
            if (node.item != null && node.item.color == color)
            {
                if (needMove)
                {
                    if (node.item.Movable() && node.item.Matchable())
                    {
                        hintItems.Add(node.item);
                    }
                }
                else
                {
                    if (node.item.Matchable())
                    {
                        hintItems.Add(node.item);
                    }
                }
            }
        }
    }

    void NoMoveRegenerate()
    {
        //print("No moves generate");

        foreach (var item in GetListItems())
        {
            if (item != null)
            {
                if (item.Movable() && item.IsCookie())
                {
                    item.color = LevelLoader.instance.RandomColor();

                    item.ChangeSpriteAndType(item.color);
                }
            }
        }
    }

    bool GetHintByColor()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        foreach (int color in Shuffle(LevelLoader.instance.usingColors))
        {
            for (int j = 0; j < column; j++)
            {
                for (int i = 0; i < row; i++)
                {
                    Node node = GetNode(i, j);

                    if (node != null)
                    {
                        if (node.item == null || !(node.item.Movable()))
                        {
                            continue;
                        }

                        // current node is x
                        // o-o-x
                        //	   o
                        CheckHintNode(GetNode(i + 1, j), color, true);
                        CheckHintNode(GetNode(i, j - 1), color);
                        CheckHintNode(GetNode(i, j - 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        //     o
                        // o-o x
                        CheckHintNode(GetNode(i - 1, j), color, true);
                        CheckHintNode(GetNode(i, j - 1), color);
                        CheckHintNode(GetNode(i, j - 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // x o o
                        // o
                        CheckHintNode(GetNode(i + 1, j), color, true);
                        CheckHintNode(GetNode(i, j + 1), color);
                        CheckHintNode(GetNode(i, j + 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o
                        // x o o
                        CheckHintNode(GetNode(i - 1, j), color, true);
                        CheckHintNode(GetNode(i, j + 1), color);
                        CheckHintNode(GetNode(i, j + 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o
                        // o
                        // x o
                        CheckHintNode(GetNode(i, j + 1), color, true);
                        CheckHintNode(GetNode(i - 1, j), color);
                        CheckHintNode(GetNode(i - 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // x o
                        // o
                        // o
                        CheckHintNode(GetNode(i, j + 1), color, true);
                        CheckHintNode(GetNode(i + 1, j), color);
                        CheckHintNode(GetNode(i + 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        //	 o
                        //   o
                        // o x
                        CheckHintNode(GetNode(i, j - 1), color, true);
                        CheckHintNode(GetNode(i - 1, j), color);
                        CheckHintNode(GetNode(i - 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o x
                        //   o
                        //   o
                        CheckHintNode(GetNode(i, j - 1), color, true);
                        CheckHintNode(GetNode(i + 1, j), color);
                        CheckHintNode(GetNode(i + 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o-x-o-o
                        CheckHintNode(GetNode(i, j - 1), color, true);
                        CheckHintNode(GetNode(i, j + 1), color);
                        CheckHintNode(GetNode(i, j + 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o-o-x-o
                        CheckHintNode(GetNode(i, j + 1), color, true);
                        CheckHintNode(GetNode(i, j - 1), color);
                        CheckHintNode(GetNode(i, j - 2), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o
                        // x
                        // o
                        // o
                        CheckHintNode(GetNode(i - 1, j), color, true);
                        CheckHintNode(GetNode(i + 1, j), color);
                        CheckHintNode(GetNode(i + 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        // o
                        // o
                        // x
                        // o
                        CheckHintNode(GetNode(i + 1, j), color, true);
                        CheckHintNode(GetNode(i - 1, j), color);
                        CheckHintNode(GetNode(i - 2, j), color);
                        if (hintItems.Count == 3)
                        {
                            return true;
                        }
                        else
                        {
                            hintItems.Clear();
                        }

                        //   o
                        // o x o
                        //   o
                        int h = 0;
                        int v = 0;
                        Node neighbor = null;

                        neighbor = node.LeftNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Matchable() && neighbor.item.color == color)
                            {
                                hintItems.Add(neighbor.item);

                                h++;
                            }
                        }

                        neighbor = node.RightNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Matchable() && neighbor.item.color == color)
                            {
                                hintItems.Add(neighbor.item);

                                h++;
                            }
                        }

                        neighbor = node.TopNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Matchable() && neighbor.item.color == color)
                            {
                                hintItems.Add(neighbor.item);

                                v++;
                            }
                        }

                        neighbor = node.BottomNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Matchable() && neighbor.item.color == color)
                            {
                                hintItems.Add(neighbor.item);

                                v++;
                            }
                        }

                        if (hintItems.Count == 3)
                        {
                            if (v > h && hintItems[0].node.item != null && hintItems[0].node.item.Movable())
                            {
                                return true;
                            }
                            else if (v < h && hintItems[2].node.item != null && hintItems[2].node.item.Movable())
                            {
                                return true;
                            }
                            else
                            {
                                hintItems.Clear();
                            }
                        }
                        else if (hintItems.Count == 4)
                        {
                            if (hintItems[0].node.item.Movable())
                            {
                                hintItems.RemoveAt(1);

                                return true;
                            }
                            else if (hintItems[1].node.item.Movable())
                            {
                                hintItems.RemoveAt(0);

                                return true;
                            }
                            else if (hintItems[2].node.item.Movable())
                            {
                                hintItems.RemoveAt(3);

                                return true;
                            }
                            else if (hintItems[3].node.item.Movable())
                            {
                                hintItems.RemoveAt(2);

                                return true;
                            }
                            else
                            {
                                hintItems.Clear();
                            }
                        }
                        else
                        {
                            hintItems.Clear();
                        }
                    }
                } // end for row
            }
        } // end foreach color

        return false;
    }

    bool GetHintByRainbowItem()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);
                if (node != null)
                {
                    if (node.item == null || !(node.item.Movable()))
                    {
                        continue;
                    }

                    if (node.item.type == ITEM_TYPE.COOKIE_RAINBOW)
                    {
                        Node neighbor = null;

                        neighbor = node.LeftNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.RightNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.TopNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.BottomNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable())
                            {
                                hintItems.Add(node.item);

                                return true;
                            }
                        }
                    } // end if item is rainbow
                }
            }
        }

        return false;
    }

    bool GetHintByBreaker()
    {
        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                Node node = GetNode(i, j);
                if (node != null)
                {
                    if (node.item == null || !(node.item.Movable()))
                    {
                        continue;
                    }

                    if (node.item.IsBreaker(node.item.type))
                    {
                        Node neighbor = null;

                        neighbor = node.LeftNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.RightNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.TopNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }

                        neighbor = node.BottomNeighbor();
                        if (neighbor != null)
                        {
                            if (neighbor.item != null && neighbor.item.Movable() && neighbor.item.IsBreaker(neighbor.item.type))
                            {
                                hintItems.Add(neighbor.item);

                                hintItems.Add(node.item);

                                return true;
                            }
                        }
                    } // end if
                }
            }
        }

        return false;
    }

    #endregion

    #region Gingerbread

    bool GenerateGingerbread()
    {
        if (IsGingerbreadTarget() == false)
        {
            return false;
        }

        if (skipGenerateGingerbread == true)
        {
            return false;
        }

        // calculate the total gingerbread need to generate
        var needGenerate = 0;

        for (int i = 1; i <= 4; i++)
        {
            switch (i)
            {
                case 1:
                    if (LevelLoader.instance.target1Type == TARGET_TYPE.GINGERBREAD)
                    {
                        needGenerate += target1Left;
                    }
                    break;
                case 2:
                    if (LevelLoader.instance.target2Type == TARGET_TYPE.GINGERBREAD)
                    {
                        needGenerate += target2Left;
                    }
                    break;
                case 3:
                    if (LevelLoader.instance.target3Type == TARGET_TYPE.GINGERBREAD)
                    {
                        needGenerate += target3Left;
                    }
                    break;
                case 4:
                    if (LevelLoader.instance.target4Type == TARGET_TYPE.GINGERBREAD)
                    {
                        needGenerate += target4Left;
                    }
                    break;
            }
        }

        if (needGenerate <= 0)
        {
            return false;
        }

        // check gingerbread on board
        var amount = GingerbreadOnBoard().Count;

        if (amount >= LevelLoader.instance.maxGingerbread)
        {
            return false;
        }

        // prevent multiple call
        if (generatingGingerbread == true)
        {
            return false;
        }

        // skip generate randomly
        if (Random.Range(0, 2) == 0 && skipGingerbreadCount < 2)
        {
            skipGingerbreadCount++;
            return false;
        }
        skipGingerbreadCount = 0;

        generatingGingerbread = true;

        // get node to generate gingerbread
        var row = LevelLoader.instance.row - 1;
        var column = LevelLoader.instance.gingerbreadMarkers[Random.Range(0, LevelLoader.instance.gingerbreadMarkers.Count)];

        var node = GetNode(row, column);

        //print(node.name);

        if (node != null && node.item != null)
        {
            node.item.ChangeToGingerbread(LevelLoader.instance.RandomGingerbread());
            return true;
        }

        return false;
    }

    bool IsGingerbreadTarget()
    {
        if (LevelLoader.instance.target1Type == TARGET_TYPE.GINGERBREAD || 
            LevelLoader.instance.target2Type == TARGET_TYPE.GINGERBREAD ||
            LevelLoader.instance.target3Type == TARGET_TYPE.GINGERBREAD ||
            LevelLoader.instance.target4Type == TARGET_TYPE.GINGERBREAD)
        {
            return true;
        }

        return false;
    }

    List<Item> GingerbreadOnBoard()
    {
        var list = new List<Item>();

        var items = GetListItems();

        foreach (var item in items)
        {
            if (item != null && item.IsGingerbread())
            {
                list.Add(item);
            }
        }

        return list;
    }

    bool MoveGingerbread()
    {
        if (IsGingerbreadTarget() == false)
        {
            return false;
        }

        // prevent multiple call
        if (movingGingerbread == true)
        {
            return false;
        }
        movingGingerbread = true;

        var isMoved = false;

        //print("Move gingerbread");

        foreach (var gingerbread in GingerbreadOnBoard())
        {
            if (gingerbread != null)
            {
                var upper = GetUpperItem(gingerbread.node);

                if (upper != null && upper.node != null && upper.IsGingerbread() == false && gingerbread.node.cage == null)
                {
                    var gingerbreadPosition = NodeLocalPosition(upper.node.i, upper.node.j);
                    var upperItemPosition = NodeLocalPosition(gingerbread.node.i, gingerbread.node.j);

                    gingerbread.neighborNode = upper.node;
                    gingerbread.swapItem = upper;

                    touchedItem = gingerbread;
                    swappedItem = upper;

                    gingerbread.SwapItem();

                    gingerbread.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

                    // animation
                    iTween.MoveTo(gingerbread.gameObject, iTween.Hash(
                        "position", gingerbreadPosition,
                        "easetype", iTween.EaseType.linear,
                        "time", Configure.instance.swapTime
                    ));

                    iTween.MoveTo(upper.gameObject, iTween.Hash(
                        "position", upperItemPosition,
                        "easetype", iTween.EaseType.linear,
                        "time", Configure.instance.swapTime
                    ));
                }
                else if (upper == null || upper.node == null)
                {
                    AudioManager.instance.GingerbreadExplodeAudio();

                    gingerbread.color = LevelLoader.instance.RandomColor();

                    gingerbread.ChangeSpriteAndType(gingerbread.color);

                    // after changing a gingerbread to a cookie. skip generate one turn on generate call right after this function
                    skipGenerateGingerbread = true;
                }

                isMoved = true;
            }
        }

        return isMoved;
    }

    public Item GetUpperItem(Node node)
    {
        var top = node.TopNeighbor();

        if (top == null)
        {
            return null;
        }
        else 
        {
            if (top.tile.type == TILE_TYPE.NONE || top.tile.type == TILE_TYPE.PASS_THROUGH)
            {
                return GetUpperItem(top);
            }
            else if (top.item != null && top.item.Movable())
            {
                return top.item;
            }
            else
            {
                return node.item;
            }
        }
    }

    #endregion

    #region Booster

    void DestroyBoosterItems(Item boosterItem)
    {
        if (boosterItem == null)
        {
            return;
        }

        if (boosterItem.Destroyable() && booster != BOOSTER_TYPE.OVEN_BREAKER)
        {
            if (booster == BOOSTER_TYPE.RAINBOW_BREAKER && boosterItem.IsCookie() == false)
            {
                return;
            }

            lockSwap = true;

            switch (booster)
            {
                case BOOSTER_TYPE.SINGLE_BREAKER:
                    DestroySingleBooster(boosterItem);
                    break;
                case BOOSTER_TYPE.ROW_BREAKER:
                    StartCoroutine(DestroyRowBooster(boosterItem));
                    break;
                case BOOSTER_TYPE.COLUMN_BREAKER:
                    StartCoroutine(DestroyColumnBooster(boosterItem));
                    break;
                case BOOSTER_TYPE.RAINBOW_BREAKER:
                    StartCoroutine(DestroyRainbowBooster(boosterItem));
                    break;
            }

            Booster.instance.BoosterComplete();

            // hide help object
            if (LevelLoader.instance.level == 7 && Help.instance.step == 2)
            {
                Help.instance.Hide();
            }
            if (LevelLoader.instance.level == 12 && Help.instance.step == 2)
            {
                Help.instance.Hide();
            }
            if (LevelLoader.instance.level == 15 && Help.instance.step == 2)
            {
                Help.instance.Hide();
            }
            if (LevelLoader.instance.level == 18 && Help.instance.step == 2)
            {
                Help.instance.Hide();
            }
        }

        if (boosterItem.Movable() && booster == BOOSTER_TYPE.OVEN_BREAKER)
        {
            StartCoroutine(DestroyOvenBooster(boosterItem));
        }
    }

    void DestroySingleBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.SingleBoosterAudio();

        if (boosterItem.type == ITEM_TYPE.COOKIE_RAINBOW)
        {
            boosterItem.DestroyItemsSameColor(LevelLoader.instance.RandomColor());
        }

        boosterItem.Destroy();

        FindMatches();
    }

    IEnumerator DestroyRowBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.RowBoosterAudio();

        // animation

        // destroy a row
        var items = new List<Item>();

        items = RowItems(boosterItem.node.i);

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    item.DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }

            yield return new WaitForSeconds(0.1f);
        }

        FindMatches();
    }

    IEnumerator DestroyColumnBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.ColumnBoosterAudio();

        // animation

        // destroy a row
        var items = new List<Item>();

        items = ColumnItems(boosterItem.node.j);

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    item.DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }

            yield return new WaitForSeconds(0.1f);
        }

        FindMatches();
    }

    IEnumerator DestroyRainbowBooster(Item boosterItem)
    {
        HideHint();

        AudioManager.instance.RainbowBoosterAudio();

        boosterItem.DestroyItemsSameColor(boosterItem.color);

        yield return new WaitForFixedUpdate();
    }

    IEnumerator DestroyOvenBooster(Item boosterItem)
    {
        HideHint();

        if (ovenTouchItem == null)
        {
            ovenTouchItem = boosterItem;

            // add active
            ovenTouchItem.node.AddOvenBoosterActive();

            AudioManager.instance.ButtonClickAudio();
        }
        else
        {
            // the same item
            if (ovenTouchItem.node.OrderOnBoard() == boosterItem.node.OrderOnBoard())
            {
                // remove active
                ovenTouchItem.node.RemoveOvenBoosterActive();
                
                ovenTouchItem = null;

                AudioManager.instance.ButtonClickAudio();
            }
            // swap
            else
            {
                lockSwap = true;

                // hide help object
                if (LevelLoader.instance.level == 25 && Help.instance.step == 2)
                {
                    Help.instance.Hide();
                }

                boosterItem.node.AddOvenBoosterActive();

                AudioManager.instance.OvenBoosterAudio();

                AudioManager.instance.ButtonClickAudio();

                // animation
                iTween.MoveTo(ovenTouchItem.gameObject, iTween.Hash(
                    "position", boosterItem.gameObject.transform.position,                    
                    "easetype", iTween.EaseType.linear,
                    "time", Configure.instance.swapTime
                ));

                iTween.MoveTo(boosterItem.gameObject, iTween.Hash(
                    "position", ovenTouchItem.gameObject.transform.position,
                    "easetype", iTween.EaseType.linear,
                    "time", Configure.instance.swapTime
                ));

                yield return new WaitForSeconds(Configure.instance.swapTime);

                ovenTouchItem.node.RemoveOvenBoosterActive();
                boosterItem.node.RemoveOvenBoosterActive();

                var ovenTouchNode = ovenTouchItem.node;
                var boosterItemNode = boosterItem.node;

                // swap item
                ovenTouchNode.item = boosterItem;
                boosterItemNode.item = ovenTouchItem;

                // swap node
                ovenTouchItem.node = boosterItemNode;
                boosterItem.node = ovenTouchNode;

                // swap on hierarchy
                ovenTouchItem.gameObject.transform.SetParent(boosterItemNode.gameObject.transform);
                boosterItem.gameObject.transform.SetParent(ovenTouchNode.gameObject.transform);

                yield return new WaitForEndOfFrame();

                ovenTouchItem = null;

                Booster.instance.BoosterComplete();

                yield return new WaitForSeconds(0.1f);

                FindMatches();
            }
        }

        yield return new WaitForFixedUpdate();
    }

    #endregion 

    #region Collectible

    List<ITEM_TYPE> CheckGenerateCollectible()
    {
        if (LevelLoader.instance.target1Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target2Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target3Type != TARGET_TYPE.COLLECTIBLE &&
            LevelLoader.instance.target4Type != TARGET_TYPE.COLLECTIBLE)
        {
            return null;
        }

        var collectibles = new List<ITEM_TYPE>();

        if (CollectibleOnBoard() >= LevelLoader.instance.collectibleMaxOnBoard)
        {
            return null;
        }

        for (int i = 0; i <= 4; i++)
        {
            TARGET_TYPE targetType = TARGET_TYPE.NONE;
            int targetColor = 0;
            int collectibleOnBoard = 0;
            int targetLeft = 0;

            switch (i)
            {
                case 1:
                    targetType = LevelLoader.instance.target1Type;
                    targetColor = LevelLoader.instance.target1Color;
                    collectibleOnBoard = CollectibleOnBoard(LevelLoader.instance.target1Color);
                    targetLeft = target1Left;
                    break;
                case 2:
                    targetType = LevelLoader.instance.target2Type;
                    targetColor = LevelLoader.instance.target2Color;
                    collectibleOnBoard = CollectibleOnBoard(LevelLoader.instance.target2Color);
                    targetLeft = target2Left;
                    break;
                case 3:
                    targetType = LevelLoader.instance.target3Type;
                    targetColor = LevelLoader.instance.target3Color;
                    collectibleOnBoard = CollectibleOnBoard(LevelLoader.instance.target3Color);
                    targetLeft = target3Left;
                    break;
                case 4:
                    targetType = LevelLoader.instance.target4Type;
                    targetColor = LevelLoader.instance.target4Color;
                    collectibleOnBoard = CollectibleOnBoard(LevelLoader.instance.target4Color);
                    targetLeft = target4Left;
                    break;
            }

            if (targetType == TARGET_TYPE.COLLECTIBLE && collectibleOnBoard < targetLeft)
            {
                for (int k = 0; k < targetLeft - collectibleOnBoard; k++)
                {
                    collectibles.Add(ColorToCollectible(targetColor));
                }
            }

        }

        return collectibles;
    }

    ITEM_TYPE ColorToCollectible(int color)
    {
        switch (color)
        {
            case 1:
                return ITEM_TYPE.COLLECTIBLE_1;
            case 2:
                return ITEM_TYPE.COLLECTIBLE_2;
            case 3:
                return ITEM_TYPE.COLLECTIBLE_3;
            case 4:
                return ITEM_TYPE.COLLECTIBLE_4;
            case 5:
                return ITEM_TYPE.COLLECTIBLE_5;
            case 6:
                return ITEM_TYPE.COLLECTIBLE_6;
            case 7:
                return ITEM_TYPE.COLLECTIBLE_7;
            case 8:
                return ITEM_TYPE.COLLECTIBLE_8;
            case 9:
                return ITEM_TYPE.COLLECTIBLE_9;
            case 10:
                return ITEM_TYPE.COLLECTIBLE_10;
            case 11:
                return ITEM_TYPE.COLLECTIBLE_11;
            case 12:
                return ITEM_TYPE.COLLECTIBLE_12;
            case 13:
                return ITEM_TYPE.COLLECTIBLE_13;
            case 14:
                return ITEM_TYPE.COLLECTIBLE_14;
            case 15:
                return ITEM_TYPE.COLLECTIBLE_15;
            case 16:
                return ITEM_TYPE.COLLECTIBLE_16;
            case 17:
                return ITEM_TYPE.COLLECTIBLE_17;
            case 18:
                return ITEM_TYPE.COLLECTIBLE_18;
            case 19:
                return ITEM_TYPE.COLLECTIBLE_19;
            case 20:
                return ITEM_TYPE.COLLECTIBLE_20;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    int CollectibleOnBoard(int color = 0)
    {
        int amount = 0;

        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var node = GetNode(i, j);

                if (node != null && node.item != null && node.item.IsCollectible() == true)
                {
                    if (color == 0)
                    {
                        amount++;
                    }
                    else
                    {
                        if (node.item.color == color)
                        {
                            amount++;
                        }
                    }
                }
            }
        }

        return amount;
    }

    #endregion

    #region Marshmallow

    bool CheckGenerateMarshmallow()
    {
        if (LevelLoader.instance.target1Type != TARGET_TYPE.MARSHMALLOW &&
            LevelLoader.instance.target2Type != TARGET_TYPE.MARSHMALLOW &&
            LevelLoader.instance.target3Type != TARGET_TYPE.MARSHMALLOW &&
            LevelLoader.instance.target4Type != TARGET_TYPE.MARSHMALLOW)
        {
            return false;
        }

        var needGenerate = 0;

        for (int i = 1; i <= 4; i++)
        {
            switch (i)
            {
                case 1:
                    if (LevelLoader.instance.target1Type == TARGET_TYPE.MARSHMALLOW)
                    {
                        needGenerate += target1Left;
                    }
                    break;
                case 2:
                    if (LevelLoader.instance.target2Type == TARGET_TYPE.MARSHMALLOW)
                    {
                        needGenerate += target2Left;
                    }
                    break;
                case 3:
                    if (LevelLoader.instance.target3Type == TARGET_TYPE.MARSHMALLOW)
                    {
                        needGenerate += target3Left;
                    }
                    break;
                case 4:
                    if (LevelLoader.instance.target4Type == TARGET_TYPE.MARSHMALLOW)
                    {
                        needGenerate += target4Left;
                    }
                    break;
            }
        }

        if (needGenerate + LevelLoader.instance.marshmallowMoreThanTarget <= MarshmallowOnBoard())
        {
            return false;
        }

        return true;
    }

    int MarshmallowOnBoard()
    {
        int amount = 0;

        var row = LevelLoader.instance.row;
        var column = LevelLoader.instance.column;

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                var node = GetNode(i, j);

                if (node != null && node.item != null && node.item.IsMarshmallow() == true)
                {
                    amount++;
                }
            }
        }

        return amount;
    }

    #endregion

    #region Server

    public IEnumerator PostLevelScoretoServer()
    {
        // if logging to FB
     
        {
            //print("Try to post score to server but Facebook is not logged in");
        }

        yield return null;
    }

    #endregion
}
