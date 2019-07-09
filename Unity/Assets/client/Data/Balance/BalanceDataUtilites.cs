
using System;
using JunkyardDogs.Simulation;

public static class BalanceDataUtilites
{
    public const string ListDelimiter = ",";
    public const char ListDelimiterChar = ',';
    public const char DataDelimiterChar = ':';
    public const string DataDelimiter = ":";

    public const string BALANCE_MENU_FOLDER = "Balance/";
    public const string BALANCE_SOURCE_MENU_FOLDER = BALANCE_MENU_FOLDER + "Source/";

    public struct EnumValue<TEnum>
    {
        public readonly int value;
        public readonly TEnum enumValue;

        public EnumValue(int value, TEnum enumValue)
        {
            this.value = value;
            this.enumValue = enumValue;
        }
    }
    
    public static string EncodeEnum<TEnum>(EnumValue<TEnum>[] data)
    {
        string[] dataValues = new string[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            string enumValueName = Enum.GetName(typeof(TEnum), data[i].enumValue);
            dataValues[i] = enumValueName + DataDelimiter + data[i].value;
        }

        string returnData = string.Join(ListDelimiter, dataValues);
        
        return returnData;
    }
    
    public static string EncodeEnumSingle<TEnum>(TEnum data)
    {
        string enumValueName = Enum.GetName(typeof(TEnum), data);
        return enumValueName;
    }

    public static TEnum DecodeEnumSingle<TEnum>(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return default(TEnum);
        }

        TEnum enumValue = (TEnum) Enum.Parse(typeof(TEnum), data);

        return enumValue;
    }

    public static EnumValue<TEnum>[] DecodeEnum<TEnum>(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return new EnumValue<TEnum>[0];
        }
        
        string[] dataValues = data.Split(ListDelimiterChar);
        EnumValue<TEnum>[] returnValues = new EnumValue<TEnum>[dataValues.Length];

        for (int i = 0; i < dataValues.Length; i++)
        {
            string[] seperated = dataValues[i].Split(DataDelimiterChar);
            TEnum enumValue = (TEnum) Enum.Parse(typeof (TEnum), seperated[0]);
            int value;
            int.TryParse(seperated[1], out value);
            returnValues[i] = new EnumValue<TEnum>(value, enumValue);
        }
        
        return returnValues;
    }

    public static Distinction[] DecodeDistinctions(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return new Distinction[0];
        }
        
        EnumValue<DistinctionType>[] values = DecodeEnum<DistinctionType>(data);
        Distinction[] distinctions=  new Distinction[values.Length];
        
        for (int i = 0; i < values.Length; i++)
        {
            Distinction distinction = new Distinction();
            distinction.Type = values[i].enumValue;
            distinction.Value = values[i].value;
            distinctions[i] = distinction;
        }

        return distinctions;
    }
}
