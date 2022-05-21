#ifndef RUBIK_CUBE_SOLVER_
#define RUBIK_CUBE_SOLVER_

#include <fstream>
#include <string>
#include <stdexcept>

enum Color
{
    white,
    green,
    red,
    blue,
    orange,
    yellow
};

enum EdgeName
{
    up,
    front,
    right,
    back,
    left,
    down
};

void draw_pixel(Color color);

class Edge
{
public:
    Edge() = default;
    explicit Edge(Color color);
    explicit Edge(Color **color);
    ~Edge() = default;

    Edge &operator=(const Edge &other) = default;
    bool operator==(const Edge &other) const;
    bool operator!=(const Edge &other) const;
    Color *operator[](std::size_t i);
    const Color *operator[](std::size_t i) const;

    void lrotate();
    void rrotate();

    friend std::istream &operator>>(std::istream &in, Edge &edge);
    friend std::ostream &operator<<(std::ostream &out, const Edge &edge);

private:
    Color _color[3][3];
};

class RubikCube
{
public:
    RubikCube();
    explicit RubikCube(const std::string &file_name);
    RubikCube(const RubikCube &cube) = default;
    ~RubikCube() = default;

    RubikCube &operator=(const RubikCube &other) = default;
    bool operator==(const RubikCube &other) const;
    bool operator!=(const RubikCube &other) const;
    bool operator<(const RubikCube &other) const;
    bool operator<=(const RubikCube &other) const;
    bool operator>(const RubikCube &other) const;
    bool operator>=(const RubikCube &other) const;
    Edge &operator[](EdgeName edge);
    const Edge &operator[](EdgeName edge) const;

    void rotate_right(EdgeName edge);
    void rotate_left(EdgeName edge);
    void rotate(const std::string &rotations);

    double correctness() const;
    std::string randomize();
    void save(const std::string &file_name) const;
    void draw() const;
    std::string solution() const;

private:
    void correctness_check() const;

    void rrotate_up();
    void rrotate_front();
    void rrotate_right();
    void rrotate_back();
    void rrotate_left();
    void rrotate_down();

    Edge _up;
    Edge _down;
    Edge _front;
    Edge _back;
    Edge _left;
    Edge _right;
};

#endif // RUBIK_CUBE_SOLVER_