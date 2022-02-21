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

        double expectedAngle = cos(180 * (_points.size() - 2) / _points.size());
        for (int i = 1; i < _points.size(); ++i)
        {
            Vector v1 = Vector(_points[i - 1]) - Vector(_points[i]);
            Vector v2 = Vector(_points[(i + 1) % _points.size()]) - Vector(_points[i]);
            double angle = DotProduct(v1, v2) / v1.Length() / v2.Length();
            if (angle != expectedAngle)
                return false;
        }

        return true;
    }
}