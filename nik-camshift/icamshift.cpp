#include "icamshift.h"

using namespace cv;

ICamshift::ICamshift(void)
{
    channels = new int[2] { 0, 1};
    nchannels = 2;
    bins = new int[2] { 30, 32};

    float* hranges = new float[2] { 0, 180 };
    float* sranges = new float[2] { 0, 255 };
    ranges = new const float*[2] { hranges, sranges};

    nextSearch = Rect(200, 300, 20, 30);
}

ICamshift::ICamshift(const int* channels, int nchannels, const int* bins, int nbins, const float** ranges)
    :channels(channels), nchannels(nchannels), bins(bins), nbins(nbins), ranges(ranges)
{
}

void ICamshift::reference(Mat& frame, int x, int y, int width, int height)
{
    cropped = frame(Rect(x, y, width, height));
    GaussianBlur(cropped, cropped, Size( 13, 13), 0 );
    cvtColor(cropped, cropped, CV_RGB2HSV);
    imshow("Reference", cropped);
    calcHist();
}

cv::Rect ICamshift::camshift(Mat& frame)
{
    Mat backproj;
    Mat hsvframe;
    cvtColor(frame, hsvframe, CV_RGB2HSV);
    GaussianBlur(hsvframe, hsvframe, Size( 13, 13), 0 );
    // imshow("blur", hsvframe);
    calcBackProject(&frame, 1,
            channels,
            hist,
            backproj,
            ranges);

    Rect search = nextSearch;
    RotatedRect results = CamShift(backproj, search,
            TermCriteria( CV_TERMCRIT_EPS | CV_TERMCRIT_ITER, 25, .25 ));
    nextSearch = results.boundingRect();
    return nextSearch;
}

void ICamshift::calcHist(void)
{
    cv::calcHist(&cropped, 1,   /* Input */
            channels,       /* Which values to use */
            Mat(),          /* No mask */
            hist,           /* Output */
            nchannels,      /* Dims */
            bins,           /* Size of bins for each dim */
            ranges,         /* Range of bounderies for each dim */
            true,           /* Uniformity */
            false );        /* Do not accumulate */
}

Mat ICamshift::histogram(void) const
{
    return hist.clone();
}
