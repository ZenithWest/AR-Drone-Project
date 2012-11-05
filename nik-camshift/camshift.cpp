#include <opencv2/opencv.hpp>
#include <iostream>
#include "icamshift.h"

using namespace cv;

const int mask_w = 50;
const int mask_h = 50;


int main(int argc, char **argv) 
{
    // Init windows
    namedWindow("Raw");
    namedWindow("Track");
    namedWindow("blur");
    namedWindow("Reference");
    cvMoveWindow("Raw", 20, 20);
    cvMoveWindow("Track", 680, 20);

    // Init camera
    VideoCapture video0(0);
    double vidW = video0.get(CV_CAP_PROP_FRAME_WIDTH);
    double vidH = video0.get(CV_CAP_PROP_FRAME_HEIGHT);

    // Init tracker
    ICamshift tracker;
    Mat frame;

    while (true) {
        video0 >> frame;
        rectangle(frame, Rect( (vidW/2) - (mask_w/2), (vidH/2) - (mask_h/2), mask_w, mask_h), Scalar(0xFF, 0xFF, 0xFF));
        imshow("Raw", frame);
        if ( waitKey(30) >= 0 ) break;
    }

    tracker.reference(frame, (vidW/2) - (mask_w/2), (vidH/2) - (mask_h/2), mask_w, mask_h);

    while (true) {
        video0 >> frame;
        imshow("Raw", frame);
        rectangle(frame, tracker.camshift(frame), Scalar(0x00, 0x00, 0xFF));
        imshow("Track",  frame);

        GaussianBlur(frame, frame, Size( 13, 13), 0 );
        
        if ( waitKey(30) >= 0 ) break;
    }

    return 0;
}
