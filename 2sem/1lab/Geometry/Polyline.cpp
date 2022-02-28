#include "Geometry.h"

namespace Geometry
{
    Polyline::Polyline() : _points({Point(0, 0), Point(1, 1)}) {}

    Polyline::Polyline(const std::vector<Point> &points) : _points(points)
    {
        if (points.size() < 2)
            throw std::logic_error("Incorrect polyline (too few points)");
    }

    Polyline::Polyline(const Polyline &pl) : _points(pl._points) {}

    Polyline::~Polyline() {}

    std::size_t Polyline::PointsCount() const
    {
        return _points.size();
    }

    double Polyline::Length() const
    {
        double res = 0;
        for (std::size_t i = 1; i < _points.size(); ++i)
            res += Dist(_points[i - 1], _points[i]);
        return res;
    }

    Polyline &Polyline::operator=(const Polyline &pl)
    {
        _points = pl._points;
        return *this;
    }

    const Point &Polyline::operator[](std::size_t i) const
    {
        return _points[i];
    }
}