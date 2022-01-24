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

        public int index  = 0;
        
        public List<FourSqaureVenues> fourSquareVenues { get; set; } = new();
        public List<ModelFour> modelFours { get; set; } = new();
        public List<ImageModel> imageModels { get; set; } = new();
        public IEnumerable<FourSqaureVenues> fourSquareVenuesResult { get; set; }
        public IEnumerable<ImageModel> imageModelResult { get; set; }
        public IEnumerable<ModelFour> searchResult { get; set; }

        public FourSqaureVenues FourSquare=new();
        public ModelFour FourModel = new();
        bool btnVisble = true;
        
       
        #endregion
        public void counterInc()
        {
            index++;
        }
        public void counterClear()
        {
            index=0;
        }
        private async void searchResults()
        {
            btnVisble = false;
            index = 0;
            StateHasChanged();
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
            modelFours = searchResult.ToList();
            imageModels = new();
            foreach (var items in searchResult)
            {
                if(items.results!=null)
                {
                    string fsq = "";
                    foreach (var item in items.results)
                    {
                        fsq = item.fsq_id;
                        imageModelResult = await FourSquareApi.GetImages(fsq);
                        foreach (var itemPic in imageModelResult)
                        {
                            if (!string.IsNullOrEmpty(itemPic.id))
                            {
                                var firstPic = imageModelResult.FirstOrDefault();
                                imageModels.Add(firstPic);
                            }
                            break;
                        }
                        
                    }
                    break;
                }
            }
            btnVisble = true;
            StateHasChanged();
        }

    }
}
