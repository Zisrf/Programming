#include "RubikCube.h"
#include <iostream>
#include <Windows.h>

Edge::Edge(Color color)
{
    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            _color[i][j] = color;
}

Edge::Edge(Color **color)
{
    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            _color[i][j] = color[i][j];
}

bool Edge::operator==(const Edge &other) const
{
    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_color[i][j] != other[i][j])
                return false;
    return true;
}

bool Edge::operator!=(const Edge &other) const
{
    return !(*this == other);
}

Color *Edge::operator[](std::size_t i)
{
    if (i > 2)
        throw std::out_of_range("Edge subscript out of range");
    return _color[i];
}

const Color *Edge::operator[](std::size_t i) const
{
    if (i > 2)
        throw std::out_of_range("Edge subscript out of range");
    return _color[i];
}

void Edge::lrotate()
{
    rrotate();
    rrotate();
    rrotate();
}

void Edge::rrotate()
{
    Edge tmp;
    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            tmp[j][2 - i] = _color[i][j];
    *this = tmp;
}

std::istream &operator>>(std::istream &in, Edge &edge)
{
    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
        {
            char tmp;
            in >> tmp;
            switch (tmp)
            {
            case 'w':
                edge[i][j] = white;
                break;
            case 'g':
                edge[i][j] = green;
                break;
            case 'r':
                edge[i][j] = red;
                break;
            case 'b':
                edge[i][j] = blue;
                break;
            case 'o':
                edge[i][j] = orange;
                break;
            case 'y':
                edge[i][j] = yellow;
                break;
            default:
                throw std::logic_error("error: incorrect color");
            }
        }
    return in;
}

std::ostream &operator<<(std::ostream &out, const Edge &edge)
{
    for (std::size_t i = 0; i < 3; ++i)
    {
        for (std::size_t j = 0; j < 3; ++j)
        {
            switch (edge[i][j])
            {
            case white:
                out << "w ";
                break;
            case green:
                out << "g ";
                break;
            case red:
                out << "r ";
                break;
            case blue:
                out << "b ";
                break;
            case orange:
                out << "o ";
                break;
            case yellow:
                out << "y ";
                break;
            }
        }
        out << std::endl;
    }
    return out;
}

void draw_pixel(Color color)
{
    HANDLE hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
    switch (color)
    {
    case white:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE);
        std::cout << "  ";
        break;
    case green:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_GREEN);
        std::cout << "  ";
        break;
    case red:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_RED);
        std::cout << "  ";
        break;
    case blue:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_BLUE);
        std::cout << "  ";
        break;
    case orange:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_RED | BACKGROUND_GREEN);
        std::cout << "  ";
        break;
    case yellow:
        SetConsoleTextAttribute(hConsoleHandle, BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_INTENSITY);
        std::cout << "  ";
        break;
    }

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
}