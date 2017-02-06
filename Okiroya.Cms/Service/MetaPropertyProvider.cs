using Okiroya.Campione.Service.Logging;
using Okiroya.Campione.SystemUtility;
using Okiroya.Cms.Domain;
using Okiroya.Cms.Domain.Meta;
using Okiroya.Cms.Domain.Property;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;

namespace Okiroya.Cms.Service
{
    public static class MetaPropertyProvider
    {
        private static ConcurrentDictionary<string, Func<string, string, object>> _typeConverter = new ConcurrentDictionary<string, Func<string, string, object>>();

        static MetaPropertyProvider()
        {
            Init();
        }

        public static void AddMetaTypeConverter(string typeName, Func<string, string, object> converter)
        {
            Guard.ArgumentNotEmpty(typeName);
            Guard.ArgumentNotNull(converter);

            _typeConverter.AddOrUpdate(typeName, converter, (p, q) => { return converter; });
        }

        public static dynamic ConvertDataToExpando(this CmsViewModule entity)
        {
            Guard.ArgumentNotNull(entity);

            var result = new ExpandoObject();

            if (entity.RawData != null)
            {
                var properties = result as IDictionary<string, object>;
                for (int i = 0; i < entity.RawData.Length; i++)
                {
                    if (_typeConverter.ContainsKey(entity.RawData[i].DataType))
                    {
                        properties.Add(entity.RawData[i].DataName, _typeConverter[entity.RawData[i].DataType](entity.RawData[i].DataName, entity.RawData[i].DataValue));
                    }
                }
            }

            return result;
        }

        public static IDictionary<string, object> ConvertPropertiesToDictionary(this CmsPropertyEntityItem[] items)
        {
            Guard.ArgumentNotNull(items);

            var result = new Dictionary<string, object>();

            for (int i = 0; i < items.Length; i++)
            {
                if (_typeConverter.ContainsKey(items[i].PropertyType))
                {
                    result.Add(items[i].PropertyName, _typeConverter[items[i].PropertyType](items[i].PropertyName, items[i].PropertyData));
                }
            }

            return result;
        }

        public static IDictionary<string, object> ConvertDataToRecord(this CmsMetaEntityItem[] items)
        {
            Guard.ArgumentNotNull(items);

            var result = new Dictionary<string, object>();

            IDictionary<string, object> tmp = null;
            bool arrayKeyFlag = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (_typeConverter.ContainsKey(items[i].FieldType))
                {
                    result.Add(items[i].FieldName, _typeConverter[items[i].FieldType](items[i].FieldName, items[i].Data));
                }
                else
                {
                    if (ConvertComplex(items[i].FieldName, items[i].FieldType, items[i].Data, out tmp))
                    {
                        if (!arrayKeyFlag)
                        {
                            result.Add($"{items[i].FieldName}[]", items.Length);
                            arrayKeyFlag = !arrayKeyFlag;
                        }
                        foreach (var item in tmp)
                        {
                            result.Add($"{items[i].FieldName}[{i}].{item.Key}", item.Value);
                        }
                    }
                }
            }

            return result;
        }

        private static bool ConvertComplex(string name, string type, string data, out IDictionary<string, object> container)
        {
            bool result = false;
            container = new Dictionary<string, object>();

            if (type.StartsWith("{"))
            {
                if (data.StartsWith("{"))
                {
                    var typeParts = type.TrimStart('{').TrimEnd('}').Split(new[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                    var dataParts = data.TrimStart('{').TrimEnd('}').Split(new[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                    if (typeParts.Length == dataParts.Length)
                    {
                        string itemType;
                        int typeSeparatorIndex;
                        for (int i = 0; i < typeParts.Length; i++)
                        {
                            typeSeparatorIndex = typeParts[i].IndexOf(':');
                            if (typeSeparatorIndex > 0)
                            {
                                itemType = typeParts[i].Substring(typeSeparatorIndex + 1, typeParts[i].Length - typeSeparatorIndex - 1);
                                container[typeParts[i].Substring(0, typeSeparatorIndex)] = _typeConverter.ContainsKey(itemType) ?
                                    _typeConverter[itemType](typeParts[i], dataParts[i]) :
                                    null;
                            }
                            else
                            {
                                //TODO: log
                            }
                        }

                        result = true;
                    }
                    else
                    {
                        //TODO: log
                    }
                }
                else
                {
                    //TODO: log
                }
            }

            return result;
        }

        private static void Init()
        {
            _typeConverter.TryAdd("int",
                (name, val) =>
                {
                    int result = 0;
                    if (!int.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом int"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("long",
                (name, val) =>
                {
                    long result = 0L;
                    if (!long.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом long"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("float",
                (name, val) =>
                {
                    float result = 0f;
                    if (!float.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом float"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("double",
                (name, val) =>
                {
                    double result = 0d;
                    if (!double.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом double"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("decimal",
                (name, val) =>
                {
                    decimal result = 0m;
                    if (!decimal.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом decimal"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("bool",
                (name, val) =>
                {
                    bool result = false;
                    if (!bool.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом bool"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("date",
                (name, val) =>
                {
                    DateTime result = DateTime.MinValue;
                    if (!DateTime.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом DateTime"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("time",
                (name, val) =>
                {
                    TimeSpan result = TimeSpan.Zero;
                    if (!TimeSpan.TryParse(val, out result))
                    {
                        LoggingFacade.GetInstance().Log(new ErrorLogEntry($"Значение {val} переменной {name} не является типом TimeSpan"));
                    }

                    return result;
                });

            _typeConverter.TryAdd("string",
                (name, val) =>
                {
                    return val;
                });
        }
    }
}
