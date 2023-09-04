namespace chotel
{
    public record Coordinate (double Latitude, double Longitude)
    {
        public static bool TryParse(string input, out Coordinate? coordinate)
        {
            coordinate = default;
            var sptitArray = input.Split(',', 2);
            if (sptitArray.Length != 2 ) return false;
            if (!double.TryParse(sptitArray[0], out var lat)) return false;
            if (!double.TryParse(sptitArray[1], out var lon)) return false;
            coordinate = new (lat, lon);
            return true;
        }

        public static async ValueTask<Coordinate?> BindAsync(HttpContext context, ParameterInfo parameter)
        {
            var input = context.GetRouteValue(parameter.Name!) as string ?? string.Empty;
            TryParse(input, out var coordinate);
            return coordinate;
        }
    }
}
