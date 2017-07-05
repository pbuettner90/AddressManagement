using Plugin.Settings;
using Plugin.Settings.Abstractions;

public static class Settings
{
	 static ISettings AppSettings
	{
		get
		{
			return CrossSettings.Current;
		}
	}

	 private const string UserApiKey = "apiKey";
	 private static readonly string ApiKeyDefault = string.Empty;

	 private const string UrlWebService = "urlWebService";
	 private static readonly string UrlWebServiceDefault = string.Empty;

	 private const string InternalWebserviceKey = "internal";
	 private static readonly bool InternalWebServiceDefault = false;

	public static string ApiKey
	{
		get { return AppSettings.GetValueOrDefault<string>(UserApiKey, ApiKeyDefault); }
		set { AppSettings.AddOrUpdateValue<string>(UserApiKey, value); }
	}

	public static string Url
	{
		get { return AppSettings.GetValueOrDefault<string>(UrlWebService, UrlWebServiceDefault); }
		set { AppSettings.AddOrUpdateValue<string>(UrlWebService, value); }
	}

	public static bool InternalWebservice
	{
		get { return AppSettings.GetValueOrDefault<bool>(InternalWebserviceKey, InternalWebServiceDefault); }
		set { AppSettings.AddOrUpdateValue<bool>(InternalWebserviceKey, value); }
	}
}