#ifndef ALGORITHMS_
#define ALGORITHMS_

namespace ZIS
{
    template <class InputIterator, class Function>
    bool all_of(InputIterator begin, InputIterator end, Function f)
    {
        for (auto it = begin; it != end; ++it)
            if (!f(*it))
                return false;
        return true;
    }

    template <class InputIterator, class Function>
    bool any_of(InputIterator begin, InputIterator end, Function f)
    {
        for (auto it = begin; it != end; ++it)
            if (f(*it))
                return true;
        return false;
    }

    template <class InputIterator, class Function>
    bool none_of(InputIterator begin, InputIterator end, Function f)
    {
        for (auto it = begin; it != end; ++it)
            if (f(*it))
                return false;
        return true;
    }

    template <class InputIterator, class Function>
    bool one_of(InputIterator begin, InputIterator end, Function f)
    {
        bool flag = false;
        for (auto it = begin; it != end; ++it)
            if (f(*it))
            {
                if (flag)
                    return false;
                flag = true;
            }
        return flag;
    }

    template <class InputIterator>
    bool is_sorted(InputIterator begin, InputIterator end)
    {
        auto prev = begin;
        auto cur = begin;
        ++cur;
        while (cur != end)
        {
            if (*cur < *prev)
                return false;
            ++prev;
            ++cur;
        }
        return true;
    }

    template <class InputIterator, class Function>
    bool is_sorted(InputIterator begin, InputIterator end, Function f)
    {
        auto prev = begin;
        auto cur = begin;
        ++cur;
        while (cur != end)
        {
            if (!f(*cur, *prev))
                return false;
            ++prev;
            ++cur;
        }
        return true;
    }

    template <class InputIterator, class Function>
    bool is_partitioned(InputIterator begin, InputIterator end, Function f)
    {
        bool flag = false;
        for (auto it = begin; it != end; ++it)
        {
            if (!f(*it) && !flag)
                flag = true;
            else if (f(*it) && flag)
                return false;
        }
        return flag;
    }

    template <class InputIterator, class T>
    InputIterator find_not(InputIterator begin, InputIterator end, T value)
    {
        for (auto it = begin; it != end; ++it)
            if (*it != value)
                return it;
        return end;
    }

    template <class BidirectIterator, class T>
    BidirectIterator find_backward(BidirectIterator begin, BidirectIterator end, T value)
    {
        --end;
        --begin;
        for (auto it = end; it != begin; --it)
            if (*it != value)
                return it;
        return ++end;
    }

    template <class BidirectIterator, class T>
    bool is_palindrome(BidirectIterator begin, BidirectIterator end)
    {
        --end;
        while (begin < end)
        {
            if (*begin != *end)
                return false;
            ++begin;
            --end;
        }
        return true;
    }

    template <class InputIterator, class Function>
    bool is_palindrome(InputIterator begin, InputIterator end, Function f)
    {
        --end;
        while (begin < end)
        {
            if (!f(*begin, *end))
                return false;
            ++begin;
            --end;
        }
        return true;
    }

} // namespace ZIS

#endif // ALGORITHMS_