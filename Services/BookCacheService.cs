using LibraryManager.Models.Entities;
using System.Collections.Concurrent;

namespace LibraryManager.Services
{
    public class BookCacheService
    {
        private readonly ConcurrentDictionary<string, List<Book>> _genreCache = new();
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(30);
        private readonly ConcurrentDictionary<string, DateTime> _cacheTimestamps = new();

        public bool TryGetBooks(string genre, out List<Book>? books)
        {
            if (_genreCache.TryGetValue(genre, out var cachedBooks) &&
                _cacheTimestamps.TryGetValue(genre, out var timestamp) &&
                DateTime.UtcNow - timestamp < _cacheExpiration)
            {
                books = cachedBooks;
                return true;
            }

            books = new List<Book>();
            return false;
        }

        public void CacheBooks(string genre, List<Book> books)
        {
            if (books == null)
                throw new ArgumentNullException(nameof(books));

            _genreCache[genre] = books;
            _cacheTimestamps[genre] = DateTime.UtcNow;
        }

        public void ClearCache()
        {
            _genreCache.Clear();
            _cacheTimestamps.Clear();
        }
    }
}