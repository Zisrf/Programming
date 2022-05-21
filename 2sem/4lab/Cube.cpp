#include "RubikCube.h"
#include <Windows.h>
#include <iostream>
#include <sstream>
#include <vector>
#include <random>
#include <ctime>

RubikCube::RubikCube()
    : _up(white),
      _front(green),
      _right(red),
      _back(blue),
      _left(orange),
      _down(yellow) {}

RubikCube::RubikCube(const std::string &file_name)
{
    std::ifstream fin(file_name);
    if (!fin.is_open())
        throw std::logic_error("error: unable to open file \"" + file_name + "\"\n");

    fin >> _up;
    fin >> _front;
    fin >> _right;
    fin >> _back;
    fin >> _left;
    fin >> _down;

    correctness_check();
}

bool RubikCube::operator==(const RubikCube &other) const
{
    if (_up != other[up] ||
        _front != other[front] ||
        _right != other[right] ||
        _back != other[back] ||
        _left != other[left] ||
        _down != other[down])
        return false;
    return true;
}

bool RubikCube::operator!=(const RubikCube &other) const
{
    return !(*this == other);
}

bool RubikCube::operator<(const RubikCube &other) const
{
    return correctness() < other.correctness();
}

bool RubikCube::operator>(const RubikCube &other) const
{
    return correctness() > other.correctness();
}

Edge &RubikCube::operator[](EdgeName edge)
{
    switch (edge)
    {
    case up:
        return _up;
    case front:
        return _front;
    case right:
        return _right;
    case back:
        return _back;
    case left:
        return _left;
    case down:
        return _down;
    }
}

const Edge &RubikCube::operator[](EdgeName edge) const
{
    switch (edge)
    {
    case up:
        return _up;
    case front:
        return _front;
    case right:
        return _right;
    case back:
        return _back;
    case left:
        return _left;
    case down:
        return _down;
    }
}

void RubikCube::rotate_right(EdgeName edge)
{
    switch (edge)
    {
    case up:
        rrotate_up();
        break;
    case front:
        rrotate_front();
        break;
    case right:
        rrotate_right();
        break;
    case back:
        rrotate_back();
        break;
    case left:
        rrotate_left();
        break;
    case down:
        rrotate_down();
        break;
    }
}

void RubikCube::rotate_left(EdgeName edge)
{
    switch (edge)
    {
    case up:
        rrotate_up();
        rrotate_up();
        rrotate_up();
        break;
    case front:
        rrotate_front();
        rrotate_front();
        rrotate_front();
        break;
    case right:
        rrotate_right();
        rrotate_right();
        rrotate_right();
        break;
    case back:
        rrotate_back();
        rrotate_back();
        rrotate_back();
        break;
    case left:
        rrotate_left();
        rrotate_left();
        rrotate_left();
        break;
    case down:
        rrotate_down();
        rrotate_down();
        rrotate_down();
        break;
    }
}

void RubikCube::rotate(const std::string &rotations)
{
    std::stringstream cnt(rotations);
    std::string rotation;

    while (cnt >> rotation)
    {
        if (rotation == "U")
            rotate_right(up);
        else if (rotation == "U'")
            rotate_left(up);
        else if (rotation == "F")
            rotate_right(front);
        else if (rotation == "F'")
            rotate_left(front);
        else if (rotation == "R")
            rotate_right(right);
        else if (rotation == "R'")
            rotate_left(right);
        else if (rotation == "B")
            rotate_right(back);
        else if (rotation == "B'")
            rotate_left(back);
        else if (rotation == "L")
            rotate_right(left);
        else if (rotation == "L'")
            rotate_left(left);
        else if (rotation == "D")
            rotate_right(down);
        else if (rotation == "D'")
            rotate_left(down);
        else
            throw std::logic_error("error: incorrect rotation type");
    }
}

double RubikCube::correctness() const
{
    std::size_t res = 0;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_up[i][j] == white)
                ++res;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_front[i][j] == green)
                ++res;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_right[i][j] == red)
                ++res;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_back[i][j] == blue)
                ++res;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_left[i][j] == orange)
                ++res;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
            if (_down[i][j] == yellow)
                ++res;

    return res / 54.0;
}

std::string RubikCube::randomize()
{
    std::string rotations;

    srand(time(NULL));

    std::size_t n = 1 + rand() % 22;
    for (std::size_t i = 0; i < n; ++i)
    {
        std::size_t rotation_type = rand() % 12;
        switch (rotation_type)
        {
        case 0:
            rotate_left(up);
            rotations += "U' ";
            break;
        case 1:
            rotate_right(up);
            rotations += "U ";
            break;
        case 2:
            rotate_left(front);
            rotations += "F' ";
            break;
        case 3:
            rotate_right(front);
            rotations += "F ";
            break;
        case 4:
            rotate_left(right);
            rotations += "R' ";
            break;
        case 5:
            rotate_right(right);
            rotations += "R ";
            break;
        case 6:
            rotate_left(back);
            rotations += "B' ";
            break;
        case 7:
            rotate_right(back);
            rotations += "B ";
            break;
        case 8:
            rotate_left(left);
            rotations += "L' ";
            break;
        case 9:
            rotate_right(left);
            rotations += "L ";
            break;
        case 10:
            rotate_left(down);
            rotations += "D' ";
            break;
        case 11:
            rotate_right(down);
            rotations += "D ";
            break;
        }
    }

    return rotations;
}

void RubikCube::save(const std::string &file_name) const
{
    std::ofstream fout(file_name);
    if (!fout.is_open())
        throw std::logic_error("error: unable to open file \"" + file_name + "\"\n");

    fout << _up;
    fout << _front;
    fout << _right;
    fout << _back;
    fout << _left;
    fout << _down;
}

void RubikCube::draw() const
{
    HANDLE hConsoleHandle = GetStdHandle(STD_OUTPUT_HANDLE);

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
    for (std::size_t i = 0; i < 3; ++i)
    {
        std::cout << "      ";
        draw_pixel(_up[i][0]);
        draw_pixel(_up[i][1]);
        draw_pixel(_up[i][2]);
        SetConsoleTextAttribute(hConsoleHandle, 0xF);
        std::cout << "      " << std::endl;
    }

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
    for (std::size_t i = 0; i < 3; ++i)
    {
        draw_pixel(_left[i][0]);
        draw_pixel(_left[i][1]);
        draw_pixel(_left[i][2]);

        draw_pixel(_front[i][0]);
        draw_pixel(_front[i][1]);
        draw_pixel(_front[i][2]);

        draw_pixel(_right[i][0]);
        draw_pixel(_right[i][1]);
        draw_pixel(_right[i][2]);

        draw_pixel(_back[2 - i][2]);
        draw_pixel(_back[2 - i][1]);
        draw_pixel(_back[2 - i][0]);

        SetConsoleTextAttribute(hConsoleHandle, 0xF);
        std::cout << std::endl;
    }

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
    for (std::size_t i = 0; i < 3; ++i)
    {
        std::cout << "      ";
        draw_pixel(_down[i][0]);
        draw_pixel(_down[i][1]);
        draw_pixel(_down[i][2]);
        SetConsoleTextAttribute(hConsoleHandle, 0xF);
        std::cout << "      " << std::endl;
    }

    SetConsoleTextAttribute(hConsoleHandle, 0xF);
}

std::string RubikCube::solution() const
{
    const std::size_t POPULATION_SIZE = 500;
    const std::size_t SURVIVE_COUNT = 50;
    const std::size_t GENERATIONS_COUNT = 300;
    const std::size_t RESETS_COUNT = 10;

    std::vector<std::pair<RubikCube, std::string>> p(POPULATION_SIZE);

    for (std::size_t iteration = 0; iteration < RESETS_COUNT; ++iteration)
    {
        for (std::size_t i = 0; i < POPULATION_SIZE; ++i)
            p[i] = {*this, ""};

        for (std::size_t generation = 0; generation < GENERATIONS_COUNT; ++generation)
        {
            for (std::size_t i = 0; i < POPULATION_SIZE; ++i)
                p[i].second += p[i].first.randomize();

            std::sort(p.rbegin(), p.rend());

            for (std::size_t i = SURVIVE_COUNT; i < POPULATION_SIZE; ++i)
                p[i] = p[i % SURVIVE_COUNT];

            if (p[0].first.correctness() == 1)
                return p[0].second;
        }
    }

    throw std::logic_error("error: unable to solve cube");
}

void RubikCube::correctness_check() const
{
    if (_up[1][1] != white ||
        _front[1][1] != green ||
        _right[1][1] != red ||
        _back[1][1] != blue ||
        _left[1][1] != orange ||
        _down[1][1] != yellow)
        throw std::logic_error("error: incorrect edge centre color");

    std::size_t count[6];
    for (std::size_t i = 0; i < 6; ++i)
        count[i] = 0;

    for (std::size_t i = 0; i < 3; ++i)
        for (std::size_t j = 0; j < 3; ++j)
        {
            ++count[_up[i][j]];
            ++count[_front[i][j]];
            ++count[_right[i][j]];
            ++count[_back[i][j]];
            ++count[_left[i][j]];
            ++count[_down[i][j]];
        }

    if (count[white] != 9 ||
        count[green] != 9 ||
        count[red] != 9 ||
        count[blue] != 9 ||
        count[orange] != 9 ||
        count[yellow] != 9)
        throw std::logic_error("error: incorrect colors count");
}

void RubikCube::rrotate_up()
{
    _up.rrotate();

    Edge tmp(_back);

    _back[2][0] = _left[0][2];
    _back[2][1] = _left[0][1];
    _back[2][2] = _left[0][0];

    _left[0][2] = _front[0][2];
    _left[0][1] = _front[0][1];
    _left[0][0] = _front[0][0];

    _front[0][2] = _right[0][2];
    _front[0][1] = _right[0][1];
    _front[0][0] = _right[0][0];

    _right[0][2] = tmp[2][0];
    _right[0][1] = tmp[2][1];
    _right[0][0] = tmp[2][2];
}

void RubikCube::rrotate_front()
{
    _front.rrotate();

    Edge tmp(_up);

    _up[2][0] = _left[2][2];
    _up[2][1] = _left[1][2];
    _up[2][2] = _left[0][2];

    _left[2][2] = _down[0][2];
    _left[1][2] = _down[0][1];
    _left[0][2] = _down[0][0];

    _down[0][2] = _right[0][0];
    _down[0][1] = _right[1][0];
    _down[0][0] = _right[2][0];

    _right[0][0] = tmp[2][0];
    _right[1][0] = tmp[2][1];
    _right[2][0] = tmp[2][2];
}

void RubikCube::rrotate_right()
{
    _right.rrotate();

    Edge tmp(_up);

    _up[2][2] = _front[2][2];
    _up[1][2] = _front[1][2];
    _up[0][2] = _front[0][2];

    _front[2][2] = _down[2][2];
    _front[1][2] = _down[1][2];
    _front[0][2] = _down[0][2];

    _down[2][2] = _back[2][2];
    _down[1][2] = _back[1][2];
    _down[0][2] = _back[0][2];

    _back[2][2] = tmp[2][2];
    _back[1][2] = tmp[1][2];
    _back[0][2] = tmp[0][2];
}

void RubikCube::rrotate_back()
{
    _back.rrotate();

    Edge tmp(_down);

    _down[2][0] = _left[0][0];
    _down[2][1] = _left[1][0];
    _down[2][2] = _left[2][0];

    _left[0][0] = _up[0][2];
    _left[1][0] = _up[0][1];
    _left[2][0] = _up[0][0];

    _up[0][2] = _right[2][2];
    _up[0][1] = _right[1][2];
    _up[0][0] = _right[0][2];

    _right[2][2] = tmp[2][0];
    _right[1][2] = tmp[2][1];
    _right[0][2] = tmp[2][2];
}

void RubikCube::rrotate_left()
{
    _left.rrotate();

    Edge tmp(_up);

    _up[0][0] = _back[0][0];
    _up[1][0] = _back[1][0];
    _up[2][0] = _back[2][0];

    _back[0][0] = _down[0][0];
    _back[1][0] = _down[1][0];
    _back[2][0] = _down[2][0];

    _down[0][0] = _front[0][0];
    _down[1][0] = _front[1][0];
    _down[2][0] = _front[2][0];

    _front[0][0] = tmp[0][0];
    _front[1][0] = tmp[1][0];
    _front[2][0] = tmp[2][0];
}

void RubikCube::rrotate_down()
{
    _down.rrotate();

    Edge tmp(_front);

    _front[2][0] = _left[2][0];
    _front[2][1] = _left[2][1];
    _front[2][2] = _left[2][2];

    _left[2][0] = _back[0][2];
    _left[2][1] = _back[0][1];
    _left[2][2] = _back[0][0];

    _back[0][2] = _right[2][0];
    _back[0][1] = _right[2][1];
    _back[0][0] = _right[2][2];

    _right[2][0] = tmp[2][0];
    _right[2][1] = tmp[2][1];
    _right[2][2] = tmp[2][2];
}