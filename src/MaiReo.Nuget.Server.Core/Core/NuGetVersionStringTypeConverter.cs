using NuGet.Versioning;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public class NuGetVersionStringTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType.In((x, y) => x == y,
                typeof(string),
                typeof(NuGetVersion),
                typeof(NuGetVersionString));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType.In((x, y) => x == y,
                typeof(string),
                typeof(NuGetVersion),
                typeof(NuGetVersionString),
                typeof(object));
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            var major = propertyValues.FirstOrDefault<int>("major");
            var minor = propertyValues.FirstOrDefault<int>("minor");
            var patch = propertyValues.FirstOrDefault<int>("patch");
            var revision = propertyValues.FirstOrDefault<int>("revision");
            var releaseLabel = propertyValues.FirstOrDefault<string>("releaselabel");
            var metadata = propertyValues.FirstOrDefault<string>("metadata");
            return new NuGetVersionString(major, minor, patch, revision, releaseLabel, metadata);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is NuGetVersionString vs)
            {
                return vs;
            }
            if (value is NuGetVersion nv)
            {
                return nv == null
                    ? default(NuGetVersionString)
                    : new NuGetVersionString(nv);
            }
            if (value is string s)
            {
                return (NuGetVersionString)s;
            }
            throw new InvalidCastException();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((NuGetVersionString)value).ToString();
            }
            if (destinationType == typeof(NuGetVersionString))
            {
                return (NuGetVersionString)value;
            }
            if (destinationType == typeof(NuGetVersion))
            {
                return (NuGetVersion)(NuGetVersionString)value;
            }
            if (destinationType == typeof(SemanticVersion))
            {
                return (SemanticVersion)(NuGetVersionString)value;
            }
            if (destinationType == typeof(object))
            {
                return value;
            }
            throw new InvalidCastException();
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            if (value is NuGetVersionString)
            {
                return true;
            }
            if (value is NuGetVersion)
            {
                return true;
            }
            if (value is string s)
            {
                try
                {
                    new NuGetVersionString(s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

    }
}
