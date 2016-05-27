//
// Created by reagan on 5/26/16.
//

#ifndef JACHASHPP_JACHASH_H
#define JACHASHPP_JACHASH_H

#include <string>
#include <stdint.h>

class jachash {
public:
    jachash();
    void computeHashFromBytes(char* dest, uint8_t* bytesPt, int byteLength);
    void computeHashFromFile(char* dest, FILE *fp);
    void computeHashFromString(char* dest, std::string text);
private:
    int pad(uint8_t *bytes, int textSize);
    uint8_t transformByte(uint8_t bl);
    uint8_t rotateLeft(uint8_t b, int bits);
    void init();
    uint8_t a;
    uint8_t b;
    uint8_t c;
    uint8_t d;
    uint8_t x;
};


#endif //JACHASHPP_JACHASH_H
