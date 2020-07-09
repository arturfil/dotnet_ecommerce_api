using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{

  public class PhotoUrlResolver : IValueResolver<Photo, PhotoToReturnDto, string>
  {
    private readonly IConfiguration _config;

    //constructor
    public PhotoUrlResolver(IConfiguration config)
    {
      this._config = config;
    }

    public string Resolve(Photo source, PhotoToReturnDto destination, string destMember, ResolutionContext context) {
      if (!string.IsNullOrEmpty(source.PictureUrl))
        return _config["ApiUrl"] + source.PictureUrl;

      return null;
    }

  }

}
