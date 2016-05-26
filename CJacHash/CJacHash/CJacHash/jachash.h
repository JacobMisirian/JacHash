#ifndef jachash_h
#define jachash_h

#include "jachash_context.h"

char *computeHashFromString(struct jachash_context *context, const char* string);
char *computeHashFromBytes(struct jachash_context *context, char *bytes, int sourceSize);

#endif