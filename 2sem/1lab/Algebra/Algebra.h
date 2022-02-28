#pragma once
#include <iostream>
#include <vector>
#include <map>

namespace Algebra
{
    class Polynomial
    {
        std::map<std::size_t, double> _coefficients;

    public:
        Polynomial();
        explicit Polynomial(const std::vector<double> &coefficients);
        Polynomial(const Polynomial &p);

        ~Polynomial();

        std::size_t Degree() const;

        double operator[](std::size_t i) const;

        Polynomial &operator=(const Polynomial &p);

        bool operator==(const Polynomial &p) const;
        bool operator!=(const Polynomial &p) const;

        Polynomial operator+(double num) const;
        Polynomial operator+(const Polynomial &p) const;
        Polynomial &operator+=(double num);
        Polynomial &operator+=(const Polynomial &p);

        Polynomial operator-() const;
        Polynomial operator-(double num) const;
        Polynomial operator-(const Polynomial &p) const;
        Polynomial &operator-=(double num);
        Polynomial &operator-=(const Polynomial &p);

        Polynomial operator>>(std::size_t k) const;
        Polynomial operator<<(std::size_t k) const;

        Polynomial operator*(double num) const;
        Polynomial operator*(const Polynomial &p) const;
        Polynomial &operator*=(double num);
        Polynomial &operator*=(const Polynomial &p);

        Polynomial operator/(double num) const;
        Polynomial &operator/=(double num);

        friend std::istream &operator>>(std::istream &in, Polynomial &p);
        friend std::ostream &operator<<(std::ostream &out, const Polynomial &p);

    private:
        void fixCoefficients();
    };
}