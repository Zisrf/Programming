#include "Geometry.h"

namespace Geometry
{
    Vector::Vector() : Point() {}

    Vector::Vector(double x, double y) : Point(x, y) {}

    Vector::Vector(const Point &p) : Point(p) {}

    Vector::Vector(const Point &p1, const Point &p2) : Point(p2.GetX() - p1.GetX(), p2.GetY() - p1.GetY()) {}

    Vector::Vector(const Vector &v) : Point(v._x, v._y) {}

    Vector::~Vector() {}

    double Vector::Length() const
    {
        return Dist(Point(0, 0), Point(_x, _y));
    }

    double DotProduct(const Vector &v1, const Vector &v2)
    {
        return v1._x * v2._x + v1._y * v2._y;
    }

    double CrossProduct(const Vector &v1, const Vector &v2)
    {
        return v1._x * v2._y - v1._y * v2._x;
    }

    Vector &Vector::operator=(const Vector &v)
    {
        _x = v._x;
        _y = v._y;
        return *this;
    }

    Vector Vector::operator+(const Vector &v) const
    {
        return Vector(_x + v._x, _y + v._y);
    }

    Vector Vector::operator-(const Vector &v) const
    {
        return Vector(_x - v._x, _y - v._y);
    }

    std::istream &operator>>(std::istream &in, Vector &v)
    {
        in >> v._x >> v._y;
        return in;
    }

    std::ostream &operator<<(std::ostream &out, const Vector &v)
    {
        out << '{' << v._x << ';' << v._y << '}';
        return out;
    }
}