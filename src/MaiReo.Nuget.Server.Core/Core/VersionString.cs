using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MaiReo.Nuget.Server.Core
{
    [TypeConverter(typeof(VersionStringTypeConverter))]
    public class VersionString : SemVer.Version
    {
        public VersionString(string input,
            bool loose = false) : base(input, loose)
        {
        }

        public VersionString(int major, int minor, int patch,
            string preRelease = null, string build = null)
            : base(major, minor, patch, preRelease, build)
        {
        }

        static VersionString()
            => Default = new VersionString(0, 0, 0);

        public static implicit operator string(VersionString version)
            => version?.ToString();

        public static implicit operator VersionString(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return Default;
            try
            {
                return new VersionString(version);
            }
            catch (ArgumentException)
            {
            }
            return Default;
        }

        public static bool operator ==(VersionString left, object right)
            => VersionString.Equals(left, right);
        public static bool operator ==(VersionString left, string right)
            => VersionString.Equals(left, right);
        public static bool operator ==(VersionString left, SemVer.Version right)
            => VersionString.Equals(left, right);
        public static bool operator ==(object left, VersionString right)
           => VersionString.Equals(right, left);
        public static bool operator ==(string left, VersionString right)
            => VersionString.Equals(right, left);
        public static bool operator ==(SemVer.Version left, VersionString right)
            => VersionString.Equals(right, left);
        public static bool operator !=(VersionString left, object right)
           => !VersionString.Equals(left, right);
        public static bool operator !=(VersionString left, string right)
            => !VersionString.Equals(left, right);
        public static bool operator !=(VersionString left, SemVer.Version right)
            => !VersionString.Equals(left, right);
        public static bool operator !=(object left, VersionString right)
            => !VersionString.Equals(right, left);
        public static bool operator !=(string left, VersionString right)
            => !VersionString.Equals(right, left);
        public static bool operator !=(SemVer.Version left, VersionString right)
            => !VersionString.Equals(right, left);

        public static bool operator >(VersionString left, string right)
            => (SemVer.Version)left > (VersionString)right;
        public static bool operator >(string left, VersionString right)
            => (VersionString)left > (SemVer.Version)right;
        public static bool operator >=(string left, VersionString right)
            => (VersionString)left >= (SemVer.Version)right;
        public static bool operator >=(VersionString left, string right)
            => (SemVer.Version)left >= (VersionString)right;
        public static bool operator <(VersionString left, string right)
            => (SemVer.Version)left < (VersionString)right;
        public static bool operator <=(VersionString left, string right)
            => (SemVer.Version)left <= (VersionString)right;
        public static bool operator <(string left, VersionString right)
            => (VersionString)left < (SemVer.Version)right;
        public static bool operator <=(string left, VersionString right)
            => (VersionString)left <= (SemVer.Version)right;


        public override bool Equals(object other)
            => Equals(this, other);
        public bool Equals(string other)
            => base.Equals((VersionString)other);

        public static bool Equals(VersionString left, object right)
        {
            if (right is VersionString sv)
            {
                return left.Equals(sv);
            }
            if (right is SemVer.Version v)
            {
                return left.Equals(v);
            }
            if (right is string s)
            {
                return left.Equals((VersionString)s);
            }
            return ReferenceEquals(left, right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static readonly VersionString Default;
    }
}
