#ifndef __PHASH_CPP_H
#define __PHASH_CPP_H

#define PHASH_BUILD_DLL

#ifdef WIN32
	#define PHASH_CPP
	#define CALL_CONV
#else
	#ifdef PHASH_BUILD_DLL
		#ifdef CPP_DLL
			#define PHASH_CPP __declspec(dllexport)
			#define CALL_CONV
		#else 
			#define PHASH_CPP extern "C" __declspec(dllexport)
			#define CALL_CONV __stdcall
		#endif
	#else
		#define PHASH_CPP __declspec(dllimport)
		#define CALL_CONV
	#endif
#endif

#include "..\pHash-0.9.4\pHash.h"

/*
 * Computes the perceptual hash of the image contents of a given file.
 *
 * Parameters:
 *		kszFile:	The name of the file containing an image to be
 *					hashed.
 *		ulHash:		The image's perceptual hash, returned by referennce.
 *
 * Return:
 *		0 if the call completed successfully, -1 otherwise.
 */
PHASH_CPP int CALL_CONV ImageHash(const char *kszFile, ulong64 &ulHash);

/*
 * Computes the Hamming distance between two perceptual hashes.
 *
 * Parameters:
 *		ulHashA:	The first hash being compared.
 *		ulHashB:	The second hash being compared.
 *
 * Return:
 *		The Hamming distance between the two hashes.
 */
PHASH_CPP int CALL_CONV HammingDistance(const ulong64 ulHashA, const ulong64 ulHashB);

/*
 * Convenience method that computes the perceptual hashes of each given
 * image file and returns the Hamming distance of those hashes.
 *
 * Parameters:
 *		kszFileA:	The first file being compared.
 *		kszFileB:	The second file being compared.
 *
 * Return:
 *		The Hamming distance between the perceptual hashes of the input
 *		files.
 */
PHASH_CPP int CALL_CONV CompareImageFiles(const char *kszFileA, const char *kszFileB);

#endif
