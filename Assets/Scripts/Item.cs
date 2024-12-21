using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour 
{
    [Header("Parent")]
    public Board board;
    public Node node;

    [Header("Variables")]
    public int color;
    public ITEM_TYPE type;
    public ITEM_TYPE next = ITEM_TYPE.NONE;    
    public BREAKER_EFFECT effect = BREAKER_EFFECT.NORMAL;

    [Header("Check")]
    public bool drag;
    public bool nextSound = true;
    public bool destroying;
    public bool dropping;
    public bool changing;
    public Vector3 mousePostion = Vector3.zero;
    public Vector3 deltaPosition = Vector3.zero;
    public Vector3 swapDirection = Vector3.zero;

    [Header("Swap")]
    public Node neighborNode;
    public Item swapItem;

    [Header("Drop")]
    public List<Vector3> dropPath;
    
    void Update()
    {
        // if a item is dragged
        if (drag)
        {
            deltaPosition = mousePostion - GetMousePosition();
            if (swapDirection == Vector3.zero)
            {
                SwapDirection(deltaPosition);
            }
        }
    }

    #region Type

    public bool Movable()
    {
        if (type == ITEM_TYPE.CHOCOLATE_1_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_2_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_3_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_4_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_5_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_6_LAYER ||
            type == ITEM_TYPE.ROCK_CANDY_1||
            type == ITEM_TYPE.ROCK_CANDY_2||
            type == ITEM_TYPE.ROCK_CANDY_3||
            type == ITEM_TYPE.ROCK_CANDY_4||
            type == ITEM_TYPE.ROCK_CANDY_5||
            type == ITEM_TYPE.ROCK_CANDY_6)
        {
            return false;
        }

        // cage
        if (node.cage != null)
        {
            if (node.cage.type == CAGE_TYPE.CAGE_1)
            {
                return false;
            }
        }

        return true;
    }

    public bool Matchable()
    {
        if (type == ITEM_TYPE.CHOCOLATE_1_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_2_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_3_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_4_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_5_LAYER ||
            type == ITEM_TYPE.CHOCOLATE_6_LAYER ||
            type == ITEM_TYPE.ROCK_CANDY_1 ||
            type == ITEM_TYPE.ROCK_CANDY_2 ||
            type == ITEM_TYPE.ROCK_CANDY_3 ||
            type == ITEM_TYPE.ROCK_CANDY_4 ||
            type == ITEM_TYPE.ROCK_CANDY_5 ||
            type == ITEM_TYPE.ROCK_CANDY_6 ||
            type == ITEM_TYPE.MARSHMALLOW ||
            type == ITEM_TYPE.COOKIE_RAINBOW ||
            type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9)
        {
            return false;
        }

        return true;
    }

    public bool Destroyable()
    {
        if (type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9 || 
            type == ITEM_TYPE.COLLECTIBLE_10 ||
            type == ITEM_TYPE.COLLECTIBLE_11 ||
            type == ITEM_TYPE.COLLECTIBLE_12 ||
            type == ITEM_TYPE.COLLECTIBLE_13 ||
            type == ITEM_TYPE.COLLECTIBLE_14 ||
            type == ITEM_TYPE.COLLECTIBLE_15 ||
            type == ITEM_TYPE.COLLECTIBLE_16 ||
            type == ITEM_TYPE.COLLECTIBLE_17 ||
            type == ITEM_TYPE.COLLECTIBLE_18 ||
            type == ITEM_TYPE.COLLECTIBLE_19 ||
            type == ITEM_TYPE.COLLECTIBLE_20)
        {
            return false;
        }

        return true;
    }

    public bool IsCookie()
    {
        if (type == ITEM_TYPE.COOKIE_1 ||
               type == ITEM_TYPE.COOKIE_2 ||
               type == ITEM_TYPE.COOKIE_3 ||
               type == ITEM_TYPE.COOKIE_4 ||
               type == ITEM_TYPE.COOKIE_5 ||
               type == ITEM_TYPE.COOKIE_6)
        {
            return true;
        }

        return false;

    }

    public bool IsCollectible()
    {
        if (type == ITEM_TYPE.COLLECTIBLE_1 ||
            type == ITEM_TYPE.COLLECTIBLE_2 ||
            type == ITEM_TYPE.COLLECTIBLE_3 ||
            type == ITEM_TYPE.COLLECTIBLE_4 ||
            type == ITEM_TYPE.COLLECTIBLE_5 ||
            type == ITEM_TYPE.COLLECTIBLE_6 ||
            type == ITEM_TYPE.COLLECTIBLE_7 ||
            type == ITEM_TYPE.COLLECTIBLE_8 ||
            type == ITEM_TYPE.COLLECTIBLE_9 ||
            type == ITEM_TYPE.COLLECTIBLE_10 ||
            type == ITEM_TYPE.COLLECTIBLE_11 ||
            type == ITEM_TYPE.COLLECTIBLE_12 ||
            type == ITEM_TYPE.COLLECTIBLE_13 ||
            type == ITEM_TYPE.COLLECTIBLE_14 ||
            type == ITEM_TYPE.COLLECTIBLE_15 ||
            type == ITEM_TYPE.COLLECTIBLE_16 ||
            type == ITEM_TYPE.COLLECTIBLE_17 ||
            type == ITEM_TYPE.COLLECTIBLE_18 ||
            type == ITEM_TYPE.COLLECTIBLE_19 ||
            type == ITEM_TYPE.COLLECTIBLE_20)
        {
            return true;
        }

        return false;
    }

    public bool IsGingerbread()
    {
        if (type == ITEM_TYPE.GINGERBREAD_1 ||
               type == ITEM_TYPE.GINGERBREAD_2 ||
               type == ITEM_TYPE.GINGERBREAD_3 ||
               type == ITEM_TYPE.GINGERBREAD_4 ||
               type == ITEM_TYPE.GINGERBREAD_5 ||
               type == ITEM_TYPE.GINGERBREAD_6)
        {
            return true;
        }

        return false;
    }

    public bool IsMarshmallow()
    {
        if (type == ITEM_TYPE.MARSHMALLOW)
        {
            return true;
        }
        return false;
    }

    public bool IsChocolate()
    {
        if (type == ITEM_TYPE.CHOCOLATE_1_LAYER ||
               type == ITEM_TYPE.CHOCOLATE_2_LAYER ||
               type == ITEM_TYPE.CHOCOLATE_3_LAYER ||
               type == ITEM_TYPE.CHOCOLATE_4_LAYER ||
               type == ITEM_TYPE.CHOCOLATE_5_LAYER ||
               type == ITEM_TYPE.CHOCOLATE_6_LAYER)
        {
            return true;
        }

        return false;
    }

    public bool IsRockCandy()
    {
        if (type == ITEM_TYPE.ROCK_CANDY_1 ||
               type == ITEM_TYPE.ROCK_CANDY_2 ||
               type == ITEM_TYPE.ROCK_CANDY_3 ||
               type == ITEM_TYPE.ROCK_CANDY_4 ||
               type == ITEM_TYPE.ROCK_CANDY_5 ||
               type == ITEM_TYPE.ROCK_CANDY_6)
        {
            return true;
        }

        return false;
    }

    public ITEM_TYPE OriginCookieType()
    {
        var order = board.NodeOrder(node.i, node.j);

        return LevelLoader.instance.itemLayerData[order];
    }

    // check if breaker is row or column
    ITEM_TYPE GetColRowBreaker(ITEM_TYPE check, Vector3 direction)
    {
        //print("direction: " + direction);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //print("row");

            switch (check)
            {
                case ITEM_TYPE.COOKIE_1:
                case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_1_X_BREAKER:
                    return ITEM_TYPE.COOKIE_1_ROW_BREAKER;
                case ITEM_TYPE.COOKIE_2:
                case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_2_X_BREAKER:
                    return ITEM_TYPE.COOKIE_2_ROW_BREAKER;
                case ITEM_TYPE.COOKIE_3:
                case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_3_X_BREAKER:
                    return ITEM_TYPE.COOKIE_3_ROW_BREAKER;
                case ITEM_TYPE.COOKIE_4:
                case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_4_X_BREAKER:
                    return ITEM_TYPE.COOKIE_4_ROW_BREAKER;
                case ITEM_TYPE.COOKIE_5:
                case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_5_X_BREAKER:
                    return ITEM_TYPE.COOKIE_5_ROW_BREAKER;
                case ITEM_TYPE.COOKIE_6:
                case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_6_X_BREAKER:
                    return ITEM_TYPE.COOKIE_6_ROW_BREAKER;
                default:
                    return ITEM_TYPE.NONE;
            }
        }
        else
        {
            //print("colmn");

            switch (check)
            {
                case ITEM_TYPE.COOKIE_1:
                case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_1_X_BREAKER:
                    return ITEM_TYPE.COOKIE_1_COLUMN_BREAKER;
                case ITEM_TYPE.COOKIE_2:
                case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_2_X_BREAKER:
                    return ITEM_TYPE.COOKIE_2_COLUMN_BREAKER;
                case ITEM_TYPE.COOKIE_3:
                case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_3_X_BREAKER:
                    return ITEM_TYPE.COOKIE_3_COLUMN_BREAKER;
                case ITEM_TYPE.COOKIE_4:
                case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_4_X_BREAKER:
                    return ITEM_TYPE.COOKIE_4_COLUMN_BREAKER;
                case ITEM_TYPE.COOKIE_5:
                case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_5_X_BREAKER:
                    return ITEM_TYPE.COOKIE_5_COLUMN_BREAKER;
                case ITEM_TYPE.COOKIE_6:
                case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
                case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
                case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
                case ITEM_TYPE.COOKIE_6_X_BREAKER:
                    return ITEM_TYPE.COOKIE_6_COLUMN_BREAKER;
                default:
                    return ITEM_TYPE.NONE;
            }
        }
    }

    public bool IsBombBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.COOKIE_1_BOMB_BREAKER ||
            check == ITEM_TYPE.COOKIE_2_BOMB_BREAKER ||
            check == ITEM_TYPE.COOKIE_3_BOMB_BREAKER ||
            check == ITEM_TYPE.COOKIE_4_BOMB_BREAKER ||
            check == ITEM_TYPE.COOKIE_5_BOMB_BREAKER ||
            check == ITEM_TYPE.COOKIE_6_BOMB_BREAKER)
        {
            return true;
        }

        return false;
    }

    public bool IsXBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.COOKIE_1_X_BREAKER ||
            check == ITEM_TYPE.COOKIE_2_X_BREAKER ||
            check == ITEM_TYPE.COOKIE_3_X_BREAKER ||
            check == ITEM_TYPE.COOKIE_4_X_BREAKER ||
            check == ITEM_TYPE.COOKIE_5_X_BREAKER ||
            check == ITEM_TYPE.COOKIE_6_X_BREAKER)
        {
            return true;
        }

        return false;
    }

    public bool IsColumnBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.COOKIE_1_COLUMN_BREAKER ||
            check == ITEM_TYPE.COOKIE_2_COLUMN_BREAKER ||
            check == ITEM_TYPE.COOKIE_3_COLUMN_BREAKER ||
            check == ITEM_TYPE.COOKIE_4_COLUMN_BREAKER ||
            check == ITEM_TYPE.COOKIE_5_COLUMN_BREAKER ||
            check == ITEM_TYPE.COOKIE_6_COLUMN_BREAKER)
        {
            return true;
        }

        return false;
    }

    public bool IsRowBreaker(ITEM_TYPE check)
    {
        if (check == ITEM_TYPE.COOKIE_1_ROW_BREAKER ||
            check == ITEM_TYPE.COOKIE_2_ROW_BREAKER ||
            check == ITEM_TYPE.COOKIE_3_ROW_BREAKER ||
            check == ITEM_TYPE.COOKIE_4_ROW_BREAKER ||
            check == ITEM_TYPE.COOKIE_5_ROW_BREAKER ||
            check == ITEM_TYPE.COOKIE_6_ROW_BREAKER)
        {
            return true;
        }

        return false;
    }

    public bool IsBreaker(ITEM_TYPE check)
    {
        if (IsBombBreaker(check) || IsXBreaker(check) || IsColumnBreaker(check) || IsRowBreaker(check))
        {
            return true;
        }

        return false;
    }

    public ITEM_TYPE GetBombBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                return ITEM_TYPE.COOKIE_1_BOMB_BREAKER;
            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                return ITEM_TYPE.COOKIE_2_BOMB_BREAKER;
            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                return ITEM_TYPE.COOKIE_3_BOMB_BREAKER;
            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                return ITEM_TYPE.COOKIE_4_BOMB_BREAKER;
            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                return ITEM_TYPE.COOKIE_5_BOMB_BREAKER;
            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                return ITEM_TYPE.COOKIE_6_BOMB_BREAKER;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetXBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                return ITEM_TYPE.COOKIE_1_X_BREAKER;
            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                return ITEM_TYPE.COOKIE_2_X_BREAKER;
            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                return ITEM_TYPE.COOKIE_3_X_BREAKER;
            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                return ITEM_TYPE.COOKIE_4_X_BREAKER;
            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                return ITEM_TYPE.COOKIE_5_X_BREAKER;
            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                return ITEM_TYPE.COOKIE_6_X_BREAKER;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetColumnBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                return ITEM_TYPE.COOKIE_1_COLUMN_BREAKER;
            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                return ITEM_TYPE.COOKIE_2_COLUMN_BREAKER;
            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                return ITEM_TYPE.COOKIE_3_COLUMN_BREAKER;
            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                return ITEM_TYPE.COOKIE_4_COLUMN_BREAKER;
            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                return ITEM_TYPE.COOKIE_5_COLUMN_BREAKER;
            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                return ITEM_TYPE.COOKIE_6_COLUMN_BREAKER;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetRowBreaker(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                return ITEM_TYPE.COOKIE_1_ROW_BREAKER;
            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                return ITEM_TYPE.COOKIE_2_ROW_BREAKER;
            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                return ITEM_TYPE.COOKIE_3_ROW_BREAKER;
            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                return ITEM_TYPE.COOKIE_4_ROW_BREAKER;
            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                return ITEM_TYPE.COOKIE_5_ROW_BREAKER;
            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                return ITEM_TYPE.COOKIE_6_ROW_BREAKER;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    public ITEM_TYPE GetCookie(ITEM_TYPE check)
    {
        switch (check)
        {
            case ITEM_TYPE.COOKIE_1:
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_1_X_BREAKER:
                return ITEM_TYPE.COOKIE_1;
            case ITEM_TYPE.COOKIE_2:
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_2_X_BREAKER:
                return ITEM_TYPE.COOKIE_2;
            case ITEM_TYPE.COOKIE_3:
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_3_X_BREAKER:
                return ITEM_TYPE.COOKIE_3;
            case ITEM_TYPE.COOKIE_4:
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_4_X_BREAKER:
                return ITEM_TYPE.COOKIE_4;
            case ITEM_TYPE.COOKIE_5:
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_5_X_BREAKER:
                return ITEM_TYPE.COOKIE_5;
            case ITEM_TYPE.COOKIE_6:
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
            case ITEM_TYPE.COOKIE_6_X_BREAKER:
                return ITEM_TYPE.COOKIE_6;
            default:
                return ITEM_TYPE.NONE;
        }
    }

    #endregion

    #region Swap
    // helper function to know the direction of swap
    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // calculate the direction
    void SwapDirection(Vector3 delta)
    {
        deltaPosition = delta;

        var direction = SWAP_DIRECTION.NONE;

        if (Vector3.Magnitude(deltaPosition) > 0.1f)
        {
            if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y) && deltaPosition.x > 0) swapDirection.x = 1;
            else if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y) && deltaPosition.x < 0) swapDirection.x = -1;
            else if (Mathf.Abs(deltaPosition.x) < Mathf.Abs(deltaPosition.y) && deltaPosition.y > 0) swapDirection.y = 1;
            else if (Mathf.Abs(deltaPosition.x) < Mathf.Abs(deltaPosition.y) && deltaPosition.y < 0) swapDirection.y = -1;

            if (swapDirection.x > 0)
            {
                //Debug.Log("Left");
                direction = SWAP_DIRECTION.LEFT;

                if (node != null)
                {
                    if (node.LeftNeighbor() != null)
                    {
                        if (node.LeftNeighbor().item != null)
                        {
                            if (node.LeftNeighbor().item.Movable())
                            {
                                neighborNode = node.LeftNeighbor();
                            }
                        }
                    }
                }
            }
            else if (swapDirection.x < 0)
            {
                //Debug.Log("Right");
                direction = SWAP_DIRECTION.RIGHT;

                if (node != null)
                {
                    if (node.RightNeighbor() != null)
                    {
                        if (node.RightNeighbor().item != null)
                        {
                            if (node.RightNeighbor().item.Movable())
                            {
                                neighborNode = node.RightNeighbor();
                            }
                        }
                    }
                }
            }
            else if (swapDirection.y > 0)
            {
                //Debug.Log("Bottom");
                direction = SWAP_DIRECTION.BOTTOM;

                if (node != null)
                {
                    if (node.BottomNeighbor() != null)
                    {
                        if (node.BottomNeighbor().item != null)
                        {
                            if (node.BottomNeighbor().item.Movable())
                            {
                                neighborNode = node.BottomNeighbor();
                            }
                        }
                    }
                }
            }
            else if (swapDirection.y < 0)
            {
                //Debug.Log("Top");
                direction = SWAP_DIRECTION.TOP;

                if (node != null)
                {
                    if (node.TopNeighbor() != null)
                    {
                        if (node.TopNeighbor().item != null)
                        {
                            if (node.TopNeighbor().item.Movable())
                            {
                                neighborNode = node.TopNeighbor();
                            }
                        }
                    }
                }
            }

            if (neighborNode != null && neighborNode.item != null && CheckHelpSwapable(direction) == true)
            {
                swapItem = neighborNode.item;

                board.touchedItem = this;
                board.swappedItem = swapItem;

                Swap();
            }
            else
            {
                // if no neighbor item we need to reset to be able to swap again
                Reset();
            }
        }
    }

    // swap animation
    public void Swap(bool forced = false)
    {
        if (swapDirection != Vector3.zero && neighborNode != null)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", swapItem.transform.position,
                "onstart", "OnStartSwap",
                "oncomplete", "OnCompleteSwap",
                "oncompleteparams", new Hashtable() { { "forced", forced } },
                "easetype", iTween.EaseType.linear,
                "time", Configure.instance.swapTime
            ));

            iTween.MoveTo(swapItem.gameObject, iTween.Hash(
                "position", transform.position,
                "easetype", iTween.EaseType.linear,
                "time", Configure.instance.swapTime
            ));
        }
    }

    public void OnStartSwap()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        AudioManager.instance.SwapAudio();

        board.lockSwap = true;

        board.HideHint();

        board.dropTime = 1;

        // hide help if need
        Help.instance.Hide();
    }

    public void OnCompleteSwap(Hashtable args)
    {
        var forced = (bool)args["forced"];

        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        SwapItem();
        
        // after swap this.node = neighbor
        var matchesHere = (node.FindMatches() != null)? node.FindMatches().Count : 0;
        var matchesAtNeighbor = (swapItem.node.FindMatches() != null)? swapItem.node.FindMatches().Count : 0;
        var special = false;

        //print("matches here:" + matchesHere);
        //print("matches at neighbor" + matchesAtNeighbor);

        // one of items is rainbow and other is cookie
        if (type == ITEM_TYPE.COOKIE_RAINBOW && (swapItem.IsCookie() || IsBreaker(swapItem.type) || swapItem.type == ITEM_TYPE.COOKIE_RAINBOW))
        {
            special = true;
        }
        else if (swapItem.type == ITEM_TYPE.COOKIE_RAINBOW && (IsCookie() || IsBreaker(type) || type == ITEM_TYPE.COOKIE_RAINBOW))
        {
            special = true;
        }
        else if (IsBreaker(type) && IsBreaker(swapItem.type))
        {
            special = true;
        }

        if (matchesHere <= 0 && matchesAtNeighbor <= 0 && special == false && Configure.instance.checkSwap == true && forced == false)
        {
            // swap back
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", swapItem.transform.position,
                "onstart", "OnStartSwapBack",
                "oncomplete", "OnCompleteSwapBack",
                "easetype", iTween.EaseType.linear,
                "time", Configure.instance.swapTime
            ));
            
            iTween.MoveTo(swapItem.gameObject, iTween.Hash(
                "position", transform.position,
                "easetype", iTween.EaseType.linear,
                "time", Configure.instance.swapTime
                ));
        }
        else
        {   
            // do not reduce move when forced swap
            if (forced == false)
            {
                board.moveLeft--;
                board.UITop.DecreaseMoves();
            }

            if (special == true)
            {
                RainbowDestroy(this, swapItem);

                TwoColRowBreakerDestroy(this, swapItem);

                TwoBombBreakerDestroy(this, swapItem);

                TwoXBreakerDestory(this, swapItem);

                ColRowBreakerAndBombBreakerDestroy(this, swapItem);

                ColRowBreakerAndXBreakerDestroy(this, swapItem);

                BombBreakerAndXBreakerDestroy(this, swapItem);
            }
            else
            {
                if (matchesHere == 4)
                {
                    next = GetColRowBreaker(this.type, transform.position - swapItem.transform.position);
                }
                else if (matchesAtNeighbor == 4)
                {
                    swapItem.next = GetColRowBreaker(swapItem.type, transform.position - swapItem.transform.position);
                }
                else if (matchesHere >= 5)
                {
                    next = ITEM_TYPE.COOKIE_RAINBOW;
                }
                else if (matchesAtNeighbor >= 5)
                {
                    swapItem.next = ITEM_TYPE.COOKIE_RAINBOW;
                }

                // find the matches to destroy (destroy match 3/4/5)
                // this function will not destroy special match such as rainbow swap with breaker etc.
                board.FindMatches();
            }

            // we reset here because the item will be destroy soon (the board is still lock)
            Reset();
        }
    }

    public void OnStartSwapBack()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

        AudioManager.instance.SwapBackAudio();

        // hide in case the help crash
        if (Help.instance.gameObject.activeSelf == true)
        {
            Help.instance.HideOnSwapBack();
        }        
    }

    public void OnCompleteSwapBack()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        SwapItemBack();

        // fix swap back wrong position cause by iTween
        transform.position = board.NodeLocalPosition(node.i, node.j);

        Reset();

        board.lockSwap = false;

        StartCoroutine(board.ShowHint());
    }

    public void SwapItem()
    {
        Node thisNode = node;

        thisNode.item = swapItem;
        neighborNode.item = this;

        this.node = neighborNode;
        swapItem.node = thisNode;

        this.gameObject.transform.SetParent(neighborNode.gameObject.transform);
        swapItem.gameObject.transform.SetParent(thisNode.gameObject.transform);
    }

    void SwapItemBack()
    {
        Node swapNode = swapItem.node;

        this.node.item = swapItem;
        swapNode.item = this;

        this.node = swapItem.node;
        swapItem.node = neighborNode;

        this.gameObject.transform.SetParent(swapNode.gameObject.transform);
        swapItem.gameObject.transform.SetParent(neighborNode.gameObject.transform);
    }

    // reset info after a swap
    public void Reset()
    {
        drag = false;

        swapDirection = Vector3.zero;

        neighborNode = null;

        swapItem = null;
    }

    bool CheckHelpSwapable(SWAP_DIRECTION direction)
    {
        if (LevelLoader.instance.level == 1)
        {
            if (Help.instance.step == 2)
            {
                if (node.OrderOnBoard() == 5 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 14 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (LevelLoader.instance.level == 2)
        {
            if (Help.instance.step == 1)
            {
                if (node.OrderOnBoard() == 10 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 17 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Help.instance.step == 2)
            {
                if (node.OrderOnBoard() == 19 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 26 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (LevelLoader.instance.level == 3)
        {
            if (Help.instance.step == 1)
            {
                if (node.OrderOnBoard() == 14 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 22 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Help.instance.step == 2)
            {
                if (node.OrderOnBoard() == 38 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 46 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Help.instance.step == 3)
            {
                if (node.OrderOnBoard() == 18 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 26 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Help.instance.step == 4)
            {
                if (node.OrderOnBoard() == 26 && direction == SWAP_DIRECTION.BOTTOM)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 34 && direction == SWAP_DIRECTION.TOP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (LevelLoader.instance.level == 4)
        {
            if (Help.instance.step == 1)
            {
                if (node.OrderOnBoard() == 36 && direction == SWAP_DIRECTION.RIGHT)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 37 && direction == SWAP_DIRECTION.LEFT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Help.instance.step == 2)
            {
                if (node.OrderOnBoard() == 52 && direction == SWAP_DIRECTION.RIGHT)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 53 && direction == SWAP_DIRECTION.LEFT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (LevelLoader.instance.level == 5)
        {
            if (Help.instance.step == 1)
            {
                if (node.OrderOnBoard() == 42 && direction == SWAP_DIRECTION.RIGHT)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 43 && direction == SWAP_DIRECTION.LEFT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (LevelLoader.instance.level == 6)
        {
            if (Help.instance.step == 1)
            {
                if (node.OrderOnBoard() == 45 && direction == SWAP_DIRECTION.RIGHT)
                {
                    return true;
                }
                else if (node.OrderOnBoard() == 46 && direction == SWAP_DIRECTION.LEFT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    #endregion

    #region ColorAndAppear

    // after the board is generate we need to alter the color to make sure there is no "pre-matches" on the board
    public void GenerateColor(int except)
    {
        var colors = new List<int>();

        var usingColors = LevelLoader.instance.usingColors;

        for (int i = 0; i < usingColors.Count; i++)
        {
            int color = usingColors[i];

            bool generatable = true;
            Node neighbor = null;

            neighbor = node.TopNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            neighbor = node.LeftNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            neighbor = node.RightNeighbor();
            if (neighbor != null)
            {
                if (neighbor.item != null)
                {
                    if (neighbor.item.color == color)
                    {
                        generatable = false;
                    }
                }
            }

            if (generatable && color != except)
            {
                colors.Add(color);
            }
        } // end for

        // by default index is a random color
        int index = usingColors[Random.Range(0, usingColors.Count)];

        // if there is generatable colors then change index
        if (colors.Count > 0)
        {
            index = colors[Random.Range(0, colors.Count)];
        }

        // if the random in colors list is a except color then change the index
        if (index == except)
        {
            index = (index++) % usingColors.Count;
        }

        this.color = index;

        ChangeSpriteAndType(index);
    }

    public void ChangeSpriteAndType(int itemColor)
    {
        GameObject prefab = null;

        switch (itemColor)
        {
            case 1:
                prefab = Resources.Load(Configure.Cookie1()) as GameObject;
                type = ITEM_TYPE.COOKIE_1;
                break;
            case 2:
                prefab = Resources.Load(Configure.Cookie2()) as GameObject;
                type = ITEM_TYPE.COOKIE_2;
                break;
            case 3:
                prefab = Resources.Load(Configure.Cookie3()) as GameObject;
                type = ITEM_TYPE.COOKIE_3;
                break;
            case 4:
                prefab = Resources.Load(Configure.Cookie4()) as GameObject;
                type = ITEM_TYPE.COOKIE_4;
                break;
            case 5:
                prefab = Resources.Load(Configure.Cookie5()) as GameObject;
                type = ITEM_TYPE.COOKIE_5;
                break;
            case 6:
                prefab = Resources.Load(Configure.Cookie6()) as GameObject;
                type = ITEM_TYPE.COOKIE_6;
                break;
        }

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ChangeToRainbow()
    {
        var prefab = Resources.Load(Configure.CookieRainbow()) as GameObject;
        
        type = ITEM_TYPE.COOKIE_RAINBOW;
        
        color = 0;
        
        GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void ChangeToGingerbread(ITEM_TYPE check)
    {
        if (node.item.IsGingerbread() == true)
        {
            return;
        }

        var upper = board.GetUpperItem(this.node);

        if (upper != null && upper.IsGingerbread())
        {
            return;
        }

        AudioManager.instance.GingerbreadAudio();

        GameObject explosion = null;

        switch (node.item.type)
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

        if (explosion != null) explosion.transform.position = node.item.transform.position;

        GameObject prefab = null;

        switch (check)
        {
            case ITEM_TYPE.GINGERBREAD_1:
                prefab = Resources.Load(Configure.Gingerbread1()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_1;
                color = 1;
                break;
            case ITEM_TYPE.GINGERBREAD_2:
                prefab = Resources.Load(Configure.Gingerbread2()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_2;
                color = 2;
                break;
            case ITEM_TYPE.GINGERBREAD_3:
                prefab = Resources.Load(Configure.Gingerbread3()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_3;
                color = 3;
                break;
            case ITEM_TYPE.GINGERBREAD_4:
                prefab = Resources.Load(Configure.Gingerbread4()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_4;
                color = 4;
                break;
            case ITEM_TYPE.GINGERBREAD_5:
                prefab = Resources.Load(Configure.Gingerbread5()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_5;
                color = 5;
                break;
            case ITEM_TYPE.GINGERBREAD_6:
                prefab = Resources.Load(Configure.Gingerbread6()) as GameObject;
                check = ITEM_TYPE.GINGERBREAD_6;
                color = 6;
                break;
        }

        if (prefab != null)
        {
            type = check;
            effect = BREAKER_EFFECT.NORMAL;

            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ChangeToBombBreaker()
    {
        GameObject prefab = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1:
                prefab = Resources.Load(Configure.Cookie1BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_1_BOMB_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_2:
                prefab = Resources.Load(Configure.Cookie2BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_2_BOMB_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_3:
                prefab = Resources.Load(Configure.Cookie3BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_3_BOMB_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_4:
                prefab = Resources.Load(Configure.Cookie4BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_4_BOMB_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_5:
                prefab = Resources.Load(Configure.Cookie5BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_5_BOMB_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_6:
                prefab = Resources.Load(Configure.Cookie6BombBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_6_BOMB_BREAKER;
                break;
        }

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ChangeToXBreaker()
    {
        GameObject prefab = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1:
                prefab = Resources.Load(Configure.Cookie1XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_1_X_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_2:
                prefab = Resources.Load(Configure.Cookie2XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_2_X_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_3:
                prefab = Resources.Load(Configure.Cookie3XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_3_X_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_4:
                prefab = Resources.Load(Configure.Cookie4XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_4_X_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_5:
                prefab = Resources.Load(Configure.Cookie5XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_5_X_BREAKER;
                break;
            case ITEM_TYPE.COOKIE_6:
                prefab = Resources.Load(Configure.Cookie6XBreaker()) as GameObject;
                type = ITEM_TYPE.COOKIE_6_X_BREAKER;
                break;
        }

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void ChangeToColRowBreaker()
    {
        GameObject prefab = null;

        if (Random.Range(0, 2) == 0)
        {
            switch (type)
            {
                case ITEM_TYPE.COOKIE_1:
                    prefab = Resources.Load(Configure.Cookie1ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_1_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_2:
                    prefab = Resources.Load(Configure.Cookie2ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_2_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_3:
                    prefab = Resources.Load(Configure.Cookie3ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_3_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_4:
                    prefab = Resources.Load(Configure.Cookie4ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_4_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_5:
                    prefab = Resources.Load(Configure.Cookie5ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_5_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_6:
                    prefab = Resources.Load(Configure.Cookie6ColumnBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_6_COLUMN_BREAKER;
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case ITEM_TYPE.COOKIE_1:
                    prefab = Resources.Load(Configure.Cookie1RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_1_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_2:
                    prefab = Resources.Load(Configure.Cookie2RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_2_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_3:
                    prefab = Resources.Load(Configure.Cookie3RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_3_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_4:
                    prefab = Resources.Load(Configure.Cookie4RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_4_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_5:
                    prefab = Resources.Load(Configure.Cookie5RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_5_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_6:
                    prefab = Resources.Load(Configure.Cookie6RowBreaker()) as GameObject;
                    type = ITEM_TYPE.COOKIE_6_ROW_BREAKER;
                    break;
            }
        }

        if (prefab != null)
        {
            GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void SetRandomNextType()
    {
        var random = Random.Range(0, 2);

        if (random == 0)
        {
            switch (type)
            {
                case ITEM_TYPE.COOKIE_1:
                    next  = ITEM_TYPE.COOKIE_1_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_2:
                    next = ITEM_TYPE.COOKIE_2_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_3:
                    next = ITEM_TYPE.COOKIE_3_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_4:
                    next = ITEM_TYPE.COOKIE_4_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_5:
                    next = ITEM_TYPE.COOKIE_5_COLUMN_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_6:
                    next = ITEM_TYPE.COOKIE_6_COLUMN_BREAKER;
                    break;
            }
        }
        else if (random == 1)
        {
            switch (type)
            {
                case ITEM_TYPE.COOKIE_1:
                    next = ITEM_TYPE.COOKIE_1_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_2:
                    next = ITEM_TYPE.COOKIE_2_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_3:
                    next = ITEM_TYPE.COOKIE_3_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_4:
                    next = ITEM_TYPE.COOKIE_4_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_5:
                    next = ITEM_TYPE.COOKIE_5_ROW_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_6:
                    next = ITEM_TYPE.COOKIE_6_ROW_BREAKER;
                    break;
            }
        }
        else if (random == 2)
        {
            switch (type)
            {
                case ITEM_TYPE.COOKIE_1:
                    next = ITEM_TYPE.COOKIE_1_BOMB_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_2:
                    next = ITEM_TYPE.COOKIE_2_BOMB_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_3:
                    next = ITEM_TYPE.COOKIE_3_BOMB_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_4:
                    next = ITEM_TYPE.COOKIE_4_BOMB_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_5:
                    next = ITEM_TYPE.COOKIE_5_BOMB_BREAKER;
                    break;
                case ITEM_TYPE.COOKIE_6:
                    next = ITEM_TYPE.COOKIE_6_BOMB_BREAKER;
                    break;
            }
        }
    }

    #endregion

    #region Destroy

    public void Destroy(bool forced = false)
    {
        if (Destroyable() == false && forced == false)
        {
            return;
        }

        // prevent multiple calls
        if (destroying == true) return;
        else destroying = true;

        if (node != null && node.cage != null)
        {
            // destroy and check collect cage
            node.CageExplode();

            return;
        }

        board.destroyingItems++;

        // destroy animation
        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", Vector3.zero,
            "onstart", "OnStartDestroy",
            "oncomplete", "OnCompleteDestroy",
            "easetype", iTween.EaseType.linear,
            "time", Configure.instance.destroyTime
        ));
    }

    public void OnStartDestroy()
    {
        // destroy and check collect waffle
        if (node != null) node.WaffleExplode();

        // check collect
        board.CollectItem(this);

        // destroy neighbor marshmallow/chocolate
        board.DestroyNeighborItems(this);

        // explosion effect
        if (effect == BREAKER_EFFECT.BIG_COLUMN_BREAKER)
        {
            BigColumnBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.BIG_ROW_BREAKER)
        {
            BigRowBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.CROSS)
        {
            CrossBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.BOMB_X_BREAKER)
        {
            BombXBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.CROSS_X_BREAKER)
        {
            CrossXBreakerExplosion();
        }
        else if (effect == BREAKER_EFFECT.NORMAL)
        {
            if (IsCookie())
            {
                CookieExplosion();
            }
            else if (IsGingerbread())
            {
                GingerbreadExplosion();
            }
            else if (IsMarshmallow())
            {
                MarshmallowExplosion();
            }
            else if (IsChocolate())
            {
                ChocolateExplosion();
            }
            else if (IsRockCandy())
            {
                RockCandyExplosion();
            }
            else if (IsCollectible())
            {
                CollectibleExplosion();
            }
            else if (IsBombBreaker(type))
            {
                BombBreakerExplosion();
            }
            else if (IsXBreaker(type))
            {
                XBreakerExplosion();
            }
            else if (type == ITEM_TYPE.COOKIE_RAINBOW)
            {
                RainbowExplosion();
            }
            else if (IsColumnBreaker(type))
            {
                ColumnBreakerExplosion();
            }
            else if (IsRowBreaker(type))
            {
                RowBreakerExplosion();
            }   
        }
    }

    public void OnCompleteDestroy()
    {
        if (board.state == GAME_STATE.PRE_WIN_AUTO_PLAYING)
        {
            board.score += Configure.instance.finishedScoreItem * board.dropTime;
        }
        else
        {
            board.score += Configure.instance.scoreItem * board.dropTime;
        }

        board.UITop.UpdateProgressBar(board.score);

        board.UITop.UpdateScoreAmount(board.score);

        if (next != ITEM_TYPE.NONE)
        {
            //print("next type: " + next);

            if (IsBombBreaker(next) || IsXBreaker(next))
            {
                if (nextSound == true) AudioManager.instance.BombBreakerAudio();
            }
            else if (IsRowBreaker(next) || IsColumnBreaker(next))
            {
                if (nextSound == true) AudioManager.instance.ColRowBreakerAudio();
            }
            else if (next == ITEM_TYPE.COOKIE_RAINBOW)
            {
                if (nextSound == true) AudioManager.instance.RainbowAudio();
            }

            // generate a item at position of the node
            node.GenerateItem(next);
        }
        else if (type == ITEM_TYPE.CHOCOLATE_2_LAYER)
        {
            // generate a new chocolate
            node.GenerateItem(ITEM_TYPE.CHOCOLATE_1_LAYER);

            // set position
            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else if (type == ITEM_TYPE.CHOCOLATE_3_LAYER)
        {
            node.GenerateItem(ITEM_TYPE.CHOCOLATE_2_LAYER);

            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else if (type == ITEM_TYPE.CHOCOLATE_4_LAYER)
        {
            node.GenerateItem(ITEM_TYPE.CHOCOLATE_3_LAYER);

            board.GetNode(node.i, node.j).item.gameObject.transform.position = board.NodeLocalPosition(node.i, node.j); ;
        }
        else
        {
            node.item = null;
        }

        if (destroying == true)
        {
            board.destroyingItems--;

            // there is a case when a item is dropping and it is destroyed by other call
            if (dropping == true) board.droppingItems--;

            GameObject.Destroy(gameObject);
        }
    }

    public IEnumerator ResetDestroying()
    {
        yield return new WaitForSeconds(Configure.instance.destroyTime);

        destroying = false;
    }

    #endregion

    #region Explosion

    void CookieExplosion()
    {
        AudioManager.instance.CookieCrushAudio();

        GameObject explosion = null;

        switch (type)
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

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    void GingerbreadExplosion()
    {
        AudioManager.instance.GingerbreadExplodeAudio();

        GameObject explosion = null;

        switch (type)
        {
            case ITEM_TYPE.GINGERBREAD_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BlueCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GINGERBREAD_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.GreenCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GINGERBREAD_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.OrangeCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GINGERBREAD_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.PurpleCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GINGERBREAD_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RedCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.GINGERBREAD_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.YellowCookieExplosion()) as GameObject);
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    void MarshmallowExplosion()
    {
        AudioManager.instance.MarshmallowExplodeAudio();

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.MarshmallowExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    public void ChocolateExplosion()
    {
        AudioManager.instance.ChocolateExplodeAudio();

        GameObject explosion = null;

        explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ChocolateExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    public void RockCandyExplosion()
    {
        AudioManager.instance.RockCandyExplodeAudio();

        GameObject explosion = null;

        switch (type)
        {
            case ITEM_TYPE.ROCK_CANDY_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BlueCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.GreenCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.OrangeCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.PurpleCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RedCookieExplosion()) as GameObject);
                break;
            case ITEM_TYPE.ROCK_CANDY_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.YellowCookieExplosion()) as GameObject);
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    void CollectibleExplosion()
    {
        AudioManager.instance.CollectibleExplodeAudio();
    }

    void BombBreakerExplosion()
    {
        AudioManager.instance.BombExplodeAudio();

        BombBreakerDestroy();

        GameObject explosion = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion1()) as GameObject);
                break;
            case ITEM_TYPE.COOKIE_2_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion2()) as GameObject);
                break;
            case ITEM_TYPE.COOKIE_3_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion3()) as GameObject);
                break;
            case ITEM_TYPE.COOKIE_4_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion4()) as GameObject);
                break;
            case ITEM_TYPE.COOKIE_5_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion5()) as GameObject);
                break;
            case ITEM_TYPE.COOKIE_6_BOMB_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.BreakerExplosion6()) as GameObject);
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
            explosion.transform.position = new Vector3(explosion.transform.position.x, explosion.transform.position.y, -12f);
        }
    }

    void RainbowExplosion()
    {
        AudioManager.instance.RainbowExplodeAudio();

        GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RainbowExplosion()) as GameObject);

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }
    }

    void XBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        XBreakerDestroy();

        GameObject explosion = null;
        GameObject animation = null;
        GameObject cross = null;

        switch (GetCookie(type))
        {
            case ITEM_TYPE.COOKIE_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (animation != null)
        {
            cross = Instantiate(animation);
            animation.transform.Rotate(Vector3.back, 45);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (cross != null)
        {
            cross.transform.Rotate(Vector3.back, -45);
            cross.transform.position = new Vector3(cross.transform.position.x, cross.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
    }

    void ColumnBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy();

        GameObject explosion = null;
        GameObject animation = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        if (animation != null)
        {
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        GameObject.Destroy(animation, 1f);
    }

    void BigColumnBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy(node.j - 1);
        ColumnDestroy(node.j);
        ColumnDestroy(node.j + 1);

        GameObject explosion = null;
        GameObject animation = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_COLUMN_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        if (animation != null)
        {
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        GameObject.Destroy(animation, 1f);
    }

    void CrossBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        ColumnDestroy();
        RowDestroy();

        GameObject explosion = null;
        GameObject columnEffect = null;
        GameObject rowEffect = null;

        switch (GetCookie(type))
        {
            case ITEM_TYPE.COOKIE_1:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                columnEffect = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (columnEffect != null)
        {
            rowEffect = Instantiate(columnEffect as GameObject, transform.position, Quaternion.identity) as GameObject;
            columnEffect.transform.position = new Vector3(columnEffect.transform.position.x, columnEffect.transform.position.y, -12f);
        }

        if (rowEffect != null)
        {
            rowEffect.transform.Rotate(Vector3.back, 90);
            rowEffect.transform.position = new Vector3(rowEffect.transform.position.x, rowEffect.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(rowEffect, 1f);

        GameObject.Destroy(columnEffect, 1f);
    }

    void RowBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        RowDestroy();

        GameObject explosion = null;
        GameObject animation = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.ColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (animation != null)
        {
            animation.transform.Rotate(Vector3.back, 90);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
    }

    void BigRowBreakerExplosion()
    {
        AudioManager.instance.ColRowBreakerExplodeAudio();

        RowDestroy(node.i - 1);
        RowDestroy(node.i);
        RowDestroy(node.i + 1);

        GameObject explosion = null;
        GameObject animation = null;

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker1()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation1()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_2_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker2()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation2()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_3_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker3()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation3()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_4_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker4()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation4()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_5_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker5()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation5()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
            case ITEM_TYPE.COOKIE_6_ROW_BREAKER:
                explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.ColRowBreaker6()) as GameObject);
                animation = Instantiate(Resources.Load(Configure.BigColumnBreakerAnimation6()) as GameObject, transform.position, Quaternion.identity) as GameObject;
                break;
        }

        if (animation != null)
        {
            animation.transform.Rotate(Vector3.back, 90);
            animation.transform.position = new Vector3(animation.transform.position.x, animation.transform.position.y, -12f);
        }

        if (explosion != null)
        {
            explosion.transform.position = transform.position;
        }

        GameObject.Destroy(animation, 1f);
    }

    void BombXBreakerExplosion()
    {
        BombBreakerExplosion();

        XBreakerExplosion();
    }

    void CrossXBreakerExplosion()
    {
        CrossBreakerExplosion();

        XBreakerExplosion();
    }

    #endregion

    #region SpecialDestroy

    void BombBreakerDestroy()
    {
        List<Item> items = board.ItemAround(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }
        }
    }

    void XBreakerDestroy()
    {
        List<Item> items = board.XCrossItems(node);

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }
        }
    }

    // destroy all items with the same color (when this color swap with a rainbow)
    public void DestroyItemsSameColor(int color)
    {
        List<Item> items = board.GetListItems();

        foreach (Item item in items)
        {
            if (item != null)
            {
                if (item.color == color)
                {
                    board.sameColorList.Add(item);
                }
            }
        }

        board.DestroySameColorList();
    }

    // Rainbow swap with other item
    public void RainbowDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem.Destroyable() == false || otherItem.Destroyable() == false)
        {
            return;
        }

        if (thisItem.type == ITEM_TYPE.COOKIE_RAINBOW)
        {
            //Debug.Log("touched item is rainbow");

            if (otherItem.IsCookie())
            {
                DestroyItemsSameColor(otherItem.color);

                thisItem.Destroy();
            }
            else if (otherItem.IsBombBreaker(otherItem.type) || otherItem.IsRowBreaker(otherItem.type) || otherItem.IsColumnBreaker(otherItem.type) || otherItem.IsXBreaker(otherItem.type))
            {
                ChangeItemsType(otherItem.color, otherItem.type);

                thisItem.Destroy();
            }
            else if (otherItem.type == ITEM_TYPE.COOKIE_RAINBOW)
            {
                board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
            }
        }
        else if (otherItem.type == ITEM_TYPE.COOKIE_RAINBOW)
        {
            //Debug.Log("swap item is rainbow");

            if (thisItem.IsCookie())
            {
                DestroyItemsSameColor(thisItem.color);

                otherItem.Destroy();
            }
            else if (thisItem.IsBombBreaker(thisItem.type) || thisItem.IsRowBreaker(thisItem.type) || thisItem.IsColumnBreaker(thisItem.type) || thisItem.IsXBreaker(thisItem.type))
            {
                ChangeItemsType(thisItem.color, thisItem.type);

                otherItem.Destroy();
            }
            else if (thisItem.type == ITEM_TYPE.COOKIE_RAINBOW)
            {
                board.DoubleRainbowDestroy();

                thisItem.Destroy();

                otherItem.Destroy();
            }
        }
    }

    void ColumnDestroy(int col = -1)
    {
        var items = new List<Item>();

        if (col == -1)
        {
            items = board.ColumnItems(node.j);
        }
        else
        {
            items = board.ColumnItems(col);
        }

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }
        }
    }

    public void RowDestroy(int row = -1)
    {
        var items = new List<Item>();

        if (row == -1)
        {
            items = board.RowItems(node.i);
        }
        else
        {
            items = board.RowItems(row);
        }

        foreach (var item in items)
        {
            // this item maybe destroyed in other call
            if (item != null)
            {
                if (item.type == ITEM_TYPE.COOKIE_RAINBOW)
                {
                    DestroyItemsSameColor(LevelLoader.instance.RandomColor());
                }

                item.Destroy();
            }
        }
    }

    void TwoColRowBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && (IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)))
        {
            thisItem.effect = BREAKER_EFFECT.CROSS;            
            otherItem.effect = BREAKER_EFFECT.NONE;

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    void TwoBombBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if (IsBombBreaker(thisItem.type) && IsBombBreaker(otherItem.type))
        {
            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    void TwoXBreakerDestory(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if (IsXBreaker(thisItem.type) && IsXBreaker(otherItem.type))
        {
            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    void ColRowBreakerAndBombBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsRowBreaker(thisItem.type) || IsColumnBreaker(thisItem.type)) && IsBombBreaker(otherItem.type))
        {
            if (IsRowBreaker(thisItem.type))
            {
                thisItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
            }
            else if (IsColumnBreaker(thisItem.type))
            {
                thisItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
            }
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.ChangeToBig();
        }
        else if ((IsRowBreaker(otherItem.type) || IsColumnBreaker(otherItem.type)) && IsBombBreaker(thisItem.type))
        {
            if (IsRowBreaker(otherItem.type))
            {
                otherItem.effect = BREAKER_EFFECT.BIG_ROW_BREAKER;
            }
            else if (IsColumnBreaker(otherItem.type))
            {
                otherItem.effect = BREAKER_EFFECT.BIG_COLUMN_BREAKER;
            }
            thisItem.type = otherItem.GetCookie(otherItem.type);

            otherItem.ChangeToBig();
        }
    }

    void ColRowBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsXBreaker(thisItem.type) && (IsColumnBreaker(otherItem.type) || IsRowBreaker(otherItem.type))) || 
             (IsXBreaker(otherItem.type) && (IsColumnBreaker(thisItem.type) || IsRowBreaker(thisItem.type))))
        {
            thisItem.effect = BREAKER_EFFECT.CROSS_X_BREAKER;
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    void BombBreakerAndXBreakerDestroy(Item thisItem, Item otherItem)
    {
        if (thisItem == null || otherItem == null)
        {
            return;
        }

        if ((IsBombBreaker(thisItem.type) && IsXBreaker(otherItem.type)) || (IsBombBreaker(otherItem.type) && IsXBreaker(thisItem.type)))
        {
            thisItem.effect = BREAKER_EFFECT.BOMB_X_BREAKER;
            otherItem.type = otherItem.GetCookie(otherItem.type);

            thisItem.Destroy();
            otherItem.Destroy();

            board.FindMatches();
        }
    }

    #endregion

    #region Change

    // change all items to column-breaker/row-breaker/bomb-breaker/x-breaker when swap a rainbow with a breaker
    void ChangeItemsType(int color, ITEM_TYPE changeToType)
    {
        List<Item> items = board.GetListItems();

        foreach (var item in items)
        {
            if (item != null)
            {
                if (item.color == color && item.IsCookie() == true)
                {
                    GameObject explosion = CFX_SpawnSystem.GetNextObject(Resources.Load(Configure.RainbowExplosion()) as GameObject);

                    if (explosion != null) explosion.transform.position = item.transform.position;

                    if (item.IsColumnBreaker(changeToType) || item.IsRowBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToColRowBreaker();
                        }
                    }
                    else if (item.IsBombBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToBombBreaker();
                        }
                    }
                    else if (item.IsXBreaker(changeToType))
                    {
                        if (item.IsCookie())
                        {
                            item.ChangeToXBreaker();
                        }
                    }

                    board.changingList.Add(item);
                }
            }
        }

        board.DestroyChangingList();
    }

    void ChangeToBig()
    {
        // prevent multiple calls
        if (changing == true) return;
        else changing = true;

        this.GetComponent<SpriteRenderer>().sortingLayerName = "Effect";

        iTween.ScaleTo(gameObject, iTween.Hash(
            "scale", new Vector3(2.5f, 2.5f, 0),
            "oncomplete", "CompleteChangeToBig",
            "easetype", iTween.EaseType.linear,
            "time", Configure.instance.changingTime
            ));
    }

    void CompleteChangeToBig()
    {
        this.Destroy();

        board.FindMatches();
    }

    #endregion

    #region Drop

    public void Drop()
    {
        // prevent multiple calls
        if (dropping) return;
        else dropping = true;

        if (dropPath.Count > 1)
        {
            board.droppingItems++;

            var dist = (transform.position.y - dropPath[0].y);
            
            //print("dist: " + dist);

            var time = (transform.position.y - dropPath[dropPath.Count - 1].y) / board.NodeSize();

            // fix iTween interesting problems http://vanstrydonck.com/working-with-itween-paths/
            while (dist > 0.1f)
            {
                dist -= board.NodeSize();
                dropPath.Insert(0, dropPath[0] + new Vector3(0, board.NodeSize(), 0));
            }

            iTween.MoveTo(gameObject, iTween.Hash(
                "path", dropPath.ToArray(),
                "movetopath", false,
                "onstart", "OnStartDrop",
                "oncomplete", "OnCompleteDrop",
                "easetype", iTween.EaseType.linear,
                "time", Configure.instance.dropTime * time
            ));
        }
        else
        {
            Vector3 target = board.NodeLocalPosition(node.i, node.j);

            if (Mathf.Abs(transform.position.x - target.x) > 0.1f || Mathf.Abs(transform.position.y - target.y) > 0.1f)
            {
                board.droppingItems++;

                var time = (transform.position.y - target.y) / board.NodeSize();

                //print("target: " + target);

                iTween.MoveTo(gameObject, iTween.Hash(
                    "position", target,
                    "onstart", "OnStartDrop",
                    "oncomplete", "OnCompleteDrop",
                    "easetype", iTween.EaseType.linear,
                    "time", Configure.instance.dropTime * time
                ));
            }
            else
            {
                dropping = false;
            }
        }
    }

    public void OnStartDrop()
    {
        
    }

    public void OnCompleteDrop()
    {
        if (dropping)
        {
            AudioManager.instance.DropAudio();

            // reset
            dropPath.Clear();

            board.droppingItems--;

            // reset
            dropping = false;            
        }
    }

    #endregion

}
