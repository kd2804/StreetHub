using NetTopologySuite.Geometries;
using StreetHub.Models;

namespace StreetHub.Services
{
    /// <summary>
    /// Interface for managing street operations.
    /// </summary>
    public interface IStreetService
    {
        /// <summary>
        /// Retrieves a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to retrieve.</param>
        /// <returns>A <see cref="Task{Street}"/> representing the asynchronous operation, with the street data.</returns>
        Task<Street> GetStreetByIdAsync(int id);

        /// <summary>
        /// Creates a new street asynchronously.
        /// </summary>
        /// <param name="street">The street object to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateStreetAsync(Street street);

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
        /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, indicating success or failure.</returns>
        Task<bool> AddPointToStreetGeometryAsync(int streetId, Point point, bool addToEnd);
    }
}