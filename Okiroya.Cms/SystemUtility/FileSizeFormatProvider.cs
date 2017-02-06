using Okiroya.Campione.SystemUtility;
using System;

namespace Okiroya.Cms.SystemUtility
{
    public class FileSizeFormatProvider : IFormatProvider, ICustomFormatter
    {
        public const string FileSizeFormat = "fs";

        private const int _oneKiloByte = 1024;
        private const int _oneMegaByte = _oneKiloByte * 1024;
        private const int _oneGigaByte = _oneMegaByte * 1024;

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            Guard.ArgumentNotNull(arg);

            string result = string.Empty;

            if (format == null)
            {
                result = DefaultFormat(format, arg, formatProvider);
            }
            else if (!format.StartsWith(FileSizeFormat))
            {
                result = DefaultFormat(format, arg, formatProvider);
            }
            else if (arg is string)
            {
                result = DefaultFormat(format, arg, formatProvider);
            }
            else
            {
                float size = 0;

                if (!float.TryParse(arg.ToString(), out size))
                {
                    result = DefaultFormat(format, arg, formatProvider);
                }
                else
                {
                    string suffix;

                    if (size > _oneGigaByte)
                    {
                        size /= _oneGigaByte;
                        suffix = "Gb";
                    }
                    else if (size > _oneMegaByte)
                    {
                        size /= _oneMegaByte;
                        suffix = "Mb";
                    }
                    else if (size > _oneKiloByte)
                    {
                        size /= _oneKiloByte;
                        suffix = "Kb";
                    }
                    else
                    {
                        suffix = "b";
                    }

                    string precision = format.Substring(2);

                    if (string.IsNullOrEmpty(precision))
                    {
                        precision = "2";
                    }

                    result = $"{size:N}{precision}{suffix}";
                }
            }

            return result;
        }

        public object GetFormat(Type formatType)
        {
            Guard.ArgumentNotNull(formatType);

            return (formatType is ICustomFormatter) ?
                this :
                null;
        }

        private static string DefaultFormat(string format, object arg, IFormatProvider formatProvider)
        {
            var formattableArg = arg as IFormattable;

            return formattableArg != null ?
                formattableArg.ToString(format, formatProvider) :
                arg.ToString();
        }
    }
}
