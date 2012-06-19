using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PHashCS
{
    public class PHashCS
    {
        /// <summary>
        /// Computes the perceptual hash of the image contents of a given file.
        /// </summary>
        /// <param name="filename">THe name of the file containing an image to be
        /// be hashed.</param>
        /// <param name="hash">The image's perceptual hash, returned by reference.
        /// </param>
        /// <returns>0 if the call completed successfully, -1 otherwise.</returns>
        public static int ImageHash(string filename, out ulong hash)
        {
            IntPtr filenamePtr = Marshal.StringToHGlobalAnsi(filename);
            IntPtr hashPtr = Marshal.AllocHGlobal(sizeof(UInt64));
            int returnCode = PHashMarshaled.ImageHash(filenamePtr, hashPtr);
            hash = (ulong) Marshal.ReadInt64(hashPtr);
            return returnCode;
        }

        /// <summary>
        /// Computes the Hamming distance between two perceptual hashes.
        /// </summary>
        /// <param name="hashA">The first hash being compared.</param>
        /// <param name="hashB">The second hash being compared.</param>
        /// <returns>The Hamming distance between the two hashes.</returns>
        public static int HammingDistance(ulong hashA, ulong hashB)
        {
            return PHashMarshaled.HammingDistance(hashA, hashB);
        }

        /// <summary>
        /// Convenience method that computes the perceptual hashes of each given
        /// image file and returns the Hamming distance of those hashes.
        /// </summary>
        /// <param name="filenameA">The first file being compared.</param>
        /// <param name="filenameB">The second file being compared.</param>
        /// <returns>The Hamming distance between the perceptual hashes of the
        /// input files.</returns>
        public static int CompareImageFiles(string filenameA, string filenameB)
        {
            IntPtr filenameAPtr = Marshal.StringToHGlobalAnsi(filenameA);
            IntPtr filenameBPtr = Marshal.StringToHGlobalAnsi(filenameB);
            return PHashMarshaled.CompareImageFiles(filenameAPtr, filenameBPtr);
        }
    }
}
