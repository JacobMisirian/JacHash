#include <stdio.h>
#include <string.h>
#include "jachash.h"

#define MAX_LENGTH 16

static char transformByte(struct jachash_context *context, char bl);
static char shiftLeft(char b, char bits);
static int pad(char *source, int size);
static void init(struct jachash_context *context);

char *computeHashFromString(struct jachash_context *context, const char* string) {
	char bytes[strlen(string)];
	int i;
	for (i = 0; i < strlen(string); i++)
		bytes[i] = (char)string[i];
	computeHashFromBytes(context, bytes, strlen(string));
}

char *computeHashFromBytes(struct jachash_context *context, char *bytes, int length) {
	init(context);
	int i;
	char result[MAX_LENGTH];
	int sourceSize = pad(bytes, length);
	for (i = 0; i < sourceSize; i++)
		context->x += (char)bytes[i];
	for (i = 0; i < sourceSize; i++)
		result[i % MAX_LENGTH] = transformByte(context, bytes[i]);
	for (i = 0; i < MAX_LENGTH; i++)
		printf("%02x", result[i] & 0xFF);
	printf("\n");
}

static char transformByte(struct jachash_context *context, char bl) {
	context->a = shiftLeft(bl, context->x);
	context->b = (context->b ^ bl) - context->x;
	context->c = (context->a + context->b) & context->x;
	context->d ^= context->x - context->b;
	context->x ^= context->d;
	return ((context->a * context->c) + context->b - context->x * context->d ^ bl);
}

static char shiftLeft(char b, char bits) {
	return ((b << bits | b >> 32 - bits));
}

static int pad(char *source, int size) {
	if (size >= MAX_LENGTH)
		return size;
	int i;
	for (i = size; i < MAX_LENGTH; i++)
		source[i] = 0xFF;
	//source[i] = 0;
	return MAX_LENGTH;
}

static void init(struct jachash_context *context) {
	context->a = 0x6B87;
	context->b = 0x7F43;
	context->c = 0xA4Ad;
	context->d = 0xDC3F;
	context->x = 0;
}