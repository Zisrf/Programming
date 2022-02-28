#include "Geometry.h"

namespace Geometry
{
    ClosedPolyline::ClosedPolyline() : Polyline() {}

    ClosedPolyline::ClosedPolyline(const std::vector<Point> &points) : Polyline(points) {}

    ClosedPolyline::ClosedPolyline(const ClosedPolyline &cpl) : Polyline(cpl._points) {}

    ClosedPolyline::~ClosedPolyline() {}

    double ClosedPolyline::Length() const
    {
        double res = Polyline::Length();
        if (_points.size() > 2)
            res += Dist(_points[0], _points.back());
        return res;
    }

    ClosedPolyline &ClosedPolyline::operator=(const ClosedPolyline &cpl)
    {
        _points = cpl._points;
        return *this;
    }
}