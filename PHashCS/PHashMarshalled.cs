using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PHashCS
{
    class PHashMarshalled
    {
        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int ImageHash(IntPtr filename, IntPtr hash);

        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int HammingDistance(ulong hashA, ulong hashB);

        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int CompareImageFiles(IntPtr fileA, IntPtr fileB);
    }
}
