#include "algebra.h"

namespace Algebra
{
	Polynomial::Polynomial() : _coefficients() {}

	Polynomial::Polynomial(const std::vector<double> &coefficients)
	{
		for (std::size_t i = 0; i < coefficients.size(); ++i)
			_coefficients[i] = coefficients[i];
		fixCoefficients();
	}

	Polynomial::Polynomial(const Polynomial &p)
		: _coefficients(p._coefficients) {}

	Polynomial::~Polynomial() {}

	void Polynomial::fixCoefficients()
	{
		std::vector<std::size_t> c;
		for (auto [i, val] : _coefficients)
			if (val == 0)
				c.push_back(i);
		for (auto i : c)
			_coefficients.erase(i);
	}

	std::size_t Polynomial::Degree() const
	{
		if (_coefficients.empty())
			return 0;
		return _coefficients.rbegin()->first;
	}

	double Polynomial::operator[](std::size_t i) const
	{
		if (_coefficients.find(i) == _coefficients.end())
			return 0;
		return _coefficients.at(i);
	}

	Polynomial &Polynomial::operator=(const Polynomial &p)
	{
		_coefficients = p._coefficients;
		return *this;
	}

	bool Polynomial::operator==(const Polynomial &p) const
	{
		return _coefficients == p._coefficients;
	}

	bool Polynomial::operator!=(const Polynomial &p) const
	{
		return _coefficients != p._coefficients;
	}

	Polynomial Polynomial::operator+(double num) const
	{
		Polynomial res(*this);
		res._coefficients[0] += num;
		res.fixCoefficients();
		return res;
	}

	Polynomial Polynomial::operator+(const Polynomial &p) const
	{
		Polynomial res;
		for (auto [i, val] : _coefficients)
			res._coefficients[i] += val;
		for (auto [i, val] : p._coefficients)
			res._coefficients[i] += val;
		res.fixCoefficients();
		return res;
	}

	Polynomial &Polynomial::operator+=(double num)
	{
		_coefficients[0] += num;
		fixCoefficients();
		return *this;
	}

	Polynomial &Polynomial::operator+=(const Polynomial &p)
	{
		for (auto [i, val] : p._coefficients)
			_coefficients[i] += val;
		fixCoefficients();
		return *this;
	}

	Polynomial Polynomial::operator-() const
	{
		Polynomial res(*this);
		for (auto [i, val] : _coefficients)
			res._coefficients[i] = -val;
		return res;
	}

	Polynomial Polynomial::operator-(double num) const
	{
		Polynomial res(*this);
		res._coefficients[0] -= num;
		res.fixCoefficients();
		return res;
	}

	Polynomial Polynomial::operator-(const Polynomial &p) const
	{
		Polynomial res;
		for (auto [i, val] : _coefficients)
			res._coefficients[i] += val;
		for (auto [i, val] : p._coefficients)
			res._coefficients[i] -= val;
		res.fixCoefficients();
		return res;
	}

	Polynomial &Polynomial::operator-=(double num)
	{
		_coefficients[0] -= num;
		fixCoefficients();
		return *this;
	}

	Polynomial &Polynomial::operator-=(const Polynomial &p)
	{
		for (auto [i, val] : p._coefficients)
			_coefficients[i] -= val;
		fixCoefficients();
		return *this;
	}

	Polynomial Polynomial::operator>>(std::size_t k) const
	{
		Polynomial res;
		for (auto [i, val] : _coefficients)
			if (i - k >= 0)
				res._coefficients[i - k] = val;
		return res;
	}

	Polynomial Polynomial::operator<<(std::size_t k) const
	{
		Polynomial res;
		for (auto [i, val] : _coefficients)
			res._coefficients[i + k] = val;
		return res;
	}

	Polynomial Polynomial::operator*(double num) const
	{
		Polynomial res(*this);
		for (auto [i, val] : _coefficients)
			res._coefficients[i] *= num;
		res.fixCoefficients();
		return res;
	}

	Polynomial Polynomial::operator*(const Polynomial &p) const
	{
		Polynomial res;
		for (auto [i, val] : p._coefficients)
			res += (*this << i) * val;
		res.fixCoefficients();
		return res;
	}

	Polynomial &Polynomial::operator*=(double num)
	{
		for (auto [i, val] : _coefficients)
			_coefficients[i] *= num;
		fixCoefficients();
		return *this;
	}

	Polynomial &Polynomial::operator*=(const Polynomial &p)
	{
		Polynomial res;
		for (auto [i, val] : p._coefficients)
			res += (*this << i) * val;
		res.fixCoefficients();
		*this = res;
		return *this;
	}

	Polynomial Polynomial::operator/(double num) const
	{
		Polynomial res(*this);
		for (auto [i, val] : res._coefficients)
			res._coefficients[i] /= num;
		return res;
	}

	Polynomial &Polynomial::operator/=(double num)
	{
		for (auto [i, val] : _coefficients)
			_coefficients[i] /= num;
		return *this;
	}

	std::istream &operator>>(std::istream &in, Polynomial &p)
	{
		std::size_t degree;
		in >> degree;
		for (std::size_t i = 0; i <= degree; ++i)
			in >> p._coefficients[i];
		p.fixCoefficients();
		return in;
	}

	std::ostream &operator<<(std::ostream &out, const Polynomial &p)
	{
		if (p._coefficients.empty())
		{
			out << "0";
			return out;
		}
		for (auto it = p._coefficients.rbegin(); it != p._coefficients.rend(); ++it)
		{
			std::size_t i = it->first;
			double val = it->second;
			if (it != p._coefficients.rbegin() && val > 0)
				out << " + ";
			else if (it != p._coefficients.rbegin() && val < 0)
				out << " - ";
			else if (val < 0)
				out << "-";
			if (i == 0)
			{
				if (val != 0 || it == p._coefficients.rend())
					out << abs(val);
			}
			else if (i == 1)
			{
				if (abs(val) == 1)
					out << "x";
				else if (val != 0)
					out << abs(val) << "*x";
			}
			else
			{
				if (abs(val) == 1)
					out << "x^" << i;
				else if (val != 0)
					out << abs(val) << "*x^" << i;
			}
		}
		return out;
	}
}