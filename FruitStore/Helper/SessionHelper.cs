using System;
using Newtonsoft.Json;
namespace FruitStore.Helper;

public static class SessionHelper
{
	//set up the session
	public static void SetObjectAsJson(this ISession session, string key, object value)
	{
		session.SetString(key, JsonConvert.SerializeObject(value));
	}
	// get session
	public static T GetObjectFromJson<T>(this ISession session, string key)
	{
		var value = session.GetString(key);
		return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
	}
	// remove session
	public static void Remove(this ISession session, string key)
	{
		session.Remove(key);
	}
}