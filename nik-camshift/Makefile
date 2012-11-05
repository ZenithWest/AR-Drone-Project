CC=g++
CFLAGS= -ggdb -std=c++11
LFLAGS= -lopencv_highgui -lopencv_core -lopencv_video -lopencv_imgproc

all: camshift

camshift: camshift.o icamshift.o
	$(CC) $(CLFAGS) $(LFLAGS) -o camshift camshift.o icamshift.o

camshift.o: camshift.cpp
	$(CC) -c $(CFLAGS) $(LFLAGS) -o camshift.o camshift.cpp

icamshift.o: icamshift.cpp
	$(CC) -c $(CFLAGS) $(LFLAGS) -o icamshift.o icamshift.cpp
