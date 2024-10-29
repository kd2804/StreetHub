using NetTopologySuite.Geometries;
using StreetHub.Models;

namespace StreetHub.Repositories
{
    /// <summary>
    /// Interface for managing street data operations.
    /// </summary>
    public interface IStreetRepository
    {
        /// <summary>
        /// Retrieves a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to retrieve.</param>
        /// <returns>A <see cref="Task{Street}"/> representing the asynchronous operation, with the street data.</returns>
        Task<Street> GetStreetByIdAsync(int id);

        /// <summary>
        /// Adds a new street asynchronously.
        /// </summary>
        /// <param name="street">The street object to add.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddStreetAsync(Street street);

        /// <summary>
        /// Updates an existing street asynchronously.
        /// </summary>
        /// <param name="street">The street object with updated data.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateStreetAsync(Street street);

        /// <summary>
        /// Deletes a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteStreetAsync(int id);

        /// <summary>
        /// Adds a point to the geometry of a street asynchronously.
        /// </summary>
        /// <param name="streetId">The ID of the street to which the point will be added.</param>
        /// <param name="point">The point to add to the street's geometry.</param>
        /// <param name="addToEnd">Indicates whether to add the point to the end of the geometry.</param>
        /// <param name="useDatabaseOperation">Indicates whether to use a database operation for geometry manipulation.</param>
        /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, indicating success or failure.</returns>
        Task<bool> AddPointToStreetGeometryAsync(int streetId, Point point, bool addToEnd, bool useDatabaseOperation);
    }
}