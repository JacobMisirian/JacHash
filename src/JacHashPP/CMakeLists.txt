cmake_minimum_required(VERSION 3.4)
project(JacHashPP)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES main.cpp jachash.cpp jachash.h)
add_executable(JacHashPP ${SOURCE_FILES})