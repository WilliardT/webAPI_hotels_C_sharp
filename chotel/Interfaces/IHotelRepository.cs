using chotel.Models;

namespace chotel.Interfaces
{
    // crud
    public interface IHotelRepository : IDisposable
    {
        Task<List<Hotel>> GetAllHotelAsync();

        Task<List<Hotel>> GetHotelByNameAsync(string name);

        Task<List<Hotel>> GetHotelCoordinatesAsync(Coordinate coordinate);

        Task<Hotel> GetHotelByIdAsync(int hotelId);

        Task InsertHotelAsync(Hotel hotel);

        Task UpdateHotelAsync(Hotel hotel);

        Task DeleteHotelAsync(int hotelId);

        Task SaveAsync();
    }
}
