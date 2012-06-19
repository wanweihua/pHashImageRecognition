
#include "pHashCpp.h"

int CALL_CONV ImageHash(const char *kszFile, ulong64 &ulHash)
{
	return ph_dct_imagehash(kszFile, ulHash);
}

int CALL_CONV HammingDistance(const ulong64 ulHashA, const ulong64 ulHashB)
{
	return ph_hamming_distance(ulHashA, ulHashB);
}

int CALL_CONV CompareImageFiles(const char *kszFileA, const char *kszFileB)
{
	ulong64 ulHashA, ulHashB;

	/* Error handle (ImageHash returns -1 on failure) */
	if (ImageHash(kszFileA, ulHashA) == -1)
		return -1;
	if (ImageHash(kszFileB, ulHashB) == -1)
		return -1;

	return HammingDistance(ulHashA, ulHashB);
}
