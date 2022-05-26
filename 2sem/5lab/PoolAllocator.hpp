#ifndef ZIS_POOL_ALLOCATOR_
#define ZIS_POOL_ALLOCATOR_

#include <iostream>
#include <new>
#include <stdexcept>

namespace ZIS
{
    template <unsigned Size>
    union Chunk
    {
        Chunk *next;
        char chunk_data[Size];
        Chunk() {}
        ~Chunk() {}
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
            return res;
        }

        void deallocate(T *p, size_t n)
        {
            if (p == nullptr)
                return;

            Chunk<chunk_size> *new_head = reinterpret_cast<Chunk<chunk_size> *>(p);
            new_head->next = *_head_ptr;
            *_head_ptr = new_head;
        }

        size_t max_size() const
        {
            return ChunkSize;
        }

        Chunk<chunk_size> **head_ptr() const
        {
            return _head_ptr;
        }

        Chunk<chunk_size> *chunks() const
        {
            return _chunks;
        }

        PoolAllocator()
        {
            if (ChunksCount == 0)
                throw std::logic_error("incorrect chunks count");

            if (chunk_size < sizeof(Chunk<chunk_size> *))
                throw std::logic_error("too small chunk size");

            _chunks = new Chunk<chunk_size>[ChunksCount];
            _head_ptr = &_chunks;

            for (size_t i = 0; i < ChunksCount - 1; ++i)
                _chunks[i].next = _chunks + i + 1;
            _chunks[ChunksCount - 1].next = nullptr;
        }

        PoolAllocator(Chunk<chunk_size> *chunks, Chunk<chunk_size> **head_ptr)
            : _chunks(chunks),
              _head_ptr(head_ptr) {}

        PoolAllocator(const PoolAllocator &other)
            : _chunks(other._chunks),
              _head_ptr(other._head_ptr) {}

        template <class U>
        PoolAllocator(const PoolAllocator<U> &other)
            : _chunks(reinterpret_cast<Chunk<chunk_size> *>(other.chunks())),
              _head_ptr(reinterpret_cast<Chunk<chunk_size> **>(other.head_ptr())) {}

        ~PoolAllocator()
        {
            delete[] _chunks;
            _chunks = nullptr;
            _head_ptr = nullptr;
        }

        PoolAllocator &operator=(const PoolAllocator &other)
        {
            _chunks = other._chunks;
            _head_ptr = other._head_ptr;
            return *this;
        }

        bool operator==(const PoolAllocator &other) const
        {
            return _chunks == other._chunks;
        }

        bool operator!=(const PoolAllocator &other) const
        {
            return _chunks != other._chunks;
        }

        static PoolAllocator select_on_container_copy_construction()
        {
            static PoolAllocator res(nullptr, nullptr);

            res._chunks = new Chunk<chunk_size>[ChunksCount];
            res._head_ptr = &res._chunks;

            for (size_t i = 0; i < ChunksCount - 1; ++i)
                res._chunks[i].next = res._chunks + i + 1;
            res._chunks[ChunksCount - 1].next = nullptr;

            return res;
        }

    private:
        Chunk<chunk_size> *_chunks;
        Chunk<chunk_size> **_head_ptr;
    };

} // namespace ZIS

#endif // ZIS_POOL_ALLOCATOR_