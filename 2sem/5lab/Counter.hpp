#ifndef ZIS_COUNTER_
#define ZIS_COUNTER_

#include <cstdint>

namespace ZIS
{
    template <class T>
    struct Counter
    {
        static size_t created;
        Counter() { ++created; }
        Counter(const Counter &other) { ++created; }
        ~Counter() { --created; }
    };

    template <class T>
    size_t Counter<T>::created = 0;

} // namespace ZIS

#endif // ZIS_COUNTER_