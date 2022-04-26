#ifndef RING_BUFFER_
#define RING_BUFFER_

#include <initializer_list>
#include <iterator>
#include <stdexcept>

namespace ZIS
{
    template <class T>
    class RBIterator : public std::iterator<std::random_access_iterator_tag, T>
    {
    public:
        RBIterator() = default;
        explicit RBIterator(T *ptr, T *first, T *last, T *begin, T *end)
            : _ptr(ptr),
              _first(first),
              _last(last),
              _begin(begin),
              _end(end) {}
        RBIterator(const RBIterator &it) = default;
        ~RBIterator() = default;

        RBIterator &operator=(const RBIterator &it)
        {
            _ptr = it._ptr;
            _first = it._first;
            _last = it._last;
            _begin = it._begin;
            _end = it._end;
            return *this;
        }
        T &operator*() const
        {
            return *_ptr;
        }
        T *operator->() const
        {
            return _ptr;
        }
        T &operator[](std::size_t index)
        {
            if (_first <= _last)
                return _ptr[index];
            if (_ptr + index < _end)
                return _ptr[index];
            return _first[index - (_end - _ptr)];
        }
        T &operator[](std::size_t index) const
        {
            if (_first <= _last)
                return _ptr[index];
            if (_ptr + index < _end)
                return _ptr[index];
            return _first[index - (_end - _ptr)];
        }

        RBIterator &operator++()
        {
            ++_ptr;
            if (_first > _last && _ptr == _end)
                _ptr = _begin;
            return *this;
        }
        RBIterator &operator--()
        {
            if (_first > _last && _ptr == _begin)
                _ptr = _end - 1;
            else
                --_ptr;
            return *this;
        }
        RBIterator &operator++(int)
        {
            RBIterator tmp(*this);
            ++_ptr;
            if (_first > _last && _ptr == _end)
                _ptr = _begin;
            return tmp;
        }
        RBIterator &operator--(int)
        {
            RBIterator tmp(*this);
            if (_first > _last && _ptr == _begin)
                _ptr = _end - 1;
            else
                --_ptr;
            return tmp;
        }
        RBIterator operator+(int k) const
        {
            if (_first > _last && _ptr + k >= _end)
                return RBIterator(_begin + k - (_end - _ptr), _first, _last, _begin, _end);
            if (_first > _last && _ptr + k < _begin)
                return RBIterator(_end - k + (_ptr - _begin), _first, _last, _begin, _end);
            return RBIterator(_ptr + k, _first, _last, _begin, _end);
        }
        RBIterator operator-(int k) const
        {
            return *this + (-k);
        }
        RBIterator &operator+=(int k)
        {
            return *this = *this + k;
        }
        RBIterator &operator-=(int k)
        {
            return *this = *this - k;
        }
        std::size_t operator-(const RBIterator &other)
        {
            if (_begin != other._begin ||
                _end != other._end ||
                _first != other._first ||
                _last != other._last)
                throw std::logic_error("Comparing iterators of different RingBuffers");

            if (other._ptr > _ptr)
                return (_end - _begin) - (_ptr - other._ptr);
            return _ptr - other._ptr;
        }

        bool operator==(const RBIterator &other) const
        {
            if (_begin != other._begin ||
                _end != other._end ||
                _first != other._first ||
                _last != other._last)
                throw std::logic_error("Comparing iterators of different RingBuffers");

            return _ptr == other._ptr;
        }
        bool operator!=(const RBIterator &other) const
        {
            return !(_ptr == other._ptr);
        }
        bool operator>(const RBIterator &other) const
        {
            if (_begin != other._begin ||
                _end != other._end ||
                _first != other._first ||
                _last != other._last)
                throw std::logic_error("Comparing iterators of different RingBuffers");

            if (_first > _last && _ptr >= _first && other._ptr < _last)
                return true;
            if (_first > _last && _ptr < _last && other._ptr >= _first)
                return false;
            return _ptr > other._ptr;
        }
        bool operator<(const RBIterator &other) const
        {
            return !(*this == other || *this > other);
        }
        bool operator>=(const RBIterator &other) const
        {
            return !(*this < other);
        }
        bool operator<=(const RBIterator &other) const
        {
            return *!(*this > other);
        }

    private:
        T *_ptr;
        T *_first;
        T *_last;
        T *_begin;
        T *_end;
    };

    template <class T>
    class RingBuffer
    {
    public:
        typedef RBIterator<T> iterator;
        typedef RBIterator<const T> const_iterator;

        iterator begin()
        {
            return iterator(_first, _first, _last, _data, _data + _capacity);
        }
        iterator end()
        {
            return iterator(_last, _first, _last, _data, _data + _capacity);
        }
        const_iterator cbegin() const
        {
            return const_iterator(_first, _first, _last, _data, _data + _capacity);
        }
        const_iterator cend() const
        {
            return const_iterator(_last, _first, _last, _data, _data + _capacity);
        }

        RingBuffer()
        {
            _data = new T[1];
            _first = _data;
            _last = _data;
            _capacity = 1;
        }
        explicit RingBuffer(std::size_t size)
        {
            _data = new T[2 * size + 1];
            _first = _data;
            _last = _data + size;
            _capacity = 2 * size + 1;
        }
        RingBuffer(std::initializer_list<T> init_list)
        {
            _data = new T[init_list.size() * 2 + 1];
            _first = _data;
            _last = _data + init_list.size();
            _capacity = init_list.size() * 2 + 1;

            for (std::size_t i = 0; i < size(); ++i)
                (*this)[i] = *(init_list.begin() + i);
        }
        RingBuffer(const RingBuffer &other)
        {
            _data = new T[2 * other.size() + 1];

            for (std::size_t i = 0; i < other.size(); ++i)
                _data[i] = other[i];

            _first = _data;
            _last = _data + other.size();
            _capacity = 2 * other.size() + 1;
        }
        ~RingBuffer()
        {
            delete[] _data;
        }

        RingBuffer &operator=(const RingBuffer &other)
        {
            delete[] _data;
            _data = new T[2 * other.size() + 1];

            for (std::size_t i = 0; i < other.size(); ++i)
                _data[i] = other[i];

            _first = _data;
            _last = _data + other.size();
            _capacity = 2 * other.size() + 1;

            return *this;
        }

        std::size_t size() const
        {
            if (_first > _last)
                return _capacity - (_first - _last);
            return _last - _first;
        }
        std::size_t capacity() const
        {
            return _capacity;
        }
        bool empty() const
        {
            return size() == 0;
        }
        void resize(std::size_t new_size)
        {
            T *new_data = new T[2 * new_size + 1];

            for (std::size_t i = 0; i < (new_size < size() ? new_size : size()); ++i)
                new_data[i] = (*this)[i];
            delete[] _data;

            _data = new_data;
            _first = _data;
            _last = _data + new_size;
            _capacity = 2 * new_size + 1;
        }
        void clear()
        {
            *this = RingBuffer(0);
        }

        T &operator[](std::size_t index)
        {
            if (_first <= _last)
            {
                if (_first + index < _last)
                    return _first[index];
                throw std::out_of_range("RingBuffer subscript out of range");
            }
            else
            {
                if (_first + index < _data + _capacity)
                    return _first[index];
                if (_data + index - (_data + _capacity - _first) < _last)
                    return _data[index - (_data + _capacity - _first)];
                throw std::out_of_range("RingBuffer subscript out of range");
            }
        }
        T &operator[](std::size_t index) const
        {
            if (_first <= _last)
            {
                if (_first + index < _last)
                    return _first[index];
                throw std::out_of_range("RingBuffer subscript out of range");
            }
            else
            {
                if (_first + index < _data + _capacity)
                    return _first[index];
                if (_data + index - (_data + _capacity - _first) < _last)
                    return _data[index - (_data + _capacity - _first)];
                throw std::out_of_range("RingBuffer subscript out of range");
            }
        }
        T &front() const
        {
            if (size() == 0)
                throw std::logic_error("Using front() for empty RingBuffer");
            return (*this)[0];
        }
        T &back() const
        {
            if (size() == 0)
                throw std::logic_error("Using back() for empty RingBuffer");
            return (*this)[size() - 1];
        }

        void push_front(const T &new_value)
        {
            if (size() == 0)
            {
                resize(1);
                (*this)[0] = new_value;
                return;
            }

            if (_capacity == size() + 1)
                update_capacity();
            if (_first == _data)
                _first = _data + _capacity - 1;
            else
                --_first;
            *_first = new_value;
        }
        void pop_front()
        {
            if (size() == 0)
                throw std::logic_error("Using pop_front() for empty RingBuffer");

            ++_first;
            if (_first == _data + _capacity && _first != _last)
                _first = _data;

            if (_capacity > 3 * size())
                update_capacity();
        }
        void push_back(const T &new_value)
        {
            if (size() == 0)
            {
                resize(1);
                (*this)[0] = new_value;
                return;
            }

            if (_capacity == size() + 1)
                update_capacity();
            if (_last == _data + _capacity)
                _last = _data + 1;
            else
                ++_last;
            *(_last - 1) = new_value;
        }
        void pop_back()
        {
            if (size() == 0)
                throw std::logic_error("Using pop_back() for empty RingBuffer");

            --_last;
            if (_last == _data && _first != _last)
                _last = _data + _capacity;

            if (_capacity > 3 * size())
                update_capacity();
        }

    private:
        void update_capacity()
        {
            std::size_t new_capacity = size() * 2 + 1;
            T *new_data = new T[new_capacity];

            for (std::size_t i = 0; i < size(); ++i)
                new_data[i] = (*this)[i];
            delete[] _data;

            _data = new_data;
            _last = _data + size();
            _first = _data;
            _capacity = new_capacity;
        }

        T *_data;
        T *_first;
        T *_last;
        std::size_t _capacity;
    };

} // namespace ZIS

#endif // RING_BUFFER_