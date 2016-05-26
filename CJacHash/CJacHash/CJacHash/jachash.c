#include <stdio.h>
#include <string.h>
#include "jachash.h"

#define MAX_LENGTH 5

static void pad(char *source, int size);
static char transformByte(struct jachash_context *context, char bl);
static char shiftLeft(char b, char bits);

char *computeHash(struct jachash_context *context, const char* string) {
	context->a = 0x6B87;
	context->b = 0x7F43;
	context->c = 0xA4Ad;
	context->d = 0xDC3F;
	context->x = 0;

	int sourceSize;
	if (strlen(string) >= MAX_LENGTH)
		sourceSize = strlen(string);
	else
		sourceSize = MAX_LENGTH;
	char source[sourceSize];
	int i;
	for (i = 0; i < strlen(string); i++) {
		source[i] = string[i];
	}

	pad(source, strlen(string));

	char result[MAX_LENGTH];

	for (i = 0; i < sourceSize; i++)
		context->x += (char)source[i];
	for (i = 0; i < sourceSize; i++)
		result[i % MAX_LENGTH] = transformByte(context, source[i]);
	for (i = 0; i < MAX_LENGTH; i++)
		printf("%02X", result[i]);
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

static void pad(char *source, int size) {
	if (size >= MAX_LENGTH)
		return;
	int i;
	for (i = size; i < MAX_LENGTH; i++)
		source[i] = 0xFF;
}