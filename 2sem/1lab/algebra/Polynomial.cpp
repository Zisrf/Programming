#include "algebra.h"
#include <algorithm>

namespace Algebra
{
	Polynomial::Polynomial() : _coefficients(1, 0) {}

	Polynomial::Polynomial(const std::vector<double> &coefficients) : _coefficients(coefficients)
	{
		if (coefficients.size() == 0)
		{
			std::cerr << "Incorrect polynomial" << std::endl;
			exit(1);
		}
		fixDegree();
	}

	Polynomial::Polynomial(const Polynomial &p) : _coefficients(p._coefficients) {}

	Polynomial::~Polynomial() {}

	std::size_t Polynomial::Degree() const
	{
		return _coefficients.size() - 1;
	}

	void Polynomial::fixDegree()
	{
		while (_coefficients.size() > 1 && _coefficients.back() == 0)
			_coefficients.pop_back();
	}

	double Polynomial::operator[](std::size_t i) const
	{
		return _coefficients[i];
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
		return res;
	}

	Polynomial Polynomial::operator+(const Polynomial &p) const
	{
		Polynomial res(*this);
		if (p._coefficients.size() > res._coefficients.size())
			res._coefficients.resize(p._coefficients.size());
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			res._coefficients[i] += p._coefficients[i];
		res.fixDegree();
		return res;
	}

	Polynomial &Polynomial::operator+=(double num)
	{
		_coefficients[0] += num;
		return *this;
	}

	Polynomial &Polynomial::operator+=(const Polynomial &p)
	{
		if (p._coefficients.size() > _coefficients.size())
			_coefficients.resize(p._coefficients.size());
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			_coefficients[i] += p._coefficients[i];
		fixDegree();
		return *this;
	}

	Polynomial Polynomial::operator-() const
	{
		Polynomial res(*this);
		for (std::size_t i = 0; i < _coefficients.size(); ++i)
			res._coefficients[i] *= -1;
		return res;
	}

	Polynomial Polynomial::operator-(double num) const
	{
		Polynomial res(*this);
		res._coefficients[0] -= num;
		return res;
	}

	Polynomial Polynomial::operator-(const Polynomial &p) const
	{
		Polynomial res(*this);
		if (p._coefficients.size() > res._coefficients.size())
			res._coefficients.resize(p._coefficients.size());
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			res._coefficients[i] -= p._coefficients[i];
		res.fixDegree();
		return res;
	}

	Polynomial &Polynomial::operator-=(double num)
	{
		_coefficients[0] -= num;
		return *this;
	}

	Polynomial &Polynomial::operator-=(const Polynomial &p)
	{
		if (p._coefficients.size() > _coefficients.size())
			_coefficients.resize(p._coefficients.size());
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			_coefficients[i] -= p._coefficients[i];
		fixDegree();
		return *this;
	}

	Polynomial Polynomial::operator>>(std::size_t k) const
	{
		if (_coefficients.size() <= k)
			return Polynomial({0});
		Polynomial res;
		res._coefficients = std::vector<double>(_coefficients.begin() + k, _coefficients.end());
		res.fixDegree();
		return res;
	}

	Polynomial &Polynomial::operator>>=(std::size_t k)
	{
		if (_coefficients.size() <= k)
			return *this = Polynomial({0});
		_coefficients = std::vector<double>(_coefficients.begin() + k, _coefficients.end());
		fixDegree();
		return *this;
	}

	Polynomial Polynomial::operator<<(std::size_t k) const
	{
		Polynomial res(*this);
		res._coefficients.insert(res._coefficients.begin(), k, 0);
		return res;
	}

	Polynomial &Polynomial::operator<<=(std::size_t k)
	{
		_coefficients.insert(_coefficients.begin(), k, 0);
		return *this;
	}

	Polynomial Polynomial::operator*(double num) const
	{
		Polynomial res(*this);
		for (std::size_t i = 0; i < res._coefficients.size(); ++i)
			res._coefficients[i] *= num;
		res.fixDegree();
		return res;
	}

	Polynomial Polynomial::operator*(const Polynomial &p) const
	{
		Polynomial res;
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			res += (*this << i) * p._coefficients[i];
		res.fixDegree();
		return res;
	}

	Polynomial &Polynomial::operator*=(double num)
	{
		for (std::size_t i = 0; i < _coefficients.size(); ++i)
			_coefficients[i] *= num;
		fixDegree();
		return *this;
	}

	Polynomial &Polynomial::operator*=(const Polynomial &p)
	{
		Polynomial res;
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			res += (*this << i) * p._coefficients[i];
		res.fixDegree();
		return *this = res;
	}

	Polynomial Polynomial::operator/(double num) const
	{
		Polynomial res(*this);
		for (std::size_t i = 0; i < res._coefficients.size(); ++i)
			res._coefficients[i] /= num;
		return res;
	}

	Polynomial &Polynomial::operator/=(double num)
	{
		for (std::size_t i = 0; i < _coefficients.size(); ++i)
			_coefficients[i] /= num;
		return *this;
	}

	std::istream &operator>>(std::istream &in, Polynomial &p)
	{
		std::size_t degree;
		in >> degree;
		p._coefficients.resize(degree + 1);
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
			in >> p._coefficients[i];
		p.fixDegree();
		return in;
	}

	std::ostream &operator<<(std::ostream &out, const Polynomial &p)
	{
		for (std::size_t i = 0; i < p._coefficients.size(); ++i)
		{
			if (i == 0)
			{
				out << p._coefficients[i];
			}
			else if (i == 1)
			{
				if (p._coefficients[i] == 0)
					continue;
				else if (p._coefficients[i] > 0)
					out << " + " << p._coefficients[i] << "*x";
				else
					out << " - " << p._coefficients[i] << "*x";
			}
			else
			{
				if (p._coefficients[i] == 0)
					continue;
				else if (p._coefficients[i] > 0)
					out << " + " << p._coefficients[i] << "*x^" << i;
				else
					out << " - " << p._coefficients[i] << "*x^" << i;
			}
		}
		return out;
	}
}