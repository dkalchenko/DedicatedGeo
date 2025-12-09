namespace DedicatedGeo.Mono.Models;

public static class MySqlConstants
{
    //Common
    public const int DecimalPrecision = 18;
    public const int DecimalScale = 4;
    public const int FirstNameMaxLength = 300;
    public const int SecondNameMaxLength = 300;
    public const int EmailMaxLength = 300;
    public const int PhoneMaxLength = 10;
    public const int PhoneCountryMaxLength = 3;

    //Products
    public const int ProductNameLength = 300;
    public const int FileNameLength = 300;
    public const int CodeLength = 10;
    public const int MetricLength = 40;
    public const int CountryLength = 300;
    public const int AnnotationLength = 300;
    public const int StorageLength = 300;
    public const int AliasMaxChars = 100;


    //Order
    public const int NameMaxLength = 300;
    public const int AreaMaxLength = 300;
    public const int CityMaxLength = 300;
    public const int OfficeMaxLength = 300;
    public const int CommunicationMethodMaxLength = 40;
    public const int PaymentMethodMaxLength = 40;
    public const int PostOfficeMethodMaxLength = 100;
    public const int CustomerMessageMaxLength = 3000;
    public const int StateMaxLenght = 30;

    // Users
    public const int PasswordMaxLength = 300;
    public const int LoginMaxLength = 200;
    public const int RoleMaxLength = 200;

    // Settings
    public const int KeySettingMaxLength = 200;
    public const int ValueSettingMaxLength = 100000;
    
    //Token
    public const int ObjectTokenMaxLength = 100000;
    public const int TokenTypeMaxLength = 200;
    
    public const int CategoryBannersLength = 1000000;
}