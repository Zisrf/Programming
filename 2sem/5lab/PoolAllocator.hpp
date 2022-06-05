#ifndef ZIS_POOL_ALLOCATOR_
#define ZIS_POOL_ALLOCATOR_

#include <new>
#include <stdexcept>

#ifdef DEBUG
#include <fstream>
std::ofstream log("pool_allocator.log");
#endif // DEBUG

namespace ZIS
{
    template <unsigned Size>
    union Chunk
    {
        Chunk *next;
        char chunk_data[Size];
    };

    template <class T, unsigned ChunkSize = 8192, unsigned ChunksCount = 8192>
    class PoolAllocator
    {
    public:
        static const size_t chunk_size = ChunkSize * sizeof(T);
        static const size_t pool_size = chunk_size * ChunksCount;

        typedef T *pointer;
        typedef const T *const_pointer;
        typedef T value_type;
        typedef size_t size_type;
        typedef ptrdiff_t difference_type;
        typedef Chunk<chunk_size> chunk_type;

        template <class U>
        struct rebind
        {
            typedef PoolAllocator<U, ChunkSize, ChunksCount> other;
        };

        T *allocate(size_t n)
        {
            if (*_head_ptr == nullptr || n > max_size())
                throw std::bad_alloc();

            T *res = reinterpret_cast<T *>(*_head_ptr);
            *_head_ptr = (*_head_ptr)->next;

#ifdef DEBUG
            log << "Allocate at " << res << std::endl;
#endif // DEBUG

            return res;
        }

        void deallocate(T *p, size_t n) noexcept
        {
            if (p == nullptr)
                return;

            Chunk<chunk_size> *new_head = reinterpret_cast<Chunk<chunk_size> *>(p);
            new_head->next = *_head_ptr;
            *_head_ptr = new_head;

#ifdef DEBUG
            log << "Deallocate at " << p << std::endl;
#endif // DEBUG
        }

        size_t max_size() const noexcept
        {
            return ChunkSize;
        }

        const Chunk<chunk_size> *chunks() const noexcept
        {
            return _chunks;
        }

        const Chunk<chunk_size> **head_ptr() const noexcept
        {
            return _head_ptr;
        }

        const size_t *count_ptr() const noexcept
        {
            return _count_ptr;
        }

        PoolAllocator()
        {
            if (ChunksCount == 0)
                throw std::logic_error("incorrect chunks count");

            _chunks = new Chunk<chunk_size>[ChunksCount];
            _head_ptr = new Chunk<chunk_size> *;
            _count_ptr = new size_t;

            *_head_ptr = _chunks;
            *_count_ptr = 1;

            for (size_t i = 0; i < ChunksCount - 1; ++i)
                _chunks[i].next = _chunks + i + 1;
            _chunks[ChunksCount - 1].next = nullptr;

#ifdef DEBUG
            log << "Construct from " << _chunks << " to " << _chunks + ChunksCount << std::endl;
#endif // DEBUG
        }

        PoolAllocator(const PoolAllocator &other) noexcept
            : _chunks(other._chunks),
              _head_ptr(other._head_ptr),
              _count_ptr(other._count_ptr)
        {
            *_count_ptr += 1;
        }

        template <class U>
        PoolAllocator(const PoolAllocator<U, ChunkSize, ChunksCount> &other)
            : _chunks((Chunk<chunk_size> *)other.chunks()),
              _head_ptr((Chunk<chunk_size> **)other.head_ptr()),
              _count_ptr((size_t *)other.count_ptr())
        {
            if (sizeof(T) != sizeof(U))
                throw std::logic_error("incorrect pool allocators cast");

            *_count_ptr += 1;
        }

        ~PoolAllocator()
        {
            if (_chunks != nullptr && *_count_ptr == 1)
            {
#ifdef DEBUG
                log << "Destruct from " << _chunks << " to " << _chunks + ChunksCount << std::endl;
#endif // DEBUG

                delete[] _chunks;
                delete _head_ptr;
                delete _count_ptr;

                _chunks = nullptr;
                _head_ptr = nullptr;
                _count_ptr = nullptr;
            }
            else
            {
                *_count_ptr -= 1;
            }
        }

        PoolAllocator &operator=(const PoolAllocator &other) noexcept
        {
            this->~PoolAllocator();

            _chunks = other._chunks;
            _head_ptr = other._head_ptr;
            _count_ptr = other._count_ptr;

            *_count_ptr += 1;

            return *this;
        }

        bool operator==(const PoolAllocator &other) const noexcept
        {
            return _chunks == other._chunks;
        }

        bool operator!=(const PoolAllocator &other) const noexcept
        {
            return _chunks != other._chunks;
        }

        static PoolAllocator select_on_container_copy_construction()
        {
            return PoolAllocator();
        }

    private:
        Chunk<chunk_size> *_chunks;
        Chunk<chunk_size> **_head_ptr;
        size_t *_count_ptr;
    };

} // namespace ZIS

#endif // ZIS_POOL_ALLOCATOR_