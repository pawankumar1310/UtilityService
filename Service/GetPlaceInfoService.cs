using Models;
using Structure;
using DTO;

namespace Service
{
    public class GetPlaceInfoService
    {
        private readonly IPlaceInformation _placeInformation;
       

        public GetPlaceInfoService(IPlaceInformation placeInformation)
        {
            _placeInformation=placeInformation;
        }
        public async Task<List<PlaceInformationModel>> GetPlaceInformationService(string zipCodeID)
        {
            List<PlaceInformationModel>pi =await _placeInformation.GetAllPlaceInformation(zipCodeID);
            return pi; 
        }

        public async Task<List<PlaceModel>> GetPlacesService(string zipCodeID)
        {
            List<PlaceModel> pi = await _placeInformation.GetLocationInfoFromZipCodeAsync(zipCodeID);
            return pi;
        }
    }
}