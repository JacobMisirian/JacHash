#include <stdio.h>
#include <string.h>
#include "jachash.h"
#include "jachash_context.h"

int main (int argc, char *argv[]) {
	struct jachash_context context;
	if (argc < 2) {
		printf("Not enough arguments!");
		return 0;
	}
	char hash[25];
	if (strcmp(argv[1], "-s") == 0)
		computeHashFromString(&context, hash, argv[2]);
	else
		computeHashFromFile(&context, hash, fopen(argv[2], "r"));
	printf("%s\n", hash);
	return 0;
}

