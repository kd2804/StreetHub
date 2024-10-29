using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using StreetHub.Data;
using StreetHub.Models;

namespace StreetHub.Repositories
{
    /// <summary>
    /// Repository class for managing street data operations.
    /// </summary>
    public class StreetRepository : IStreetRepository
    {
        private readonly StreetContext _context;

        public StreetRepository(StreetContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to retrieve.</param>
        /// <returns>A <see cref="Task{Street}"/> representing the asynchronous operation, with the street data.</returns>
        public async Task<Street> GetStreetByIdAsync(int id)
        {
            return await _context.Streets.FindAsync(id);
        }

        /// <summary>
        /// Adds a new street asynchronously.
        /// </summary>
        /// <param name="street">The street object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddStreetAsync(Street street)
        {
            await _context.Streets.AddAsync(street);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing street asynchronously.
        /// </summary>
        /// <param name="street">The street object with updated data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateStreetAsync(Street street)
        {
            _context.Streets.Update(street);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteStreetAsync(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            if (street != null)
            {
                _context.Streets.Remove(street);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Adds a point to the geometry of a street asynchronously.
        /// </summary>
        /// <param name="streetId">The ID of the street to which the point will be added.</param>
        /// <param name="point">The point to add to the street's geometry.</param>
        /// <param name="addToEnd">Indicates whether to add the point to the end of the geometry.</param>
        /// <param name="useDatabaseOperation">Indicates whether to use a database operation for geometry manipulation.</param>
        /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, indicating success or failure.</returns>
        public async Task<bool> AddPointToStreetGeometryAsync(int streetId, Point point, bool addToEnd, bool useDatabaseOperation)
        {
            var street = await _context.Streets.FindAsync(streetId);
            if (street == null) return false;

            if (useDatabaseOperation)
            {
                string sql = addToEnd
                    ? $"UPDATE Streets SET Geometry = ST_AddPoint(Geometry, ST_MakePoint({point.X}, {point.Y})) WHERE Id = {streetId}"
                    : $"UPDATE Streets SET Geometry = ST_AddPoint(Geometry, 0, ST_MakePoint({point.X}, {point.Y})) WHERE Id = {streetId}";
                await _context.Database.ExecuteSqlRawAsync(sql);
            }
            else
            {
                var coordinates = street.Geometry?.Coordinates.ToList() ?? new List<Coordinate>();
                if (addToEnd)
                    coordinates.Add(new Coordinate(point.X, point.Y));
                else
                    coordinates.Insert(0, new Coordinate(point.X, point.Y));

                street.Geometry = new LineString(coordinates.ToArray());
                _context.Streets.Update(street);
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}