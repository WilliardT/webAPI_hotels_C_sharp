using chotel.Auth;
using chotel.Interfaces;
using chotel.Models;

namespace chotel.APIs
{
    public class HotelApi : IApi
    {
        public void Register(WebApplication app)
        {
            app.MapGet("/hotels", Get)
                .Produces<List<Hotel>>(StatusCodes.Status200OK)
                .WithName("GetAllHotels")
                .WithTags("Getters");

            app.MapGet("/hotels/{id}", GetById)
                    .Produces<Hotel>(StatusCodes.Status200OK)
                    .WithName("GetHotelById")
                    .WithTags("Getters");

            app.MapPost("/hotels", Post)
                .Accepts<Hotel>("application/json")
                .Produces<Hotel>(StatusCodes.Status201Created)
                .WithName("CreateHotel")
                .WithTags("Creators");

            app.MapPut("/hotels", Put)
                .Accepts<Hotel>("application/json")
                .Produces<Hotel>(StatusCodes.Status200OK)
                .WithName("UpdateHotel")
                .WithTags("Updaters");

            app.MapDelete("/hotels/{id}", Delete)
                .WithName("DeleteHotel")
                .WithTags("Deleters");

            app.MapGet("/hotels/search/name/{query}", SearchByName)
                    .Produces<List<Hotel>>(StatusCodes.Status200OK)
                    .Produces<Hotel>(StatusCodes.Status404NotFound)
                    .WithName("SearchHotels")
                    .WithTags("Getters")
                    .ExcludeFromDescription();

            app.MapGet("/hotels/search/location/{coordinate}", SearchByCoordinates)
                .ExcludeFromDescription();
        }


        [Authorize]
        private async Task<IResult> Get(IHotelRepository repository) =>
            Results.Extensions.Xml(await repository.GetAllHotelAsync());

        [Authorize]
        private async Task<IResult> GetById(int id, IHotelRepository repository) =>
            await repository.GetHotelByIdAsync(id) is Hotel hotel
            ? Results.Ok(hotel)
            : Results.NotFound();

        [Authorize]
        private async Task<IResult> Post ([FromBody] Hotel hotel, IHotelRepository repository)
            {
                await repository.InsertHotelAsync(hotel);
                await repository.SaveAsync();
                return Results.Created($"/hotels/{hotel.Id}", hotel);
            }

        [Authorize]
        private async Task<IResult> Put ([FromBody] Hotel hotel, IHotelRepository repository)
            {
                await repository.UpdateHotelAsync(hotel);
                await repository.SaveAsync();
                return Results.NoContent();
            }

        [Authorize]
        private async Task<IResult> Delete (int id, IHotelRepository repository)
            {
                await repository.DeleteHotelAsync(id);
                await repository.SaveAsync();
                return Results.NoContent();
            }


        [Authorize]
        private async Task<IResult> SearchByName(string query, IHotelRepository repository) =>
            await repository.GetHotelByNameAsync(query) is IEnumerable<Hotel> hotels
                ? Results.Ok(hotels)
                : Results.NotFound(Array.Empty<Hotel>());

        [Authorize]
        private async Task<IResult> SearchByCoordinates(Coordinate coordinate, IHotelRepository repository) =>
            await repository.GetHotelCoordinatesAsync(coordinate) is IEnumerable<Hotel> hotels
                ? Results.Ok(hotels)
                : Results.NotFound(Array.Empty<Hotel>());
}
}

