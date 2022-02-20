#include "geometry.h"
#include <cmath>

namespace Geometry
{
    Point::Point() : _x(0), _y(0) {}

    Point::Point(double x, double y) : _x(x), _y(y) {}

    Point::Point(const Point &p) : _x(p._x), _y(p._y) {}

    Point::~Point() {}

    double Point::GetX() const
    {
        return _x;
    }

    double Point::GetY() const
    {
        return _y;
    }

    double Point::SetX(double x)
    {
        return _x = x;
    }

    double Point::SetY(double y)
    {
        return _y = y;
    }

    double Dist(const Point &p1, const Point &p2)
    {
        return sqrt((p1._x - p2._x) * (p1._x - p2._x) + (p1._y - p2._y) * (p1._y - p2._y));
    }

    Point &Point::operator=(const Point &p)
    {
        _x = p._x;
        _y = p._y;
        return *this;
    }

    std::istream &operator>>(std::istream &in, Point &p)
    {
        in >> p._x >> p._y;
        return in;
    }

    std::ostream &operator<<(std::ostream &out, const Point &p)
    {
        out << '(' << p._x << ';' << p._y << ')';
        return out;
    }
}