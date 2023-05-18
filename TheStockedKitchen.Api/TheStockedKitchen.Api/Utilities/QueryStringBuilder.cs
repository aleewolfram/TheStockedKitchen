using System.ComponentModel;

namespace TheStockedKitchen.Api.Utilities
{
    public static class QueryStringBuilder
    {
        public static string BuildQueryString(object parameters)
        {
            var keyValuePairs = new List<string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(parameters))
            {
                var value = property.GetValue(parameters);
                if (value is IList<string> listValue)
                {
                    var encodedValues = listValue.Select(item => Uri.EscapeDataString(item));
                    var joinedValues = string.Join(",", encodedValues);
                    keyValuePairs.Add($"{property.Name}={joinedValues}");
                }
                else
                {
                    keyValuePairs.Add($"{property.Name}={Uri.EscapeDataString(value.ToString())}");
                }
            }

            return string.Join("&", keyValuePairs);
        }
    }
}
