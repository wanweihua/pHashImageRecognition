using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PHashCS
{
    public class PHashMarshaled
    {
        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int ImageHash(IntPtr filename, IntPtr hash);
        
        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int HammingDistance(UInt64 hashA, UInt64 hashB);

        [DllImport(@"pHashCpp.dll")]
        unsafe public static extern int CompareImageFiles(IntPtr fileA, IntPtr fileB);
    }
}
