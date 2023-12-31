﻿namespace RentHouse.Service.Commons.Healpers;

public class MediaHealper
{
    public static string MakeImageName(string filename)
    {
        FileInfo fileInfo = new FileInfo(filename);
        string extension = fileInfo.Extension;
        string name = "IMG_" + Guid.NewGuid()+extension;
        return name;
    }
    public static string[] GetImageExtensions() 
    {
        return new string[]
        {
            ".jpg",
            ".PNG",
            ".svg",
            ".jpeg",
            ".bmp",
            ".heic"

        };
    }
}
