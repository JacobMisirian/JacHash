//
// Created by reagan on 5/26/16.
//

#include <iostream>
#include <stdio.h>
#include "jachash.h"

#define MAX_LENGTH 16
#define FILLER_BYTE 0xF

jachash::jachash() {}

std::string jachash::computeHashFromBytes(uint8_t* bytesPt, int byteLength) {
    init();
    uint8_t bytes[byteLength < MAX_LENGTH ? MAX_LENGTH : byteLength];
    for (int i = 0; i < byteLength; i++)
        bytes[i] = bytesPt[i];
    int byteSize = pad(bytes, byteLength);

    for (int i = 0; i < byteSize; i++)
        x += bytes[i];
    uint8_t result[MAX_LENGTH];
    for (int i = 0; i < byteSize; i++)
        result[i % MAX_LENGTH] = transformByte(bytes[i]);
    for (int i = 0; i < MAX_LENGTH; i++)
        printf("%02x", result[i] & 0xFF);
    printf("\n");
}

std::string jachash::computeHashFromString(std::string text) {
    int textSize = text.length();
    uint8_t bytes[textSize];
    for (int i = 0; i < textSize; i++)
        bytes[i] = text[i];
    computeHashFromBytes(bytes, textSize);
}

uint8_t jachash::transformByte(uint8_t bl) {
    a = rotateLeft(bl, x);
    b = (b ^ bl) - x;
    c = (a + b) & x;
    d ^= x - b;
    x ^= d;

    return ((a * c) + b - x * d ^ bl);
}

uint8_t jachash::rotateLeft(uint8_t b, int bits) {
    return ((b << bits) | (b >> 32 - bits));
}

int jachash::pad(uint8_t *bytes, int textSize) {
    if (textSize >= MAX_LENGTH)
        return textSize;
    for (int i = textSize; i < MAX_LENGTH; i++)
        bytes[i] = FILLER_BYTE;
    return MAX_LENGTH;
}

void jachash::init() {
    a = 0x6B87 & 0xFF;
    b = 0x7F43 & 0xFF;
    c = 0xA4Ad & 0xFF;
    d = 0xDC3F & 0xFF;
    x = 0;
}