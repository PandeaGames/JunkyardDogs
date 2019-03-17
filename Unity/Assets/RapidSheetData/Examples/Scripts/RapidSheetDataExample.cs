/// 
/// File:				RapidSheetDataExample.cs
/// 
/// System:				Rapid Sheet Data (RSD) Unity3D client library
/// Version:			1.0.0
/// 
/// Language:			C#
/// 
/// License:				
/// 
/// Author:				Tasos Giannakopoulos (tasosg@voidinspace.com)
/// Date:				08 Mar 2017
/// 
/// Description:		
/// 


using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace Lib.RapidSheetData.Examples
{
    /// 
    /// Class:       ExampleRSDUtils
    /// Description: 
    ///
    public static class ExampleRSDUtils
    {
        /// <summary>
        /// Converts a list of T object to string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToString<T>(List<T> list)
        {
            if (list != null)
            {
                string items = "";
                for (int i = 0; i < list.Count; ++i)
                {
                    items += (list[i] != null) ? list[i].ToString() : "null";
                    if (i < (list.Count - 1)) { items += ", "; }
                }

                return items;
            }

            return "null";
        }

        /// <summary>
        /// Converts a list of T object to string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ListToString<T>(T[] list)
        {
            if (list != null)
            {
                string items = "";
                for (int i = 0; i < list.Length; ++i)
                {
                    items += (list[i] != null) ? list[i].ToString() : "null";
                    if (i < (list.Length - 1)) { items += ", "; }
                }

                return items;
            }

            return "null";
        }
    }

    /// 
    /// Class:       ExampleDOB
    /// Description: 
    ///
    [RSDObject]
    public class ExampleDOB
    {
        // 
        public enum ExampleEnum
        {
            Invalid = 0,
            First,
            Second,
            Third
        }

        // Properties
        public int IntProp { get; set; }
        public float FloatProp { get; protected set; }
        public string StringProp { get; private set; }
        public bool BoolProp { get; protected set; }
        public ExampleEnum EnumProp { get; private set; }

        // Fields
        public int IntField = 0;
        public float FloatField = 0f;
        public string StringField = "";
        public bool BoolField = false;
        public ExampleEnum EnumField = ExampleEnum.Invalid;

        // Arrays
        public List<int> IntListField = null;
        public List<float> FloatListField { get; private set; }
        public string[] StringListField = null;
        public bool[] BoolListField { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- ExampleDOB ---</b>\n");

            b.Append("<i>Properties</i>\n");

            b.AppendFormat("<b>IntProp:</b> {0}\n", IntProp);
            b.AppendFormat("<b>FloatProp:</b> {0}\n", FloatProp);
            b.AppendFormat("<b>StringProp:</b> {0}\n", StringProp);
            b.AppendFormat("<b>BoolProp:</b> {0}\n", BoolProp);
            b.AppendFormat("<b>EnumProp:</b> {0}\n\n", EnumProp);

            b.Append("<i>Fields</i>\n");

            b.AppendFormat("<b>IntField:</b> {0}\n", IntField);
            b.AppendFormat("<b>FloatField:</b> {0}\n", FloatField);
            b.AppendFormat("<b>StringField:</b> {0}\n", StringField);
            b.AppendFormat("<b>BoolField:</b> {0}\n", BoolField);
            b.AppendFormat("<b>EnumField:</b> {0}\n\n", EnumField);

            b.Append("<i>Arrays</i>\n");

            b.AppendFormat("<b>IntListField:</b> {0}\n", ExampleRSDUtils.ListToString(IntListField));
            b.AppendFormat("<b>FloatListField:</b> {0}\n", ExampleRSDUtils.ListToString(FloatListField));
            b.AppendFormat("<b>StringListField:</b> {0}\n", ExampleRSDUtils.ListToString(StringListField));
            b.AppendFormat("<b>BoolListField:</b> {0}\n", ExampleRSDUtils.ListToString(BoolListField));

            return b.ToString();
        }
    }

    /// 
    /// Class:       GameDOB
    /// Description: 
    ///
    [RSDObject]
    public class GameDOB
    {
        public string ID { get; private set; }
        public int PlayerInitGold { get; private set; }
        public int NumberOfStartingPlayers { get; private set; }
        public float DaytimeLength { get; private set; }
        public float NightimeLength { get; private set; }
        public string StartingLevel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- GameDOB ---</b>\n");

            b.AppendFormat("<b>ID:</b> {0}\n", ID);
            b.AppendFormat("<b>PlayerInitGold:</b> {0}\n", PlayerInitGold);
            b.AppendFormat("<b>NumberOfStartingPlayers:</b> {0}\n", NumberOfStartingPlayers);
            b.AppendFormat("<b>DaytimeLength:</b> {0}\n", DaytimeLength);
            b.AppendFormat("<b>NightimeLength:</b> {0}\n", NightimeLength);
            b.AppendFormat("<b>StartingLevel:</b> {0}\n", StartingLevel);

            return b.ToString();
        }
    }

    /// 
    /// Class:       CharacterDOB
    /// Description: 
    ///
    [RSDObject]
    public class CharacterDOB
    {
        //
        public enum Professions
        {
            None = 0,
            Assasin, 
            Blocker, 
            Healer
        }

        // 
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Professions Profession { get; private set; }
        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Intelligence { get; private set; }
        public bool CanDance { get; private set; }
        public List<string> InitialItems { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- CharacterDOB ---</b>\n");

            b.AppendFormat("<b>Id:</b> {0}\n", Id);
            b.AppendFormat("<b>Name:</b> {0}\n", Name);
            b.AppendFormat("<b>Description:</b> {0}\n", Description);
            b.AppendFormat("<b>Profession:</b> {0}\n", Profession);
            b.AppendFormat("<b>STR:</b> {0}, <b>DEX:</b> {1}, <b>INT:</b> {2}\n", Strength, Dexterity, Intelligence);
            b.AppendFormat("<b>CanDance:</b> {0}\n", CanDance);
            b.AppendFormat("<b>InitialItems:</b> {0}\n", ExampleRSDUtils.ListToString(InitialItems));

            return b.ToString();
        }
    }

    /// 
    /// Class:       LevelDOB
    /// Description: 
    ///
    [RSDObject]
    public struct LevelDOB
    {
        public string Id { get; private set; }
        public string LevelName { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- LevelDOB ---</b>\n");

            b.AppendFormat("<b>Id:</b> {0}\n", Id);
            b.AppendFormat("<b>LevelName:</b> {0}\n", LevelName);
            b.AppendFormat("<b>Description:</b> {0}\n", Description);

            return b.ToString();
        }
    }

    /// 
    /// Class:       ItemDOB
    /// Description: 
    ///
    [RSDObject]
    public class ItemDOB
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- ItemDOB ---</b>\n");

            b.AppendFormat("<b>Id:</b> {0}\n", Id);
            b.AppendFormat("<b>Name:</b> {0}\n", Name);
            b.AppendFormat("<b>Description:</b> {0}\n", Description);

            return b.ToString();
        }
    }

    /// 
    /// Class:       WeaponDOB
    /// Description: 
    ///
    [RSDObject]
    public class WeaponDOB : ItemDOB
    {
        // 
        public enum WeaponTypes
        {
            None = 0,
            Knife,
            Crossbow,
            Rifle,
            Stave,
            Axe,
            Shield
        }

        // 
        public WeaponTypes WeaponType { get; private set; }
        public int Attack { get; private set; }
        public int AttackSpeed { get; private set; }
        public int Defense { get; private set; }
        public float BlockChance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder b = new StringBuilder("<b>--- ItemDOB ---</b>\n");

            b.AppendFormat("<b>Id:</b> {0}\n", Id);
            b.AppendFormat("<b>Name:</b> {0}\n", Name);
            b.AppendFormat("<b>Description:</b> {0}\n", Description);
            b.AppendFormat("<b>WeaponType:</b> {0}\n", WeaponType);
            b.AppendFormat("<b>Attack:</b> {0}, <b>AttackSpeed:</b> {1}\n", Attack, AttackSpeed);
            b.AppendFormat("<b>Defense:</b> {0}, <b>BlockChance:</b> {1}\n", Defense, BlockChance);

            return b.ToString();
        }
    }

    /// 
    /// Class:       RapidSheetDataExample
    /// Description: 
    ///
    public class RapidSheetDataExample : MonoBehaviour
    {
        // Reference to the Rapid Sheet Data Asset
        [SerializeField]
        private RSDAsset _dataAsset = null;

        [SerializeField]
        private RapidSheetDataExampleView _exampleView = null;

        /// 
        /// Rapid Sheet Data Asset
        /// 

        #region Rapid Sheet Data Asset

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void Init()
        {
            Debug.LogFormat("[RapidSheetDataExample] Testing Rapid Sheet Data Asset '{0}' ...", _dataAsset);

            _exampleView.Init();
            _exampleView.OnPullDataClicked += PullData;

            // Initialize the data asset
#if ENABLE_IL2CPP
            // Use the AOT converter as TypeDescriptor.GetConverter() won't work here -- use in iOS or Android IL2CPP
            _dataAsset.Init(this, new RSDSerializerDefaultLit(new RSDConverterAOT()));
#else
            // Use the default deserializer / converter provided
            _dataAsset.Init(this);
#endif

            // 
            // Cached data
            //

            Debug.LogFormat("[RapidSheetDataExample] Data cached in '{0}'", _dataAsset.name);

            string offlineDataDump = GetRSDAssetDataAsString();
            _exampleView.SetOfflineDataText(offlineDataDump);

            Debug.Log(offlineDataDump);

            // 
            // Live data
            //

            PullData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetRSDAssetDataAsString()
        {
            string dump = "";

            // Get all the data from 'Example Data' sheet as a list of ExampleDOB instances
            List<ExampleDOB> exampleData = _dataAsset.GetSheet<ExampleDOB>("Example Data");
            foreach (var data in exampleData)
            {
                dump += data + "\n";
            }

            // Returns the first index of the Game Data list
            // Useful for game configuration data on init
            GameDOB releaseGameData = _dataAsset.GetFromSheet<GameDOB>(0, "Game Data");
            dump += releaseGameData + "\n";

            // Returns the data with ID 'Development'
            GameDOB developmentGameData = _dataAsset.GetFromSheet<GameDOB>("Development", "Game Data");
            dump += developmentGameData + "\n";

            // Leve data
            List<LevelDOB> levelData = _dataAsset.GetSheet<LevelDOB>("Level Data");
            foreach (var data in levelData)
            {
                dump += data + "\n";
            }

            // Character data
            List<CharacterDOB> characterData = _dataAsset.GetSheet<CharacterDOB>("Character Data");
            foreach (var data in characterData)
            {
                dump += data + "\n";

                // List of 'Initial Items' field
                foreach (string weapon in data.InitialItems)
                {
                    // Get WeaponDOB instance using InitialItems as ID
                    WeaponDOB weaponData = _dataAsset.GetFromSheet<WeaponDOB>(weapon, "Weapon Data");
                    if (weaponData != null)
                    {
                        dump += weaponData + "\n";
                    }
                }
            }

            return dump;
        }

        #endregion /// Rapid Sheet Data Asset

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void PullData()
        {
            Debug.Log("[RapidSheetDataExample] Pulling data asynchronously from RSD service via RSDAsset ...");

            _exampleView.SetLiveDataText("Fetching ...");

            _dataAsset.PullData((bool success) =>
            {
                if (success)
                {
                    Debug.Log("[RapidSheetDataExample] Successfully pulled data from RSD service.");

                    string liveDataDump = GetRSDAssetDataAsString();
                    _exampleView.SetLiveDataText(liveDataDump);

                    Debug.Log(liveDataDump);
                }
                else
                {
                    Debug.LogWarning("[RapidSheetDataExample] Failed to pull data from RSD service!");
                }
            });
        }

        /// 
        /// Mono Behaviour
        /// 

        #region Mono Behaviour

        // Use this for initialization
        private void Start()
        {
            Init();
        }

        #endregion /// Mono Behaviour
    }
} /// Lib.RapidSheetData.Examples