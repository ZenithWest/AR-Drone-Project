#ifndef _ICAMSHIFT_H_
#define _ICAMSHIFT_H_

#include <opencv2/opencv.hpp>

class ICamshift {
    cv::Mat cropped;
    cv::MatND hist;
    cv::Rect nextSearch;

    const int* channels;    // channels to use
    int nchannels;
    const int* bins;      /* 0-th channels to 30 bins, 1-st to 32 */
    int nbins;
    const float** ranges;


    public:
        ICamshift(void);
        ICamshift(const int* channels, int nchannels, const int* bins, int nbins, const float** ranges);
        void reference(cv::Mat& frame, int x, int y, int width, int height);
        cv::Rect camshift(cv::Mat& frame);
        cv::Mat histogram(void) const;


    private:
        void calcHist(void);
};

#endif
