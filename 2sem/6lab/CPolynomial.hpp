#ifndef CONSTEXPR_POLYNOMIAL_
#define CONSTEXPR_POLYNOMIAL_

#include <iostream>

namespace ZIS
{
    template <unsigned X, unsigned P>
    struct CPow
    {
        static long long const value = X * CPow<X, P - 1>::value;
    };

    template <unsigned X>
    struct CPow<X, 0>
    {
        static long long const value = 1;
    };

    template <int X, unsigned CurPow, int... Coefs>
    struct CSumOfMonomials;

    template <int X, unsigned CurPow, int CurCoef, int... OtherCoefs>
    struct CSumOfMonomials<X, CurPow, CurCoef, OtherCoefs...>
    {
        static long long const value = CurCoef * CPow<X, CurPow>::value +
                                       CSumOfMonomials<X, CurPow + 1, OtherCoefs...>::value;
    };

    template <int X, unsigned CurPow>
    struct CSumOfMonomials<X, CurPow>
    {
        static long long const value = 0;
    };

    template <int X, int... Coefs>
    struct CPolynomial
    {
        static long long const value = CSumOfMonomials<X, 0, Coefs...>::value;
    };

    template <int X, int CurCoef, int... OtherCoefs>
    std::ostream &operator<<(std::ostream &out, CPolynomial<X, CurCoef, OtherCoefs...>)
    {
        out << CurCoef << ' ';
        out << CPolynomial<X, OtherCoefs...>();
        return out;
    }

    template <int X>
    std::ostream &operator<<(std::ostream &out, CPolynomial<X>)
    {
        return out;
    }

} // namespace ZIS

#endif // CONSTEXPR_POLYNOMIAL_