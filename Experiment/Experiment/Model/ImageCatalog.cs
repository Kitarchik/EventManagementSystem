﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Experiment.Model
{
    public class ImageSigned
    {
        public Bitmap Image { get; }
        public string Caption { get; }

        public ImageSigned (Bitmap image, string caption)
        {
            Image = image;
            Caption = caption;
        }
    }
}