using AuctionApplication.Services;
using System;
using System.Collections.Generic;

namespace AuctionApplication.Models
{
    public class AlgorithmModel
    {
        public AlgorithmModel()
        {
            this.DurationOfAuctionRating = 1;
            this.FrequencyOfTheItemsRating = 1;
            this.AveragePriceRating = 1;
            this.RatingOfCategoriesRating = 1;
            this.RatingOfItemsRating = 1;
        }

        public double DurationOfAuctionRating { get; set; }

        public double FrequencyOfTheItemsRating { get; set; }

        public double AveragePriceRating { get; set; }

        public double RatingOfCategoriesRating { get; set; }

        public double RatingOfItemsRating { get; set; }

        public List<(int ItemId, string ItemName, double Result)> GetBestAuction(int userId)
        {
            AlgorithmDAO algorithm = new AlgorithmDAO();
            List<(int ItemId, string ItemName, int CategoryId, int Frequency)> itemsFrequency = algorithm.GetItemsFrequency();
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = algorithm.GetMinMaxPriceAndDate();
            List<(int ItemId, int Rating)> itemPreferences = algorithm.GetItemPreferences(userId);
            List<(int ItemId, int Rating)> categoryPreferences = algorithm.GetCategoryPreferences(userId);
            List<(int ItemId, int CategoryId, int AvgPrice, string StartTime, string EndTime)> averagePriceCategoryStartEndDate = algorithm.GetAveragePriceCategoryStartEndDate();


            double sum;
            //TimeSpan dateDifference;
            int i;
            double averagePrice;
            int counter;
            List<(int ItemId, string ItemName, double Result)> finalResult = new List<(int ItemId, string ItemName, double Result)>();
            double result;
            double frequency;
            double itemRating;
            double categoryRating;

            foreach (var item in itemsFrequency)
            {
                /* COUNT AVERAGE PRICE AND AVERAGE DATE */
                sum = 0;
                counter = 0;
                result = 0;
                //dateDifference = TimeSpan.Zero;
                for (i = 0; i < averagePriceCategoryStartEndDate.Count; ++i)
                {
                    if(averagePriceCategoryStartEndDate[i].ItemId == item.ItemId)
                    {
                        sum += averagePriceCategoryStartEndDate[i].AvgPrice;
                        //dateDifference += Convert.ToDateTime(averagePriceCategoryStartEndDate[i].EndTime).Subtract(Convert.ToDateTime(averagePriceCategoryStartEndDate[i].StartTime));
                        counter++;
                    }
                }
                //averageDateDifference = dateDifference / counter;
                averagePrice = sum / counter;
                result = averagePrice * AveragePriceRating;


                /* COUNT FREQUENCY COEFFICIENT */
                sum = 0;
                frequency = 0;
                for (i = 0; i < itemsFrequency.Count; ++i)
                {
                    sum += itemsFrequency[i].Frequency;
                }
                frequency = item.Frequency / sum;
                result += frequency * FrequencyOfTheItemsRating;


                /* COUNT ITEM RATING */
                itemRating = 0;
                for (i = 0; i < itemPreferences.Count; ++i)
                {
                    if (itemPreferences[i].ItemId == item.ItemId)
                    {
                        itemRating = itemPreferences[i].Rating;
                        break;
                    }
                }
                result += itemRating * RatingOfItemsRating;


                /* COUNT CATEGORY RATING */
                categoryRating = 0;
                for (i = 0; i < categoryPreferences.Count; ++i)
                {
                    if (categoryPreferences[i].ItemId == item.CategoryId)
                    {
                        categoryRating = categoryPreferences[i].Rating;
                        break;
                    }
                }
                result += categoryRating * RatingOfCategoriesRating;

                finalResult.Add( (item.ItemId, item.ItemName, result) );
                //FinalResult.Add( (item.ItemId, result) );
            }

            return finalResult;
        }
    }
}
