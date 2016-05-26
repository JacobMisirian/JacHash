#include <stdio.h>
#include <string.h>
#include "jachash.h"
#include "jachash_context.h"

int main (int argc, char *argv[]) {
	struct jachash_context context;
	computeHash(&context, "h");
	return 0;
}

