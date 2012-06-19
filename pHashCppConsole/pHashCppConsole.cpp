// pHashCppConsole.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "pHashCpp.h"

int _tmain(int argc, _TCHAR* argv[])
{
	const char *diff_file1 = "diff1.jpg";
	const char *same_file1 = "same1.jpg";
	const char *same_file2 = "same2.jpg";

	int result_diffsame = CompareImageFiles(diff_file1, same_file1);
	int result_samesame = CompareImageFiles(same_file1, same_file2);

	printf("Images \'%s\' and \'%s\' have Hamming distance %i\n", diff_file1, same_file1, result_diffsame);
	printf("Images \'%s\' and \'%s\' have Hamming distance %i\n", same_file1, same_file2, result_samesame);

	return 0;
}
