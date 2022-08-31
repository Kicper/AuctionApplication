using AuctionApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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


        public (double averagePriceScale, double frequencyScale, double itemRatingScale, double categoryRatingScale) Scalarization(int userId)
        {
            AlgorithmDAO algorithm = new AlgorithmDAO();
            List<(int ItemId, string ItemName, int CategoryId, int Frequency)> itemsFrequency = algorithm.GetItemsFrequency();
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = algorithm.GetMinMaxPriceAndDate();
            List<(int ItemId, int Rating)> itemPreferences = algorithm.GetItemPreferences(userId);
            List<(int ItemId, int Rating)> categoryPreferences = algorithm.GetCategoryPreferences(userId);
            List<(int ItemId, int CategoryId, int AvgPrice, string StartTime, string EndTime)> averagePriceCategoryStartEndDate = algorithm.GetAveragePriceCategoryStartEndDate();


            /* COUNT AVERAGE PRICE */
            int i;
            int counter = 0;
            double sum = 0;
            double averagePrice;
            for (i = 0; i < averagePriceCategoryStartEndDate.Count; ++i)
            {
                sum += averagePriceCategoryStartEndDate[i].AvgPrice;
                counter++;
            }
            averagePrice = sum / counter;
            double averagePriceScale = 10 / averagePrice;


            /* COUNT FREQUENCY COEFFICIENT */
            double frequencyScale = 20;


            /* COUNT ITEM RATING */
            sum = 0;
            counter = 0;
            double itemRating = 0;
            for (i = 0; i < itemPreferences.Count; ++i)
            {
                sum += Math.Abs(itemPreferences[i].Rating);
                counter++;
            }
            itemRating = (sum / counter);
            double itemRatingScale = 10 / itemRating;


            /* COUNT CATEGORY RATING */
            sum = 0;
            counter = 0;
            double categoryRating = 0;
            for (i = 0; i < categoryPreferences.Count; ++i)
            {
                sum += Math.Abs(categoryPreferences[i].Rating);
                counter++;
            }
            categoryRating = (sum / counter);
            double categoryRatingScale = 10 / categoryRating;

            return (averagePriceScale, frequencyScale, itemRatingScale, categoryRatingScale);
        }

        public List<(int ItemId, string ItemName, double Result)> GetBestAuction(int userId)
        {
            AlgorithmDAO algorithm = new AlgorithmDAO();
            List<(int ItemId, string ItemName, int CategoryId, int Frequency)> itemsFrequency = algorithm.GetItemsFrequency();
            List<(int ItemId, int MinPrice, int MaxPrice, string MinTime, string MaxTime)> minMaxPriceAndDate = algorithm.GetMinMaxPriceAndDate();
            List<(int ItemId, int Rating)> itemPreferences = algorithm.GetItemPreferences(userId);
            List<(int ItemId, int Rating)> categoryPreferences = algorithm.GetCategoryPreferences(userId);
            List<(int ItemId, int CategoryId, int AvgPrice, string StartTime, string EndTime)> averagePriceCategoryStartEndDate = algorithm.GetAveragePriceCategoryStartEndDate();


            int i;
            int counter;
            double sum;
            List<(int ItemId, string ItemName, double Result)> finalResult = new List<(int ItemId, string ItemName, double Result)>();
            double averagePrice;
            double frequency;
            double itemRating;
            double categoryRating;
            double result;
            double averagePriceScale, frequencyScale, itemRatingScale, categoryRatingScale;
            (averagePriceScale, frequencyScale, itemRatingScale, categoryRatingScale) = Scalarization(userId);

            foreach (var item in itemsFrequency)
            {
                /* COUNT AVERAGE PRICE */
                sum = 0;
                counter = 0;
                result = 0;
                for (i = 0; i < averagePriceCategoryStartEndDate.Count; ++i)
                {
                    if(averagePriceCategoryStartEndDate[i].ItemId == item.ItemId)
                    {
                        sum += averagePriceCategoryStartEndDate[i].AvgPrice;
                        counter++;
                    }
                }
                averagePrice = sum / counter;
                result = averagePrice * AveragePriceRating * averagePriceScale;


                /* COUNT FREQUENCY COEFFICIENT */
                sum = 0;
                frequency = 0;
                for (i = 0; i < itemsFrequency.Count; ++i)
                {
                    sum += itemsFrequency[i].Frequency;
                }
                frequency = item.Frequency / sum;
                result += frequency * FrequencyOfTheItemsRating * frequencyScale;


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
                result += itemRating * RatingOfItemsRating * itemRatingScale;


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
                result += categoryRating * RatingOfCategoriesRating * categoryRatingScale;

                finalResult.Add( (item.ItemId, item.ItemName, result) );

            }

            finalResult = finalResult.OrderByDescending(x => x.Result).ToList();
            return finalResult;
        }
    }
}
