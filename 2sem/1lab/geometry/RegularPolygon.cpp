#include "geometry.h"
#include <cmath>

namespace Geometry
{
    RegularPolygon::RegularPolygon() : Polygon({Point(0, 0), Point(0.5, sqrt(3) / 2), Point(1, 0)}) {}

    RegularPolygon::RegularPolygon(const std::vector<Point> &points) : Polygon(points)
    {
        if (!isRegular())
        {
            std::cerr << "Incorrect regular polygon" << std::endl;
            exit(5);
        }
    }
    RegularPolygon::RegularPolygon(const RegularPolygon &rpg) : Polygon(rpg._points) {}

    RegularPolygon::~RegularPolygon() {}

    RegularPolygon &RegularPolygon::operator=(const RegularPolygon &rpg)
    {
        _points = rpg._points;
        return *this;
    }

    bool RegularPolygon::isRegular() const
    {
        for (std::size_t i = 2; i < _points.size(); ++i)
            if (Dist(_points[i - 2], _points[i - 1]) != Dist(_points[i - 1], _points[i]))
                return false;
        return true;
    }
}