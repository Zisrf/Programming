#ifndef MATH_GEOMETRY_
#define MATH_GEOMETRY_

#include <iostream>
#include <vector>

namespace Geometry
{
    class Point
    {
    protected:
        double _x;
        double _y;

    public:
        Point();
        Point(double x, double y);
        Point(const Point &p);

        ~Point();

        double GetX() const;
        double GetY() const;

        double SetX(double x);
        double SetY(double y);

        friend double Dist(const Point &p1, const Point &p2);

        Point &operator=(const Point &p);
        friend std::istream &operator>>(std::istream &in, Point &p);
        friend std::ostream &operator<<(std::ostream &out, const Point &p);
    };

    class Vector final : public Point
    {
    public:
        Vector();
        Vector(double x, double y);
        explicit Vector(const Point &p);
        explicit Vector(const Point &p1, const Point &p2);
        Vector(const Vector &v);

        ~Vector();

        double Length() const;
        friend double DotProduct(const Vector &v1, const Vector &v2);
        friend double CrossProduct(const Vector &v1, const Vector &v2);

        Vector &operator=(const Vector &v);
        Vector operator+(const Vector &v) const;
        Vector operator-(const Vector &v) const;
        friend std::istream &operator>>(std::istream &in, Vector &v);
        friend std::ostream &operator<<(std::ostream &out, const Vector &v);
    };

    class Polyline
    {
    protected:
        std::vector<Point> _points;

    public:
        Polyline();
        explicit Polyline(const std::vector<Point> &points);
        Polyline(const Polyline &pl);

        ~Polyline();

        std::size_t PointsCount() const;
        virtual double Length() const;

        Polyline &operator=(const Polyline &pl);
        const Point &operator[](std::size_t i) const;
    };

    class ClosedPolyline : public Polyline
    {
    public:
        ClosedPolyline();
        explicit ClosedPolyline(const std::vector<Point> &points);
        ClosedPolyline(const ClosedPolyline &cpl);

        ~ClosedPolyline();

        double Length() const override;

        ClosedPolyline &operator=(const ClosedPolyline &cpl);
    };

    class Polygon : public ClosedPolyline
    {
    public:
        Polygon();
        explicit Polygon(const std::vector<Point> &points);
        Polygon(const Polygon &pg);

        ~Polygon();

        double Perimeter() const;
        double Area();

        Polygon &operator=(const Polygon &pg);

    private:
        bool isIntersect(const Point &p1, const Point &p2, const Point &p3, const Point &p4) const;
    };

    class Triangle final : public Polygon
    {
    public:
        Triangle();
        explicit Triangle(const Point &p1, const Point &p2, const Point &p3);
        Triangle(const Triangle &t);

        ~Triangle();

        Triangle &operator=(const Triangle &t);
    };

    class Trapezoid final : public Polygon
    {
    public:
        Trapezoid();
        explicit Trapezoid(const Point &p1, const Point &p2, const Point &p3, const Point &p4);
        Trapezoid(const Trapezoid &tr);

        ~Trapezoid();

        Trapezoid &operator=(const Trapezoid &tr);

    private:
        bool isTrapezoid(const Point &p1, const Point &p2, const Point &p3, const Point &p4) const;
    };

    class RegularPolygon : public Polygon
    {
    public:
        RegularPolygon();
        explicit RegularPolygon(const std::vector<Point> &points);
        RegularPolygon(const RegularPolygon &rpg);

        ~RegularPolygon();

        RegularPolygon &operator=(const RegularPolygon &rpg);

    private:
        bool isRegular() const;
    };

} // namespace Geometry

#endif  // MATH_GEOMETRY_