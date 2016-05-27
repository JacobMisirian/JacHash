#ifndef jachash_h
#define jachash_h

#include "jachash_context.h"

char *computeHashFromString(struct jachash_context *context, char *dest, const char* string);
char *computeHashFromBytes(struct jachash_context *context, char *dest, char *bytes, int sourceSize);
char *computeHashFromFile(struct jachash_context *context, char *dest, FILE *fp);

#endif