#ifndef jachash_context_h
#define jachash_context_h

#include <stdint.h>

struct jachash_context {
	uint8_t a;
	uint8_t b;
	uint8_t c;
	uint8_t d;
	uint8_t x;
};

#endif