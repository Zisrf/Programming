#include "geometry.h"

namespace Geometry
{
    Trapezoid::Trapezoid() : Polygon({Point(0, 0), Point(1, 1), Point(2, 1), Point(3, 0)}) {}

    Trapezoid::Trapezoid(const Point &p1, const Point &p2, const Point &p3, const Point &p4) : Polygon({p1, p2, p3, p4})
    {
        if (!isTrapezoid(p1, p2, p3, p4))
        {
            std::cerr << "Incorrect trapezoid" << std::endl;
            exit(4);
        }
    }

    Trapezoid::Trapezoid(const Trapezoid &tr) : Polygon(tr._points) {}

    Trapezoid::~Trapezoid() {}

    Trapezoid &Trapezoid::operator=(const Trapezoid &tr)
    {
        _points = tr._points;
        return *this;
    }

    bool Trapezoid::isTrapezoid(const Point &p1, const Point &p2, const Point &p3, const Point &p4) const
    {
        if (CrossProduct(Vector(p1) - Vector(p2), Vector(p3) - Vector(p4)) == 0 &&
            CrossProduct(Vector(p2) - Vector(p3), Vector(p4) - Vector(p1)) != 0)
            return true;
        if (CrossProduct(Vector(p2) - Vector(p3), Vector(p4) - Vector(p1)) == 0 &&
            CrossProduct(Vector(p1) - Vector(p2), Vector(p3) - Vector(p4)) != 0)
            return true;
        return false;
    }
}