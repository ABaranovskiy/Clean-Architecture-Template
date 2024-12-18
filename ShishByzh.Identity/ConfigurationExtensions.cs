namespace ShishByzh.Identity;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddSecrets(this IConfigurationBuilder builder, string secretFilePath, string key)
    {
        if (!File.Exists(secretFilePath)) return builder;
        
        var secretValues = File.ReadAllText(secretFilePath).Trim();
        List<KeyValuePair<string, string?>> collection = [];
        collection.AddRange(
            from line in secretValues.Split('\n') 
            select line.Split('=') into parts 
            let fullKey = key + ":" + parts[0] 
            let value = parts[1] 
            select new KeyValuePair<string, string?>(fullKey, value));

        builder.AddInMemoryCollection(collection);

        return builder;
    }
}