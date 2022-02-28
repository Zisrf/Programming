#include "geometry.h"
#include <cmath>

namespace Geometry
{
    Polygon::Polygon() : ClosedPolyline({Point(0, 0), Point(1, 1), Point(1, -1)}) {}

    Polygon::Polygon(const std::vector<Point> &points) : ClosedPolyline(points)
    {
        if (points.size() < 3)
            throw std::logic_error("Incorrect polygon (too few points)");
        for (std::size_t i = 1; i < points.size(); ++i)
            for (std::size_t j = i + 2; j < points.size(); ++j)
                if (isIntersect(points[i - 1], points[i], points[j - 1], points[j]))
                    throw std::logic_error("Incorrect polygon (self intersects)");
    }

    Polygon::Polygon(const Polygon &pg) : ClosedPolyline(pg._points) {}

    Polygon::~Polygon() {}

    double Polygon::Perimeter() const
    {
        return Length();
    }

    double Polygon::Area()
    {
        double res = 0;
        for (int i = 1; i < _points.size(); ++i)
            res += CrossProduct(Vector(_points[i - 1]), Vector(_points[i]));
        return abs(res) / 2;
    }

    Polygon &Polygon::operator=(const Polygon &pg)
    {
        _points = pg._points;
        return *this;
    }

    bool Polygon::isIntersect(const Point &p1, const Point &p2, const Point &p3, const Point &p4) const
    {
        double prod1 = CrossProduct(Vector(p2) - Vector(p1), Vector(p3) - Vector(p1));
        double prod2 = CrossProduct(Vector(p2) - Vector(p1), Vector(p4) - Vector(p1));
        if (prod1 != 0)
            prod1 /= abs(prod1);
        if (prod2 != 0)
            prod2 /= abs(prod2);
        if (prod1 == prod2 && prod1 != 0 && prod2 != 0)
            return false;

        prod1 = CrossProduct(Vector(p4) - Vector(p3), Vector(p1) - Vector(p3));
        prod2 = CrossProduct(Vector(p4) - Vector(p3), Vector(p2) - Vector(p3));
        if (prod1 != 0)
            prod1 /= abs(prod1);
        if (prod2 != 0)
            prod2 /= abs(prod2);
        if (prod1 == prod2 && prod1 != 0 && prod2 != 0)
            return false;

        return true;
    }
}