using Newtonsoft.Json;
using System;

namespace codestackdemo.Extensions
{
    public static class Helpers
    {
        public static string ToJSONString(this object item) => JsonConvert.SerializeObject(item);
        public static bool IsNull(this object item) => item == null;
        public static int ToInt(this object item) => Convert.ToInt32(item);
        public static double ToDouble(this object item) => Convert.ToDouble(item);
        public static bool ToBool(this object item) => Convert.ToBoolean(item);
    }
}
