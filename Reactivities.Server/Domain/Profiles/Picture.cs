using System;
using Domain.Common.Base;
using Domain.Profiles.ErrorHandling;

namespace Domain.Profiles;

public class Picture : DomainEntity
{
    private Picture() {}

    private Picture(string publicId, string url)
    {
        this.PublicId = publicId;
        this.Url = url;
    }

    public string PublicId { get; private set; }

    public string Url { get; private set; }


    public static Picture New(string publicId, string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentException(
                ProfileErrorMessages.InvalidPictureUrl);
        }

        return new Picture(publicId, url);
    }
}