using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace PHashCS
{
    public delegate void EndImageHashHandler(object sender, EventArgs e);
    public delegate void EndHammingDistanceHandler(object sender, EventArgs e);
    public delegate void EndCompareImageFilesHandler(object sender, EventArgs e);

    public class PHashCS
    {
        #region Asynchronous Events
        /// <summary>
        /// Handler for completion of BeginImageHash().
        /// </summary>
        public static event EndImageHashHandler EndImageHash;

        /// <summary>
        /// Handler for completion of HammingDistance().
        /// </summary>
        public static event EndHammingDistanceHandler EndHammingDistance;

        /// <summary>
        /// Handler for completion of CompareImageFiles().
        /// </summary>
        public static event EndCompareImageFilesHandler EndCompareImageFiles;

        /// <summary>
        /// Called when the hash of an image is computed.
        /// </summary>
        /// <param name="e">Event args. See HashEventArgs for more
        /// info.</param>
        protected static void OnHashComputed(EventArgs e)
        {
            if (EndImageHash != null)
                EndImageHash(null, e);
        }

        /// <summary>
        /// Called when the Hamming distance between two hashes is
        /// computed.
        /// </summary>
        /// <param name="e">Event args. See DistanceEventArgs for more
        /// info.</param>
        protected static void OnDistanceComputed(EventArgs e)
        {
            if (EndHammingDistance != null)
                EndHammingDistance(null, e);
        }

        /// <summary>
        /// Called when the comparison of two image files completes.
        /// </summary>
        /// <param name="e">Event args. See CompareEventArgs for more
        /// info.</param>
        protected static void OnImagesCompared(EventArgs e)
        {
            if (EndCompareImageFiles != null)
                EndCompareImageFiles(null, e);
        }
        #endregion

        #region Synchronous Calls
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
            int returnCode = PHashMarshalled.ImageHash(filenamePtr, hashPtr);
            hash = (ulong)Marshal.ReadInt64(hashPtr);
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
            return PHashMarshalled.HammingDistance(hashA, hashB);
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
            return PHashMarshalled.CompareImageFiles(filenameAPtr, filenameBPtr);
        }
        #endregion

        #region Asynchronous Calls
        /// <summary>
        /// Begins an asynchronous image hashing routine.
        /// </summary>
        /// <param name="filename">The input image file.</param>
        /// <param name="hash">The output perceptual hash.</param>
        public static void BeginImageHash(string filename, out ulong hash)
        {
            int ret = ImageHash(filename, out hash);
            OnHashComputed(new HashEventArgs(ret, hash));
        }

        /// <summary>
        /// Begins an asynchronous Hamming distance routine.
        /// </summary>
        /// <param name="hashA">The first hash to compare.</param>
        /// <param name="hashB">The second hash to compare.</param>
        public static void BeginHammingDistance(ulong hashA, ulong hashB)
        {
            int ret = HammingDistance(hashA, hashB);
            OnDistanceComputed(new DistanceEventArgs(ret));
        }

        /// <summary>
        /// Begins an asynchronous image file comparison routine.
        /// </summary>
        /// <param name="filenameA">The first file to compare.</param>
        /// <param name="filenameB">The second file to compare.</param>
        public static void BeginCompareImageFiles(string filenameA, string filenameB)
        {
            int ret = CompareImageFiles(filenameA, filenameB);
            OnImagesCompared(new CompareEventArgs(ret));
        }
        #endregion
    }

    public class EventListenerSample
    {
        public EventListenerSample()
        {
            PHashCS.EndImageHash += new EndImageHashHandler(EndHash);
            PHashCS.EndHammingDistance += new EndHammingDistanceHandler(EndDistance);
            PHashCS.EndCompareImageFiles += new EndCompareImageFilesHandler(EndCompare);
        }

        #region Private End Methods
        private void EndHash(object sender, EventArgs e)
        {
            HashEventArgs args = (HashEventArgs)e;
            Console.WriteLine(String.Format("Return code: {0}", args.retCode));
            Console.WriteLine(String.Format("Hash value: {0}", args.hash));
        }

        private void EndDistance(object sender, EventArgs e)
        {
            DistanceEventArgs args = (DistanceEventArgs)e;
            Console.WriteLine(String.Format("Hamming distance: {0}", args.distance));
        }

        private void EndCompare(object sender, EventArgs e)
        {
            CompareEventArgs args = (CompareEventArgs)e;
            Console.WriteLine(String.Format("Hamming distance: {0}", args.distance));
        }
        #endregion
    }
}
