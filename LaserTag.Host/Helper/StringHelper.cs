using LaserTag.Host.Frame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Helper
{
    public static class GameHelper
    {
        public static bool IsMacAddress(this string value)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(value, "^([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})$");
        }

        public static T DecodeGunReport<T>(byte[] buffer) where T : struct, IGunReport
        {
            int size = Marshal.SizeOf<T>();
            if (buffer.Length < size)
            {
                throw new ArgumentException($"Buffer is too small to contain a {typeof(T).Name}", nameof(buffer));
            }

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr ptr = handle.AddrOfPinnedObject();
                T result = Marshal.PtrToStructure<T>(ptr);

                return result;
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


    }
}
