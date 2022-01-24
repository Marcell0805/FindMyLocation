using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindMyLocation.Domain.Entities;
using FindMyLocation.Web.APIStructs;
using Microsoft.AspNetCore.Components;
using DevExpress.Blazor;

namespace FindMyLocation.Web.Pages
{
    public partial class FindLocations
    {
        #region Injections
        [Inject]
        private IFourSqaureApi FourSquareApi { get; set; }
        [Inject] public IFourSquareVenuApi FourSquareVenueApi { get; set; }
        #endregion
        #region models  
        public string locationSearch { get; set; }
        public string latSearch { get; set; }
        public string lonSearch { get; set; }
        public string countSearch { get; set; }
        public List<FourSqaureVenues> fourSquareVenues { get; set; }
        public IEnumerable<FourSqaureVenues> fourSquareVenuesResult { get; set; }
        public FourSqaureVenues FourSquare=new();
        public ModelFour FourModel = new();
        public List<ModelFour> modelFours{ get; set; }
        public List<ImageModel> imageModels { get; set; }
        public IEnumerable<ImageModel> imageModelResult { get; set; }
        #endregion
        private async void searchResults()
        {

            fourSquareVenues = new();
            fourSquareVenuesResult = await FourSquareVenueApi.GetAllResults();
            imageModels = new();
            foreach (var items in fourSquareVenuesResult)
            {
                imageModelResult = await FourSquareApi.GetImages(items.fsq_id);
                imageModels.AddRange(imageModelResult);
            }
            StateHasChanged();
        }

    }
}
