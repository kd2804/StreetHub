using NetTopologySuite.Geometries;
using StreetHub.Models;
using StreetHub.Repositories;

namespace StreetHub.Services
{
    /// <summary>
    /// Service class for managing street operations.
    /// </summary>
    public class StreetService : IStreetService
    {
        private readonly IStreetRepository _repository; 
        private readonly bool _useDatabaseGeometryManipulation; // Flag to determine if database geometry manipulation is enabled

        public StreetService(IStreetRepository repository, IConfiguration config)
        {
            _repository = repository;
            _useDatabaseGeometryManipulation = config.GetValue<bool>("FeatureFlags:UseDatabaseGeometryManipulation");
        }

        /// <summary>
        /// Retrieves a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to retrieve.</param>
        /// <returns>A <see cref="Task{Street}"/> representing the asynchronous operation, with the street data.</returns>
        public async Task<Street> GetStreetByIdAsync(int id)
        {
            return await _repository.GetStreetByIdAsync(id);
        }

        /// <summary>
        /// Creates a new street asynchronously.
        /// </summary>
        /// <param name="street">The street object to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task CreateStreetAsync(Street street)
        {
            await _repository.AddStreetAsync(street);
        }

        /// <summary>
        /// Deletes a street by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the street to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteStreetAsync(int id)
        {
            await _repository.DeleteStreetAsync(id);
        }

        /// <summary>
        /// Adds a point to the geometry of a street asynchronously.
        /// </summary>
        /// <param name="streetId">The ID of the street to which the point will be added.</param>
        /// <param name="point">The point to add to the street's geometry.</param>
        /// <param name="addToEnd">Indicates whether to add the point to the end of the geometry.</param>
        /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, indicating success or failure.</returns>
        public async Task<bool> AddPointToStreetGeometryAsync(int streetId, Point point, bool addToEnd)
        {
            return await _repository.AddPointToStreetGeometryAsync(streetId, point, addToEnd, _useDatabaseGeometryManipulation);
        }
    }
}