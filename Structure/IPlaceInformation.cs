using DTO;
using Models;

namespace Structure
{
    public interface IPlaceInformation
    {
        public Task<List<PlaceInformationModel>> GetAllPlaceInformation(string zipCodeID);

        public Task<List<PlaceModel>> GetLocationInfoFromZipCodeAsync(string zipCodeID);

    }
}