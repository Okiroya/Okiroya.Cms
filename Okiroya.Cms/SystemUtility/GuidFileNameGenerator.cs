using System;

namespace Okiroya.Cms.SystemUtility
{
    public sealed class GuidFileNameGenerator : IFileNameGenerator
    {
        public string GenerateName(string extension)
        {
            return $"{Guid.NewGuid()}{extension}";
        }
    }
}
