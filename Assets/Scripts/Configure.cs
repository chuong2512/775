using UnityEngine;
using System.Collections;
using System;

public enum MAP_LEVEL_STATUS
{
    LOCKED,
    CURRENT,
    OPENED
}

public enum TILE_TYPE
{
    NONE,
    PASS_THROUGH,
    LIGHT_TILE,
    DARD_TILE
}

// waffle
public enum WAFFLE_TYPE
{
    NONE,
    WAFFLE_1,
    WAFFLE_2,
    WAFFLE_3 // do not use
}

public enum ITEM_TYPE
{
    NONE,

    COOKIE_RAMDOM,
    COOKIE_RAINBOW,

    COOKIE_1,
    COOKIE_1_COLUMN_BREAKER,
    COOKIE_1_ROW_BREAKER,
    COOKIE_1_BOMB_BREAKER,

    COOKIE_2,
    COOKIE_2_COLUMN_BREAKER,
    COOKIE_2_ROW_BREAKER,
    COOKIE_2_BOMB_BREAKER,
    
    COOKIE_3,
    COOKIE_3_COLUMN_BREAKER,
    COOKIE_3_ROW_BREAKER,
    COOKIE_3_BOMB_BREAKER,
    
    COOKIE_4,
    COOKIE_4_COLUMN_BREAKER,
    COOKIE_4_ROW_BREAKER,
    COOKIE_4_BOMB_BREAKER,
    
    COOKIE_5,
    COOKIE_5_COLUMN_BREAKER,
    COOKIE_5_ROW_BREAKER,
    COOKIE_5_BOMB_BREAKER,
    
    COOKIE_6,
    COOKIE_6_COLUMN_BREAKER,
    COOKIE_6_ROW_BREAKER,
    COOKIE_6_BOMB_BREAKER,
    
    MARSHMALLOW,

    GINGERBREAD_RANDOM,
    GINGERBREAD_1,
    GINGERBREAD_2,
    GINGERBREAD_3,
    GINGERBREAD_4,
    GINGERBREAD_5,
    GINGERBREAD_6,

    CHOCOLATE_1_LAYER,
    CHOCOLATE_2_LAYER,
    CHOCOLATE_3_LAYER,
    CHOCOLATE_4_LAYER,
    CHOCOLATE_5_LAYER,
    CHOCOLATE_6_LAYER,

    ROCK_CANDY_RANDOM,
    ROCK_CANDY_1,
    ROCK_CANDY_2,
    ROCK_CANDY_3,
    ROCK_CANDY_4,
    ROCK_CANDY_5,
    ROCK_CANDY_6,

    COOKIE_1_X_BREAKER,
    COOKIE_2_X_BREAKER,
    COOKIE_3_X_BREAKER,
    COOKIE_4_X_BREAKER,
    COOKIE_5_X_BREAKER,
    COOKIE_6_X_BREAKER,

    COLLECTIBLE_1,
    COLLECTIBLE_2,
    COLLECTIBLE_3,
    COLLECTIBLE_4,
    COLLECTIBLE_5,
    COLLECTIBLE_6,
    COLLECTIBLE_7,
    COLLECTIBLE_8,
    COLLECTIBLE_9,
    COLLECTIBLE_10,
    COLLECTIBLE_11,
    COLLECTIBLE_12,
    COLLECTIBLE_13,
    COLLECTIBLE_14,
    COLLECTIBLE_15,
    COLLECTIBLE_16,
    COLLECTIBLE_17,
    COLLECTIBLE_18,
    COLLECTIBLE_19,
    COLLECTIBLE_20
}

// cage
public enum CAGE_TYPE
{
    NONE,
    CAGE_1
}

public enum GAME_STATE
{
    PREPARING_LEVEL,
    WAITING_USER_SWAP,
    PRE_WIN_AUTO_PLAYING,
    OPENING_POPUP,
    NO_MATCHES_REGENERATING,
    DESTROYING_ITEMS
}

public enum BOOSTER_TYPE
{
    NONE = 0,
    SINGLE_BREAKER,
    COLUMN_BREAKER,
    ROW_BREAKER,
    RAINBOW_BREAKER,
    OVEN_BREAKER,
    BEGIN_FIVE_MOVES,
    BEGIN_RAINBOW_BREAKER,
    BEGIN_BOMB_BREAKER
}

public enum FIND_DIRECTION
{
    NONE = 0,
    ROW,
    COLUMN
}

public enum BREAKER_EFFECT
{
    NORMAL = 0,
    BIG_ROW_BREAKER,
    BIG_COLUMN_BREAKER,
    CROSS,
    CROSS_X_BREAKER,
    BOMB_X_BREAKER,
    NONE
}

public enum TARGET_TYPE
{
    NONE = 0,
    SCORE, // do not use
    COOKIE,
    MARSHMALLOW,
    WAFFLE,
    COLLECTIBLE,
    COLUMN_ROW_BREAKER,
    BOMB_BREAKER,
    X_BREAKER,
    CAGE,
    RAINBOW,
    GINGERBREAD,
    CHOCOLATE,
    ROCK_CANDY
}

public enum SWAP_DIRECTION
{
    NONE, 
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}

public class Configure : MonoBehaviour 
{
    public static Configure instance = null;

    [Header("Configuration")]
    public float swapTime;
    public float destroyTime;
    public float dropTime;
    public float changingTime;
    public float hintDelay;

    [Header("")]
    public int scoreItem;
    public int finishedScoreItem;
    [Header("")]
    public int bonus1Star;
    public int bonus2Star;
    public int bonus3Star;

    [Header("")]
    public int package1Amount;
    public int package2Amount;

    [Header("")]
    public int beginFiveMovesLevel;
    public int beginRainbowLevel;
    public int beginBombBreakerLevel;

    [Header("")]
    public int beginFiveMovesCost1;
    public int beginFiveMovesCost2;
    [Header("")]
    public int beginRainbowCost1;
    public int beginRainbowCost2;
    [Header("")]
    public int beginBombBreakerCost1;
    public int beginBombBreakerCost2;

    // play
    [Header("")]
    public int keepPlayingCost;
    public int skipLevelCost;

    [Header("")]
    public int singleBreakerCost1;
    public int singleBreakerCost2;

    [Header("")]
    public int rowBreakerCost1;
    public int rowBreakerCost2;

    [Header("")]
    public int columnBreakerCost1;
    public int columnBreakerCost2;

    [Header("")]
    public int rainbowBreakerCost1;
    public int rainbowBreakerCost2;

    [Header("")]
    public int ovenBreakerCost1;
    public int ovenBreakerCost2;

    [Header("")]
    public int plusMoves = 5;
    public bool showHint;

    [Header("Shop")]
    public float product1Price;
    public float product2Price;
    public float product3Price;
    public float product4Price;
    public float product5Price;
    [Header("")]
    public int product1Coin;
    public int product2Coin;
    public int product3Coin;
    public int product4Coin;
    public int product5Coin;
    public int watchVideoCoin;

    [Header("Check")]
    // map config
    public int autoPopup;

    [Header("")]

    // play config
    public bool beginFiveMoves;
    public bool beginRainbow;
    public bool beginBombBreaker;

    [Header("")]
    public bool touchIsSwallowed;

    // settings
    public static int maxCookies = 6;

    // max level
    public int maxLevel = 100;
    
    [Header("Check to disable debug")]
    public bool checkSwap; // TEST ONLY

    [Header("Facebook Leaderboard")]
    public bool FBLeaderboard;
    public int FBLoginCoin;

    [Header("Encouraging Popup")]
    public int encouragingPopup;

	[Header("Life")]
	public int life;
	public float timer;
	public string exitDateTime;
	[Header("")]
	public int maxLife;
	public int lifeRecoveryHour;
	public int lifeRecoveryMinute;
	public int lifeRecoverySecond;
    public int recoveryCostPerLife;

    // game data
    public static string game_data = "cookie.dat";
    public static string opened_level = "opened_level";
    public static string level_statistics = "level_statistics";
    public static string level_star = "level_star";
    public static string level_score = "level_score";
    public static string level_number = "level_number";
    public static string player_coin = "player_coin";
    public static string single_breaker = "single_breaker";
    public static string row_breaker = "row_breaker";
    public static string column_breaker = "column_breaker";
    public static string rainbow_breaker = "rainbow_breaker";
    public static string oven_breaker = "oven_breaker";
    public static string begin_five_moves = "begin_five_moves";
    public static string begin_rainbow = "begin_rainbow";
    public static string begin_bomb_breaker = "begin_bomb_breaker";

	// life
	public static string exit_date_time = "string_exit_date_time";
	public static string stringLife = "string_life";
	public static string stringTimer = "string_timer";

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

    #region Prefab

    // node
    public static string NodePrefab()
    {
        return "Prefabs/PlayScene/Node";
    }

    // tile
    public static string LightTilePrefab()
    {
        return "Prefabs/PlayScene/TileLayer/LightTile";
    }

    public static string DarkTilePrefab()
    {
        return "Prefabs/PlayScene/TileLayer/DarkTile";
    }

    public static string NoneTilePrefab()
    {
        return "Prefabs/PlayScene/TileLayer/NoneTile";
    }

    // tile border
    public static string TileBorderTop()
    {
        return "Prefabs/PlayScene/TileLayer/Top/";
    }

    public static string TileBorderBottom()
    {
        return "Prefabs/PlayScene/TileLayer/Bottom/";
    }

    public static string TileBorderLeft()
    {
        return "Prefabs/PlayScene/TileLayer/Left/";
    }

    public static string TileBorderRight()
    {
        return "Prefabs/PlayScene/TileLayer/Right/";
    }

    // cookie1
    public static string Cookie1()
    {
        return "Prefabs/Cookies/blue";
    }

    public static string Cookie1BombBreaker()
    {
        return "Prefabs/Cookies/blue_bomb_breaker";
    }

    public static string Cookie1ColumnBreaker()
    {
        return "Prefabs/Cookies/blue_column_breaker";
    }

    public static string Cookie1RowBreaker()
    {
        return "Prefabs/Cookies/blue_row_breaker";
    }

    public static string Cookie1XBreaker()
    {
        return "Prefabs/Cookies/blue_x_breaker";
    }

    // cookie2
    public static string Cookie2()
    {
        return "Prefabs/Cookies/green";
    }

    public static string Cookie2BombBreaker()
    {
        return "Prefabs/Cookies/green_bomb_breaker";
    }

    public static string Cookie2ColumnBreaker()
    {
        return "Prefabs/Cookies/green_column_breaker";
    }

    public static string Cookie2RowBreaker()
    {
        return "Prefabs/Cookies/green_row_breaker";
    }

    public static string Cookie2XBreaker()
    {
        return "Prefabs/Cookies/green_x_breaker";
    }

    // cookie3
    public static string Cookie3()
    {
        return "Prefabs/Cookies/orange";
    }

    public static string Cookie3BombBreaker()
    {
        return "Prefabs/Cookies/orange_bomb_breaker";
    }

    public static string Cookie3ColumnBreaker()
    {
        return "Prefabs/Cookies/orange_column_breaker";
    }

    public static string Cookie3RowBreaker()
    {
        return "Prefabs/Cookies/orange_row_breaker";
    }

    public static string Cookie3XBreaker()
    {
        return "Prefabs/Cookies/orange_x_breaker";
    }

    // cookie4
    public static string Cookie4()
    {
        return "Prefabs/Cookies/purple";
    }

    public static string Cookie4BombBreaker()
    {
        return "Prefabs/Cookies/purple_bomb_breaker";
    }

    public static string Cookie4ColumnBreaker()
    {
        return "Prefabs/Cookies/purple_column_breaker";
    }

    public static string Cookie4RowBreaker()
    {
        return "Prefabs/Cookies/purple_row_breaker";
    }

    public static string Cookie4XBreaker()
    {
        return "Prefabs/Cookies/purple_x_breaker";
    }

    // cookie5
    public static string Cookie5()
    {
        return "Prefabs/Cookies/red";
    }

    public static string Cookie5BombBreaker()
    {
        return "Prefabs/Cookies/red_bomb_breaker";
    }

    public static string Cookie5ColumnBreaker()
    {
        return "Prefabs/Cookies/red_column_breaker";
    }

    public static string Cookie5RowBreaker()
    {
        return "Prefabs/Cookies/red_row_breaker";
    }

    public static string Cookie5XBreaker()
    {
        return "Prefabs/Cookies/red_x_breaker";
    }

    // cookie6
    public static string Cookie6()
    {
        return "Prefabs/Cookies/yellow";
    }

    public static string Cookie6BombBreaker()
    {
        return "Prefabs/Cookies/yellow_bomb_breaker";
    }

    public static string Cookie6ColumnBreaker()
    {
        return "Prefabs/Cookies/yellow_column_breaker";
    }

    public static string Cookie6RowBreaker()
    {
        return "Prefabs/Cookies/yellow_row_breaker";
    }

    public static string Cookie6XBreaker()
    {
        return "Prefabs/Cookies/yellow_x_breaker";
    }

    // rainbow
    public static string CookieRainbow()
    {
        return "Prefabs/Cookies/rainbow";
    }

    // marshmallow
    public static string Marshmallow()
    {
        return "Prefabs/Items/marshmallow";
    }

    // gingerbread
    public static string Gingerbread1()
    {
        return "Prefabs/Items/gingerbread_1";
    }

    public static string Gingerbread2()
    {
        return "Prefabs/Items/gingerbread_2";
    }

    public static string Gingerbread3()
    {
        return "Prefabs/Items/gingerbread_3";
    }

    public static string Gingerbread4()
    {
        return "Prefabs/Items/gingerbread_4";
    }

    public static string Gingerbread5()
    {
        return "Prefabs/Items/gingerbread_5";
    }

    public static string Gingerbread6()
    {
        return "Prefabs/Items/gingerbread_6";
    }

    public static string GingerbreadGeneric()
    {
        return "Prefabs/Items/gingerbread_generic";
    }

    // chocolate
    public static string Chocolate1()
    {
        return "Prefabs/Items/chocolate_1";
    }

    public static string Chocolate2()
    {
        return "Prefabs/Items/chocolate_2";
    }

    public static string Chocolate3()
    {
        return "Prefabs/Items/chocolate_3";
    }

    public static string Chocolate4()
    {
        return "Prefabs/Items/chocolate_4";
    }

    public static string Chocolate5()
    {
        return "Prefabs/Items/chocolate_5";
    }

    public static string Chocolate6()
    {
        return "Prefabs/Items/chocolate_6";
    }

    // rock candy
    public static string RockCandy1()
    {
        return "Prefabs/Items/rock_candy_1";
    }

    public static string RockCandy2()
    {
        return "Prefabs/Items/rock_candy_2";
    }

    public static string RockCandy3()
    {
        return "Prefabs/Items/rock_candy_3";
    }

    public static string RockCandy4()
    {
        return "Prefabs/Items/rock_candy_4";
    }

    public static string RockCandy5()
    {
        return "Prefabs/Items/rock_candy_5";
    }

    public static string RockCandy6()
    {
        return "Prefabs/Items/rock_candy_6";
    }

    public static string RockCandyGeneric()
    {
        return "Prefabs/Items/rock_candy_generic";
    }

    // collectible
    public static string Collectible1()
    {
        return "Prefabs/Items/collectible_1";
    }

    public static string Collectible2()
    {
        return "Prefabs/Items/collectible_2";
    }

    public static string Collectible3()
    {
        return "Prefabs/Items/collectible_3";
    }

    public static string Collectible4()
    {
        return "Prefabs/Items/collectible_4";
    }

    public static string Collectible5()
    {
        return "Prefabs/Items/collectible_5";
    }

    public static string Collectible6()
    {
        return "Prefabs/Items/collectible_6";
    }

    public static string Collectible7()
    {
        return "Prefabs/Items/collectible_7";
    }

    public static string Collectible8()
    {
        return "Prefabs/Items/collectible_8";
    }

    public static string Collectible9()
    {
        return "Prefabs/Items/collectible_9";
    }

    public static string Collectible10()
    {
        return "Prefabs/Items/collectible_10";
    }

    public static string Collectible11()
    {
        return "Prefabs/Items/collectible_11";
    }

    public static string Collectible12()
    {
        return "Prefabs/Items/collectible_12";
    }

    public static string Collectible13()
    {
        return "Prefabs/Items/collectible_13";
    }

    public static string Collectible14()
    {
        return "Prefabs/Items/collectible_14";
    }

    public static string Collectible15()
    {
        return "Prefabs/Items/collectible_15";
    }

    public static string Collectible16()
    {
        return "Prefabs/Items/collectible_16";
    }

    public static string Collectible17()
    {
        return "Prefabs/Items/collectible_17";
    }

    public static string Collectible18()
    {
        return "Prefabs/Items/collectible_18";
    }

    public static string Collectible19()
    {
        return "Prefabs/Items/collectible_19";
    }

    public static string Collectible20()
    {
        return "Prefabs/Items/collectible_20";
    }

    public static string CollectibleBox()
    {
        return "Prefabs/Items/collectible_box";
    }

    // cookie effects
    public static string BlueCookieExplosion()
    {
        return "Effects/Cookie Explosion (Blue)";
    }

    public static string GreenCookieExplosion()
    {
        return "Effects/Cookie Explosion (Green)";
    }

    public static string OrangeCookieExplosion()
    {
        return "Effects/Cookie Explosion (Orange)";
    }

    public static string PurpleCookieExplosion()
    {
        return "Effects/Cookie Explosion (Purple)";
    }

    public static string RedCookieExplosion()
    {
        return "Effects/Cookie Explosion (Red)";
    }

    public static string YellowCookieExplosion()
    {
        return "Effects/Cookie Explosion (Yellow)";
    }

    // breaker explosion
    public static string BreakerExplosion1()
    {
        return "Effects/Explosion (Blue)";
    }

    public static string BreakerExplosion2()
    {
        return "Effects/Explosion (Green)";
    }

    public static string BreakerExplosion3()
    {
        return "Effects/Explosion (Orange)";
    }

    public static string BreakerExplosion4()
    {
        return "Effects/Explosion (Purple)";
    }

    public static string BreakerExplosion5()
    {
        return "Effects/Explosion (Red)";
    }

    public static string BreakerExplosion6()
    {
        return "Effects/Explosion (Yellow)";
    }

    // generic
    public static string ColumnRowBreaker()
    {
        return "Prefabs/Items/column_row_breaker";
    }

	public static string GenericBombBreaker()
	{
		return "Prefabs/Items/generic_bomb_breaker";
	}

	public static string GenericXBreaker()
	{
		return "Prefabs/Items/generic_x_breaker";
	}

    // rainbow explosion
    public static string RainbowExplosion()
    {
        return "Effects/Rainbow";
    }

    // ring
    public static string RingExplosion()
    {
        return "Effects/Moves Ring";
    }

    // column explosion
    public static string ColRowBreaker1()
    {
        return "Effects/Striped (Blue)";
    }

    // marshmallow explosion
    public static string MarshmallowExplosion()
    {
        return "Effects/Marshmallow Explosion";
    }

    // chocolate explosion
    public static string ChocolateExplosion()
    {
        return "Effects/Chocolate Explosion";
    }

    public static string ColRowBreaker2()
    {
        return "Effects/Striped (Green)";
    }

    public static string ColRowBreaker3()
    {
        return "Effects/Striped (Orange)";
    }

    public static string ColRowBreaker4()
    {
        return "Effects/Striped (Purple)";
    }

    public static string ColRowBreaker5()
    {
        return "Effects/Striped (Red)";
    }

    public static string ColRowBreaker6()
    {
        return "Effects/Striped (Yellow)";
    }

    // booster
    public static string BoosterActive()
    {
        return "Effects/Booster Active";
    }

    // column breaker animation
    public static string ColumnBreakerAnimation1()
    {
        return "StripeAnim/StripeAnim1";
    }

    public static string ColumnBreakerAnimation2()
    {
        return "StripeAnim/StripeAnim2";
    }

    public static string ColumnBreakerAnimation3()
    {
        return "StripeAnim/StripeAnim3";
    }

    public static string ColumnBreakerAnimation4()
    {
        return "StripeAnim/StripeAnim4";
    }

    public static string ColumnBreakerAnimation5()
    {
        return "StripeAnim/StripeAnim5";
    }

    public static string ColumnBreakerAnimation6()
    {
        return "StripeAnim/StripeAnim6";
    }

    // big column breaker animation
    public static string BigColumnBreakerAnimation1()
    {
        return "StripeAnim/BigStripeAnim1";
    }

    public static string BigColumnBreakerAnimation2()
    {
        return "StripeAnim/BigStripeAnim2";
    }

    public static string BigColumnBreakerAnimation3()
    {
        return "StripeAnim/BigStripeAnim3";
    }

    public static string BigColumnBreakerAnimation4()
    {
        return "StripeAnim/BigStripeAnim4";
    }

    public static string BigColumnBreakerAnimation5()
    {
        return "StripeAnim/BigStripeAnim5";
    }

    public static string BigColumnBreakerAnimation6()
    {
        return "StripeAnim/BigStripeAnim6";
    }

    // waffle
    public static string Waffle1()
    {
        return "Prefabs/Items/waffle_1";
    }

    public static string Waffle2()
    {
        return "Prefabs/Items/waffle_2";
    }

    public static string Waffle3()
    {
        return "Prefabs/Items/waffle_3";
    }

    // cage
    public static string Cage1()
    {
        return "Prefabs/Items/cage_1";
    }

    // cake
    public static string Cake(string name)
    {
        return "Cakes/" + name;
    }

    // star
    public static string StarGold()
    {
        return "Prefabs/PlayScene/UI/StarGold";
    }

    // mask
    public static string Mask()
    {
        return "Prefabs/PlayScene/Mask";
    }   

    // Help
    public static string Level1Step1()
    {
        return "Prefabs/PlayScene/Help/Level1Step1";
    }

    public static string Level1Step2()
    {
        return "Prefabs/PlayScene/Help/Level1Step2";
    }

    public static string Level1Step3()
    {
        return "Prefabs/PlayScene/Help/Level1Step3";
    }

    public static string Level2Step1()
    {
        return "Prefabs/PlayScene/Help/Level2Step1";
    }

    public static string Level2Step2()
    {
        return "Prefabs/PlayScene/Help/Level2Step2";
    }

    public static string Level2Step3()
    {
        return "Prefabs/PlayScene/Help/Level2Step3";
    }

    public static string Level3Step1()
    {
        return "Prefabs/PlayScene/Help/Level3Step1";
    }

    public static string Level3Step2()
    {
        return "Prefabs/PlayScene/Help/Level3Step2";
    }

    public static string Level3Step3()
    {
        return "Prefabs/PlayScene/Help/Level3Step3";
    }

    public static string Level3Step4()
    {
        return "Prefabs/PlayScene/Help/Level3Step4";
    }

    public static string Level3Step5()
    {
        return "Prefabs/PlayScene/Help/Level3Step5";
    }

    public static string Level4Step1()
    {
        return "Prefabs/PlayScene/Help/Level4Step1";
    }

    public static string Level4Step2()
    {
        return "Prefabs/PlayScene/Help/Level4Step2";
    }

    public static string Level4Step3()
    {
        return "Prefabs/PlayScene/Help/Level4Step3";
    }

    public static string Level5Step1()
    {
        return "Prefabs/PlayScene/Help/Level5Step1";
    }

    public static string Level5Step2()
    {
        return "Prefabs/PlayScene/Help/Level5Step2";
    }

    public static string Level6Step1()
    {
        return "Prefabs/PlayScene/Help/Level6Step1";
    }

    public static string Level7Step1()
    {
        return "Prefabs/PlayScene/Help/Level7Step1";
    }

    public static string Level7Step2()
    {
        return "Prefabs/PlayScene/Help/Level7Step2";
    }

    public static string Level9Step1()
    {
        return "Prefabs/PlayScene/Help/Level9Step1";
    }

    public static string Level10BeginStep1()
    {
        return "Prefabs/PlayScene/Help/Level10BeginStep1";
    }

    public static string Level12Step1()
    {
        return "Prefabs/PlayScene/Help/Level12Step1";
    }

    public static string Level12Step2()
    {
        return "Prefabs/PlayScene/Help/Level12Step2";
    }

    public static string Level13Step1()
    {
        return "Prefabs/PlayScene/Help/Level13Step1";
    }

    public static string Level13Step2()
    {
        return "Prefabs/PlayScene/Help/Level13Step2";
    }

    public static string Level15Step1()
    {
        return "Prefabs/PlayScene/Help/Level15Step1";
    }

    public static string Level15Step2()
    {
        return "Prefabs/PlayScene/Help/Level15Step2";
    }

    public static string Level16Step1()
    {
        return "Prefabs/PlayScene/Help/Level16Step1";
    }

    public static string Level18Step1()
    {
        return "Prefabs/PlayScene/Help/Level18Step1";
    }

    public static string Level18Step2()
    {
        return "Prefabs/PlayScene/Help/Level18Step2";
    }

    public static string Level20BeginStep1()
    {
        return "Prefabs/PlayScene/Help/Level20BeginStep1";
    }

    public static string Level23BeginStep1()
    {
        return "Prefabs/PlayScene/Help/Level23BeginStep1";
    }

    public static string Level25Step1()
    {
        return "Prefabs/PlayScene/Help/Level25Step1";
    }

    public static string Level25Step2()
    {
        return "Prefabs/PlayScene/Help/Level25Step2";
    }

    public static string Level31Step1()
    {
        return "Prefabs/PlayScene/Help/Level31Step1";
    }

    public static string Level61Step1()
    {
        return "Prefabs/PlayScene/Help/Level61Step1";
    }

    public static string Level76Step1()
    {
        return "Prefabs/PlayScene/Help/Level76Step1";
    }

    // Loading Image
    public static string LoadingImage()
    {
        return "Prefabs/MapScene/Loading Image";
    }

    // Progress gold star
    public static string ProgressGoldStar()
    {
        return "Prefabs/PlayScene/UI/StarGold";
    }

    // Win pop up star explode
    public static string StarExplode()
    {
        return "Effects/StarExplode";
    }

    #endregion

	#region Life

	void OnApplicationQuit()
	{
		//print("Configure: On application quit / Exit date time: " + DateTime.Now.ToString() + " / Life: " + life + " / Timer: " + timer);

		SaveLifeInfo();
	}

	public void SaveLifeInfo()
	{
        PlayerPrefs.SetFloat(Configure.stringTimer, timer);

		PlayerPrefs.SetInt(stringLife, life);

		PlayerPrefs.SetString(exit_date_time, DateTime.Now.ToString());

		PlayerPrefs.Save();
	}

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            //android onPause()
            SaveLifeInfo();
        }
        else
        {
            //android onResume()
            if (GameObject.Find("LifeBar"))
            {
                Configure.instance.exitDateTime = PlayerPrefs.GetString(Configure.exit_date_time, new DateTime().ToString());
                Configure.instance.timer = PlayerPrefs.GetFloat(Configure.stringTimer, 0f);
                Configure.instance.life = PlayerPrefs.GetInt(Configure.stringLife, Configure.instance.maxLife);

                GameObject.Find("LifeBar").GetComponent<Life>().runTimer = false;
            }
        }
    }

	#endregion
}
