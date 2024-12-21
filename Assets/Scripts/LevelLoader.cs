using UnityEngine;
using System.Collections;
using System.IO;
using MiniJSON;
using System.Collections.Generic;
using System.Linq;
using System;

public class LevelLoader : MonoBehaviour 
{
    public static LevelLoader instance = null;

    [Header("Basic")]
    public int level;
    public int column;
    public int row;
    public int moves;

    // layers
    [Header("Layers")]
    public List<TILE_TYPE> tileLayerData;
    public List<WAFFLE_TYPE> waffleLayerData;
    public List<ITEM_TYPE> itemLayerData;
    public List<CAGE_TYPE> cageLayerData;

    Dictionary<string, string> names = new Dictionary<string, string>();

    // properties
    [Header("Cookie")]
    public List<ITEM_TYPE> usingCookies;
    public List<int> usingColors;
    public List<int> cookieWeight;

    [Header("Gingerbread")]
    public List<ITEM_TYPE> usingGingerbreads;    
    public List<int> gingerbreadWeight;
    public List<int> gingerbreadMarkers;
    public int maxGingerbread;

    [Header("Target")]
    public TARGET_TYPE target1Type = TARGET_TYPE.NONE;
    public TARGET_TYPE target2Type = TARGET_TYPE.NONE;
    public TARGET_TYPE target3Type = TARGET_TYPE.NONE;
    public TARGET_TYPE target4Type = TARGET_TYPE.NONE;
    [Header("")]
    public int target1Amount;
    public int target2Amount;
    public int target3Amount;
    public int target4Amount;
    [Header("")]
    public int target1Color;
    public int target2Color;
    public int target3Color;
    public int target4Color;
    [Header("")]
    public int score1Star;
    public int score2Star;
    public int score3Star;
    [Header("")]
    public string targetText;

    [Header("Collectible")]
    public List<int> collectibleCollectColumnMarkers;
    public List<int> collectibleCollectNodeMarkers;
    public List<int> collectibleGenerateMarkers;
    public int collectibleMaxOnBoard;

    [Header("Marshmallow")]
    public int marshmallowMoreThanTarget;

    [Header("Cake")]
    public int cake;    

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

        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(bool debug = false)
    {
        TextAsset jsonString;

        if (debug == true)
        {
            level = 999;
        }

        jsonString = Resources.Load("Levels/" + level, typeof(TextAsset)) as TextAsset;

        if (jsonString == null)
        {
            print("Can not load level data");
            return;
        }

        Clear();

        //print(jsonString);

        var dict = Json.Deserialize(jsonString.text) as Dictionary<string, object>;

        //var str = Json.Serialize(dict);

        //Debug.Log("serialized: " + str);

        column = int.Parse(dict["width"].ToString());
        row = int.Parse(dict["height"].ToString());

        #region tilesets

        var tilesets = (List<object>)dict["tilesets"];

        var tiles = (Dictionary<string, object>)tilesets[0];

        var images = (Dictionary<string, object>)tiles["tiles"];

        foreach (KeyValuePair<string, object> entry in images)
        {
            // do something with entry.Value or entry.Key
            var value = (Dictionary<string, object>)entry.Value;

            var image = (string)value["image"];

            string[] tokens = image.Split(new[] { "/" }, StringSplitOptions.None);

            var full = tokens[tokens.Length - 1];

            string[] parts = full.Split(new[] { "." }, StringSplitOptions.None);

            var name = parts[0];

            //print(name);

            if (!names.ContainsKey(entry.Key))
            {
                names.Add(entry.Key, name);
            }
        }

        #endregion

        #region layers

        var layers = (List<object>)dict["layers"];
                
        foreach (object obj in layers)
        {
            var layer = (Dictionary<string, object>)obj;

            var data = (List<object>)layer["data"];
            //print(data);

            var name = (string)layer["name"];

            // tile
            if (name == "tile_layer")
            {
                foreach (object datum in data)
                {
                    var type = DataToTileType(int.Parse(datum.ToString()));
                    tileLayerData.Add(type);
                }
            }

            // waffle
            else if (name == "waffle_layer")
            {
                foreach (object datum in data)
                {
                    var type = DataToWaffleType(int.Parse(datum.ToString()));
                    waffleLayerData.Add(type);
                }
            }

            // item
            else if (name == "item_layer")
            {
                foreach (object datum in data)
                {
                    var type = DataToItemType(int.Parse(datum.ToString()));
                    itemLayerData.Add(type);
                }
            }

            // cage
            else if (name == "cage_layer")
            {
                foreach (object datum in data)
                {
                    var type = DataToCageType(int.Parse(datum.ToString()));
                    cageLayerData.Add(type);
                }
            }
        }

        #endregion

        #region properties
        
        var properties = (Dictionary<string, object>)dict["properties"];

        // REQUIRED
        var cookies = ((string)properties["cookies"]).Split(',').ToList();
        var weights = ((string)properties["cookies_weight"]).Split(',').ToList();
                
        var count = 0;

        foreach (object obj in cookies)
        {
            var cookie = int.Parse(obj.ToString());
            var weight = int.Parse(weights[count].ToString());

            count++;

            if (cookie == 1)
            {
                switch (count)
                {
                    case 1:
                        usingCookies.Add(ITEM_TYPE.COOKIE_1);
                        usingColors.Add(1);
                        break;
                    case 2:
                        usingCookies.Add(ITEM_TYPE.COOKIE_2);
                        usingColors.Add(2);
                        break;
                    case 3:
                        usingCookies.Add(ITEM_TYPE.COOKIE_3);
                        usingColors.Add(3);
                        break;
                    case 4:
                        usingCookies.Add(ITEM_TYPE.COOKIE_4);
                        usingColors.Add(4);
                        break;
                    case 5:
                        usingCookies.Add(ITEM_TYPE.COOKIE_5);
                        usingColors.Add(5);
                        break;
                    case 6:
                        usingCookies.Add(ITEM_TYPE.COOKIE_6);
                        usingColors.Add(6);
                        break;
                }

                cookieWeight.Add(weight);
            }
        }

        if (properties.ContainsKey("gingerbread"))
        {
            if ((string)properties["gingerbread"] != "" && (string)properties["gingerbread_weight"] != "")
            {
                var gingerbreads = ((string)properties["gingerbread"]).Split(',').ToList();
                var gWeights = ((string)properties["gingerbread_weight"]).Split(',').ToList();

                count = 0;

                foreach (object obj in gingerbreads)
                {
                    var gingerbread = int.Parse(obj.ToString());
                    var gWeight = int.Parse(gWeights[count].ToString());

                    count++;

                    if (gingerbread == 1)
                    {
                        switch (count)
                        {
                            case 1:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_1);
                                break;
                            case 2:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_2);
                                break;
                            case 3:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_3);
                                break;
                            case 4:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_4);
                                break;
                            case 5:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_5);
                                break;
                            case 6:
                                usingGingerbreads.Add(ITEM_TYPE.GINGERBREAD_6);
                                break;
                        }

                        gingerbreadWeight.Add(gWeight);

                    }
                }
            }
        }

        if (properties.ContainsKey("gingerbread_max_on_board"))
        {
            if (properties["gingerbread_max_on_board"].ToString() != "")
            {
                maxGingerbread = int.Parse(properties["gingerbread_max_on_board"].ToString());
            }
        }

        if (properties.ContainsKey("gingerbread_generate_marker"))
        {
            var markers = ((string)properties["gingerbread_generate_marker"]).Split(',').ToList();

            foreach (object obj in markers)
            {
                if (obj.ToString() != "")
                {
                    var marker = int.Parse(obj.ToString());

                    gingerbreadMarkers.Add(marker);
                }                
            }
        }
        
        // REQUIRED
        moves = int.Parse(properties["moves"].ToString());

        // REQUIRED
        var target1 = ((string)properties["target_1"]).Split(',').ToList();

        if (target1.Count >= 2)
        {
            target1Type = DataToTargetType(int.Parse(target1[0].ToString()));
            target1Amount = int.Parse(target1[1].ToString());

            if (target1.Count == 3) target1Color = int.Parse(target1[2].ToString());
        }

        var target2 = ((string)properties["target_2"]).Split(',').ToList();

        if (target2.Count >= 2)
        {
            target2Type = DataToTargetType(int.Parse(target2[0].ToString()));
            target2Amount = int.Parse(target2[1].ToString());

            if (target2.Count == 3) target2Color = int.Parse(target2[2].ToString());
        }

        var target3 = ((string)properties["target_3"]).Split(',').ToList();

        if (target3.Count >= 2)
        {
            target3Type = DataToTargetType(int.Parse(target3[0].ToString()));
            target3Amount = int.Parse(target3[1].ToString());

            if (target3.Count == 3) target3Color = int.Parse(target3[2].ToString());
        }

        var target4 = ((string)properties["target_4"]).Split(',').ToList();

        if (target4.Count >= 2)
        {
            target4Type = DataToTargetType(int.Parse(target4[0].ToString()));
            target4Amount = int.Parse(target4[1].ToString());

            if (target4.Count == 3) target4Color = int.Parse(target4[2].ToString());
        }

        // REQUIRED
        score1Star = int.Parse(properties["score_1_star"].ToString());
        score2Star = int.Parse(properties["score_2_star"].ToString());
        score3Star = int.Parse(properties["score_3_star"].ToString());

        // REQUIRED
        cake = int.Parse(properties["cake"].ToString());

        // REQUIRED
        targetText = properties["target_text"].ToString();

        if (properties.ContainsKey("collectible_collect_marker"))
        {
            // collectible collect marker NOT required
            var markers = ((string)properties["collectible_collect_marker"]).Split(',').ToList();

            foreach (object obj in markers)
            {
                if (obj.ToString() != "")
                {
                    var marker = int.Parse(obj.ToString());

                    collectibleCollectColumnMarkers.Add(marker);
                }
            }
        }

        if (properties.ContainsKey("collectible_collect_node_marker"))
        {
            // collectible collect node marker NOT required
            var nodeMarkers = ((string)properties["collectible_collect_node_marker"]).Split(',').ToList();

            foreach (object obj in nodeMarkers)
            {
                if (obj.ToString() != "")
                {
                    var marker = int.Parse(obj.ToString());

                    collectibleCollectNodeMarkers.Add(marker);
                }
            }
        }

        if (properties.ContainsKey("collectible_generate_marker"))
        {
            // collectible generate marker NOT required
            var markers = ((string)properties["collectible_generate_marker"]).Split(',').ToList();

            foreach (object obj in markers)
            {
                if (obj.ToString() != "")
                {
                    var marker = int.Parse(obj.ToString());

                    collectibleGenerateMarkers.Add(marker);
                }
            }
        }

        if (properties.ContainsKey("collectible_max_on_board"))
        {
            // collectible max on board NOT required
            if (properties["collectible_max_on_board"].ToString() != "")
            {
                collectibleMaxOnBoard = int.Parse(properties["collectible_max_on_board"].ToString());
            }            
        }

        if (properties.ContainsKey("marshmallow_more_than_target"))
        {
            // marshmallow more than target NOT required
            if (properties["marshmallow_more_than_target"].ToString() != "")
            {
                marshmallowMoreThanTarget = int.Parse(properties["marshmallow_more_than_target"].ToString());
            }
        }

        #endregion
    }

    void Clear()
    {
        tileLayerData.Clear();
        waffleLayerData.Clear();
        itemLayerData.Clear();
        cageLayerData.Clear();

        usingCookies.Clear();
        usingColors.Clear();
        cookieWeight.Clear();

        usingGingerbreads.Clear();
        gingerbreadWeight.Clear();
        gingerbreadMarkers.Clear();
        maxGingerbread = 0;

        target1Type = TARGET_TYPE.NONE;
        target2Type = TARGET_TYPE.NONE;
        target3Type = TARGET_TYPE.NONE;
        target4Type = TARGET_TYPE.NONE;

        target1Amount = 0;
        target2Amount = 0;
        target3Amount = 0;
        target4Amount = 0;

        target1Color = 0;
        target2Color = 0;
        target3Color = 0;
        target4Color = 0;

        collectibleCollectColumnMarkers.Clear();
        collectibleCollectNodeMarkers.Clear();
        collectibleGenerateMarkers.Clear();
        collectibleMaxOnBoard = 0;

        marshmallowMoreThanTarget = 0;
    }

    #region Type

    TILE_TYPE DataToTileType(int key)
    {
        if (key == 0) return TILE_TYPE.NONE;

        var data = names[(key - 1).ToString()];

        switch (data)
        {
            case "none_tile":
                return TILE_TYPE.NONE;
            case "pass_through_tile":
                return TILE_TYPE.PASS_THROUGH;
            case "light_tile":
                return TILE_TYPE.LIGHT_TILE;
            case "dark_tile":
                return TILE_TYPE.DARD_TILE;
        }

        return TILE_TYPE.NONE;
    }

    ITEM_TYPE DataToItemType(int key)
    {
        if (key == 0) return ITEM_TYPE.NONE;
        
        var data = names[(key - 1).ToString()];

        switch (data)
        {
            case "random_item":
                return ITEM_TYPE.COOKIE_RAMDOM;
            case "rainbow":
                return ITEM_TYPE.COOKIE_RAINBOW;

            case "blue":
                return ITEM_TYPE.COOKIE_1;
            case "blue_bomb_breaker":
                return ITEM_TYPE.COOKIE_1_BOMB_BREAKER;
            case "blue_column_breaker":
                return ITEM_TYPE.COOKIE_1_COLUMN_BREAKER;
            case "blue_row_breaker":
                return ITEM_TYPE.COOKIE_1_ROW_BREAKER;
            case "blue_x_breaker":
                return ITEM_TYPE.COOKIE_1_X_BREAKER;

            case "green":
                return ITEM_TYPE.COOKIE_2;
            case "green_bomb_breaker":
                return ITEM_TYPE.COOKIE_2_BOMB_BREAKER;
            case "green_column_breaker":
                return ITEM_TYPE.COOKIE_2_COLUMN_BREAKER;
            case "green_row_breaker":
                return ITEM_TYPE.COOKIE_2_ROW_BREAKER;
            case "green_x_breaker":
                return ITEM_TYPE.COOKIE_2_X_BREAKER;

            case "orange":
                return ITEM_TYPE.COOKIE_3;
            case "orange_bomb_breaker":
                return ITEM_TYPE.COOKIE_3_BOMB_BREAKER;
            case "orange_column_breaker":
                return ITEM_TYPE.COOKIE_3_COLUMN_BREAKER;
            case "orange_row_breaker":
                return ITEM_TYPE.COOKIE_3_ROW_BREAKER;
            case "orange_x_breaker":
                return ITEM_TYPE.COOKIE_3_X_BREAKER;

            case "purple":
                return ITEM_TYPE.COOKIE_4;
            case "purple_bomb_breaker":
                return ITEM_TYPE.COOKIE_4_BOMB_BREAKER;
            case "purple_column_breaker":
                return ITEM_TYPE.COOKIE_4_COLUMN_BREAKER;
            case "purple_row_breaker":
                return ITEM_TYPE.COOKIE_4_ROW_BREAKER;
            case "purple_x_breaker":
                return ITEM_TYPE.COOKIE_4_X_BREAKER;

            case "red":
                return ITEM_TYPE.COOKIE_5;
            case "red_bomb_breaker":
                return ITEM_TYPE.COOKIE_5_BOMB_BREAKER;
            case "red_column_breaker":
                return ITEM_TYPE.COOKIE_5_COLUMN_BREAKER;
            case "red_row_breaker":
                return ITEM_TYPE.COOKIE_5_ROW_BREAKER;
            case "red_x_breaker":
                return ITEM_TYPE.COOKIE_5_X_BREAKER;

            case "yellow":
                return ITEM_TYPE.COOKIE_6;
            case "yellow_bomb_breaker":
                return ITEM_TYPE.COOKIE_6_BOMB_BREAKER;
            case "yellow_column_breaker":
                return ITEM_TYPE.COOKIE_6_COLUMN_BREAKER;
            case "yellow_row_breaker":
                return ITEM_TYPE.COOKIE_6_ROW_BREAKER;
            case "yellow_x_breaker":
                return ITEM_TYPE.COOKIE_6_X_BREAKER;

            case "marshmallow":
                return ITEM_TYPE.MARSHMALLOW;

            case "generic_gingerbread":
                return ITEM_TYPE.GINGERBREAD_RANDOM;
            case "blue_gingerbread":
                return ITEM_TYPE.GINGERBREAD_1;
            case "green_gingerbread":
                return ITEM_TYPE.GINGERBREAD_2;
            case "orange_gingerbread":
                return ITEM_TYPE.GINGERBREAD_3;
            case "purple_gingerbread":
                return ITEM_TYPE.GINGERBREAD_4;
            case "red_gingerbread":
                return ITEM_TYPE.GINGERBREAD_5;
            case "yellow_gingerbread":
                return ITEM_TYPE.GINGERBREAD_6;

            case "chocolate_1":
                return ITEM_TYPE.CHOCOLATE_1_LAYER;
            case "chocolate_2":
                return ITEM_TYPE.CHOCOLATE_2_LAYER;
            case "chocolate_3":
                return ITEM_TYPE.CHOCOLATE_3_LAYER;
            case "chocolate_4":
                return ITEM_TYPE.CHOCOLATE_4_LAYER;
            case "chocolate_5":
                return ITEM_TYPE.CHOCOLATE_5_LAYER;
            case "chocolate_6":
                return ITEM_TYPE.CHOCOLATE_6_LAYER;

            case "generic_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_RANDOM;
            case "blue_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_1;
            case "green_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_2;
            case "orange_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_3;
            case "purple_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_4;
            case "red_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_5;
            case "yellow_rock_candy":
                return ITEM_TYPE.ROCK_CANDY_6;

            case "collectible_1":
                return ITEM_TYPE.COLLECTIBLE_1;
            case "collectible_2":
                return ITEM_TYPE.COLLECTIBLE_2;
            case "collectible_3":
                return ITEM_TYPE.COLLECTIBLE_3;
            case "collectible_4":
                return ITEM_TYPE.COLLECTIBLE_4;
            case "collectible_5":
                return ITEM_TYPE.COLLECTIBLE_5;
            case "collectible_6":
                return ITEM_TYPE.COLLECTIBLE_6;
            case "collectible_7":
                return ITEM_TYPE.COLLECTIBLE_7;
            case "collectible_8":
                return ITEM_TYPE.COLLECTIBLE_8;
            case "collectible_9":
                return ITEM_TYPE.COLLECTIBLE_9;
            case "collectible_10":
                return ITEM_TYPE.COLLECTIBLE_10;
            case "collectible_11":
                return ITEM_TYPE.COLLECTIBLE_11;
            case "collectible_12":
                return ITEM_TYPE.COLLECTIBLE_12;
            case "collectible_13":
                return ITEM_TYPE.COLLECTIBLE_13;
            case "collectible_14":
                return ITEM_TYPE.COLLECTIBLE_14;
            case "collectible_15":
                return ITEM_TYPE.COLLECTIBLE_15;
            case "collectible_16":
                return ITEM_TYPE.COLLECTIBLE_16;
            case "collectible_17":
                return ITEM_TYPE.COLLECTIBLE_17;
            case "collectible_18":
                return ITEM_TYPE.COLLECTIBLE_18;
            case "collectible_19":
                return ITEM_TYPE.COLLECTIBLE_19;
            case "collectible_20":
                return ITEM_TYPE.COLLECTIBLE_20;
        }

        return ITEM_TYPE.NONE;
    }

    WAFFLE_TYPE DataToWaffleType(int key)
    {
        if (key == 0) return WAFFLE_TYPE.NONE;

        var data = names[(key - 1).ToString()];

        switch (data)
        {
            case "waffle_1":
                return WAFFLE_TYPE.WAFFLE_1;
            case "waffle_2":
                return WAFFLE_TYPE.WAFFLE_2;
            case "waffle_3":
                return WAFFLE_TYPE.WAFFLE_3;
        }

        return WAFFLE_TYPE.NONE;
    }

    CAGE_TYPE DataToCageType(int key)
    {
        if (key == 0) return CAGE_TYPE.NONE;

        var data = names[(key - 1).ToString()];

        switch (data)
        {
            case "cage_1":
                return CAGE_TYPE.CAGE_1;
        }

        return CAGE_TYPE.NONE;
    }

    TARGET_TYPE DataToTargetType(int data)
    {
        switch (data)
        {
            case 1:
                return TARGET_TYPE.SCORE;
            case 2:
                return TARGET_TYPE.COOKIE;
            case 3:
                return TARGET_TYPE.MARSHMALLOW;
            case 4:
                return TARGET_TYPE.WAFFLE;
            case 5:
                return TARGET_TYPE.COLLECTIBLE;
            case 6:
                return TARGET_TYPE.COLUMN_ROW_BREAKER;
            case 7:
                return TARGET_TYPE.BOMB_BREAKER;
            case 8:
                return TARGET_TYPE.X_BREAKER;
            case 9:
                return TARGET_TYPE.CAGE;
            case 10:
                return TARGET_TYPE.RAINBOW;
            case 11:
                return TARGET_TYPE.GINGERBREAD;
            case 12:
                return TARGET_TYPE.CHOCOLATE;
            case 13:
                return TARGET_TYPE.ROCK_CANDY;
        }

        return TARGET_TYPE.NONE;
    }

    #endregion

    #region Utility

    // random with weighted
    public ITEM_TYPE RandomCookie()
    {
        var cookies = new List<ITEM_TYPE>();

        for (int i = 0; i < usingCookies.Count; i++)
        {
            var type = usingCookies[i];

            for (int j = 0; j < cookieWeight[i]; j++)
            {
                switch (type)
                {
                    case ITEM_TYPE.COOKIE_1:
                        cookies.Add(ITEM_TYPE.COOKIE_1);
                        break;
                    case ITEM_TYPE.COOKIE_2:
                        cookies.Add(ITEM_TYPE.COOKIE_2);
                        break;
                    case ITEM_TYPE.COOKIE_3:
                        cookies.Add(ITEM_TYPE.COOKIE_3);
                        break;
                    case ITEM_TYPE.COOKIE_4:
                        cookies.Add(ITEM_TYPE.COOKIE_4);
                        break;
                    case ITEM_TYPE.COOKIE_5:
                        cookies.Add(ITEM_TYPE.COOKIE_5);
                        break;
                    case ITEM_TYPE.COOKIE_6:
                        cookies.Add(ITEM_TYPE.COOKIE_6);
                        break;
                }
            }
        }

        //print(cookies.Count);

        var index = UnityEngine.Random.Range(0, cookies.Count);

        return cookies[index];
    }

    public int RandomColor()
    {
        var type = RandomCookie();

        switch (type)
        {
            case ITEM_TYPE.COOKIE_1:
                return 1;
            case ITEM_TYPE.COOKIE_2:
                return 2;
            case ITEM_TYPE.COOKIE_3:
                return 3;
            case ITEM_TYPE.COOKIE_4:
                return 4;
            case ITEM_TYPE.COOKIE_5:
                return 5;
            case ITEM_TYPE.COOKIE_6:
                return 6;
            default:
                return 0;
        }
    }

    // with weighted
    public ITEM_TYPE RandomGingerbread()
    {
        var gingerbreads = new List<ITEM_TYPE>();

        for (int i = 0; i < usingGingerbreads.Count; i++)
        {
            var type = usingGingerbreads[i];

            for (int j = 0; j < gingerbreadWeight[i]; j++)
            {
                switch (type)
                {
                    case ITEM_TYPE.GINGERBREAD_1:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_1);
                        break;
                    case ITEM_TYPE.GINGERBREAD_2:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_2);
                        break;
                    case ITEM_TYPE.GINGERBREAD_3:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_3);
                        break;
                    case ITEM_TYPE.GINGERBREAD_4:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_4);
                        break;
                    case ITEM_TYPE.GINGERBREAD_5:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_5);
                        break;
                    case ITEM_TYPE.GINGERBREAD_6:
                        gingerbreads.Add(ITEM_TYPE.GINGERBREAD_6);
                        break;
                }
            }
        }

        var index = UnityEngine.Random.Range(0, gingerbreads.Count);

        return gingerbreads[index];
    }

    #endregion
}
