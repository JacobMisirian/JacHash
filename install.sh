#!/bin/bash

xbuild src/JacHash.sln
cp src/JacHash/bin/Debug/JacHash.exe /usr/bin/JacHash.exe
cp JacHash.sh /usr/bin/JacHash
chmod +x /usr/bin/JacHash
