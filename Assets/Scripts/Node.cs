using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
    [Header("Variables")]
    public Board board;
    public Tile tile;
    public Waffle waffle;
    public Item item;
    public Cage cage;
    public GameObject ovenActive;
    [Header("")]
    public int i; // row of node
    public int j; // column of node

    #region Neighbor

    public Node LeftNeighbor()
    {
        return board.GetNode(i, j - 1);
    }

    public Node RightNeighbor()
    {
        return board.GetNode(i, j + 1);
    }

    public Node TopNeighbor()
    {
        return board.GetNode(i - 1, j);
    }

    public Node BottomNeighbor()
    {
        return board.GetNode(i + 1, j);
    }

    public Node TopLeftNeighbor()
    {
        return board.GetNode(i - 1, j - 1);
    }

    public Node TopRightNeighbor()
    {
        return board.GetNode(i - 1, j + 1);
    }

    public Node BottomLeftNeighbor()
    {
        return board.GetNode(i + 1, j - 1);
    }

    public Node BottomRightNeighbor()
    {
        return board.GetNode(i + 1, j + 1);
    }

    #endregion

    #region Item

    // Some how the function does not return a object. 
    // It always return a null pointer.
    public Item GenerateItem(ITEM_TYPE type)
    {
        Item item = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_RAMDOM:
                GenerateRandomCookie();
                break;

            case ITEM_TYPE.COOKIE_RAINBOW:

            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:

            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:

            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:

            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:

            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:

            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:

            case ITEM_TYPE.MARSHMALLOW:

                InstantiateItem(type);
                break;

            case ITEM_TYPE.GINGERBREAD_RANDOM:
                GenerateRandomGingerbread();
                break;

            case ITEM_TYPE.GINGERBREAD_1:
            case ITEM_TYPE.GINGERBREAD_2:
            case ITEM_TYPE.GINGERBREAD_3:
            case ITEM_TYPE.GINGERBREAD_4:
            case ITEM_TYPE.GINGERBREAD_5:
            case ITEM_TYPE.GINGERBREAD_6:

            case ITEM_TYPE.CHOCOLATE_1_LAYER:
            case ITEM_TYPE.CHOCOLATE_2_LAYER:
            case ITEM_TYPE.CHOCOLATE_3_LAYER:
            case ITEM_TYPE.CHOCOLATE_4_LAYER:
            case ITEM_TYPE.CHOCOLATE_5_LAYER:
            case ITEM_TYPE.CHOCOLATE_6_LAYER:
                InstantiateItem(type);
                break;

            case ITEM_TYPE.ROCK_CANDY_RANDOM:
                GenerateRandomRockCandy();
                break;

            case ITEM_TYPE.ROCK_CANDY_1:
            case ITEM_TYPE.ROCK_CANDY_2:
            case ITEM_TYPE.ROCK_CANDY_3:
            case ITEM_TYPE.ROCK_CANDY_4:
            case ITEM_TYPE.ROCK_CANDY_5:
            case ITEM_TYPE.ROCK_CANDY_6:

            case ITEM_TYPE.COOKIE_1_X_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:

            case ITEM_TYPE.COLLECTIBLE_1:
            case ITEM_TYPE.COLLECTIBLE_2:
            case ITEM_TYPE.COLLECTIBLE_3:
            case ITEM_TYPE.COLLECTIBLE_4:
            case ITEM_TYPE.COLLECTIBLE_5:
            case ITEM_TYPE.COLLECTIBLE_6:
            case ITEM_TYPE.COLLECTIBLE_7:
            case ITEM_TYPE.COLLECTIBLE_8:
            case ITEM_TYPE.COLLECTIBLE_9:
            case ITEM_TYPE.COLLECTIBLE_10:
            case ITEM_TYPE.COLLECTIBLE_11:
            case ITEM_TYPE.COLLECTIBLE_12:
            case ITEM_TYPE.COLLECTIBLE_13:
            case ITEM_TYPE.COLLECTIBLE_14:
            case ITEM_TYPE.COLLECTIBLE_15:
            case ITEM_TYPE.COLLECTIBLE_16:
            case ITEM_TYPE.COLLECTIBLE_17:
            case ITEM_TYPE.COLLECTIBLE_18:
            case ITEM_TYPE.COLLECTIBLE_19:
            case ITEM_TYPE.COLLECTIBLE_20:

                InstantiateItem(type);
                break;

        }

        return item;
    }

    Item GenerateRandomCookie()
    {
        var type = LevelLoader.instance.RandomCookie();

        return InstantiateItem(type);
    }

    Item GenerateRandomGingerbread()
    {
        var type = LevelLoader.instance.RandomCookie();

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_1);
                break;

            case ITEM_TYPE.COOKIE_2:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_2);
                break;

            case ITEM_TYPE.COOKIE_3:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_3);
                break;

            case ITEM_TYPE.COOKIE_4:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_4);
                break;

            case ITEM_TYPE.COOKIE_5:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_5);
                break;

            case ITEM_TYPE.COOKIE_6:
                InstantiateItem(ITEM_TYPE.GINGERBREAD_6);
                break;
        }

        return null;
    }

    Item GenerateRandomRockCandy()
    {
        var type = LevelLoader.instance.RandomCookie();

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_1);
                break;

            case ITEM_TYPE.COOKIE_2:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_2);
                break;

            case ITEM_TYPE.COOKIE_3:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_3);
                break;

            case ITEM_TYPE.COOKIE_4:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_4);
                break;

            case ITEM_TYPE.COOKIE_5:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_5);
                break;

            case ITEM_TYPE.COOKIE_6:
                InstantiateItem(ITEM_TYPE.ROCK_CANDY_6);
                break;
        }

        return null;
    }

    Item InstantiateItem(ITEM_TYPE type)
    {
        GameObject piece = null;
        var color = 0;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_RAINBOW:
                piece = Instantiate(Resources.Load(Configure.CookieRainbow())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_1:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.Cookie1())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.Cookie1ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.Cookie1RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.Cookie1BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_2:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.Cookie2())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.Cookie2ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.Cookie2RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.Cookie2BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_3:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.Cookie3())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.Cookie3ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.Cookie3RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.Cookie3BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_4:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.Cookie4())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.Cookie4ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.Cookie4RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.Cookie4BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_5:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.Cookie5())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.Cookie5ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.Cookie5RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.Cookie5BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_6:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.Cookie6())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.Cookie6ColumnBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.Cookie6RowBreaker())) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.Cookie6BombBreaker())) as GameObject;
                break;

            case ITEM_TYPE.MARSHMALLOW:
                piece = Instantiate(Resources.Load(Configure.Marshmallow())) as GameObject;
                break;

            case ITEM_TYPE.GINGERBREAD_1:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.Gingerbread1())) as GameObject;
                break;
            case ITEM_TYPE.GINGERBREAD_2:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.Gingerbread2())) as GameObject;
                break;
            case ITEM_TYPE.GINGERBREAD_3:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.Gingerbread3())) as GameObject;
                break;
            case ITEM_TYPE.GINGERBREAD_4:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.Gingerbread4())) as GameObject;
                break;
            case ITEM_TYPE.GINGERBREAD_5:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.Gingerbread5())) as GameObject;
                break;
            case ITEM_TYPE.GINGERBREAD_6:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.Gingerbread6())) as GameObject;
                break;

            case ITEM_TYPE.CHOCOLATE_1_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate1())) as GameObject;
                break;
            case ITEM_TYPE.CHOCOLATE_2_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate2())) as GameObject;
                break;
            case ITEM_TYPE.CHOCOLATE_3_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate3())) as GameObject;
                break;
            case ITEM_TYPE.CHOCOLATE_4_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate4())) as GameObject;
                break;
            case ITEM_TYPE.CHOCOLATE_5_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate5())) as GameObject;
                break;
            case ITEM_TYPE.CHOCOLATE_6_LAYER:
                piece = Instantiate(Resources.Load(Configure.Chocolate6())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_1:
                color = 1;
                piece = Instantiate(Resources.Load(Configure.RockCandy1())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_2:
                color = 2;
                piece = Instantiate(Resources.Load(Configure.RockCandy2())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_3:
                color = 3;
                piece = Instantiate(Resources.Load(Configure.RockCandy3())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_4:
                color = 4;
                piece = Instantiate(Resources.Load(Configure.RockCandy4())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_5:
                color = 5;
                piece = Instantiate(Resources.Load(Configure.RockCandy5())) as GameObject;
                break;

            case ITEM_TYPE.ROCK_CANDY_6:
                color = 6;
                piece = Instantiate(Resources.Load(Configure.RockCandy6())) as GameObject;
                break;

            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie1XBreaker())) as GameObject;
                color = 1;
                break;

            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie2XBreaker())) as GameObject;
                color = 2;
                break;

            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie3XBreaker())) as GameObject;
                color = 3;
                break;

            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie4XBreaker())) as GameObject;
                color = 4;
                break;

            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie5XBreaker())) as GameObject;
                color = 5;
                break;

            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                piece = Instantiate(Resources.Load(Configure.Cookie6XBreaker())) as GameObject;
                color = 6;
                break;

            case ITEM_TYPE.COLLECTIBLE_1:
                piece = Instantiate(Resources.Load(Configure.Collectible1())) as GameObject;
                color = 1;
                break;

            case ITEM_TYPE.COLLECTIBLE_2:
                piece = Instantiate(Resources.Load(Configure.Collectible2())) as GameObject;
                color = 2;
                break;

            case ITEM_TYPE.COLLECTIBLE_3:
                piece = Instantiate(Resources.Load(Configure.Collectible3())) as GameObject;
                color = 3;
                break;

            case ITEM_TYPE.COLLECTIBLE_4:
                piece = Instantiate(Resources.Load(Configure.Collectible4())) as GameObject;
                color = 4;
                break;

            case ITEM_TYPE.COLLECTIBLE_5:
                piece = Instantiate(Resources.Load(Configure.Collectible5())) as GameObject;
                color = 5;
                break;

            case ITEM_TYPE.COLLECTIBLE_6:
                piece = Instantiate(Resources.Load(Configure.Collectible6())) as GameObject;
                color = 6;
                break;

            case ITEM_TYPE.COLLECTIBLE_7:
                piece = Instantiate(Resources.Load(Configure.Collectible7())) as GameObject;
                color = 7;
                break;

            case ITEM_TYPE.COLLECTIBLE_8:
                piece = Instantiate(Resources.Load(Configure.Collectible8())) as GameObject;
                color = 8;
                break;

            case ITEM_TYPE.COLLECTIBLE_9:
                piece = Instantiate(Resources.Load(Configure.Collectible9())) as GameObject;
                color = 9;
                break;

            case ITEM_TYPE.COLLECTIBLE_10:
                piece = Instantiate(Resources.Load(Configure.Collectible10())) as GameObject;
                color = 10;
                break;

            case ITEM_TYPE.COLLECTIBLE_11:
                piece = Instantiate(Resources.Load(Configure.Collectible11())) as GameObject;
                color = 11;
                break;

            case ITEM_TYPE.COLLECTIBLE_12:
                piece = Instantiate(Resources.Load(Configure.Collectible12())) as GameObject;
                color = 12;
                break;

            case ITEM_TYPE.COLLECTIBLE_13:
                piece = Instantiate(Resources.Load(Configure.Collectible13())) as GameObject;
                color = 13;
                break;

            case ITEM_TYPE.COLLECTIBLE_14:
                piece = Instantiate(Resources.Load(Configure.Collectible14())) as GameObject;
                color = 14;
                break;

            case ITEM_TYPE.COLLECTIBLE_15:
                piece = Instantiate(Resources.Load(Configure.Collectible15())) as GameObject;
                color = 15;
                break;

            case ITEM_TYPE.COLLECTIBLE_16:
                piece = Instantiate(Resources.Load(Configure.Collectible16())) as GameObject;
                color = 16;
                break;

            case ITEM_TYPE.COLLECTIBLE_17:
                piece = Instantiate(Resources.Load(Configure.Collectible17())) as GameObject;
                color = 17;
                break;

            case ITEM_TYPE.COLLECTIBLE_18:
                piece = Instantiate(Resources.Load(Configure.Collectible18())) as GameObject;
                color = 18;
                break;

            case ITEM_TYPE.COLLECTIBLE_19:
                piece = Instantiate(Resources.Load(Configure.Collectible19())) as GameObject;
                color = 19;
                break;

            case ITEM_TYPE.COLLECTIBLE_20:
                piece = Instantiate(Resources.Load(Configure.Collectible20())) as GameObject;
                color = 20;
                break;
        }

        if (piece != null)
        {
            piece.transform.SetParent(gameObject.transform);
            piece.name = "Item";
            piece.transform.localPosition = board.NodeLocalPosition(i, j);
            piece.GetComponent<Item>().node = this;
            piece.GetComponent<Item>().board = this.board;
            piece.GetComponent<Item>().type = type;
            piece.GetComponent<Item>().color = color;

            this.item = piece.GetComponent<Item>();
            
            return piece.GetComponent<Item>();
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region Match

    // find matches at a node
    public List<Item> FindMatches(FIND_DIRECTION direction = FIND_DIRECTION.NONE, int matches = 3)
    {
        var list = new List<Item>();
        var countedNodes = new Dictionary<int, Item>();

        if (item == null || !item.Matchable())
        {
            return null;
        }

        if (direction != FIND_DIRECTION.COLUMN)
        {
            countedNodes = FindMoreMatches(item.color, countedNodes, FIND_DIRECTION.ROW);
        }

        if (countedNodes.Count < matches)
        {
            countedNodes.Clear();
        }

        if (direction != FIND_DIRECTION.ROW)
        {
            countedNodes = FindMoreMatches(item.color, countedNodes, FIND_DIRECTION.COLUMN);
        }

        if (countedNodes.Count < matches)
        {
            countedNodes.Clear();
        }

        foreach (KeyValuePair<int, Item> entry in countedNodes)
        {
            list.Add(entry.Value);
        }

        return list;
    }

    // helper function to find matches
    Dictionary<int, Item> FindMoreMatches(int color, Dictionary<int, Item> countedNodes, FIND_DIRECTION direction)
    {
        if (item == null || item.destroying)
        {
            return countedNodes;
        }

        if (item.color == color && !countedNodes.ContainsValue(item) && item.Matchable() && item.node != null)
        {
            countedNodes.Add(item.node.OrderOnBoard(), item);

            if (direction == FIND_DIRECTION.ROW)
            {
                if (LeftNeighbor() != null)
                {
                    countedNodes = LeftNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
                }

                if (RightNeighbor() != null)
                {
                    countedNodes = RightNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.ROW);
                }
            }
            else if (direction == FIND_DIRECTION.COLUMN)
            {
                if (TopNeighbor() != null)
                {
                    countedNodes = TopNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
                }

                if (BottomNeighbor() != null)
                {
                    countedNodes = BottomNeighbor().FindMoreMatches(color, countedNodes, FIND_DIRECTION.COLUMN);
                }
            }
        }

        return countedNodes;
    }

    #endregion

    #region Utility

    // return the order base on i and j
    public int OrderOnBoard()
    {
        return (i * LevelLoader.instance.column + j);
    }

    #endregion

    #region Type

    public bool CanStoreItem()
    {
        if (tile != null)
        {
            if (tile.type == TILE_TYPE.DARD_TILE || tile.type == TILE_TYPE.LIGHT_TILE)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanGoThrough()
    {
        if (tile == null || tile.type == TILE_TYPE.NONE)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool CanGenerateNewItem()
    {
        if (CanStoreItem() == true)
        {
            for (int row = i - 1; row >= 0; row--)
            {
                Node upNode = board.GetNode(row, j);

                if (upNode != null)
                {
                    if (upNode.CanGoThrough() == false)
                    {
                        return false;
                    }
                    else
                    {
                        if (upNode.item != null)
                        {
                            if (upNode.item.Movable() == false)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Node

    // get source node of an empty node
    public Node GetSourceNode()
    {
        Node source = null;

        // top
        Node top = board.GetNode(i - 1, j);
        if (top != null)
        {
            if (top.item == null && top.CanGoThrough())
            {
                if (top.GetSourceNode() != null)
                {
                    source = top.GetSourceNode();
                }
            }
        }

        if (source != null)
        {
            return source;
        }

        // left
        Node left = board.GetNode(i - 1, j - 1);
        if (left != null)
        {
            if (left.item == null && left.CanGoThrough())
            {
                if (left.GetSourceNode() != null)
                {
                    source = left.GetSourceNode();
                }
            }
            else
            {
                if (left.item != null && left.item.Movable())
                {
                    source = left;
                }
            }
        }

        if (source != null)
        {
            return source;
        }

        // right
        Node right = board.GetNode(i - 1, j + 1);
        if (right != null)
        {
            if (right.item == null && right.CanGoThrough())
            {
                if (right.GetSourceNode() != null)
                {
                    source = right.GetSourceNode();
                }
            }
            else
            {
                if (right.item != null && right.item.Movable())
                {
                    source = right;
                }
            }
        }

        return source;
    }

    // get move path from an empty node to source node
    public List<Vector3> GetMovePath()
    {
        List<Vector3> path = new List<Vector3>();

        path.Add(board.NodeLocalPosition(i, j));

        // top
        Node top = board.GetNode(i - 1, j);
        if (top != null)
        {
            if (top.item == null && top.CanGoThrough())
            {
                if (top.GetSourceNode() != null)
                {
                    path.AddRange(top.GetMovePath());
                    return path;
                }
            }
        }

        // left
        Node left = board.GetNode(i - 1, j - 1);
        if (left != null)
        {
            if (left.item == null && left.CanGoThrough())
            {
                if (left.GetSourceNode() != null)
                {
                    path.AddRange(left.GetMovePath());
                    return path;
                }
            }
            else
            {
                if (left.item != null && left.item.Movable())
                {
                    return path;
                }
            }
        }

        // right
        Node right = board.GetNode(i - 1, j + 1);
        if (right != null)
        {
            if (right.item == null && right.CanGoThrough())
            {
                if (right.GetSourceNode() != null)
                {
                    path.AddRange(right.GetMovePath());
                    return path;
                }
            }
            else
            {
                if (right.item != null && right.item.Movable())
                {
                    return path;
                }
            }
        }

        return path;
    }

    #endregion

    #region Waffle

    public void WaffleExplode()
    {
        if (waffle != null && item != null & (item.IsCookie() == true || item.IsBreaker(item.type) || item.type == ITEM_TYPE.COOKIE_RAINBOW))
        {
            AudioManager.instance.WaffleExplodeAudio();

            board.CollectWaffle(waffle);

            GameObject prefab = null;

            if (waffle.type == WAFFLE_TYPE.WAFFLE_3)
            {
                prefab = Resources.Load(Configure.Waffle2()) as GameObject;

                waffle.gameObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

                waffle.type = WAFFLE_TYPE.WAFFLE_2;
            }
            else if (waffle.type == WAFFLE_TYPE.WAFFLE_2)
            {
                prefab = Resources.Load(Configure.Waffle1()) as GameObject;

                waffle.gameObject.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;

                waffle.type = WAFFLE_TYPE.WAFFLE_1;
            }
            else if (waffle.type == WAFFLE_TYPE.WAFFLE_1)
            {
                Destroy(waffle.gameObject);

                waffle = null;
            }
        }
    }

    #endregion

    #region Cage

    public void CageExplode()
    {
        if (cage == null)
        {
            return;
        }

        GameObject explosion = null;

        if (item != null)
        {
            switch (item.GetCookie(item.type))
            {
                case ITEM_TYPE.COOKIE_1:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BlueCookieExplosion()) as GameObject);
                    break;
                case ITEM_TYPE.COOKIE_2:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.GreenCookieExplosion()) as GameObject);
                    break;
                case ITEM_TYPE.COOKIE_3:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.OrangeCookieExplosion()) as GameObject);
                    break;
                case ITEM_TYPE.COOKIE_4:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.PurpleCookieExplosion()) as GameObject);
                    break;
                case ITEM_TYPE.COOKIE_5:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RedCookieExplosion()) as GameObject);
                    break;
                case ITEM_TYPE.COOKIE_6:
                    explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.YellowCookieExplosion()) as GameObject);
                    break;
            }
        }

        board.CollectCage(cage);

        if (explosion != null) explosion.transform.position = item.transform.position;

        AudioManager.instance.CageExplodeAudio();

        Destroy(cage.gameObject);

        cage = null;

        StartCoroutine(item.ResetDestroying());
    }

    #endregion

    #region Booster

    public void AddOvenBoosterActive()
    {
        ovenActive = Instantiate(Resources.Load(Configure.BoosterActive())) as GameObject;

        ovenActive.transform.localPosition = board.NodeLocalPosition(i, j);
    }

    public void RemoveOvenBoosterActive()
    {
        Destroy(ovenActive);

        ovenActive = null;
    }

    #endregion
}
