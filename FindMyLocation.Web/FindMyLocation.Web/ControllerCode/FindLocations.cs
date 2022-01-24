using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using FindMyLocation.Web.APIStructs;
using Microsoft.AspNetCore.Components;

namespace FindMyLocation.Web.Pages
{
    public partial class FindLocations
    {
        #region Injections
        [Inject]
        private IFourSqaureApi FourSquareApi { get; set; }
        [Inject] 
        public IFourSquareVenuApi FourSquareVenueApi { get; set; }
        #endregion
        #region models  
        public string locationSearch { get; set; }
        public string latSearch { get; set; }
        public string lonSearch { get; set; }
        public string countSearch { get; set; }
        public List<FourSqaureVenues> fourSquareVenues { get; set; } = new();
        public IEnumerable<FourSqaureVenues> fourSquareVenuesResult { get; set; } 
        public FourSqaureVenues FourSquare=new();
        public ModelFour FourModel = new();
        public List<ModelFour> modelFours{ get; set; } = new();
        public List<ImageModel> imageModels { get; set; } = new();
        public IEnumerable<ImageModel> imageModelResult { get; set; }
        public IEnumerable<ModelFour> searchResult { get; set; }
        #endregion
        private async void searchResults()
        {
            FourModel = new();
            if (string.IsNullOrEmpty(countSearch))
                countSearch = "5";
            if (!string.IsNullOrEmpty(latSearch)&& !string.IsNullOrEmpty(lonSearch) && !string.IsNullOrEmpty(locationSearch))
            {                
                searchResult = await FourSquareApi.GetByAll(locationSearch,lonSearch,latSearch,countSearch);
            }
            else if(!string.IsNullOrEmpty(latSearch) && !string.IsNullOrEmpty(lonSearch))
            {
                searchResult = await FourSquareApi.GetByGeo(lonSearch, latSearch, countSearch);
            }
            else if(!string.IsNullOrEmpty(locationSearch))
            {
                searchResult = await FourSquareApi.GetByName(locationSearch, countSearch);
            }
            imageModels = new();
            foreach (var items in searchResult)
            {
                string fsq = "";
                foreach (var item in items.results)
                {
                    fsq = item.fsq_id;
                }
                imageModelResult = await FourSquareApi.GetImages(fsq);
                foreach (var item in imageModelResult)
                {
                    if(!string.IsNullOrEmpty(item.id))
                    {
                        imageModels.AddRange(imageModelResult);
                        break;
                    }
                }
                
            }
            StateHasChanged();
        }

    }
}
