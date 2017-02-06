using System;

namespace Okiroya.Cms.SystemUtility
{
    public interface IFileNameGenerator
    {
        string GenerateName(string extension);
    }
}
