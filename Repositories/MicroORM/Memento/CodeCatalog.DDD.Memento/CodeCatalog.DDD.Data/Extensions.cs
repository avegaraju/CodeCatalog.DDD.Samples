using System;

namespace CodeCatalog.DDD.Data
{
    public static class Extensions
    {
        public static bool ToBoolean(this char yesOrNo)
        {
            return yesOrNo == 'Y';
        }

        public static Guid ToGuid(this string guidAsString)
        {
            return new Guid(guidAsString);
        }
    }
}