using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    public class VersionStringTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType.In((x, y) => x == y,
                typeof(string),
                typeof(SemVer.Version),
                typeof(VersionString));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType.In((x, y) => x == y,
                typeof(string),
                typeof(SemVer.Version),
                typeof(VersionString),
                typeof(object));
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            var major = propertyValues.FirstOrDefault<int>("major");
            var minor = propertyValues.FirstOrDefault<int>("minor");
            var patch = propertyValues.FirstOrDefault<int>("patch");
            var preRelease = propertyValues.FirstOrDefault<string>("prerelease");
            var build = propertyValues.FirstOrDefault<string>("build");

            return new VersionString(major, minor, patch, preRelease, build);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is VersionString vs)
            {
                return vs;
            }
            if (value is SemVer.Version sv)
            {
                return sv == null
                    ? default(VersionString)
                    : new VersionString(sv.ToString());
            }
            if (value is string s)
            {
                return (VersionString)s;
            }
            throw new InvalidCastException();
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((VersionString)value).ToString();
            }
            if (destinationType == typeof(VersionString))
            {
                return (VersionString)value;
            }
            if (destinationType == typeof(SemVer.Version))
            {
                return (SemVer.Version)(VersionString)value;
            }
            if (destinationType == typeof(object))
            {
                return value;
            }
            throw new InvalidCastException();
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            if (value is VersionString)
            {
                return true;
            }
            if (value is SemVer.Version)
            {
                return true;
            }
            if (value is string s)
            {
                try
                {
                    new SemVer.Version(s);
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
