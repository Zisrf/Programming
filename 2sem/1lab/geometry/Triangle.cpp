#include "Geometry.h"

namespace Geometry
{
    Triangle::Triangle() : Polygon() {}

    Triangle::Triangle(const Point &p1, const Point &p2, const Point &p3)
        : Polygon({p1, p2, p3}) {}

    Triangle::Triangle(const Triangle &t) : Polygon(t._points) {}

    Triangle::~Triangle() {}

    Triangle &Triangle::operator=(const Triangle &t)
    {
        _points = t._points;
        return *this;
    }
}