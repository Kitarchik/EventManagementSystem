using Android.Content;
using Android.Graphics;
using Experiment.Model;
using System.Collections.Generic;

namespace Experiment.DataLayer
{
    public static class RulesDataAccess
    {
        public static void DownloadImages(Context context, Rules rules)
        {
            List<ImageSigned> images = new List<ImageSigned>();
            if (rules.Name == "Формулы")
            {
                var image = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.rulesImagesFormula);
                string caption = "Алхимическая формула";
                images.Add(new ImageSigned(image, caption));
            }

            if (rules.Name == "Пример изготовления зелья")
            {
                var image = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.rulesImagesRecipe);
                string caption = "Рецепт";
                images.Add(new ImageSigned(image, caption));

                image = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.rulesImagesRecipeDone);
                caption = "Собранный рецепт";
                images.Add(new ImageSigned(image, caption));
            }

            if (rules.Name == "Подлинное мастерство")
            {
                var image = BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.rulesImagesAlchemyStar);
                string caption = "Расширенная алхимическая звезда";
                images.Add(new ImageSigned(image, caption));
            }

            rules.Images = images;
        }
    }
}