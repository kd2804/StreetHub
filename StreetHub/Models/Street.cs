using NetTopologySuite.Geometries;

namespace StreetHub.Models
{
    /// <summary>
    /// Represents a street with associated properties.
    /// </summary>
    public class Street
    {
        /// <summary>
        /// Gets or sets the unique identifier for the street.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the street.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the geometric representation of the street.
        /// </summary>
        public LineString Geometry { get; set; }

        /// <summary>
        /// Gets or sets the capacity of the street, which may represent the number of lanes or vehicles it can accommodate.
        /// </summary>
        public int Capacity { get; set; }
    }
}