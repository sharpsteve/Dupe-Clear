﻿// Copyright (C) 2024 Antik Mozib. All rights reserved.

using System.Runtime.Versioning;

namespace DupeClear.Native.Windows.ImageService;

[SupportedOSPlatform("windows")]
internal static class ImageExtensions
{
    public static Avalonia.Media.Imaging.Bitmap? ConvertToAvaloniaImage(this System.Drawing.Bitmap? bitmap)
    {
        if (bitmap != null)
        {
            // Convert to Avalonia.Media.Imaging.Bitmap

            var tempBitmap = new System.Drawing.Bitmap(bitmap);
            var bitmapData = tempBitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, tempBitmap.Width, tempBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var avaloniaBitmap = new Avalonia.Media.Imaging.Bitmap(
                Avalonia.Platform.PixelFormat.Bgra8888,
                Avalonia.Platform.AlphaFormat.Unpremul,
                bitmapData.Scan0,
                new Avalonia.PixelSize(bitmapData.Width, bitmapData.Height),
                new Avalonia.Vector(96, 96),
                bitmapData.Stride);

            tempBitmap.UnlockBits(bitmapData);
            tempBitmap.Dispose();

            return avaloniaBitmap;
        }

        return null;
    }
}