using Domains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Models.VwAdmin;

namespace BusinessLogic.BlHelper.BlWebsite
{
    public class ClsRating
    {

        public static int GetRating(TbItem item) 
        {
            var reviews = item.Reviews.ToList();  
            var maxRating = 0;
            var result = 1;

            List<int> freq = new List<int>();

                foreach (var review in reviews) 
                {
                    freq[review.Rating]++;
                    if (freq[review.Rating] > maxRating)
                    {
                        maxRating = freq[review.Rating];
                        result = review.Rating;
                    }
                    

                }

                return result;
            

        }
    }
}
