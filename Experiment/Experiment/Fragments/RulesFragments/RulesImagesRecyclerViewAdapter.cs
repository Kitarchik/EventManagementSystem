using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Experiment.Model;
using System.Collections.Generic;

namespace Experiment.Fragments.RulesFragments
{
    public class RulesImagesRecyclerViewAdapter : RecyclerView.Adapter
    {
        private List<ImageSigned> images;
        private RulesOneSubsectionFragment parent;

        public RulesImagesRecyclerViewAdapter(RulesOneSubsectionFragment parent, List<ImageSigned> images)
        {
            this.parent = parent;
            this.images = images;
        }

        public override int ItemCount => images.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ViewHolderImageRule vh = holder as ViewHolderImageRule;
            vh.ImageCaption.Text = images[position].Caption;
            vh.Image.SetImageBitmap(images[position].Image);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.rulesSubsectionsImageRow, parent, false);
            return new ViewHolderImageRule(itemView);
        }
    }

    public class ViewHolderImageRule : RecyclerView.ViewHolder
    {
        public View View { get; set; }
        public ImageView Image { get; set; }
        public TextView ImageCaption { get; set; }

        public ViewHolderImageRule(View itemView) : base(itemView)
        {
            View = itemView;
            ImageCaption = itemView.FindViewById<TextView>(Resource.Id.rulesImageCaption);
            Image = ItemView.FindViewById<ImageView>(Resource.Id.rulesImage);
        }
    }
}